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

            if(txtNombreUsuario.Text == cuenta.NombreUsuario && cuenta.CompararContraseña(txtContraseña.Text))
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
            if(cuentaUsuario.ContraseñaTemporal)
            {
                string contrasena;

                Utilidades.CreacionDeContraseña iform = new Utilidades.CreacionDeContraseña();

                if (iform.ShowDialog(this) != DialogResult.OK) return;
                contrasena = iform.Contrasena;

                iform.Dispose();

                cuentaUsuario = new Cuenta(
                    _id: cuentaUsuario.Id,
                    _nombre: cuentaUsuario.NombreUsuario,
                    _nombredisplay: cuentaUsuario.NombreDisplay,
                    _rol: cuentaUsuario.Rol,
                    _contraseña: new HashSalt(contrasena)
                    );

                Program.sistemaCentral.cuentas[cuentaUsuario.Id] = cuentaUsuario;
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
