using FactuCrossing.Estructuras;

namespace FactuCrossing.Formularios;

public partial class InicioDeSesion : Form
{
    // Constructor del form en donde ponemos en orden todo
    public InicioDeSesion()
    {
        // Inicializamos el componente de WinForms
        InitializeComponent();
        // Si la fuente principal no es nula, la aplicamos
        if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
    }
    
    // Evento si se cliquea '¿Olvidaste tu Contraseña?'
    private void linkOlvidaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        // Mostrar un mensajito para avisar al usuario que debe contactar con su administrador del sistema
        MessageBox.Show("Porfavor contacta con el administrador del sistema", "Contraseña Olvidada",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Evento si se da click en Iniciar Sesión
    private void btnIniciarSesion_Click(object sender, EventArgs e)
    {
        // Primero validamos que ninguno de los campos este vacío
        if(txtContraseña.Text == string.Empty || txtNombreUsuario.Text == string.Empty)
        {
            // Mensajito para mostrarle el error al usuario
            MessageBox.Show("El campo de usuario o contraseña esta vacío", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        // Segundo validamos que ninguno de los campos tenga espacios
        if (txtContraseña.Text.Any(char.IsWhiteSpace) || txtNombreUsuario.Text.Any(char.IsWhiteSpace))
        {
            // Mensajito para mostrarle el error al usuario
            MessageBox.Show("Los campos de usuario y contraseña no pueden tener espacios", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        // Aqui ahora vamos a ver si el usuario logra acceder a alguna cuenta
        // Empezamos con una cuenta nula
        Cuenta? cuentaUsuario = null;
        //Hacemos un foreach para ver si alguna cuenta coincide con los datos de registro
        foreach(Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
        {
            // Validamos si los nombres de usuario y las contraseñas coinciden
            if(txtNombreUsuario.Text == cuenta.NombreUsuario && cuenta.CompararContraseña(txtContraseña.Text))
            {
                // Si los datos son correctos pero la cuenta esta deshabilitada, mandamos un mensaje
                if(!cuenta.Habilitada)
                {
                    // Mensajito para mostrarle el error al usuario
                    MessageBox.Show("Esta cuenta se encuentra deshabilitada, en el caso de ser un error contactar con un administrador", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Si no, asignamos la cuenta y ya nos podemos ir del foreach
                cuentaUsuario = cuenta;
                break; // Este break rompe el foreach porque ya encontramos la cuenta
            }
        }

        // Si cuentaUsuario es nulo quiere decir que no se encontró la cuenta
        if (cuentaUsuario is null)
        {
            // Mensajito para mostrarle el error al usuario
            MessageBox.Show("Nombre de Usuario o Contraseña incorrecto", "No se pudo iniciar sesión",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Si se encontró, hacemos el proceso para iniciar sesión
        else
        {
            // Si la contraseña del usuario es temporal, hacer el proceso para que cree su contraseña
            if(cuentaUsuario.ContraseñaTemporal)
            {
                // String que almacenará la contraseña nueva del usuario
                string contrasena;
                // Creamos un form de creación de contraseña
                Utilidades.CreacionDeContraseña iform = new Utilidades.CreacionDeContraseña();
                // Si no retorna DialogResult.OK (es decir, la interacción no fue exitosa)
                // nos vamos de aquí
                if (iform.ShowDialog(this) != DialogResult.OK) return;
                // En el caso que si retorne DialogResult.OK, seguimos con el proceso
                // Guardamos la contraseña proveída en nuestra variable
                contrasena = iform.Contrasena;
                // Nos deshacemos del form
                iform.Dispose();
                // Aquí creamos las nuevas credenciales de la cuenta haciendo uso del constructor de la clase
                cuentaUsuario = new Cuenta(
                    // La cuenta tendrá el mismo ID
                    _id: cuentaUsuario.Id,
                    // La cuenta tendrá el mismo nombre de usuario
                    _nombre: cuentaUsuario.NombreUsuario,
                    // La cuenta tendrá el mismo nombre de display
                    _nombredisplay: cuentaUsuario.NombreDisplay,
                    // La cuenta tendrá el mismo rol
                    _rol: cuentaUsuario.Rol,
                    // La cuenta tendrá una nueva contraseña basada en la proveída
                    _contraseña: new HashSalt(contrasena)
                    );
                // Sobreescribimos directamente en el sistema central
                // Buscamos si existe el registro en la memoria
                if(SistemaCentral.Cuentas.CuentaEnMemoria(cuentaUsuario) == -1)
                {
                    // Si no, mandamos un mensajito de error
                    MessageBox.Show($"No se pudo encontrar la cuenta referenciada, id: {cuentaUsuario.Id}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Si si, entonces sobreescribimos
                SistemaCentral.Cuentas.RefrezcarCuenta(cuentaUsuario);
                // Guardamos los cambios
                SistemaCentral.Cuentas.GuardarCuentas();
            }
            // Variable 'nombreDeUsuario' deprecada en favor a 'SistemaCentral.Cuentas.cuentaEnSesion'
            /* -- Program.nombreDeUsuario = cuentaUsuario.NombreDisplay; */
            // Establecemos la cuenta en sesión como la cuenta registrada del usuario
            SistemaCentral.Cuentas.cuentaEnSesion =
                SistemaCentral.Cuentas.cuentasEnMemoria[SistemaCentral.Cuentas.CuentaEnMemoria(cuentaUsuario)];
            // Añadimos el acceso a la memoria
            SistemaCentral.Accesos.accesosEnMemoria.Add(new Acceso(cuentaUsuario.Id, DateTime.Now, TipoDeAcceso.ENTRADA));
            // Guardamos los accesos
            SistemaCentral.Accesos.GuardarAccesos();
            // Ocultamos este form
            this.Hide();
            // Creamos un form de Menu Principal
            MenuPrincipal frm = new();
            // Lo mostramos
            frm.Show();
            // Asignamos una función delegada, cuando el form del Menu Principal se cierre,
            // este se mostrará de nuevo
            frm.FormClosed += delegate { this.Show(); };
        }
    }
}
