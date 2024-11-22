namespace FactuCrossing.Formularios
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);

            lblHola.Text = $"Hola, {Program.nombreDeUsuario}";

            DateTime dt = DateTime.Now;

            lblTiempo.Text = (dt.Hour <= 12) ? $"Sesión Iniciada: {dt.Hour:00}:{dt.Minute:00}am" :
                $"Sesión Iniciada: {(dt.Hour % 12):00}:{dt.Minute:00}pm";
        }

        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Cerrar Sesión",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                e.Cancel = true;
            }

        }

        private void btnFacturación_Click(object sender, EventArgs e)
        {
            this.Hide();
            Facturación.Facturación frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inventario.Inventario frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reportes.GeneracionDeReportes frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }

        private void btnAdministradores_Click(object sender, EventArgs e)
        {
            this.Hide();
            Administrador.MenuAdministrador frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }
    }
}
