using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Facturación
{
    /// <summary>
    /// Formulario de Facturación
    /// </summary>
    public partial class Facturación : Form
    {
        /// <summary>
        /// Lista de productos facturados
        /// </summary>
        List<Tuple<Producto, int>> productosFacturados = new List<Tuple<Producto, int>>() { };
        // Variables para los totales
        /// <summary>Subtotal de la factura</summary>
        double Subtotal = 0;
        /// <summary>Descuento de la factura</summary>
        double Descuento = 0;
        /// <summary>Total de la factura</summary>
        double Total = 0;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Facturación()
        {
            // Inicializamos los componentes del formulario
            InitializeComponent();
            // Aplicamos el estilo de fuente del programa
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el nombre del facturador
            lblFacturador.Text = $"Facturador: {Program.nombreDeUsuario}";
            // Actualizamos la fecha actual
            ActualizarDataGrid();
            // Actualizamos los totales
            RefrezcarTotales();
            // Actualizamos la fecha actual
            rdbFechaActual.Text = $"Fecha Actual: {DateTime.Now.ToString("yyyy-MM-dd")}";
        }
        /// <summary>
        /// Método para actualizar el DataGrid con los productos facturados
        /// </summary>
        private void ActualizarDataGrid()
        {
            // Limpiamos el DataGrid
            dgvFacturado.DataSource = null;
            // Creamos un DataTable
            DataTable dt = new();
            // Añadimos las columnas
            dt.Columns.AddRange(new DataColumn[]{ new("cantidad"), new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Total")});
            // Añadimos las filas
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                // Obtenemos los valores de la pareja
                Producto producto = pareja.Item1;
                // Obtenemos la cantidad
                int cantidad = pareja.Item2;
                // Añadimos la fila
                dt.Rows.Add(new object[]{ $"x{cantidad}", producto.Nombre, producto.Proveedor, producto.Descripcion,
                    $"{producto.Precio:0.00}$", $"{producto.Precio * cantidad:0.00}$"});
            }
            // Asignamos el DataTable al DataGrid
            dgvFacturado.DataSource = dt;
        }
        /// <summary>
        /// Método para refrescar los totales de la factura
        /// </summary>
        private void RefrezcarTotales()
        {
            // Inicializamos los totales
            Subtotal = 0;
            // Calculamos el subtotal
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                // Sumamos el precio del producto por la cantidad
                Subtotal += (double)pareja.Item1.Precio * pareja.Item2;
            }
            // Calculamos el descuento
            Total = Subtotal * (1 - Descuento);
            // Actualizamos los labels
            lblSubtotal.Text = $"Subtotal: {Subtotal:0.00}$";
            // Actualizamos el descuento
            lblDescuento.Text = $"Descuento: {Descuento:0.00}%";
            // Actualizamos el total
            lblTotal.Text = $"Total: {Total:0.00}$";
        }
        /// <summary>
        /// Método para agregar un producto a la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Deshabilitamos el formulario
            this.Enabled = false;
            // Creamos un formulario de agregar producto
            AgregarProducto frm = new();
            // Mostramos el formulario
            frm.Show();
            // Añadimos un evento al cerrar el formulario
            frm.FormClosed += delegate
            {
                // Habilitamos el formulario
                this.Enabled = true;
                // Si el producto a facturar no es nulo
                if (frm.productoAFacturar is not null)
                    this.procesarProducto(frm.productoAFacturar, frm.cantidadEnStock);
                // Actualizamos el DataGrid
                ActualizarDataGrid();
                // Refrezcamos los totales
                RefrezcarTotales();
            };
        }
        /// <summary>
        /// Método para procesar un producto
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="cantidad"></param>
        private void procesarProducto(Producto producto, int cantidad)
        {
            // Buscamos si el producto ya esta facturado
            for (int i = 0; i < productosFacturados.Count; i++)
            {
                // Obtenemos la pareja
                Tuple<Producto, int> pareja = productosFacturados[i];
                // Si el producto ya esta facturado
                if (pareja.Item1.Id == producto.Id)
                {
                    // Actualizamos la cantidad
                    productosFacturados[i] = new Tuple<Producto, int>(producto, cantidad);
                    // Salimos del método
                    return;
                }
            }
            // Añadimos el producto a la lista de productos facturados
            productosFacturados.Add(new Tuple<Producto, int>(producto, cantidad));
        }
        /// <summary>
        /// Método para eliminar un producto de la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // Si no hay productos facturados
            this.Enabled = false;
            // Creamos un formulario de eliminar producto
            EditarProducto frm = new();
            // Mostramos el formulario
            frm.Show();
            // Añadimos un evento al cerrar el formulario
            frm.FormClosed += delegate { this.Enabled = true; };
        }
        /// <summary>
        /// Método para eliminar un producto de la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            // Habilitamos o deshabilitamos el DateTimePicker
            dtpFecha.Enabled = rdbSeleccionar.Checked;
        }
        /// <summary>
        /// Método para facturar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            // Validamos los campos
            if (txtNombreUsuario.Text == string.Empty)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("El campo 'Nombre de la Factura' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validamos los campos
            if (txtSede.Text == string.Empty)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("El campo 'Sede/Local' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validamos los campos
            if (productosFacturados.Count == 0)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("No hay productos facturados!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validamos los campos
            string TodosProductos = "";
            // Mostramos los productos facturados
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                // Añadimos el producto a la lista
                TodosProductos += $"\n{pareja.Item1.Nombre}-(x{pareja.Item2})-----------S:{pareja.Item1.Precio}--T:{pareja.Item1.Precio * pareja.Item2:0.00}";
            }
            // Mostramos un mensaje con los productos facturados
            MessageBox.Show($"NOMBRE: {txtNombreUsuario.Text}" +
                $"\nSUCURSAL: {txtSede.Text}" +
                $"\nFECHA: {(rdbFechaActual.Checked ? DateTime.Now.ToString("yyyy-MM-dd") : dtpFecha.Value.ToString("yyyy-MM-dd"))}" +
                $"{TodosProductos}" +
                $"\nSUBTOTAL: {Subtotal:0.00}$" +
                $"\nDESCUENTO: {Descuento:0.00}%" +
                $"\nTOTAL: {Total:0.00}$");
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
