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
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            ActualizarDataGrid();
        }

        public void ActualizarDataGrid()
        {
            dgvInventario.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[]{ new("ID"), new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Stock"), new("Fecha de Ingreso")});

            foreach (Producto producto in Program.sistemaCentral.inventario)
            {
                dt.Rows.Add(new object[]{ producto.Id, producto.Nombre, producto.Proveedor, producto.Descripcion,
                    producto.Precio, producto.CantidadEnStock, producto.FechaIngreso.ToString("yyyy-MM-dd")});
            }

            dgvInventario.DataSource = dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == string.Empty) {
                MessageBox.Show("El campo 'Nombre' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtProveedor.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Proveedor' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Precio' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!decimal.TryParse(txtPrecio.Text, out decimal nuevoPrecio))
            {
                MessageBox.Show("El campo 'Precio' no se pudo convertir a decimal", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto nuevoProducto = new(
                _id: Program.sistemaCentral.inventario.Count,
                _nombre: txtNombre.Text,
                _descripcion: rtxtDescripcion.Text,
                _precio: nuevoPrecio,
                _proveedor: txtProveedor.Text,
                _fechaIngreso: DateTime.Now,
                _stock: (int) nudStock.Value
                );

            Program.sistemaCentral.inventario.Add(nuevoProducto);
            ActualizarDataGrid();

            Program.sistemaCentral.GuardarInventario();

            txtNombre.Text = string.Empty;
            rtxtDescripcion.Text = string.Empty;
            txtProveedor.Text = string.Empty;
            nudStock.Value = 0;
            txtPrecio.Text = string.Empty;
        }
    }
}
