using FactuCrossing.Formularios;

namespace FactuCrossing
{
    public partial class InicioDeSesion : Form
    {
        public InicioDeSesion()
        {
            InitializeComponent();
        }

        private void linkOlvidaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Porfavor contacta con el administrador del sistema", "Contrase�a Olvidada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if(txtContrase�a.Text == string.Empty || txtNombreUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo de usuario o contrase�a esta vac�o", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Program.nombreDeUsuario = txtNombreUsuario.Text;

            this.Hide();
            MenuPrincipal frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }
    }
}
