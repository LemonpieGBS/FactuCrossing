namespace FactuCrossing.Formularios.Administrador
{
    public partial class MenuAdministrador : Form
    {
        public MenuAdministrador()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ResetearContraseñas frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }

        private void btnAdministrar_Click(object sender, EventArgs e)
        {
            
            this.Enabled = false;
            AdministrarPersonal frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Generador_de_Reportes frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }
    }
}
