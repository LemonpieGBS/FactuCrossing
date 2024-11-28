using FactuCrossing.Estructuras;
using FactuCrossing.Properties;
using System.Data;
using System.Resources;
using static FactuCrossing.SistemaCentral;

namespace FactuCrossing.Formularios.Administrador
{
    /// <summary>
    /// Clase parcial del formulario para administrar el personal
    /// </summary>
    public partial class AdministrarPersonal : Form
    {
        // Propiedad para almacenar el ID de la cuenta seleccionada
        int cuentaSeleccionada = -1;
        // Propiedad para almacenar la cuenta en sesión
        Cuenta cuentaEnSesion;

        // Diccionario para mapear los nombres de roles a los valores del enumerador Roles
        Dictionary<string, Roles> camposDeAcceso = new()
        {
            {"Facturista", Roles.FACTURISTA},
            {"Gestor de Inventario", Roles.GESTORDEINVENTARIO},
            {"Analista", Roles.ANALISTA},
            {"Administrador", Roles.ADMINISTRADOR},
            {"Gerente", Roles.GERENTE}
        };
        // Cosa
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministrarPersonal));

        /// <summary>
        /// Constructor del formulario
        /// </summary>
        public AdministrarPersonal()
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
                this.cuentaEnSesion = Cuenta.CuentaDefault;
                return;
            }
            // Si se encontró, asignamos a la propiedad de la clase
            this.cuentaEnSesion = SistemaCentral.Cuentas.cuentaEnSesion;

            // Inicializa el componente de Winforms
            InitializeComponent();
            // Actualizamos el DataGrid con las cuentas
            ActualizarDataGrid();
            // Aplicamos la tipografía del programa si existe
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Inicializamos el texto del status label
            statusLabel.Text = "No hay ningun empleado seleccionado";

            // Limpiamos los ítems del ComboBox de acceso
            cmbAcceso.Items.Clear();

            // Añadimos los ítems al ComboBox de acceso
            cmbAcceso.Items.AddRange(new object[]
            {
            "Facturista",
            "Gestor de Inventario",
            "Analista",
            "Administrador"
            });

            // Desseleccionamos cualquier ítem del ComboBox
            cmbAcceso.SelectedItem = null;

            // Si el rol de la cuenta en sesión es Gerente, activamos la opción de coronación
            btnCoronar.Enabled = (cuentaEnSesion.Rol == Roles.GERENTE);
            btnCoronar.Visible = (cuentaEnSesion.Rol == Roles.GERENTE);
        }

        /// <summary>
        /// Método para actualizar el DataGrid con las cuentas en memoria
        /// </summary>
        /// <param name="mostrarDeshabilitadas">Indica si se deben mostrar las cuentas deshabilitadas</param>
        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            // Limpiamos el DataSource del DataGridView
            dgvPersonal.DataSource = null;

            // Creamos una nueva tabla de datos
            DataTable dt = new();
            // Añadimos las columnas necesarias
            dt.Columns.AddRange(new DataColumn[] { new("ID"), new("Activo/a"), new("Nombre"), new("Usuario"), new("Rol"), new("Temporal") });

            // Iteramos por cada cuenta en memoria
            foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
            {
                // Inicializamos el rol como "No Reconocido"
                string stringRol = "No Reconocido";
                // Buscamos el rol en el diccionario de campos de acceso
                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) stringRol = kp.Key;
                }

                // Si no se deben mostrar las cuentas deshabilitadas y la cuenta está deshabilitada, continuamos
                if (!mostrarDeshabilitadas && !cuenta.Habilitada) continue;
                // Añadimos una nueva fila con los datos de la cuenta
                dt.Rows.Add(new object[] { cuenta.Id, cuenta.Habilitada ? "Si" : "No", cuenta.NombreDisplay,
                cuenta.NombreUsuario, stringRol, cuenta.ContraseñaTemporal ? "Si" : "No" });
            }

            // Asignamos la tabla de datos como DataSource del DataGridView
            dgvPersonal.DataSource = dt;
        }

        /// <summary>
        /// Método para validar los campos del formulario
        /// </summary>
        /// <returns>True si los campos son válidos, False en caso contrario</returns>
        private bool ValidarCampos()
        {
            // Validamos que el campo de nombre no esté vacío
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Texto' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validamos que el campo de nombre de usuario no esté vacío
            if (txtNombreUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Nombre de Usuario' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validamos que el campo de nombre de usuario no contenga espacios
            else if (txtNombreUsuario.Text.Contains(' '))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' no puede contener espacios", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validamos que se haya seleccionado un nivel de acceso
            if (cmbAcceso.SelectedItem is null)
            {
                MessageBox.Show("El campo 'Nivel de Acceso' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Validamos que el nivel de acceso seleccionado sea válido
            else if (!camposDeAcceso.TryGetValue(cmbAcceso.Text, out _))
            {
                MessageBox.Show($"No se reconoce el nivel de acceso '{cmbAcceso.Text}'", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Método para vaciar los campos del formulario
        /// </summary>
        private void VaciarCampos()
        {
            txtNombre.Text = "";
            txtNombreUsuario.Text = "";
            cmbAcceso.SelectedItem = null;
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de agregar cuenta
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validamos los campos del formulario
            if (!ValidarCampos()) return;

            // Validamos si el nombre de usuario no está en uso
            if (SistemaCentral.Cuentas.cuentasEnMemoria.Any(c => c.NombreUsuario == txtNombreUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario ya está en uso", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validamos si se seleccionó el rol de gerencia

            // Obtenemos el rol seleccionado
            Roles rol = camposDeAcceso[cmbAcceso.Text];

            // Variable para almacenar la contraseña temporal
            string contrasenaTemporal;

            // Creamos un formulario de input para ingresar la nueva contraseña temporal
            Utilidades.InputForm iform =
                new Utilidades.InputForm("Crear Cuenta", "Ingrese la contraseña temporal para el usuario");

            // Definimos una regla de validación para el input
            Func<string, bool> validationRule = (string _input) =>
            {
                // Si el input está vacío, mostramos un mensaje de error
                if (_input == string.Empty)
                {
                    MessageBox.Show("Por favor rellene el campo de input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else return true;
            };

            // Asignamos la regla de validación al formulario de input
            iform.setValidationRule(validationRule);

            // Mostramos el formulario de input y verificamos si el resultado es OK
            if (iform.ShowDialog(this) != DialogResult.OK) return;
            // Asignamos la contraseña temporal ingresada
            contrasenaTemporal = iform.InputtedString;

            // Liberamos los recursos del formulario de input
            iform.Dispose();

            // Creamos una nueva cuenta con los datos ingresados
            Cuenta cuentaNueva = new(
                    _id: SistemaCentral.Cuentas.cuentasEnMemoria.Count,
                    _nombre: txtNombreUsuario.Text,
                    _nombredisplay: txtNombre.Text,
                    _contraseña: new HashSalt(contrasenaTemporal),
                    _rol: rol
                    );
            // Marcamos la cuenta como con contraseña temporal
            cuentaNueva.ContraseñaTemporal = true;
            // Marcamos la cuenta como no iniciada
            cuentaNueva.SesionIniciada = false;

            // Añadimos la nueva cuenta al sistema central
            SistemaCentral.Cuentas.AñadirCuenta(cuentaNueva);

            // Mostramos un mensaje de éxito
            MessageBox.Show("Cuenta creada con éxito!", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Mostramos un mensaje informando que la cuenta es temporal
            MessageBox.Show("La cuenta agregada es temporal, si no se inicia sesión antes de cerrar el programa, se borrará automáticamente", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Vaciamos los campos del formulario
            VaciarCampos();
            // Guardamos las cuentas en el sistema central
            SistemaCentral.Cuentas.GuardarCuentas();

            // Actualizamos el DataGrid con las cuentas
            ActualizarDataGrid(chbHabilitada.Checked);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se cambia el estado del CheckBox de habilitadas
        /// </summary>
        private void chbHabilitada_CheckedChanged(object sender, EventArgs e)
        {
            // Actualizamos el DataGrid con las cuentas
            ActualizarDataGrid(chbHabilitada.Checked);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace doble clic en una celda del DataGridView
        /// </summary>
        private void dgvPersonal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idConseguido = -1;

            // Intentamos obtener el ID de la cuenta seleccionada
            if (!int.TryParse((((DataTable)dgvPersonal.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out idConseguido))
            {
                // Si hay un problema con la selección, mostramos un mensaje de error
                MessageBox.Show("Hubo un problema con la selección", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtenemos la cuenta seleccionada en memoria
            Cuenta dbi = SistemaCentral.Cuentas.cuentasEnMemoria[idConseguido];
            // Asignamos el ID seleccionado
            cuentaSeleccionada = dbi.Id;
            // Actualizamos el texto del status label
            statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";

            // Actualizamos el texto del botón de deshabilitar
            btnDeshabilitar.Text = (dbi.Habilitada) ? "Deshabilitar" : "Habilitar";

            // Asignamos los valores de la cuenta a los campos del formulario
            txtNombre.Text = dbi.NombreDisplay;
            txtNombreUsuario.Text = dbi.NombreUsuario;

            // Buscamos el rol en el diccionario de campos de acceso
            foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
            {
                if (kp.Value == dbi.Rol) cmbAcceso.Text = kp.Key;
            }

            // Si el rol de la cuenta seleccionada es mayor al rol de la cuenta en sesión, deshabilitamos los campos
            if (dbi.Rol > cuentaEnSesion.Rol)
            {
                txtNombre.Enabled = false;
                txtNombreUsuario.Enabled = false;
                cmbAcceso.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnEditar.Enabled = false;
                btnAgregar.Enabled = false;
                btnCoronar.Enabled = false;
                btnCoronar.Visible = false;

                MessageBox.Show("No puedes editar cuentas con un nivel de acceso mayor al tuyo", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Si la cuenta seleccionada es la misma que la cuenta en sesión, deshabilitamos ciertos campos
            else if (dbi.Id == this.cuentaEnSesion.Id)
            {
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = false;
                cmbAcceso.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = false;
                btnCoronar.Enabled = false;
                btnCoronar.Visible = false;

                MessageBox.Show("Has seleccionado tu propia cuenta, no podrás editar tu nombre de usuario, tu nivel de acceso o deshabilitar tu cuenta", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Si la cuenta seleccionada está deshabilitada o es temporal, desactivar algunas funciones
            else if (dbi.Id == this.cuentaEnSesion.Id || !dbi.SesionIniciada)
            {
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = true;
                cmbAcceso.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;
                btnCoronar.Enabled = false;
                btnCoronar.Visible = false;
            }
            // Si la cuenta seleccionada no tiene restricciones, habilitamos todos los campos
            else
            {
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = true;
                cmbAcceso.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;
                btnCoronar.Enabled = (cuentaEnSesion.Rol == Roles.GERENTE);
                btnCoronar.Visible = btnCoronar.Enabled;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de deshabilitar cuenta
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            // Si hay una cuenta seleccionada
            if (cuentaSeleccionada != -1)
            {
                // Obtenemos la cuenta seleccionada en memoria
                Cuenta cuenta = SistemaCentral.Cuentas.cuentasEnMemoria[cuentaSeleccionada];
                // Cambiamos el estado de habilitada de la cuenta
                cuenta.Habilitada = !cuenta.Habilitada;
                // Actualizamos el DataGrid con las cuentas
                ActualizarDataGrid(chbHabilitada.Checked);

                // Guardamos las cuentas en el sistema central
                SistemaCentral.Cuentas.GuardarCuentas();

                // Actualizamos el texto del botón de deshabilitar
                btnDeshabilitar.Text = (cuenta.Habilitada) ? "Deshabilitar" : "Habilitar";

                // Buscamos el rol en el diccionario de campos de acceso
                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) cmbAcceso.Text = kp.Key;
                }
            }
            else
            {
                // Si no hay ninguna cuenta seleccionada, mostramos un mensaje de error
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de editar cuenta
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Si hay una cuenta seleccionada
            if (cuentaSeleccionada != -1)
            {
                // Obtenemos la cuenta seleccionada en memoria
                Cuenta cuenta = SistemaCentral.Cuentas.cuentasEnMemoria[cuentaSeleccionada];

                // Validamos los campos del formulario
                if (!ValidarCampos()) return;

                // Obtenemos el rol seleccionado
                Roles rol = camposDeAcceso[cmbAcceso.Text];

                // Creamos una nueva cuenta con los datos ingresados
                Cuenta nuevaCuenta = new Cuenta(
                    _id: cuenta.Id,
                    _nombredisplay: txtNombre.Text,
                    _nombre: txtNombreUsuario.Text,
                    _contraseña: new HashSalt(cuenta.Contraseña.Hash, cuenta.Contraseña.Salt),
                    _rol: rol
                );

                // Actualizamos la cuenta en el sistema central
                SistemaCentral.Cuentas.RefrezcarCuenta(nuevaCuenta);

                // Si la cuenta seleccionada es la misma que la cuenta en sesión, actualizamos la cuenta en sesión
                if (cuenta.Id == this.cuentaEnSesion.Id)
                {
                    SistemaCentral.Cuentas.cuentaEnSesion = nuevaCuenta;
                }

                // Guardamos las cuentas en el sistema central
                SistemaCentral.Cuentas.GuardarCuentas();

                // Reiniciamos el ID seleccionado
                cuentaSeleccionada = -1;
                // Actualizamos el texto del status label
                statusLabel.Text = "No hay ningun empleado seleccionado";

                // Habilitamos todos los campos del formulario
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = true;
                cmbAcceso.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;
                btnCoronar.Enabled = (cuentaEnSesion.Rol == Roles.GERENTE);
                btnCoronar.Visible = btnCoronar.Enabled;

                // Actualizamos el DataGrid con las cuentas
                ActualizarDataGrid(chbHabilitada.Checked);

                // Buscamos el rol en el diccionario de campos de acceso
                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) cmbAcceso.Text = kp.Key;
                }

                // Vaciamos los campos del formulario
                VaciarCampos();
            }
            else
            {
                // Si no hay ninguna cuenta seleccionada, mostramos un mensaje de error
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de deseleccionar cuenta
        /// </summary>
        private void btnDeseleccionar_Click(object sender, EventArgs e)
        {
            // Reiniciamos el ID seleccionado
            cuentaSeleccionada = -1;
            // Actualizamos el texto del status label
            statusLabel.Text = "No hay ningun empleado seleccionado";

            // Habilitamos todos los campos del formulario
            txtNombre.Enabled = true;
            txtNombreUsuario.Enabled = true;
            cmbAcceso.Enabled = true;
            btnDeshabilitar.Enabled = true;
            btnEditar.Enabled = true;
            btnAgregar.Enabled = true;
            btnCoronar.Enabled = (cuentaEnSesion.Rol == Roles.GERENTE);
            btnCoronar.Visible = btnCoronar.Enabled;

            VaciarCampos();
        }

        /// <summary>
        /// Evento para transferir la gerencia del sistema
        /// </summary>
        private void btnCoronar_Click(object sender, EventArgs e)
        {
            // Si hay una cuenta seleccionada
            if (cuentaSeleccionada != -1)
            {
                // No se puede transferir la gerencia a una cuenta deshabilitada
                if (!SistemaCentral.Cuentas.cuentasEnMemoria[cuentaSeleccionada].Habilitada)
                {
                    MessageBox.Show("No puedes transferir la gerencia a una cuenta deshabilitada", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // No se puede transferir la gerencia a la cuenta en sesión
                if (cuentaSeleccionada == cuentaEnSesion.Id)
                {
                    MessageBox.Show("No puedes transferir la gerencia a tu propia cuenta", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Preguntamos al usuario si desea transferir el nivel de acceso Gerente
                if (MessageBox.Show("¿Deseas transferir el nivel de acceso 'Gerente' a otra cuenta? Esta acción no es reversible a menos que el usuario te vuelva a transferir la gerencia a ti", "Advertencia!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                // Cambiamos el rol de la cuenta en sesión a Administrador
                cuentaEnSesion.Rol = Roles.ADMINISTRADOR;
                // Actualizamos la cuenta en sesión en el sistema central
                SistemaCentral.Cuentas.cuentaEnSesion = cuentaEnSesion;
                SistemaCentral.Cuentas.RefrezcarCuenta(cuentaEnSesion);

                // Obtenemos la cuenta seleccionada en memoria
                Cuenta cuenta = SistemaCentral.Cuentas.cuentasEnMemoria[cuentaSeleccionada];
                // Cambiamos el rol de la cuenta seleccionada a Gerente
                cuenta.Rol = Roles.GERENTE;
                // Actualizamos la cuenta en el sistema central
                SistemaCentral.Cuentas.RefrezcarCuenta(cuenta);

                // Guardamos las cuentas en el sistema central
                SistemaCentral.Cuentas.GuardarCuentas();

                // Cerrar el formulario
                this.Close();
            }
            else
            {
                // Si no hay ninguna cuenta seleccionada, mostramos un mensaje de error
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
