using FactuCrossing.Estructuras;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace FactuCrossing.Formularios.Inventario
{
    public partial class Descuentos : Form
    {
        /*private List<Producto> productos;
        private List<Descuento> descuentos;
        private DataTable descuentosTable;*/

        public Descuentos()
        {
            InitializeComponent();
            //productos = (SistemaCentral.Inventario.productosEnMemoria).ToList();
            //descuentos = new List<Descuento>();
            //InicializarComboBoxProductos();
            //InicializarDataTable();
        }

        /*
        private void DescuentoForm_Load(object sender, EventArgs e)
        {
            cmbTipoDescuento.SelectedIndex = 0; // Seleccionar el primer elemento por defecto
            ActualizarVisibilidadControles();
            ActualizarDataGridView();
        }

        private void cmbTipoDescuento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadControles();
        }

        private void ActualizarVisibilidadControles()
        {
            // Mostrar u ocultar controles según el tipo de descuento seleccionado
            bool esPorcentaje = cmbTipoDescuento.SelectedItem?.ToString() == "Porcentaje";
            txtPorcentaje.Visible = esPorcentaje;
            label5.Visible = esPorcentaje;
            txtX.Visible = !esPorcentaje;
            label6.Visible = !esPorcentaje;
            txtY.Visible = !esPorcentaje;
            label7.Visible = !esPorcentaje;
        }

        private void InicializarComboBoxProductos()
        {
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Id";
        }

        [MemberNotNull(nameof(descuentosTable))]
        private void InicializarDataTable()
        {
            descuentosTable = new DataTable();
            descuentosTable.Columns.Add("Nombre");
            descuentosTable.Columns.Add("Tipo");
            descuentosTable.Columns.Add("Fecha Inicio");
            descuentosTable.Columns.Add("Fecha Fin");
            descuentosTable.Columns.Add("Porcentaje o Ración");
            descuentosTable.Columns.Add("Máximo");
            descuentosTable.Columns.Add("Producto Aplicable");

            dgvDescuentos.DataSource = descuentosTable;
        }

        private void ActualizarDataGridView()
        {
            descuentosTable.Clear();
            foreach (var descuento in descuentos)
            {
                string tipo = descuento is DescuentoPorcentaje ? "Porcentaje" : "X por Y";
                string porcentajeORacion = descuento is DescuentoPorcentaje dp ? dp.Porcentaje.ToString() : $"{((DescuentoXporY)descuento).X}x{((DescuentoXporY)descuento).Y}";
                string productoAplicable = productos.FirstOrDefault(p => p.Id == descuento.ProductosAplicables.FirstOrDefault())?.Nombre;

                descuentosTable.Rows.Add(descuento.Nombre, tipo, descuento.FechaInicio.ToShortDateString(), descuento.FechaFin.ToShortDateString(), porcentajeORacion, descuento.MaximoProductos, productoAplicable);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                DateTime fechaInicio = dtpFechaInicio.Value;
                DateTime fechaFin = dtpFechaFin.Value;
                int maximoProductos = int.Parse(txtMaximoProductos.Text);
                int productoAplicableId = (int)cmbProductos.SelectedValue;

                Descuento descuento;

                if (cmbTipoDescuento.SelectedItem?.ToString() == "Porcentaje")
                {
                    double porcentaje = double.Parse(txtPorcentaje.Text);
                    descuento = new DescuentoPorcentaje(0, nombre, porcentaje, fechaInicio, fechaFin, maximoProductos, new List<int> { productoAplicableId });
                }
                else
                {
                    int x = int.Parse(txtX.Text);
                    int y = int.Parse(txtY.Text);
                    descuento = new DescuentoXporY(0, nombre, x, y, fechaInicio, fechaFin, maximoProductos, new List<int> { productoAplicableId });
                }

                descuentos.Add(descuento);
                ActualizarDataGridView();
                MessageBox.Show("Descuento guardado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el descuento: {ex.Message}");
            }
        }
        */
    }
}
