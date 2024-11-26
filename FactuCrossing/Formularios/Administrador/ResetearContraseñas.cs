using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class ResetearContraseñas : Form
    {
        int idSeleccionado = -1;
        Cuenta cuentaEnSesion =
            new Cuenta(-1, "default", "default", Roles.GERENTE, new HashSalt("1234"));

        public ResetearContraseñas()
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
        }

        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            dgvPersonal.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[]{new("ID"), new("Nombre"), new("Usuario"), new("Rol"), new("Contraseña Temporal")});

            foreach (Cuenta cuenta in SistemaCentral.cuentasEnMemoria)
            {
                dt.Rows.Add(new object[] { cuenta.Id, $"{cuenta.NombreDisplay}", cuenta.NombreUsuario, cuenta.Rol, cuenta.ContraseñaTemporal ? "Si" : "No" });
            }

            dgvPersonal.DataSource = dt;
        }

        private void dgvPersonal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!int.TryParse((((DataTable)dgvPersonal.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out int idConseguido))
            {
                MessageBox.Show("Hubo un problema con la selección", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cuenta dbi = SistemaCentral.cuentasEnMemoria[idConseguido];
            idSeleccionado = dbi.Id;
            statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Porfavor selecciona una cuenta", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cuenta cuenta = SistemaCentral.cuentasEnMemoria[idSeleccionado];
            cuenta.ContraseñaTemporal = true;

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
            cuenta.CambiarContraseña(new HashSalt(contrasenaTemporal));

            SistemaCentral.cuentasEnMemoria[idSeleccionado] = cuenta;
            ActualizarDataGrid();

            MessageBox.Show("Contraseña reseteada con éxito", "Resetear Contraseña",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            statusLabel.Text = "No hay ningun empleado seleccionado";
            idSeleccionado = -1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
