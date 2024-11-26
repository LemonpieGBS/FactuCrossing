using FactuCrossing.Estructuras;
using System.Diagnostics.CodeAnalysis;

namespace FactuCrossing.Formularios
{
    // Comentemos esta putisima mierda porque de paso tengo que refactorizar
    public partial class MenuPrincipal : Form
    {
        // Propiedad cuentaEnSesion de la propia clase
        Cuenta cuentaEnSesion;

        // Constructor del formulario
        public MenuPrincipal()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, MATAR
            if (SistemaCentral.cuentaEnSesion is null)
            {
                // Mostramos un mensajito diciendo que salió mal
                MessageBox.Show("Hubo un problema de autenticación, porfavor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                return;
            }
            // Si se encontró, asignamos a la propiedad de la clase
            cuentaEnSesion = SistemaCentral.cuentaEnSesion;
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
        
        // Si le damos a cerrar sesión cerramos
        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            // Cerrar
            this.Close();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }
        // Código para que cuando cierre el formulario pregunte al usuario si está seguro
        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Mostrar un mensaje al usuario para saber si esta seguro
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Cerrar Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                // Si el resultado no es 'si' cancelar el evento
                e.Cancel = true;
            }

        }

        private void OnShow()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, MATAR
            if (SistemaCentral.cuentaEnSesion is null)
            {
                // Mostramos un mensajito diciendo que salió mal
                MessageBox.Show("Hubo un problema de autenticación, porfavor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                return;
            }

            this.cuentaEnSesion = SistemaCentral.cuentaEnSesion;

            lblHola.Text = $"Hola, {cuentaEnSesion.NombreDisplay}";
            this.Show();
        }

        private void btnFacturación_Click(object sender, EventArgs e)
        {
            this.Hide();
            Facturación.Facturación frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.OnShow(); };
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inventario.Inventario frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.OnShow(); };
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reportes.GeneracionDeReportes frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.OnShow(); };
        }

        private void btnAdministradores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Administrador.MenuAdministrador frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.OnShow(); };
        }

        private void MenuPrincipal_Activated(object sender, EventArgs e)
        {
        }
    }
}
