using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    /// <summary>
    /// Clase parcial del formulario para resetear contraseñas
    /// </summary>
    public partial class ResetearContraseñas : Form
    {
        // Propiedad para almacenar el ID seleccionado
        int idSeleccionado = -1;
        // Propiedad para almacenar la cuenta en sesión
        Cuenta cuentaEnSesion;

        /// <summary>
        /// Constructor del formulario
        /// </summary>
        public ResetearContraseñas()
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
            dt.Columns.AddRange(new DataColumn[] { new("ID"), new("Nombre"), new("Usuario"), new("Rol"), new("Contraseña Temporal") });

            // Iteramos por cada cuenta en memoria
            foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
            {
                // Si no se deben mostrar las cuentas deshabilitadas y la cuenta está deshabilitada, continuamos
                if (!mostrarDeshabilitadas && !cuenta.Habilitada) continue;
                // Añadimos una nueva fila con los datos de la cuenta
                dt.Rows.Add(new object[] { cuenta.Id, $"{cuenta.NombreDisplay}", cuenta.NombreUsuario, cuenta.Rol, cuenta.ContraseñaTemporal ? "Si" : "No" });
            }

            // Asignamos la tabla de datos como DataSource del DataGridView
            dgvPersonal.DataSource = dt;
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace doble clic en una celda del DataGridView
        /// </summary>
        private void dgvPersonal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Try por si algo sale mal
            try
            {
                // Intentamos obtener el ID de la cuenta seleccionada
                if (!int.TryParse((((DataTable)dgvPersonal.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out int idConseguido))
                {
                    // Si hay un problema con la selección, mostramos un mensaje de error
                    throw new FormatException("Hubo un problema con la selección");
                }

                // Obtenemos la cuenta seleccionada en memoria (y tiramos un error si no se encuentra)
                Cuenta dbi = SistemaCentral.Cuentas.ObtenerCuentaPorId(idConseguido)
                    ?? throw new ArgumentNullException("El ID obtenido de la cuenta no existe en memoria");
                // Asignamos el ID seleccionado
                idSeleccionado = dbi.Id;
                // Actualizamos el texto del status label
                statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";

            }
            // Catch para atrapar el error y mostrarlo al usuario
            catch(Exception ex)
            {
                // Si hay un error, mostramos un mensaje de error
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de resetear contraseña
        /// </summary>
        private void btnResetear_Click(object sender, EventArgs e)
        {
            // Si no hay ninguna cuenta seleccionada, mostramos un mensaje de error
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Por favor selecciona una cuenta", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtenemos la cuenta seleccionada en memoria
            Cuenta cuenta;
            // Intentamos obtener la cuenta seleccionada en memoria
            try
            {
                cuenta = SistemaCentral.Cuentas.ObtenerCuentaPorId(idSeleccionado)
                    ?? throw new ArgumentNullException("El ID obtenido de la cuenta no existe en memoria");
            }
            // Catch para atrapar el error y mostrarlo al usuario
            catch (Exception ex)
            {
                // Si hay un error, mostramos un mensaje de error
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Marcamos la cuenta como con contraseña temporal
            cuenta.ContraseñaTemporal = true;

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
            // Cambiamos la contraseña de la cuenta
            cuenta.CambiarContraseña(new HashSalt(contrasenaTemporal));

            // Actualizamos la cuenta en memoria
            SistemaCentral.Cuentas.RefrezcarCuenta(cuenta);
            // Actualizamos el DataGrid con las cuentas
            ActualizarDataGrid();

            // Mostramos un mensaje de éxito
            MessageBox.Show("Contraseña reseteada con éxito", "Resetear Contraseña",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Actualizamos el texto del status label
            statusLabel.Text = "No hay ningun empleado seleccionado";
            // Reiniciamos el ID seleccionado
            idSeleccionado = -1;
        }

        /// <summary>
        /// Evento que se ejecuta cuando se hace clic en el botón de salir
        /// </summary>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cerramos el formulario
            this.Close();
        }
    }
}
