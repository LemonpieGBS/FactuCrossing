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
        /// Cuenta Activa
        /// </summary>
        Cuenta cuentaActiva;
        /// <summary>
        /// Lista de productos facturados
        /// </summary>
        Factura facturaActiva;
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
            // Si la cuenta en sesion es nula mandar un mensaje de error de autenticación
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                // Nothing
                cuentaActiva = Cuenta.CuentaDefault;
                // Asignamos la cuenta default
                facturaActiva = new Factura(1, Cuenta.CuentaDefault, null, null);
                return;
            } else cuentaActiva = SistemaCentral.Cuentas.cuentaEnSesion;
            // Inicializamos los componentes del formulario
            InitializeComponent();
            // Aplicamos el estilo de fuente del programa
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el nombre del facturador
            lblFacturador.Text = $"Facturador: {SistemaCentral.Cuentas.cuentaEnSesion?.NombreDisplay}";
            // Actualizamos la fecha actual
            rdbFechaActual.Text = $"Fecha Actual: {DateTime.Now.ToString("yyyy-MM-dd")}";
            // Valor default del statusStripLabel
            strLabel.Text = "No hay ningun producto seleccionado.";
            // Asignar factura a nueva factura
            facturaActiva = new Factura(SistemaCentral.Facturas.GenerarId(), cuentaActiva, null, null);
            // Actualizamos la fecha actual
            ActualizarDataGrid();
            // Actualizamos los totales
            RefrezcarTotales();
        }
        /// <summary>
        /// Método para actualizar el DataGrid con los productos facturados
        /// </summary>
        private void ActualizarDataGrid()
        {
            // Limpiamos el DataGrid
            dgvFacturado.DataSource = null;
            // Asignamos el DataTable al DataGrid
            dgvFacturado.DataSource = facturaActiva.ToDataTable();
        }
        /// <summary>
        /// Método para refrescar los totales de la factura
        /// </summary>
        private void RefrezcarTotales()
        {
            // Mandamos a refrezcar los totales
            facturaActiva.CalcularTotales();
            // Inicializamos los totales
            Subtotal = facturaActiva.Subtotal;
            Total = facturaActiva.Total;
            Descuento = facturaActiva.Descuento;

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
            // Añadimos el producto a la factura
            facturaActiva.AgregarProductoFacturado(producto, cantidad);
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
                facturaActiva.RemoverProductoFacturado(productoSeleccionado);
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
            facturaActiva.NombreFactura = txtNombreUsuario.Text;
            // Validamos los campos
            if (txtSede.Text == string.Empty)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("El campo 'Sede/Local' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            facturaActiva.Sucursal = txtSede.Text;
            facturaActiva.FechaFactura = rdbFechaActual.Checked ? DateTime.Now : dtpFecha.Value;
            // Validamos los campos
            if (facturaActiva.ProductosFacturados.Count == 0)
            {
                // Mostramos un mensaje de error
                MessageBox.Show("No hay productos facturados!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Creamos el reporte
            // Creamos el DataTable (lit no hay necesidad de crear una clase)
            DataTable dt = facturaActiva.ToReportDataTable();
            // Creamos el DataSource
            ReportDataSource rds = new ReportDataSource("DsVenta", dt);
            // Damos la locación del RDLC
            string embedLocation = "FactuCrossing.Reportes.RptFactura.rdlc";
            // Parametros
            List<ReportParameter> listaParametros = new List<ReportParameter>()
            {
                new ReportParameter("FechaFactura", facturaActiva.FechaFactura.ToString("dd/MM/yyyy") ),
                new ReportParameter("Total", $"{facturaActiva.Total:0.00}$"),
                new ReportParameter("NombreFactura", facturaActiva.NombreFactura),
                new ReportParameter("SucursalFactura", facturaActiva.Sucursal),
                new ReportParameter("NumeroFactura", $"F{facturaActiva.NumFactura:0000000}"),
                new ReportParameter("Facturista", facturaActiva.Facturista),
                new ReportParameter("Subtotal", $"{facturaActiva.Subtotal:0.00}$"),
                new ReportParameter("Descuento", $"{facturaActiva.Descuento:0.00}$")
            };

            if(facturaActiva.DescuentoGlobal is not null)
                listaParametros.Add(new ReportParameter("DescuentoGlobal", $"{facturaActiva.DescuentoGlobal.Nombre}, {facturaActiva.DescuentoGlobal.Porcentaje}%"));
            // Creamos el reporte
            Report reporteFactura = new Report(embedLocation, [rds], listaParametros);
            // Abrimos el dialogo
            new VistaPreviaReporte(reporteFactura).ShowDialog();
            // Aplicamos los cambios a los stock de los productos
            foreach (ProductoFacturado producto in facturaActiva.ProductosFacturados)
            {
                Producto productoEnInventario = SistemaCentral.Inventario.ObtenerProductoPorId(producto.IDenInventario)
                    ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido");
                productoEnInventario.CantidadEnStock -= producto.Cantidad;
            }
            // Valor default del statusStripLabel
            strLabel.Text = "No hay ningun producto seleccionado.";
            // Reseteamos el producto seleccionado
            productoSeleccionado = -1;
            // Limpiamos los campos
            txtNombreUsuario.Text = string.Empty;
            // Guardamos
            SistemaCentral.Inventario.GuardarProductos();
            // Agregamos la factura a memoria
            SistemaCentral.Facturas.AñadirFactura(facturaActiva);
            // Guardamos
            SistemaCentral.Facturas.GuardarFacturas();
            // REgistrar la acción
            Accion accion = new Accion(cuentaActiva.Id, $"Facturó {facturaActiva.ProductosFacturados.Count} productos, ID: {facturaActiva.NumFactura}", DateTime.Now);
            // Agregar a la lista
            SistemaCentral.Acciones.accionesEnMemoria.Add(accion);
            // Guardar a disco
            SistemaCentral.Acciones.GuardarAcciones();
            // Limpiamos la factura
            facturaActiva = new Factura(SistemaCentral.Facturas.GenerarId(), cuentaActiva, null, null);
            // Actualizamos el DataGrid
            ActualizarDataGrid();
            // Refrezcamos los totales
            RefrezcarTotales();
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
                // Ahora reescribiremos el codigo haciendo uso de la clase Factura
                if(facturaActiva.DescuentosAplicados.Any(c => c.Id == frm.descuentoAplicado.Id))
                {
                    // Mostramos un mensaje de error
                    MessageBox.Show("El descuento ya ha sido aplicado", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Si el descuento no es global vemos si ya existe el producto al que se aplica
                if (facturaActiva.DescuentosAplicados.Any(c => c.ProductoAplicable == frm.descuentoAplicado.ProductoAplicable))
                {
                    // Preguntamos al usuario si desea sobreescribir el descuento del producto
                    DialogResult dialogResult = MessageBox.Show("Ya hay un descuento aplicado a este producto, ¿desea sobreescribirlo?", "Advertencia",
                        MessageBoxButtons.YesNo);
                    // Si el usuario dice que sí, sobreescribimos el descuento
                    if (dialogResult == DialogResult.Yes)
                        facturaActiva.DescuentosAplicados.Add(frm.descuentoAplicado);
                } else if(frm.descuentoAplicado.ProductoAplicable != -1) facturaActiva.DescuentosAplicados.Add(frm.descuentoAplicado);

                // Ahora vemos si sobreescribimos el global
                if (frm.descuentoAplicado.ProductoAplicable == -1)
                {
                    // Preguntamos al usuario si desea sobreescribir el descuento global
                    if(facturaActiva.DescuentoGlobal is not null)
                    {
                        DialogResult dialogResult = MessageBox.Show("Ya hay un descuento global aplicado, ¿desea sobreescribirlo?",
                            "Descuento Global", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes) facturaActiva.EstablecerDescuentoGlobal(frm.descuentoAplicado);
                    } else facturaActiva.EstablecerDescuentoGlobal(frm.descuentoAplicado);
                }

                // Refrezcamos los totales
                RefrezcarTotales();
                // Actualizamos el DataGrid
                ActualizarDataGrid();
            }
        }
    }
}
