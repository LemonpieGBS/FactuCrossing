using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Facturación
{
    public partial class AgregarProducto : Form
    {
        int productoSeleccionado = -1;

        public Producto? productoAFacturar = null;
        public int cantidadEnStock = -1;

        public AgregarProducto()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            ActualizarDataGrid();

            lblNombre.Text = "No seleccionado";
            lblProveedor.Text = "No seleccionado";
            lblPrecio.Text = "No seleccionado";
            lblStock.Text = "No seleccionado";

            nudCantidad.Minimum = 0;
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
            if (productoSeleccionado == -1)
            {
                MessageBox.Show("Por favor seleccione un producto", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad a facturar no puede ser 0", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (nudCantidad.Value >
                Program.sistemaCentral.inventario[productoSeleccionado].CantidadEnStock)
            {
                MessageBox.Show("No hay suficientes productos en stock para facturar", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else
            {
                productoAFacturar = Program.sistemaCentral.inventario[productoSeleccionado];
                cantidadEnStock = (int)nudCantidad.Value;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idConseguido = -1;

            if (!int.TryParse((((DataTable)dgvInventario.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out idConseguido))
            {
                MessageBox.Show("Hubo un problema con la selección", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto dbi = Program.sistemaCentral.inventario[idConseguido];
            productoSeleccionado = dbi.Id;

            lblNombre.Text = dbi.Nombre;
            lblProveedor.Text = dbi.Proveedor;
            lblPrecio.Text = $"{dbi.Precio}$";
            lblStock.Text = $"{dbi.CantidadEnStock} unidades";

            nudCantidad.Maximum = dbi.CantidadEnStock;
        }
    }
}
