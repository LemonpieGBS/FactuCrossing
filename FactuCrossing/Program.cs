using FactuCrossing.Estructuras;
using FactuCrossing.Servicios;
using System.Drawing.Text;
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

    internal static class Program
    {
        /// <summary>
        /// Propiedad deprecada en favor de SistemaCentral.cuentaEnSesion<br/>
        /// No la quito por si se rompe algo pwp
        /// </summary>
        public static string nombreDeUsuario = "";

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

        /// <summary> Función para aplicar una tipografía (<paramref name="ff"/>) en un formulario (<paramref name="form"/>) 
        /// <para>
        /// Esta función usa la función<br/>
        /// El retorno de esta función es una lista que NO incluye el control pasado como argumento<br/><br/>
        /// Si quieres obtener todos los descendientes de un control te puede interesar <seealso cref="GetControls(Control)"/>
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
            SistemaCentral.archivoCuentas = "cuentas.fcacc";
            SistemaCentral.CargarCuentas();

            SistemaCentral.archivoInventario = "inventario.fcinv";
            SistemaCentral.CargarInventario();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //if(Program.mFont is not null) Application.SetDefaultFont(new Font(Program.mFont, 9));
            Application.Run(new Formularios.InicioDeSesion());
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
        /// <summary> Cuenta en sesión a cargo del programa </summary>
        public static Cuenta? cuentaEnSesion = null;
        /// <summary> Lista de cuentas cargadas en memoria </summary>
        public static List<Cuenta> cuentasEnMemoria = new();
        /// <summary> String que contiene la ruta de archivo en donde se almacenan las cuentas </summary>
        public static string archivoCuentas = "";

        /// <summary> Lista de productos en memoria </summary>
        public static List<Producto> inventarioEnMemoria = new();
        /// <summary> String que contiene la ruta de archivo en donde se almacenan los productos </summary>
        public static string archivoInventario = "";

        /// <summary> Constructor del sistema </summary>
        static SistemaCentral()
        {
            // Si no hay cuentas, se agrega una de administrador por default
            Cuenta adminDefault = new Cuenta(
                _id: 0,
                _nombre: "admin",
                _nombredisplay: "Administrador",
                _rol: Roles.GERENTE,
                _contraseña: new HashSalt("1234")
                );
            // Se establece la contraseña como temporal para que el administrador cree la suya propia
            adminDefault.ContraseñaTemporal = true;
            // Se añade a las cuentas en memoria
            cuentasEnMemoria.Add(adminDefault);
        }

        /// <summary>
        ///  Función para guardar las cuentas en memoria en archivoCuentas
        /// </summary>
        public static void GuardarCuentas()
        {
            // Se crea una instancia del servicio de archivo para cuentas
            ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
            // Se llama la función para guardar
            sac.GuardarCuentas(cuentasEnMemoria, archivoCuentas);
        }

        /// <summary>
        ///  Función para cargar las cuentas a memoria del archivo archivoCuentas
        /// </summary>
        public static void CargarCuentas()
        {
            // Si el archivo no existe, se retorna
            if (!File.Exists(archivoCuentas)) return;
            // Se crea una instancia del servicio de archivo para cuentas
            ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
            // Se llama la función para cargar
            List<Cuenta> cuentasCargadas = sac.CargarCuentas(archivoCuentas);
            // Si no se encontraron cuentas cargadas, dejar las cuentasEnMemoria como estan
            cuentasEnMemoria = (cuentasCargadas.Count == 0) ? cuentasEnMemoria : cuentasCargadas;
        }

        /// <summary>
        /// Función para obtener el índice en memoria de una cuenta dada su ID,
        /// los IDs son únicos así que esta función es primordial para acceder a cuentas específicas
        /// </summary>
        /// <param name="_id">Id de la cuenta a buscar</param>
        /// <returns> El id de la cuenta en memoria o -1 si no se encontró </returns>
        public static int IndiceEnMemoria(int _id)
        {
            // Se busca en un for todas las cuentas en memoria
            for(int i = 0; i < cuentasEnMemoria.Count; i++)
            {
                // Si el id de alguna coincide, se retorna el índice actual
                if (cuentasEnMemoria[i].Id == _id) return i;
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
        public static int IndiceEnMemoria(Cuenta _cuenta)
        {
            // Se llama a IndiceEnMemoria con el id de la cuenta
            return IndiceEnMemoria(_cuenta.Id);
        }

        /// <summary>
        ///  Función para guardar los productos en memoria en archivoInventario
        /// </summary>
        public static void GuardarInventario()
        {
            // Se crea una instancia del servicio de archivo para inventario
            ServicioArchivoProductos sap = new();
            // Se llama la función para guardar
            sap.GuardarProductos(inventarioEnMemoria, archivoInventario);
        }

        /// <summary>
        ///  Función para cargar los productos a memoria del archivo archivoInventario
        /// </summary>
        public static void CargarInventario()
        {
            // Si el archivo no existe, se retorna
            if (!File.Exists(archivoInventario)) return;
            // Se crea una instancia del servicio de archivo para inventario
            ServicioArchivoProductos sap = new();
            // Se llama la función para cargar
            List<Producto> productosCargados = sap.CargarProductos(archivoInventario);
            // Si no se encontraron productos cargados, dejar el inventarioEnMemoria como esta
            inventarioEnMemoria = (productosCargados.Count == 0) ? inventarioEnMemoria : productosCargados;
        }
    }
}