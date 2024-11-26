using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class AdministrarPersonal : Form
    {
        int cuentaSeleccionada = -1;
        Cuenta cuentaEnSesion =
            new Cuenta(-1, "default", "default", Roles.GERENTE, new HashSalt("1234"));

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
            if (SistemaCentral.cuentaEnSesion is null)
            {
                MessageBox.Show("Hubo un problema de autenticación, porfavor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            this.cuentaEnSesion = SistemaCentral.cuentaEnSesion;

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

            if (SistemaCentral.cuentaEnSesion.Rol == Roles.GERENTE)
            {
                cmbAcceso.Items.Add(new string("Gerente"));
            }

            cmbAcceso.SelectedItem = null;
        }

        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            dgvPersonal.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[] { new("ID"), new("Activo/a"), new("Nombre"), new("Usuario"), new("Rol"), new("Temporal") });

            foreach (Cuenta cuenta in SistemaCentral.cuentasEnMemoria)
            {
                string stringRol = "No Reconocido";
                foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
                {
                    if (kp.Value == cuenta.Rol) stringRol = kp.Key;
                }

                if (!mostrarDeshabilitadas && !cuenta.Habilitada) continue;
                dt.Rows.Add(new object[] { cuenta.Id, cuenta.Habilitada ? "Si" : "No", cuenta.NombreDisplay,
                    cuenta.NombreUsuario, stringRol, cuenta.ContraseñaTemporal ? "Si" : "No" });
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

            Utilidades.InputForm iform =
                new Utilidades.InputForm("Crear Cuenta", "Ingrese la contraseña temporal para el usuario");

            Func<string, bool> validationRule = (string _input) =>
            {
                if (_input == string.Empty)
                {
                    MessageBox.Show("Porfavor rellenar el campo de input", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else return true;
            };

            iform.setValidationRule(validationRule);

            if (iform.ShowDialog(this) != DialogResult.OK) return;
            contrasenaTemporal = iform.InputtedString;

            iform.Dispose();

            Cuenta cuentaNueva = new(
                    _id: SistemaCentral.cuentasEnMemoria.Count,
                    _nombre: txtNombreUsuario.Text,
                    _nombredisplay: txtNombre.Text,
                    _contraseña: new HashSalt(contrasenaTemporal),
                    _rol: rol
                    );
            cuentaNueva.ContraseñaTemporal = true;
            cuentaNueva.SesionIniciada = false;

            SistemaCentral.cuentasEnMemoria.Add(cuentaNueva);

            MessageBox.Show("Cuenta creada con exito!", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            MessageBox.Show("La cuenta agregada es temporal, si no se inicia sesión antes de cerrar el programa, se borrará automaticamente", "Cuenta Creada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

            VaciarCampos();
            SistemaCentral.GuardarCuentas();

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

            Cuenta dbi = SistemaCentral.cuentasEnMemoria[idConseguido];
            cuentaSeleccionada = dbi.Id;
            statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";

            btnDeshabilitar.Text = (dbi.Habilitada) ? "Deshabilitar" : "Habilitar";

            txtNombre.Text = dbi.NombreDisplay;
            txtNombreUsuario.Text = dbi.NombreUsuario;

            foreach (KeyValuePair<string, Roles> kp in camposDeAcceso)
            {
                if (kp.Value == dbi.Rol) cmbAcceso.Text = kp.Key;
            }

            if (dbi.Rol > cuentaEnSesion.Rol)
            {
                txtNombre.Enabled = false;
                txtNombreUsuario.Enabled = false;
                cmbAcceso.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnEditar.Enabled = false;
                btnAgregar.Enabled = false;
                MessageBox.Show("No puedes editar cuentas con un nivel de acceso mayor al tuyo", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dbi.Id == this.cuentaEnSesion.Id)
            {
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = false;
                cmbAcceso.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;

                MessageBox.Show("Has seleccionado tu propia cuenta, no podras editar tu nombre de usuario, tu nivel de acceso o deshabilitar tu cuenta", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = true;
                cmbAcceso.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (cuentaSeleccionada != -1)
            {
                Cuenta cuenta = SistemaCentral.cuentasEnMemoria[cuentaSeleccionada];
                cuenta.Habilitada = !cuenta.Habilitada;
                ActualizarDataGrid(chbHabilitada.Checked);

                SistemaCentral.GuardarCuentas();
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
                Cuenta cuenta = SistemaCentral.cuentasEnMemoria[cuentaSeleccionada];

                if (!ValidarCampos()) return;

                Roles rol = camposDeAcceso[cmbAcceso.Text];

                Cuenta nuevaCuenta = new Cuenta(
                    _id: cuenta.Id,
                    _nombredisplay: txtNombre.Text,
                    _nombre: txtNombreUsuario.Text,
                    _contraseña: new HashSalt(cuenta.Contraseña.Hash, cuenta.Contraseña.Salt),
                    _rol: rol
                );

                bool cambioGerente = false;
                if (rol == Roles.GERENTE && cuenta.Id != cuentaEnSesion.Id)
                {
                    if (MessageBox.Show("¿Deseas transferir el nivel de acceso 'Gerente' a otra cuenta? (Esta pestaña se cerrará)", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                    cambioGerente = true;
                    cuentaEnSesion.Rol = Roles.ADMINISTRADOR;
                    SistemaCentral.cuentaEnSesion = cuentaEnSesion;
                    SistemaCentral.cuentasEnMemoria[cuentaEnSesion.Id] = cuentaEnSesion;
                }

                SistemaCentral.cuentasEnMemoria[cuentaSeleccionada] = nuevaCuenta;

                if (cuenta.Id == this.cuentaEnSesion.Id)
                {
                    SistemaCentral.cuentaEnSesion = nuevaCuenta;
                }

                SistemaCentral.GuardarCuentas();

                if (cambioGerente)
                {
                    this.Close();
                    return;
                }

                cuentaSeleccionada = -1;
                statusLabel.Text = "No hay ningun empleado seleccionado";

                txtNombre.Enabled = true;
                txtNombreUsuario.Enabled = true;
                cmbAcceso.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnEditar.Enabled = true;
                btnAgregar.Enabled = true;

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

        private void btnDeseleccionar_Click(object sender, EventArgs e)
        {
            cuentaSeleccionada = -1;
            statusLabel.Text = "No hay ningun empleado seleccionado";

            txtNombre.Enabled = true;
            txtNombreUsuario.Enabled = true;
            cmbAcceso.Enabled = true;
            btnDeshabilitar.Enabled = true;
            btnEditar.Enabled = true;
            btnAgregar.Enabled = true;

            VaciarCampos();
        }
    }
}
