using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class AdministrarPersonal : Form
    {
        int cuentaSeleccionada = -1;

        public AdministrarPersonal()
        {
            InitializeComponent();
            ActualizarDataGrid();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
        }

        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            dgvPersonal.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange([new("ID"), new("Activo/a"), new("Nombre"), new("Usuario"), new("Rol"), new("Temporal")]);

            foreach (Cuenta cuenta in Program.sistemaCentral.cuentas)
            {
                if (!mostrarDeshabilitadas && !cuenta.Habilitada) continue;
                dt.Rows.Add([cuenta.Id, cuenta.Habilitada ? "Si" : "No", $"{cuenta.NombreDisplay}", cuenta.NombreUsuario, cuenta.Rol, cuenta.Temporal ? "Si" : "No"]);
            }

            dgvPersonal.DataSource = dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Texto' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNombreUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Nombre de Usuario' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtNombreUsuario.Text.Contains(' '))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' no puede contener espacios", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dictionary<string, Roles> camposDeAcceso = new()
            {
                {"Facturista", Roles.FACTURISTA},
                {"Gestor de Inventario", Roles.GESTORDEINVENTARIO},
                {"Analista",Roles.ANALISTA},
                {"Administrador",Roles.ADMINISTRADOR},
                {"Gerente", Roles.GERENTE}
            };

            Roles rol;
            if (cmbAcceso.SelectedItem is null)
            {
                MessageBox.Show("El campo 'Nivel de Acceso' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!camposDeAcceso.TryGetValue(cmbAcceso.Text, out rol))
            {
                MessageBox.Show($"No se reconoce el nivel de acceso '{cmbAcceso.Text}'", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string contrasenaTemporal;

            do
            {
                contrasenaTemporal = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la contraseña temporal para el usuario", "Crear Cuenta", "1234");
                if (contrasenaTemporal == string.Empty)
                {
                    MessageBox.Show("La contraseña temporal no puede estar vacía", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } while (contrasenaTemporal == string.Empty);

            Cuenta cuentaNueva = new(
                    _id: Program.sistemaCentral.cuentas.Count,
                    _nombre: txtNombreUsuario.Text,
                    _nombredisplay: txtNombre.Text,
                    _contrasena: contrasenaTemporal,
                    _rol: rol
                    );
            cuentaNueva.EstablecerTemporal(true);

            Program.sistemaCentral.cuentas.Add(cuentaNueva);

            MessageBox.Show("Cuenta creada con exito!", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            MessageBox.Show("La cuenta está marcada como temporal, si no se inicia sesión antes de cerrar el programa, se borrará automaticamente", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            Program.sistemaCentral.GuardarCuentas();

            ActualizarDataGrid(chbHabilitada.Checked);
        }

        private void chbHabilitada_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarDataGrid(chbHabilitada.Checked);
        }

        private void dgvPersonal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idConseguido = -1;

            if(!int.TryParse((((DataTable)dgvPersonal.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out idConseguido)) {
                MessageBox.Show("Hubo un problema con la selección", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cuenta dbi = Program.sistemaCentral.cuentas[idConseguido];
            cuentaSeleccionada = dbi.Id;
            statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";

            btnDeshabilitar.Text = (dbi.Habilitada) ? "Deshabilitar" : "Habilitar";

            txtNombre.Text = dbi.NombreDisplay;
            txtNombreUsuario.Text = dbi.NombreUsuario;

            Dictionary<string, Roles> camposDeAcceso = new()
            {
                {"Facturista", Roles.FACTURISTA},
                {"Gestor de Inventario", Roles.GESTORDEINVENTARIO},
                {"Analista",Roles.ANALISTA},
                {"Administrador",Roles.ADMINISTRADOR},
                {"Gerente", Roles.GERENTE}
            };

            foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
            {
                if (kp.Value == dbi.Rol) cmbAcceso.Text = kp.Key;
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (cuentaSeleccionada != -1)
            {
                Cuenta cuenta = Program.sistemaCentral.cuentas[cuentaSeleccionada];
                cuenta.EstablecerHabilitada(!cuenta.Habilitada);
                ActualizarDataGrid(chbHabilitada.Checked);

                Program.sistemaCentral.GuardarCuentas();
                btnDeshabilitar.Text = (cuenta.Habilitada) ? "Deshabilitar" : "Habilitar";
            } else
            {
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
