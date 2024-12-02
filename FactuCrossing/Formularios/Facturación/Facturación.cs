using FactuCrossing.Estructuras;
using Microsoft.Reporting.WinForms;
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
        /// <summary>
        /// Lista de descuentos aplicados
        /// </summary>
        List<Descuento> descuentosAplicados = new List<Descuento>() { };
        // Variables para los totales
        /// <summary>Subtotal de la factura</summary>
        double Subtotal = 0;
        /// <summary>Descuento de la factura</summary>
        double Descuento = 0;
        /// <summary>Total de la factura</summary>
        double Total = 0;
        /// <summary>Producto seleccionado</summary>
        int productoSeleccionado = -1;

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
            lblFacturador.Text = $"Facturador: {SistemaCentral.Cuentas.cuentaEnSesion?.NombreDisplay}";
            // Actualizamos la fecha actual
            ActualizarDataGrid();
            // Actualizamos los totales
            RefrezcarTotales();
            // Actualizamos la fecha actual
            rdbFechaActual.Text = $"Fecha Actual: {DateTime.Now.ToString("yyyy-MM-dd")}";
            // Valor default del statusStripLabel
            strLabel.Text = "No hay ningun producto seleccionado.";
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
            dt.Columns.AddRange(new DataColumn[]{ new("Cantidad"), new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Subtotal")});
            // Añadimos las filas
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                // Obtenemos los valores de la pareja
                Producto producto = pareja.Item1;
                // Obtenemos la cantidad
                int cantidad = pareja.Item2;
                // Añadimos la fila
                dt.Rows.Add(new object[]{ $"{cantidad}", producto.Nombre, producto.Proveedor, producto.Descripcion,
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
            Total = 0;
            double totalDescuento = 0;

            // Calculamos el subtotal
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                double precioProducto = (double)pareja.Item1.Precio * pareja.Item2;
                Subtotal += precioProducto;

                // Aplicamos descuentos específicos del producto
                double descuentoProducto = 0;
                foreach (Descuento descuento in descuentosAplicados)
                {
                    if (descuento.ProductoAplicable == pareja.Item1.Id)
                    {
                        descuentoProducto += precioProducto * (descuento.Porcentaje / 100);
                    }
                }
                totalDescuento += descuentoProducto;
            }

            // Aplicamos descuentos que aplican a todos los productos
            foreach (Descuento descuento in descuentosAplicados)
            {
                if (descuento.ProductoAplicable == -1)
                {
                    totalDescuento += Subtotal * (descuento.Porcentaje / 100);
                }
            }

            // Calculamos el total después de aplicar los descuentos
            Total = Subtotal - totalDescuento;

            // Calculamos el descuento total
            Descuento = totalDescuento;

            // Actualizamos los labels
            lblSubtotal.Text = $"Subtotal: {Subtotal:0.00}$";
            lblDescuento.Text = $"Descuento: {Descuento:0.00}$";
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
            // Revisamos si hay un producto seleccionado
            if (productoSeleccionado == -1)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("Por favor seleccione un producto", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Creamos un formulario de eliminar producto
            EditarProducto frm = new EditarProducto(
                SistemaCentral.Inventario.ObtenerProductoPorId(productoSeleccionado) ??
                    throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido")
                );
            // Abrimos frm como dialogo
            frm.ShowDialog();
            // Si se elimina el producto
            if (frm.eliminar)
            {
                // Eliminamos el producto de la lista de productos facturados
                productosFacturados.RemoveAll(p => p.Item1.Id == productoSeleccionado);
                // Actualizamos el DataGrid
                ActualizarDataGrid();
                // Refrezcamos los totales
                RefrezcarTotales();
            }
            // Si se actualiza el stock
            else if (frm.nuevoStock != -1)
            {
                // Obtenemos el producto
                Producto producto = SistemaCentral.Inventario.ObtenerProductoPorId(productoSeleccionado)
                    ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido");
                // Actualizamos la cantidad en el tuple
                procesarProducto(producto, frm.nuevoStock);
                // Actualizamos el DataGrid
                ActualizarDataGrid();
                // Refrezcamos los totales
                RefrezcarTotales();
            }
            // Descartamos el formulario
            frm.Dispose();
            // Valor default del statusStripLabel
            strLabel.Text = "No hay ningun producto seleccionado.";
            // Reseteamos el producto seleccionado
            productoSeleccionado = -1;
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

            // Creamos el reporte
            // Creamos el DataTable (lit no hay necesidad de crear una clase)
            DataTable dt = new DataTable();
            // Creamos las columnas (deben tener el mismo nombre de los atributos en el DataSet)
            dt.Columns.AddRange(new DataColumn[] { new("Nombre"), new("Proveedor"),
                new("Cantidad"), new("PrecioUnitario"), new("PrecioTotal") });
            // Foreach para llenar el DataTable
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                Producto producto = pareja.Item1;
                int cantidad = pareja.Item2;
                // Agregamos los datos
                dt.Rows.Add(new object[] { producto.Nombre, producto.Proveedor, cantidad,
                    $"{producto.Precio:0.00}$", $"{producto.Precio * cantidad:0.00}$"});
            }
            // Creamos el DataSource
            ReportDataSource rds = new ReportDataSource("DsVenta", dt);
            // Damos la locación del RDLC
            string embedLocation = "FactuCrossing.Reportes.RptFactura.rdlc";
            // Parametros
            List<ReportParameter> listaParametros = new List<ReportParameter>()
            {
                new ReportParameter("FechaFactura", dtpFecha.Value.ToString("dd/MM/yyyy") ),
                new ReportParameter("Total", $"{Total:0.00}$"),
                new ReportParameter("NombreFactura", txtNombreUsuario.Text),
                new ReportParameter("SucursalFactura", txtSede.Text),
                new ReportParameter("NumeroFactura", $"F{1:0000000}"),
                new ReportParameter("Facturista", SistemaCentral.Cuentas.cuentaEnSesion?.NombreDisplay)
            };
            // Creamos el reporte
            Report reporteFactura = new Report(embedLocation, [rds], listaParametros);
            // Abrimos el dialogo
            new VistaPreviaReporte(reporteFactura).ShowDialog();
            // Aplicamos los cambios a los stock de los productos
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                Producto producto = pareja.Item1;
                int cantidad = pareja.Item2;
                producto.CantidadEnStock -= cantidad;
            }

            // Limpiamos los productos facturados
            productosFacturados.Clear();
            // Limpiamos los descuentos aplicados
            descuentosAplicados.Clear();
            // Actualizamos el DataGrid
            ActualizarDataGrid();
            // Refrezcamos los totales
            RefrezcarTotales();
            // Valor default del statusStripLabel
            strLabel.Text = "No hay ningun producto seleccionado.";
            // Reseteamos el producto seleccionado
            productoSeleccionado = -1;
            // Limpiamos los campos
            txtNombreUsuario.Text = string.Empty;
            // Guardamos
            SistemaCentral.Inventario.GuardarProductos();
            // Cerramos despues de que el dialogo se cierre
            this.Close();
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvFacturado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Conseguimos el producto del inventario
            string nombre = (((DataTable)dgvFacturado.DataSource).Rows[e.RowIndex]["Nombre"]).ToString()
                ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido");
            // Conseguimos el producto del inventario
            Producto producto = SistemaCentral.Inventario.EncontrarProductoPorNombre(nombre)
                ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido");
            // Asignamos el producto seleccionado
            productoSeleccionado = producto.Id;
            // Actualizamos el statusStrip
            strLabel.Text = $"Producto seleccionado: {producto.Nombre}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AplicarDescuento frm;
            if (productoSeleccionado == -1)
            {
                frm = new AplicarDescuento();
            } else
            {
                frm = new AplicarDescuento(SistemaCentral.Inventario.ObtenerProductoPorId(productoSeleccionado)
                    ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido"));
            }
            // Mostramos el formulario como dialogo
            frm.ShowDialog();
            // Si se selecciona un descuento
            if (frm.descuentoAplicado is not null)
            {
                // Si el descuento ya está no hacemos nada
                if (descuentosAplicados.Contains(frm.descuentoAplicado))
                {
                    // Mostramos un mensaje de error
                    MessageBox.Show("El descuento ya ha sido aplicado", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Añadimos el descuento a la lista de descuentos aplicados
                descuentosAplicados.Add(frm.descuentoAplicado);
                // Refrezcamos los totales
                RefrezcarTotales();
            }
        }
    }
}
