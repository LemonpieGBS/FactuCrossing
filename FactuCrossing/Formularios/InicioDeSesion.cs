using FactuCrossing.Estructuras;

namespace FactuCrossing.Formularios;

public partial class InicioDeSesion : Form
{
    public InicioDeSesion()
    {
        InitializeComponent();
        if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
    }

    private void linkOlvidaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        MessageBox.Show("Porfavor contacta con el administrador del sistema", "Contraseña Olvidada",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnIniciarSesion_Click(object sender, EventArgs e)
    {
        if(txtContraseña.Text == string.Empty || txtNombreUsuario.Text == string.Empty)
        {
            MessageBox.Show("El campo de usuario o contraseña esta vacío", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Cuenta? cuentaUsuario = null;
        foreach(Cuenta cuenta in Program.sistemaCentral.cuentas)
        {
            if (!cuenta.Habilitada) continue;

            if(txtNombreUsuario.Text == cuenta.NombreUsuario && cuenta.ValidarContrasena(txtContraseña.Text))
            {
                cuentaUsuario = cuenta;
                break;
            }
        }

        if (cuentaUsuario is null)
        {
            MessageBox.Show("Nombre de Usuario o Contraseña incorrecto", "No se pudo iniciar sesión",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            if(cuentaUsuario.Temporal)
            {
                string contrasena;

                Utilidades.CreacionDeContraseña iform = new Utilidades.CreacionDeContraseña();

                if (iform.ShowDialog(this) != DialogResult.OK) return;
                contrasena = iform.Contrasena;

                iform.Dispose();

                cuentaUsuario.EstablecerContrasena(contrasena);
                cuentaUsuario.EstablecerTemporal(false);

                Program.sistemaCentral.GuardarCuentas();
            }

            Program.nombreDeUsuario = cuentaUsuario.NombreDisplay;
            Program.sistemaCentral.cuentaEnSesion = cuentaUsuario;

            this.Hide();
            MenuPrincipal frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }
    }
}
