using FactuCrossing.Estructuras;
using FactuCrossing.Servicios;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FactuCrossing
{
    internal static class Program
    {
        public static SistemaCentral sistemaCentral = new();
        public static string nombreDeUsuario = "";

        // Gracias a knighter en Stack Overflow, pueden ver su respuesta en como poner una fuente custom en WinForms:
        // https://stackoverflow.com/questions/556147/how-do-i-embed-my-own-fonts-in-a-winforms-app
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private static PrivateFontCollection fonts = new PrivateFontCollection();

        public static FontFamily? mFont = null;

        static FontFamily? InitCustomFont()
        {
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

        // Estas funciones las voy a usar despues, consiguen recursivamente todos los controles de un formulario
        // Incluyendo los controles que albergan mas controles, como los GroupBoxes
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

        public static List<Control> GetControls(Form f)
        {
            List<Control> returnList = new List<Control>() { };
            foreach(Control c in f.Controls)
            {
                returnList.Add(c);
                if (c.HasChildren) returnList.AddRange(GetControls(c));
            }
            return returnList;
        }

        public static void ApplyFont(FontFamily ff, Form form)
        {
            foreach(Control c in GetControls(form))
                c.Font = new Font(ff, c.Font.Size, c.Font.Style);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mFont = InitCustomFont();
            sistemaCentral.EstablecerArchivoCuentas("cuentas.fcacc");
            sistemaCentral.CargarCuentas();

            sistemaCentral.EstablecerArchivoInventario("inventario.fcinv");
            sistemaCentral.CargarInventario();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Formularios.InicioDeSesion());
        }
    }

    internal class SistemaCentral
    {
        public List<Cuenta> cuentas = new();
        public string archivoCuentas = "";

        public List<Producto> inventario = new();
        public string archivoInventario = "";

        public SistemaCentral()
        {
            cuentas.Add( new(cuentas.Count,"admin","1234",Roles.GERENTE) );
        }

        public void EstablecerArchivoCuentas(string rutaArchivo) { archivoCuentas = rutaArchivo; }

        public void GuardarCuentas()
        {
            ServicioArchivoCuentas sac = new();
            sac.GuardarCuentas(cuentas, archivoCuentas);
        }

        public void CargarCuentas()
        {
            if (!File.Exists(archivoCuentas)) return;

            ServicioArchivoCuentas sac = new();
            List<Cuenta> cuentasCargadas = sac.CargarCuentas(archivoCuentas);

            cuentas = (cuentasCargadas.Count == 0) ? cuentas : cuentasCargadas;
        }

        public void EstablecerArchivoInventario(string rutaArchivo) { archivoInventario = rutaArchivo; }

        public void GuardarInventario()
        {
            ServicioArchivoProductos sap = new();
            sap.GuardarProductos(inventario, archivoInventario);
        }

        public void CargarInventario()
        {
            if (!File.Exists(archivoInventario)) return;

            ServicioArchivoProductos sap = new();
            List<Producto> productosCargados = sap.CargarProductos(archivoInventario);

            inventario = (productosCargados.Count == 0) ? inventario : productosCargados;
        }
    }
}