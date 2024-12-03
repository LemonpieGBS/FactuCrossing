using FactuCrossing.Estructuras;
using System.Diagnostics.CodeAnalysis;

namespace FactuCrossing.Formularios
{
    /// <summary>
    /// Clase parcial del formulario principal del menú
    /// </summary>
    public partial class MenuPrincipal : Form
    {
        /// <summary>
        /// Propiedad cuentaEnSesion de la propia clase
        /// </summary>
        Cuenta cuentaEnSesion;

        /// <summary>
        /// Constructor del formulario
        /// </summary>
        public MenuPrincipal()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, cerrar el formulario
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                // Asignamos la cuenta default
                cuentaEnSesion = Cuenta.CuentaDefault;
                return;
            }
            // Si se encontró, asignamos a la propiedad de la clase
            cuentaEnSesion = SistemaCentral.Cuentas.cuentaEnSesion;
            // Inicializa el componente de Winforms
            InitializeComponent();
            // Aplicamos la tipografía del programa si existe
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Ponemos el texto del label para saludar a la persona que inició sesión
            lblHola.Text = "Hola, " + cuentaEnSesion.NombreDisplay;
            // dt en lugar de DateTime.Now para acortar
            DateTime dt = DateTime.Now;
            // Ponemos el tiempo de la sesión iniciada, dependiendo de la hora para usar am o pm
            lblTiempo.Text = (dt.Hour <= 12) ? $"Sesión Iniciada: {dt.Hour:00}:{dt.Minute:00}am" :
                $"Sesión Iniciada: {(dt.Hour % 12):00}:{dt.Minute:00}pm";
        }

        // Evento para cerrar sesión cuando se hace clic en el botón de cerrar sesión
        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario
            this.Close();
        }

        // Evento que se ejecuta cuando el formulario se carga
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            // Aquí se puede agregar código que se ejecuta cuando el formulario se carga
        }

        // Evento para manejar el cierre del formulario y preguntar al usuario si está seguro
        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Mostrar un mensaje al usuario para saber si está seguro de cerrar sesión
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Cerrar Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                // Si el resultado no es 'sí', cancelar el evento de cierre
                e.Cancel = true;
            }
            else
            {
                // Añadimos el acceso de salida a la memoria [DEPRECADO]
                // SistemaCentral.Accesos.accesosEnMemoria.Add(new Acceso(cuentaEnSesion.Id, DateTime.Now, TipoDeAcceso.SALIDA));
                // Guardamos los accesos
                SistemaCentral.Accesos.GuardarAccesos();
            }
            // Actualizamos la sesión
            SistemaCentral.Cuentas.CalcularTiempoDeSesion();
        }

        /// <summary>
        /// Método que se ejecuta cuando se muestra el formulario
        /// </summary>
        private void OnShow()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, cerrar el formulario
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                return;
            }
            // Asignamos la cuenta en sesión
            this.cuentaEnSesion = SistemaCentral.Cuentas.cuentaEnSesion;
            // Actualizamos el texto del label de saludo
            lblHola.Text = $"Hola, {cuentaEnSesion.NombreDisplay}";
            // Mostramos el formulario
            this.Show();
            // Actualizamos la sesión
            SistemaCentral.Cuentas.CalcularTiempoDeSesion();
        }

        /// <summary>
        /// Método para abrir un formulario y ocultar el actual
        /// </summary>
        /// <param name="frm">Formulario a abrir</param>
        private void AbrirOcultarDelegar(Form frm)
        {
            // Ocultamos el formulario actual
            this.Hide();
            // Mostramos el nuevo formulario
            frm.Show();
            // Cuando el nuevo formulario se cierra, volvemos a mostrar el formulario actual
            frm.FormClosed += delegate { this.OnShow(); };
        }

        /// <summary>
        /// Evento para abrir el formulario de facturación
        /// </summary>
        private void btnFacturación_Click(object sender, EventArgs e)
        {
            AbrirOcultarDelegar(new Facturación.Facturación());
        }

        /// <summary>
        /// Evento para abrir el formulario de inventario
        /// </summary>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            if(cuentaEnSesion.Rol < Roles.GESTORDEINVENTARIO)
            {
                MessageBox.Show("No tienes permisos para acceder a esta sección", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AbrirOcultarDelegar(new Inventario.Inventario());
        }

        /// <summary>
        /// Evento para abrir el formulario de reportes
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirOcultarDelegar(new Reportes.GeneracionDeReportes());
        }

        /// <summary>
        /// Evento para abrir el formulario de administradores
        /// </summary>
        private void btnAdministradores_Click(object sender, EventArgs e)
        {
            if (cuentaEnSesion.Rol < Roles.ADMINISTRADOR)
            {
                MessageBox.Show("No tienes permisos para acceder a esta sección", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AbrirOcultarDelegar(new Administrador.MenuAdministrador());
        }

        // Evento que se ejecuta cuando el formulario se activa
        private void MenuPrincipal_Activated(object sender, EventArgs e)
        {
            // Aquí se puede agregar código que se ejecuta cuando el formulario se activa
        }
    }
}
