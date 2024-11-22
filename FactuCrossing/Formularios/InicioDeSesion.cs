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

        Cuenta? cuentaUsuario = null;
        foreach(Cuenta cuenta in Program.sistemaCentral.cuentas)
        {
            if (!cuenta.Habilitada) continue;

            if(txtNombreUsuario.Text == cuenta.NombreUsuario && cuenta.ValidarContrasena(txtContrase�a.Text))
            {
                cuentaUsuario = cuenta;
                break;
            }
        }

        if (cuentaUsuario is null)
        {
            MessageBox.Show("Nombre de Usuario o Contrase�a incorrecto", "No se pudo iniciar sesi�n",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            if(cuentaUsuario.Temporal)
            {
                string contrasena;

                do
                {
                    contrasena = Microsoft.VisualBasic.Interaction.InputBox("Es tu primera vez iniciando sesi�n, crea la contrase�a para tu usuario", "Crear Contrase�a", "");
                    if (contrasena == string.Empty)
                    {
                        MessageBox.Show("La contrase�a no puede estar vac�a", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } while (contrasena == string.Empty);

                cuentaUsuario.EstablecerContrasena(contrasena);
                cuentaUsuario.EstablecerTemporal(false);

                Program.sistemaCentral.GuardarCuentas();
            }

            Program.nombreDeUsuario = cuentaUsuario.NombreDisplay;

            this.Hide();
            MenuPrincipal frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Show(); };
        }
    }
}
