using FactuCrossing.Estructuras;
using FactuCrossing.Formularios;
using FactuCrossing.Formularios.Utilidades;
using System.Drawing.Text;

namespace FactuCrossing
{
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

            logForm = new DebugLog();
            logForm.Show();

            // Si el directorio de datos no existe, lo creamos
            if(!Directory.Exists(FileHelper.SaveDataPath))
            {
                // Creamos el folder
                Directory.CreateDirectory(FileHelper.SaveDataPath);
            }

            // Cargamos todos los elementos del sistema
            SistemaCentral.Cuentas.CargarCuentas();
            SistemaCentral.Inventario.CargarProductos();
            SistemaCentral.Accesos.CargarAccesos();

            // Creamos una cuenta de administrador si no hay nignuna cuenta existente
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
            // Corremos el formulario con el reporte creado
            Application.Run(new InicioDeSesion());
        }
    }
}