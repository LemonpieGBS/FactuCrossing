using FactuCrossing.Estructuras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                dt.Rows.Add(new object[]{ descuento.Id, descuento.Nombre, $"{descuento.Porcentaje:00.0}%",
                    descuento.FechaInicio.ToString("yyyy-MM-dd"), descuento.FechaFin.ToString("yyyy-MM-dd"),
                    descuento.ProductoAplicable == -1 ? "Todos" : SistemaCentral.Inventario.ObtenerProductoPorId(descuento.ProductoAplicable).Nombre});
            }
            dgvDescuentos.DataSource = dt;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductos.Enabled = radioButton2.Checked;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingrese un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nudPorcentaje.Value <= 0)
            {
                MessageBox.Show("El porcentaje de descuento no puede ser 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Agregamos el descuento a memoria
            if (radioButton1.Checked)
            {
                SistemaCentral.Descuentos.descuentosEnMemoria.Add(new Descuento(SistemaCentral.Descuentos.descuentosEnMemoria.Count,
                    txtNombre.Text, (double)(nudPorcentaje.Value), dtpInicio.Value, dtpFin.Value, -1));
            }
            else
            {
                if (cmbProductos.SelectedItem is null || cmbProductos.Text == string.Empty)
                {
                    MessageBox.Show("Por favor seleccione un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SistemaCentral.Descuentos.descuentosEnMemoria.Add(new Descuento(SistemaCentral.Descuentos.descuentosEnMemoria.Count,
                    txtNombre.Text, (double)(nudPorcentaje.Value), dtpInicio.Value, dtpFin.Value,
                    (SistemaCentral.Inventario.EncontrarProductoPorNombre(cmbProductos.Text)).Id));
            }
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
            }

            // Actualizar el DataGridView
            ActualizarDataGrid();
            // Hoa
            SistemaCentral.Descuentos.GuardarDescuentos();
        }
    }
}
