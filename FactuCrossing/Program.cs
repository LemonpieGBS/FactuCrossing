using FactuCrossing.Estructuras;
using FactuCrossing.Formularios;
using FactuCrossing.Formularios.Utilidades;
using FactuCrossing.Servicios;
using System.ComponentModel;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FactuCrossing
{
    // Gracias a Derek W en Stack Overflow, pueden ver su respuesta en como hacer una barra de progreso que no se laguea:
    // https://stackoverflow.com/questions/6071626/progressbar-is-slow-in-windows-forms
    public static class ExtensionMethods
    {
        /// <summary>
        /// Sets the progress bar value, without using 'Windows Aero' animation.
        /// This is to work around a known WinForms issue where the progress bar 
        /// is slow to update. 
        /// </summary>
        public static void SetProgressNoAnimation(this ProgressBar pb, int value)
        {
            // To get around the progressive animation, we need to move the 
            // progress bar backwards.
            if (value == pb.Maximum)
            {
                // Special case as value can't be set greater than Maximum.
                pb.Maximum = value + 1;     // Temporarily Increase Maximum
                pb.Value = value + 1;       // Move past
                pb.Maximum = value;         // Reset maximum
            }
            else
            {
                pb.Value = value + 1;       // Move past
            }
            pb.Value = value;               // Move to correct value
        }
    }

    /// <summary>
    /// Clase con funciones que ayudan al manejo de un form
    /// </summary>
    public static class FormHelper
    {
        // Estas funciones las voy a usar despues, consiguen recursivamente todos los controles de un formulario
        // Incluyendo los controles que albergan mas controles, como los GroupBoxes

        /// <summary> Función para obtener todos los descendientes de un control (<paramref name="c"/>)
        /// <para>
        /// Esta función usa recursión para conseguir los hijos de sus hijos también<br/>
        /// El retorno de esta función es una lista que NO incluye el control pasado como argumento<br/><br/>
        /// Si quieres obtener todos los controles de un formulario te puede interesar <seealso cref="GetControls(Form)"/>
        /// </para>
        /// </summary>
        /// <param name="c">Control el cual se buscan obtener sus descendientes</param>
        /// <returns>Lista que contiene todos los descendientes de un control</returns>
        public static List<Control> GetControls(Control c)
        {
            var controls = new List<Control>();
            foreach (Control child in c.Controls)
            {
                controls.Add(child);
                if (child.HasChildren)
                {
                    controls.AddRange(GetControls(child));
                }
            }
            return controls;
        }

        /// <summary> Función para obtener todos los controles (incluyendo hijos y descendientes) de un formulario (<paramref name="f"/>)
        /// <para>
        /// Esta función consigue todos los controles en un formulario<br/>
        /// Un ejemplo de como usar la función es aplicar una tipografía a todo un formulario:<br/><br/>
        /// <code> public static void ApplyFont(FontFamily ff, Form form)
        /// {
        ///     foreach(Control c in GetControls(form))
        ///     c.Font = new Font(ff, c.Font.Size, c.Font.Style);
        /// } </code>
        /// Si quieres obtener todos los descendientes de un control te puede interesar <seealso cref="GetControls(Control)"/>
        /// </para>
        /// </summary>
        /// <param name="f">Formulario del cual se busca obtener todos sus controles</param>
        /// <returns>Lista que contiene todos los controles en un formulario</returns>
        public static List<Control> GetControls(Form f)
        {
            List<Control> returnList = new List<Control>() { };
            foreach (Control c in f.Controls)
            {
                returnList.Add(c);
                if (c.HasChildren) returnList.AddRange(GetControls(c));
            }
            return returnList;
        }
    }

    /// <summary>
    /// Clase con funciones que ayudan al manejo de enumeradores
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Función para obtener el nombre de un enumerador
        /// </summary>
        /// <param name="e">Enumerador a obtener su nombre</param>
        /// <returns>Nombre del enumerador</returns>
        public static string GetEnumName(Enum e)
        {
            return GetDescription(e) ?? e.ToString();
        }

        /// <summary>
        /// Función para obtener la descripción de un enumerador
        /// Gracias a Thomas Levesque en stack overflow por la función
        /// https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string? GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute? attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }

    internal static class Program
    {
        /// <summary>
        /// Propiedad deprecada en favor de SistemaCentral.Cuentas.cuentaEnSesion<br/>
        /// No la quito por si se rompe algo pwp
        /// </summary>
        public static string nombreDeUsuario = "";
        public static DebugLog? logForm = null;

        // Gracias a knighter en Stack Overflow, pueden ver su respuesta en como poner una fuente custom en WinForms:
        // https://stackoverflow.com/questions/556147/how-do-i-embed-my-own-fonts-in-a-winforms-app
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private static PrivateFontCollection fonts = new PrivateFontCollection();

        /// <summary>
        /// Fuente Principal del programa, si es null no se cargó bien.
        /// </summary>
        public static FontFamily? mFont = null;

        static FontFamily? InitCustomFont()
        {
            // No me pregunten que hace esta mamada porque NO SE
            // Lo unico que se es que hay que poner la fuente como un Embedded Resource
            try
            {
                byte[] fontData = Properties.Resources.Rubik;
                IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
                System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                uint dummy = 0;
                fonts.AddMemoryFont(fontPtr, Properties.Resources.Rubik.Length);
                AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Rubik.Length, IntPtr.Zero, ref dummy);
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

                return fonts.Families[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error cargando fuente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Loggea algun resultado para el debug.
        /// </summary>
        public static void Log(string _mensaje)
        {
            // Si el logForm no es nulo, procedemos:
            if (logForm is not null)
            {
                // Escribimos la hora loggeada y el mensaje
                logForm.listContainer.Text += (
                    $"[{DateTime.Now:hh:mm:ss:ffff}]: {_mensaje}\n");
            }
        }

        /// <summary> Función para aplicar una tipografía (<paramref name="ff"/>) en un formulario (<paramref name="form"/>) 
        /// <para>
        /// Esta función usa la función <seealso cref="FormHelper.GetControls(Form)"/><br/>
        /// El retorno de esta función es una lista que NO incluye el control pasado como argumento<br/><br/>
        /// Si quieres obtener todos los descendientes de un control te puede interesar <seealso cref="FormHelper.GetControls(Control)"/>
        /// </para>
        /// </summary>
        /// <param name="ff">Tipografía a usar</param>
        /// <param name="form">Formulario en donde se aplicará la tipografía</param>
        public static void ApplyFont(FontFamily ff, Form form)
        {
            foreach(Control c in FormHelper.GetControls(form))
                c.Font = new Font(ff, c.Font.Size, c.Font.Style);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mFont = InitCustomFont();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //logForm = new DebugLog();
            //logForm.Show();

            SistemaCentral.Cuentas.archivoCuentas = "cuentas.bin";
            SistemaCentral.Cuentas.CargarCuentas();

            SistemaCentral.Inventario.archivoProductos = "inventario.bin";
            SistemaCentral.Inventario.CargarProductos();

            SistemaCentral.Accesos.archivoAccesos = "accesos.bin";
            SistemaCentral.Accesos.CargarAccesos();

            if(SistemaCentral.Cuentas.cuentasEnMemoria.Count == 0)
            {
                Cuenta adminDefault = new Cuenta(
                    _id: 0,
                    _nombre: "admin",
                    _nombredisplay: "Administrador",
                    _rol: Roles.GERENTE,
                    _contraseña: new HashSalt("1234")
                    );
                adminDefault.ContraseñaTemporal = true;
                SistemaCentral.Cuentas.AñadirCuenta(adminDefault);
            }

            //if(Program.mFont is not null) Application.SetDefaultFont(new Font(Program.mFont, 9));
            Application.Run(new InicioDeSesion());
        }
    }

    // La clase del sistema central yay :3333333
    // Aqui se guarda toda la información de manera centralizada
    // Decidi hacer la clase estática porque tener una instancia me es horripilante

    /// <summary>
    /// Clase del sistema central donde se almacenan todos lo datos
    /// </summary>
    static class SistemaCentral
    {
        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con cuentas
        /// </summary>
        public static class Cuentas
        {
            /// <summary> Cuenta en sesión a cargo del programa </summary>
            public static Cuenta? cuentaEnSesion = null;
            /// <summary> Lista de cuentas cargadas en memoria </summary>
            private static List<Cuenta> _IcuentasEnMemoria = new();
            /// <summary> Una función solo puede leer las cuentas, para cambiar la lista tendrá que usar una de las funciones de la clase </summary>
            public static IReadOnlyList<Cuenta> cuentasEnMemoria { get { return _IcuentasEnMemoria; } }
            /// <summary> String que contiene la ruta de archivo en donde se almacenan las cuentas </summary>
            public static string archivoCuentas = "";

            /// <summary>
            ///  Función para establecer una cuenta en memoria
            /// </summary>
            public static void EstablecerIndice(int indice, Cuenta nuevaCuenta)
            {
                // Si el índice o el nombre de usuario ya existen en la lista, retornar falso
                if (cuentasEnMemoria.Any(c => c.Id == nuevaCuenta.Id || c.NombreUsuario == nuevaCuenta.NombreUsuario))
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El ID o el nombre de usuario ya existen en la lista de cuentas");
                }

                // Si el índice esta fuera de rango, se tira otro error
                if (indice < 0 || indice > _IcuentasEnMemoria.Count)
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El índice está fuera de rango");
                }

                // Si no, se establece
                _IcuentasEnMemoria[indice] = nuevaCuenta;
            }

            /// <summary>
            /// Función para añadir una cuenta a memoria
            /// </summary>
            public static void AñadirCuenta(Cuenta nuevaCuenta)
            {
                // Si el índice o el nombre de usuario ya existen en la lista, retornar falso
                if (cuentasEnMemoria.Any(c => c.Id == nuevaCuenta.Id || c.NombreUsuario == nuevaCuenta.NombreUsuario))
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El ID o el nombre de usuario ya existen en la lista de cuentas");
                }
                // Si no, se añade
                _IcuentasEnMemoria.Add(nuevaCuenta);
            }

            /// <summary>
            /// Refrezcar cuenta en memoria
            /// </summary>
            public static void RefrezcarCuenta(Cuenta cuenta)
            {
                // Se busca el índice de la cuenta
                int indice = CuentaEnMemoria(cuenta);
                // Si no se encuentra, tira un error
                if (indice == -1) throw new Exception("La cuenta no se encuentra en memoria");
                // Si se encuentra, se establece
                else _IcuentasEnMemoria[indice] = cuenta;
            }

            /// <summary>
            ///  Función para guardar las cuentas en memoria en archivoCuentas
            /// </summary>
            public static void GuardarCuentas()
            {
                // Se crea una instancia del servicio de archivo para cuentas
                ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
                // Dar una lista con solo las cuentas que YA tienen su sesión iniciada
                List<Cuenta> listaVerdadera = new List<Cuenta>() { };
                // Iterar por cada cuenta en memoria para ver cuales tienen su sesión iniciada
                foreach (Cuenta cuenta in _IcuentasEnMemoria)
                    // Si la cuenta tiene su sesión iniciada, añadirla a la lista
                    if (cuenta.SesionIniciada) listaVerdadera.Add(cuenta);
                // Se llama la función para guardar
                sac.EscribirCuentas(listaVerdadera, archivoCuentas);
            }

            /// <summary>
            ///  Función para cargar las cuentas a memoria del archivo archivoCuentas
            /// </summary>
            public static void CargarCuentas()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoCuentas))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoCuentas} no existe, se iniciará con la cuenta default de administrador (admin, 1234)");
                    return;
                }
                // Se crea una instancia del servicio de archivo para cuentas
                ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
                // Se llama la función para cargar
                List<Cuenta> cuentasCargadas = sac.LeerCuentas(archivoCuentas);
                // Si no se cargaron cuentas se mantiene la lista original (admin, 1234)
                if (cuentasCargadas.Count != 0)
                    _IcuentasEnMemoria = cuentasCargadas;
                else
                    Program.Log($"El archivo {archivoCuentas} no contiene datos o no se pudo leer, se iniciará con la cuenta default de administrador (admin, 1234)");
            }

            /// <summary>
            /// Función para obtener el índice en memoria de una cuenta dada su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a cuentas específicas
            /// </summary>
            /// <param name="_id">Id de la cuenta a buscar</param>
            /// <returns> El id de la cuenta en memoria o -1 si no se encontró </returns>
            public static int CuentaEnMemoria(int _id)
            {
                // Se busca en un for todas las cuentas en memoria
                for (int i = 0; i < _IcuentasEnMemoria.Count; i++)
                {
                    // Si el id de alguna coincide, se retorna el índice actual
                    if (_IcuentasEnMemoria[i].Id == _id) return i;
                }
                // Si no se encuentra, se retorna -1
                return -1;
            }

            /// <summary>
            /// Función para obtener el índice en memoria de una cuenta,
            /// se encuentra usando su ID y <seealso cref="IndiceEnMemoria(int)"/>
            /// </summary>
            /// <param name="_cuenta">Objeto de la cuenta a buscar</param>
            /// <returns> El id de la cuenta en memoria o -1 si no se encontró </returns>
            public static int CuentaEnMemoria(Cuenta _cuenta)
            {
                // Se llama a IndiceEnMemoria con el id de la cuenta
                return CuentaEnMemoria(_cuenta.Id);
            }

            /// <summary>
            /// Función para obtener una cuenta en memoria dada su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a cuentas específicas
            /// </summary>
            /// <param name="_id">Id de la cuenta a buscar</param>
            /// <returns> La cuenta en memoria o null si no se encontró </returns>
            public static Cuenta? ObtenerCuentaPorId(int _id)
            {
                // Se busca en un for todas las cuentas en memoria
                for (int i = 0; i < _IcuentasEnMemoria.Count; i++)
                {
                    // Si el id de alguna coincide, se retorna la cuenta actual
                    if (_IcuentasEnMemoria[i].Id == _id) return _IcuentasEnMemoria[i];
                }
                // Si no se encuentra, se retorna null
                return null;
            }
        }

        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con el inventario
        /// </summary>
        public static class Inventario
        {
            /// <summary> Lista de productos en memoria </summary>
            public static List<Producto> productosEnMemoria = new();
            /// <summary> String que contiene la ruta de archivo en donde se almacenan los productos </summary>
            public static string archivoProductos = "";

            /// <summary>
            /// Función para guardar los productos en memoria en archivoProductos
            /// </summary>
            public static void GuardarProductos()
            {
                // Se crea una instancia del servicio de archivo para productos
                ServicioArchivoInventario sai = new ServicioArchivoInventario();
                // Se llama la función para guardar
                sai.EscribirProductos(productosEnMemoria, archivoProductos);
            }

            /// <summary>
            /// Función para cargar los productos a memoria del archivo archivoProductos
            /// </summary>
            public static void CargarProductos()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoProductos))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoProductos} no existe, se iniciará sin productos previos.");
                    return;
                }
                // Se crea una instancia del servicio de archivo para productos
                ServicioArchivoInventario sai = new ServicioArchivoInventario();
                // Se llama la función para cargar
                List<Producto> productosCargados = sai.LeerProductos(archivoProductos);
                // Si no se cargaron productos se mantiene la lista original vacía
                if (productosCargados.Count != 0)
                    productosEnMemoria = productosCargados;
                else
                    Program.Log($"El archivo {archivoProductos} no contiene datos o no se pudo leer, se iniciará sin productos previos.");
            }

            /// <summary>
            /// Función para obtener el índice en memoria de un producto dado su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a productos específicos
            /// </summary>
            /// <param name="_id">Id del producto a buscar</param>
            /// <returns> El índice del producto en memoria o -1 si no se encontró </returns>
            public static int ProductoEnMemoria(int _id)
            {
                // Se busca en un for todos los productos en memoria
                for (int i = 0; i < productosEnMemoria.Count; i++)
                {
                    // Si el id de alguno coincide, se retorna el índice actual
                    if (productosEnMemoria[i].Id == _id) return i;
                }
                // Si no se encuentra, se retorna -1
                return -1;
            }

            /// <summary>
            /// Función para obtener el índice en memoria de un producto,
            /// se encuentra usando su ID y <seealso cref="IndiceEnMemoria(int)"/>
            /// </summary>
            /// <param name="_producto">Objeto del producto a buscar</param>
            /// <returns> El índice del producto en memoria o -1 si no se encontró </returns>
            public static int ProductoEnMemoria(Producto _producto)
            {
                // Se llama a IndiceEnMemoria con el id del producto
                return ProductoEnMemoria(_producto.Id);
            }

            /// <summary>
            /// Función para obtener un producto en memoria dado su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a productos específicos
            /// </summary>
            /// <param name="_id">Id del producto a buscar</param>
            /// <returns> El producto en memoria o null si no se encontró </returns>
            public static Producto? ObtenerProductoPorId(int _id)
            {
                // Se busca en un for todos los productos en memoria
                for (int i = 0; i < productosEnMemoria.Count; i++)
                {
                    // Si el id de alguno coincide, se retorna el producto actual
                    if (productosEnMemoria[i].Id == _id) return productosEnMemoria[i];
                }
                // Si no se encuentra, se retorna null
                return null;
            }
        }

        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con los accesos
        /// </summary>
        public class Accesos
        {
            /// <summary> Lista de accesos en memoria </summary>
            public static List<Acceso> accesosEnMemoria = new();
            /// <summary> String que contiene la ruta de archivo en donde se almacenan los productos </summary>
            public static string archivoAccesos = "";

            /// <summary>
            /// Función para guardar los accesos en memoria en archivoAccesos
            /// </summary>
            public static void GuardarAccesos()
            {
                // Se crea una instancia del servicio de archivo para accesos
                ServicioArchivoAccesos saa = new ServicioArchivoAccesos();
                // Se llama la función para guardar
                saa.EscribirAccesos(accesosEnMemoria, archivoAccesos);
            }

            /// <summary>
            /// Función para cargar los accesos a memoria del archivo archivoAccesos
            /// </summary>
            public static void CargarAccesos()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoAccesos))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoAccesos} no existe, se iniciará sin accesos previos.");
                    return;
                }
                // Se crea una instancia del servicio de archivo para accesos
                ServicioArchivoAccesos saa = new ServicioArchivoAccesos();
                // Se llama la función para cargar
                List<Acceso> accesosCargados = saa.LeerAccesos(archivoAccesos);
                // Si no se cargaron accesos se mantiene la lista original vacía
                if (accesosCargados.Count != 0)
                    accesosEnMemoria = accesosCargados;
                else
                    Program.Log($"El archivo {archivoAccesos} no contiene datos o no se pudo leer, se iniciará sin accesos previos.");
            }
        }
    }
}