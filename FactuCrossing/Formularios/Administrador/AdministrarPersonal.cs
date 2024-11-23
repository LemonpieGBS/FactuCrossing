using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class AdministrarPersonal : Form
    {
        int cuentaSeleccionada = -1;

        Dictionary<string, Roles> camposDeAcceso = new()
        {
            {"Facturista", Roles.FACTURISTA},
            {"Gestor de Inventario", Roles.GESTORDEINVENTARIO},
            {"Analista",Roles.ANALISTA},
            {"Administrador",Roles.ADMINISTRADOR},
            {"Gerente", Roles.GERENTE}
        };

        public AdministrarPersonal()
        {
            if (Program.sistemaCentral.cuentaEnSesion is null)
            {
                MessageBox.Show("Hubo un problema de autenticación, porfavor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            InitializeComponent();
            ActualizarDataGrid();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            statusLabel.Text = "No hay ningun empleado seleccionado";

            cmbAcceso.Items.Clear();

            cmbAcceso.Items.AddRange(new object[]
            {
                "Facturista",
                "Gestor de Inventario",
                "Analista",
                "Administrador"
            });

            if(Program.sistemaCentral.cuentaEnSesion.Rol == Roles.GERENTE)
            {
                cmbAcceso.Items.Add( new string("Gerente") );
            }

            cmbAcceso.SelectedItem = null;
        }

        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            dgvPersonal.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[] { new("ID"), new("Activo/a"), new("Nombre"), new("Usuario"), new("Rol"), new("Temporal") });

            foreach (Cuenta cuenta in Program.sistemaCentral.cuentas)
            {
                string stringRol = "No Reconocido";
                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) stringRol = kp.Key;
                }

                if (!mostrarDeshabilitadas && !cuenta.Habilitada) continue;
                dt.Rows.Add(new object[] { cuenta.Id, cuenta.Habilitada ? "Si" : "No", cuenta.NombreDisplay,
                    cuenta.NombreUsuario, stringRol, cuenta.Temporal ? "Si" : "No" });
            }

            dgvPersonal.DataSource = dt;
        }

        private bool ValidarCampos()
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Texto' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtNombreUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Nombre de Usuario' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtNombreUsuario.Text.Contains(' '))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' no puede contener espacios", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbAcceso.SelectedItem is null)
            {
                MessageBox.Show("El campo 'Nivel de Acceso' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!camposDeAcceso.TryGetValue(cmbAcceso.Text, out _))
            {
                MessageBox.Show($"No se reconoce el nivel de acceso '{cmbAcceso.Text}'", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void VaciarCampos()
        {
            txtNombre.Text = "";
            txtNombreUsuario.Text = "";
            cmbAcceso.SelectedItem = null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            Roles rol = camposDeAcceso[cmbAcceso.Text];

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

            VaciarCampos();
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

            if (!int.TryParse((((DataTable)dgvPersonal.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out idConseguido))
            {
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

                cuentaSeleccionada = -1;
                statusLabel.Text = "No hay ningun empleado seleccionado";

                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) cmbAcceso.Text = kp.Key;
                }
            }
            else
            {
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cuentaSeleccionada != -1)
            {
                Cuenta cuenta = Program.sistemaCentral.cuentas[cuentaSeleccionada];

                if (!ValidarCampos()) return;

                Roles rol = camposDeAcceso[cmbAcceso.Text];

                Cuenta nuevaCuenta = new Cuenta(
                    _id: cuenta.Id,
                    _nombredisplay: txtNombre.Text,
                    _nombre: txtNombreUsuario.Text,
                    _hash: cuenta.Hash,
                    _salt: cuenta.Salt,
                    _rol: rol
                );

                Program.sistemaCentral.cuentas[cuentaSeleccionada] = nuevaCuenta;

                cuentaSeleccionada = -1;
                statusLabel.Text = "No hay ningun empleado seleccionado";

                Program.sistemaCentral.GuardarCuentas();

                ActualizarDataGrid(chbHabilitada.Checked);

                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) cmbAcceso.Text = kp.Key;
                }

                VaciarCampos();
            }
            else
            {
                MessageBox.Show("No hay ningun empleado seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
