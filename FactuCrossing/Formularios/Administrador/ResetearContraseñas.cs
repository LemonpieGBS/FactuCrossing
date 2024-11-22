using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class ResetearContraseñas : Form
    {
        public ResetearContraseñas()
        {
            InitializeComponent();
            ActualizarDataGrid();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
        }

        public void ActualizarDataGrid(bool mostrarDeshabilitadas = false)
        {
            dgvPersonal.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[]{new("ID"), new("Nombre"), new("Usuario"), new("Rol"), new("Temporal")});

            foreach (Cuenta cuenta in Program.sistemaCentral.cuentas)
            {
                dt.Rows.Add(new object[] { cuenta.Id, $"{cuenta.NombreDisplay}", cuenta.NombreUsuario, cuenta.Rol, cuenta.Temporal ? "Si" : "No" });
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

            Cuenta dbi = Program.sistemaCentral.cuentas[idConseguido];
            statusLabel.Text = $"Cuenta seleccionada: {dbi.NombreDisplay}";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
