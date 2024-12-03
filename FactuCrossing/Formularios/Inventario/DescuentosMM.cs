using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Inventario
{
    public partial class DescuentosMM : Form
    {
        int descuentoSeleccionado = -1;

        public DescuentosMM()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);

            // Agregamos los productos en memoria al cmbProductos
            foreach (var producto in SistemaCentral.Inventario.productosEnMemoria)
            {
                cmbProductos.Items.Add(producto.Nombre);
            }

            ActualizarDataGrid();
        }

        // Actualizar DataGridView con los datos
        public void ActualizarDataGrid()
        {
            dgvDescuentos.DataSource = null;
            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[]{ new("ID"), new("Nombre"), new("Porcentaje"), new("Fecha de Inicio"),
                new("Fecha de Fin"), new("Producto Aplicable")});
            foreach (Descuento descuento in SistemaCentral.Descuentos.descuentosEnMemoria)
            {
                Producto? producto = null;
                if (descuento.ProductoAplicable != -1)
                {
                    producto = SistemaCentral.Inventario.ObtenerProductoPorId(descuento.ProductoAplicable);
                    if (producto is null)
                    {
                        throw new Exception("Producto no encontrado");
                    }
                }
                dt.Rows.Add(new object[]{ descuento.Id, descuento.Nombre, $"{descuento.Porcentaje:00.0}%",
                    descuento.FechaInicio.ToString("yyyy-MM-dd"), descuento.FechaFin.ToString("yyyy-MM-dd"),
                    producto is null ? "Todos" : producto.Nombre});
            }
            dgvDescuentos.DataSource = dt;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductos.Enabled = radioButton2.Checked;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingrese un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nudPorcentaje.Value <= 0)
            {
                MessageBox.Show("El porcentaje de descuento no puede ser 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto? producto = SistemaCentral.Inventario.EncontrarProductoPorNombre(cmbProductos.Text);

            if (producto is null && radioButton2.Checked)
            {
                MessageBox.Show("Por favor seleccione un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id = (radioButton1.Checked || producto is null) ? -1 : producto.Id;
            DateTime fechaInicio = (chkPermanente.Checked) ? DateTime.MinValue : dtpInicio.Value;
            DateTime fechaFinal = (chkPermanente.Checked) ? DateTime.MaxValue : dtpFin.Value;

            SistemaCentral.Descuentos.descuentosEnMemoria.Add(new Descuento(SistemaCentral.Descuentos.descuentosEnMemoria.Count, txtNombre.Text, (double) nudPorcentaje.Value, fechaInicio, fechaFinal, id));

            // Añadir la accion al sistema central
            Accion accion = new Accion(SistemaCentral.Cuentas.cuentaEnSesion?.Id ?? throw new Exception("ID de cuenta no encontrado")
                , $"Se agregó un nuevo descuento con nombre {txtNombre.Text}", DateTime.Now);
            SistemaCentral.Acciones.accionesEnMemoria.Add(accion);
            SistemaCentral.Acciones.GuardarAcciones();

            // Actualizar el DataGridView
            ActualizarDataGrid();
            // Reiniciamos los campos
            txtNombre.Text = string.Empty;
            nudPorcentaje.Value = 0;
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
            cmbProductos.SelectedIndex = -1;

            SistemaCentral.Descuentos.GuardarDescuentos();
        }

        private void dgvDescuentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seleccionar un descuento
            int idConseguido = -1;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDescuentos.Rows[e.RowIndex];
                if (int.TryParse(row.Cells[0].Value.ToString(), out idConseguido))
                {
                    descuentoSeleccionado = idConseguido;
                }
            }

            // Preguntar al usuario si quiere eliminar el descuento
            if (MessageBox.Show("¿Desea eliminar el descuento seleccionado?", "Eliminar descuento",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SistemaCentral.Descuentos.descuentosEnMemoria.RemoveAll(descuento => descuento.Id == descuentoSeleccionado);
                // Reordenar los IDs
                for (int i = 0; i < SistemaCentral.Descuentos.descuentosEnMemoria.Count; i++)
                {
                    SistemaCentral.Descuentos.descuentosEnMemoria[i].Id = i;
                }

                // Agregar la accion al Sistema Central
                Accion accion = new Accion(SistemaCentral.Cuentas.cuentaEnSesion?.Id ?? throw new Exception("ID de cuenta no encontrado")
                    , $"Se eliminó el descuento con ID {descuentoSeleccionado}", DateTime.Now);
                SistemaCentral.Acciones.accionesEnMemoria.Add(accion);
                SistemaCentral.Acciones.GuardarAcciones();
            }

            // Actualizar el DataGridView
            ActualizarDataGrid();
            // Hoa
            SistemaCentral.Descuentos.GuardarDescuentos();
        }

        private void chkPermanente_CheckedChanged(object sender, EventArgs e)
        {
            dtpFin.Enabled = !chkPermanente.Checked;
            dtpInicio.Enabled = !chkPermanente.Checked;
        }
    }
}
