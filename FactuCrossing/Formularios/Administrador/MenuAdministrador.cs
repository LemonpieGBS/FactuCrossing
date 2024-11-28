using FactuCrossing.Estructuras;
using System.Data;
using static FactuCrossing.SistemaCentral;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class MenuAdministrador : Form
    {
        /// <summary>
        /// Lista de accesos por cuenta
        /// </summary>
        private List<Tuple<int, int, DateTime>> accesosPorCuenta = new List<Tuple<int, int, DateTime>>() { };

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public MenuAdministrador()
        {
            // Inicializamos los componentes del formulario
            InitializeComponent();
            // Aplicamos el estilo de fuente del programa
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el data grid
            ActualizarDataGrid();
        }

        /// <summary>
        /// Método para calcular los accesos por cuenta
        /// </summary>
        private void CalcularAccesosPorCuenta()
        {
            // Limpiamos la lista
            accesosPorCuenta.Clear();
            // Recorremos los accesos
            foreach(Acceso acceso in SistemaCentral.Accesos.accesosEnMemoria)
            {
                // Variable por si encontramos el índice
                bool indiceEncontrado = false;
                // Buscamos si la cuenta ya está en la lista
                for(int i = accesosPorCuenta.Count - 1; i >= 0; i--) {
                    // Variable para hacer todo más ameno
                    Tuple<int, int, DateTime> tupleIdAcceso = accesosPorCuenta[i];
                    // Si si está, actualizamos el indice a editar
                    if (tupleIdAcceso.Item1 == acceso.IdDeCuenta)
                    {
                        // Revisamos la fecha
                        DateTime masReciente = DateTime.Compare(tupleIdAcceso.Item3, acceso.TiempoDeAcceso) < 0
                            ? acceso.TiempoDeAcceso : tupleIdAcceso.Item3;
                        // Asignamos el índice
                        accesosPorCuenta[i] =
                            new Tuple<int, int, DateTime>(tupleIdAcceso.Item1, tupleIdAcceso.Item2 + 1, masReciente);
                        // Marcamos el índice como encontrado
                        indiceEncontrado = true;
                        break;
                    }
                }

                // Si no se encontró el indice
                if (!indiceEncontrado)
                {
                    // Agregamos la entrada
                    accesosPorCuenta.Add(new Tuple<int, int, DateTime>(acceso.IdDeCuenta, 1, acceso.TiempoDeAcceso));
                }
            }
        }

        /// <summary>
        /// Método para actualizar el DataGrid con los productos facturados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualizarDataGrid()
        {
            // Calculamos los accesos por cuenta
            CalcularAccesosPorCuenta();
            // Limpiamos el DataGrid
            dgvAccesos.DataSource = null;
            // Creamos un DataTable
            DataTable dt = new();

            // CÓDIGO PARA MOSTRAR ACCESOS INDIVIDUALMENTE, 
            /*
            // Añadimos las columnas
            dt.Columns.AddRange(new DataColumn[]{ new("Fecha"), new("Hora"), new("Personal"), new("Tipo de Acceso") });
            // Añadimos las filas
            for (int i = SistemaCentral.Accesos.accesosEnMemoria.Count - 1; i >= 0; i--)
            {
                // Obtenemos el acceso
                Acceso acceso = SistemaCentral.Accesos.accesosEnMemoria[i];
                // Obtenemos la cuenta asociada al acceso
                Cuenta? cuenta = SistemaCentral.Cuentas.ObtenerCuentaPorId(acceso.IdDeCuenta);
                // Si no se encontró la cuenta, lanzamos una excepción
                if (cuenta is null)
                {
                    throw new ArgumentNullException($"No se encontró la cuenta asociada al acceso, id: {acceso.IdDeCuenta}");
                }
                // Añadimos la fila
                dt.Rows.Add(new object[] { acceso.TiempoDeAcceso.ToString("yyyy-MM-dd"), acceso.TiempoDeAcceso.ToString("hh:mm tt"),
                    cuenta.NombreDisplay, EnumHelper.GetEnumName(acceso.Tipo)});
            }
            */

            // Generamos el data table
            // Añadimos las columnas
            dt.Columns.AddRange(new DataColumn[] { new("Usuario"), new("# de Accesos"), new("Ultimo Acceso (Fecha)"), new("Ultimo Acceso (Hora)") });
            // Añadimos las filas
            foreach(Tuple<int, int, DateTime> trioDeAtributos in accesosPorCuenta)
            {
                // Cargamos la fecha
                DateTime ultimoAcceso = trioDeAtributos.Item3;
                // Cargamos la cuenta del id
                Cuenta? cuenta = SistemaCentral.Cuentas.ObtenerCuentaPorId(trioDeAtributos.Item1);
                // Si no se encontró la cuenta, lanzamos una excepción
                if (cuenta is null)
                {
                    throw new ArgumentNullException($"No se encontró la cuenta asociada al acceso, id: {trioDeAtributos.Item1}");
                }
                // Añadimos la fila
                dt.Rows.Add(new object[] { cuenta.NombreDisplay, trioDeAtributos.Item2,
                    ultimoAcceso.ToString("yyyy-MM-dd"), ultimoAcceso.ToString("hh:mm tt") });
            }
            // Asignamos el DataTable al DataGrid
            dgvAccesos.DataSource = dt;
        }

        /// <summary>
        /// Método que se ejecuta cuando se muestra el formulario
        /// </summary>
        private void OnShow()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, cerrar el formulario
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                return;
            }
            // Actualizamos el data grid
            ActualizarDataGrid();
            // Asignamos la cuenta en sesión
            this.Enabled = true;
        }

        /// <summary>
        /// Método para abrir un formulario y ocultar el actual
        /// </summary>
        /// <param name="frm">Formulario a abrir</param>
        private void AbrirDeshabilitarDelegar(Form frm)
        {
            // Ocultamos el formulario actual
            this.Enabled = false;
            // Mostramos el nuevo formulario
            frm.Show();
            // Cuando el nuevo formulario se cierra, volvemos a mostrar el formulario actual
            frm.FormClosed += delegate { this.OnShow(); };
        }

        /// <summary>
        /// Evento para abrir el formulario de Resetear Contraseñas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetear_Click(object sender, EventArgs e)
        {
            AbrirDeshabilitarDelegar(new Administrador.ResetearContraseñas());
        }

        /// <summary>
        /// Evento para abrir el formulario de Administrador de Personal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdministrar_Click(object sender, EventArgs e)
        {
            
            AbrirDeshabilitarDelegar(new Administrador.AdministrarPersonal());
        }

        /// <summary>
        /// Evento para abrir el formulario de Generador de Reportes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            AbrirDeshabilitarDelegar(new Administrador.Generador_de_Reportes());
        }
    }
}
