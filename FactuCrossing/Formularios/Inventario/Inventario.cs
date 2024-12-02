using FactuCrossing.Estructuras;
using System.Data;

namespace FactuCrossing.Formularios.Inventario
{
    public partial class Inventario : Form
    {
        // Cuenta en sesión
        private Cuenta cuentaEnSesion;

        // Id
        private int productoSeleccionado = -1;

        /// <summary>
        /// Constructor de la clase Inventario
        /// </summary>
        public Inventario()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, cerrar el formulario
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                // Asignamos la cuenta default
                cuentaEnSesion = Cuenta.CuentaDefault;
                return;
            }
            // Asignamos la cuenta
            cuentaEnSesion = SistemaCentral.Cuentas.cuentaEnSesion;

            // Inicializamos los componentes
            InitializeComponent();
            // Si la fuente del programa está cargada, la aplicamos
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el DataGridView
            ActualizarDataGrid();
            // Actualizamos el statusStrip
            strLabel.Text = "No hay ningún producto seleccionado";
            // Actualizamos los proveedores del inventario como opciones en el ComboBox
            ActualizarProveedores();
        }

        private void ActualizarProveedores()
        {
            txtProveedor.Items.Clear();
            foreach (Producto producto in SistemaCentral.Inventario.productosEnMemoria)
            {
                if (!txtProveedor.Items.Contains(producto.Proveedor))
                {
                    txtProveedor.Items.Add(producto.Proveedor);
                }
            }
        }

        /// <summary>
        /// Código para limpiar campos
        /// </summary>
        private void LimpiarCampos()
        {
            // Limpiamos los campos
            // Limpiamos el campo de Nombre
            txtNombre.Text = string.Empty;
            // Limpiamos el campo de Descripción
            rtxtDescripcion.Text = string.Empty;
            // Limpiamos el campo de Proveedor
            txtProveedor.Text = string.Empty;
            // Limpiamos el campo de Stock
            nudStock.Value = 0;
            // Limpiamos el campo de Precio
            txtPrecio.Text = "";
        }

        /// <summary>
        /// Actualiza el DataGridView con los productos en memoria
        /// </summary>
        public void ActualizarDataGrid(bool mostrarDescontinuado = false)
        {
            // Limpiamos el DataGridView
            dgvInventario.DataSource = null;
            // Creamos un DataTable
            DataTable dt = new();
            // Agregamos las columnas al DataTable
            dt.Columns.AddRange(new DataColumn[]{ new("ID"), new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Stock"), new("Fecha de Ingreso"), new("Descontinuado?")});
            // Por cada producto en la lista de productos en memoria
            foreach (Producto producto in SistemaCentral.Inventario.productosEnMemoria)
            {
                // Si el producto está descontinuado y no queremos mostrar productos descontinuados, continuar
                if (producto.Descontinuado && !mostrarDescontinuado) continue;
                // Agregamos una fila al DataTable con los datos del producto
                dt.Rows.Add(new object[]{ producto.Id, producto.Nombre, producto.Proveedor, producto.Descripcion,
                    $"{producto.Precio:0.00}$", producto.CantidadEnStock, producto.FechaIngreso.ToString("yyyy-MM-dd"),
                    producto.Descontinuado ? "Si" : "No"});
            }
            // Asignamos el DataTable al DataSource del DataGridView
            dgvInventario.DataSource = dt;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar campos
            // Nombre, Proveedor, Precio, Stock

            // Si el campo de Nombre está vacío, mostrar mensaje de error
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Nombre' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el campo de Proveedor está vacío, mostrar mensaje de error
            if (txtProveedor.Text == string.Empty)
            {
                MessageBox.Show("El campo 'Proveedor' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el campo de Precio está vacío, mostrar mensaje de error
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("El campo 'Precio' esta vacío", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Creamos una variable decimal para el precio parseado
            decimal precioParseado;
            // Si no se pudo convertir el precio a decimal, mostrar mensaje de error
            if (!decimal.TryParse(txtPrecio.Text, out precioParseado))
            {
                MessageBox.Show("No se pudo convertir el precio a decimal", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el campo de Precio es negativo o 0, mostrar mensaje de error
            if (precioParseado <= 0)
            {
                MessageBox.Show("El campo 'Precio' no puede ser negativo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el campo de Stock es negativo, mostrar mensaje de error
            if (nudStock.Value < 0)
            {
                MessageBox.Show("El campo 'Stock' no puede ser negativo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Creamos el producto con los datos ingresados
            Producto nuevoProducto = new(
                // ID del producto es el tamaño de la lista de productos
                _id: SistemaCentral.Inventario.productosEnMemoria.Count,
                // Nombre del producto es el texto del campo de Nombre
                _nombre: txtNombre.Text,
                // Descripción del producto es el texto del campo de Descripción
                _descripcion: rtxtDescripcion.Text,
                // Precio del producto es el precio parseado
                _precio: precioParseado,
                // Proveedor del producto es el texto del campo de Proveedor
                _proveedor: txtProveedor.Text,
                // Fecha de ingreso del producto es la fecha actual
                _fechaIngreso: DateTime.Now,
                // Cantidad en stock del producto es el valor del campo de Stock
                _stock: (int)nudStock.Value
                );
            // Revisamos si el producto ya existe en el inventario
            if (SistemaCentral.Inventario.productosEnMemoria.Any(p => p.Nombre == nuevoProducto.Nombre))
            {
                // Si el producto ya existe, preguntamos si simplemente quieren añadir stock
                DialogResult resultado = MessageBox.Show("El producto ya existe en el inventario, ¿desea añadir stock?",
                    "Producto ya existe", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si la respuesta es No, retornamos
                if (resultado == DialogResult.No) return;
                // Obtener el Id del producto por su nombre
                int idProductoExistente =
                    SistemaCentral.Inventario.EncontrarProductoPorNombre(nuevoProducto.Nombre)?.Id
                    ?? throw new ArgumentNullException("Hubo un problema procesando el stock");
                // Si la respuesta es Sí, buscamos el producto en la lista de productos
                Producto productoExistente = SistemaCentral.Inventario.ObtenerProductoPorId(idProductoExistente)
                    ?? throw new ArgumentNullException("Hubo un problema procesado el stock");
                // Añadimos el stock al producto existente
                productoExistente.CantidadEnStock += nuevoProducto.CantidadEnStock;
                // Refrezcamos el producto
                SistemaCentral.Inventario.RefrezcarProducto(productoExistente);
                // Agregamos la acción al historial
                Accion accionHecha = new Accion(cuentaEnSesion.Id, $"Añadió {nuevoProducto.CantidadEnStock} al stock del producto {nuevoProducto.Nombre}",
                    DateTime.Now);
                // Agregamos la acción a la lista de acciones en memoria
                SistemaCentral.Acciones.accionesEnMemoria.Add(accionHecha);
            }
            else // Agregamos el producto
            {
                // Agregamos el producto a la lista de productos
                SistemaCentral.Inventario.AñadirProducto(nuevoProducto);
                // Agregamos la acción al historial
                Accion nuevaAccion = new Accion(cuentaEnSesion.Id, $"Agregó el producto {nuevoProducto.Nombre} al inventario",
                    DateTime.Now);
                // Agregamos la acción a la lista de acciones en memoria
                SistemaCentral.Acciones.accionesEnMemoria.Add(nuevaAccion);
            }
            SistemaCentral.Acciones.GuardarAcciones();
            // Actualizamos el DataGridView
            ActualizarDataGrid();
            // Guardamos el inventario
            SistemaCentral.Inventario.GuardarProductos();
            // Limpiamos los campos
            LimpiarCampos();
            // Actualizamos los proveedores
            ActualizarProveedores();
        }

        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idConseguido = -1;

            // Intentamos obtener el ID de la cuenta seleccionada
            if (!int.TryParse((((DataTable)dgvInventario.DataSource).Rows[e.RowIndex]["ID"]).ToString(), out idConseguido))
            {
                // Si hay un problema con la selección, mostramos un mensaje de error
                MessageBox.Show("Hubo un problema con la selección", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Conseguimos el producto del inventario
            Producto producto = SistemaCentral.Inventario.ObtenerProductoPorId(idConseguido)
                ?? throw new ArgumentNullException("El producto seleccionado no se detectó como uno válido");

            // Asignamos el producto seleccionado
            productoSeleccionado = producto.Id;

            // Actualizamos el statusStrip
            strLabel.Text = $"Producto seleccionado: {producto.Nombre}";

            // Actualizamos el texto del boton
            btnDescontinuar.Text = producto.Descontinuado ? "Recontinuar" : "Descontinuar";

            // Actualizamos los campos con los detalles del producto
            txtNombre.Text = producto.Nombre;
            rtxtDescripcion.Text = producto.Descripcion;
            txtProveedor.Text = producto.Proveedor;
            txtPrecio.Text = producto.Precio.ToString();
            nudStock.Value = producto.CantidadEnStock;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Revisamos si hay un producto seleccionado
            if (productoSeleccionado == -1)
            {
                // Si no hay un producto seleccionado, mostramos un mensaje de error
                MessageBox.Show("No hay un producto seleccionado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // Obtenemos el producto existente
                Producto productoExistente = SistemaCentral.Inventario.ObtenerProductoPorId(productoSeleccionado)
                    ?? throw new ArgumentNullException("Hubo un problema editando el producto");
                // Creamos el nuevo producto para reemplazar el anterior
                Producto nuevoProducto = new(
                    // ID del producto es el ID del producto seleccionado
                    _id: productoExistente.Id,
                    // Nombre del producto es el texto del campo de Nombre
                    _nombre: txtNombre.Text,
                    // Descripción del producto es el texto del campo de Descripción
                    _descripcion: rtxtDescripcion.Text,
                    // Precio del producto es el precio parseado
                    _precio: decimal.Parse(txtPrecio.Text),
                    // Proveedor del producto es el texto del campo de Proveedor
                    _proveedor: txtProveedor.Text,
                    // Fecha de ingreso del producto es la fecha actual
                    _fechaIngreso: productoExistente.FechaIngreso,
                    // Cantidad en stock del producto es el valor del campo de Stock
                    _stock: (int)nudStock.Value
                    );
                // Reemplazamos el producto existente con el nuevo producto
                SistemaCentral.Inventario.RefrezcarProducto(nuevoProducto);
                // Actualizamos el DataGridView
                ActualizarDataGrid();
                // Guardamos el inventario
                SistemaCentral.Inventario.GuardarProductos();
                // Limpiamos los campos
                LimpiarCampos();
                // Agregamos la acción al historial
                Accion accion = new(cuentaEnSesion.Id, "Editó un producto", DateTime.Now);
                // Agregamos la acción a la memoria
                SistemaCentral.Acciones.accionesEnMemoria.Add(accion);
                // Agregamos la acción a la lista de acciones en memoria
                SistemaCentral.Acciones.GuardarAcciones();
                // Actualizamos el statusStrip
                strLabel.Text = "No hay ningún producto seleccionado";
                // Removemos el producto seleccionado
                productoSeleccionado = -1;
                // Actualizamos los proveedores
                ActualizarProveedores();
            }
        }

        private void btnDescontinuar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == -1)
            {
                MessageBox.Show("No hay un producto seleccionado", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // Obtenemos el producto existente
                Producto productoExistente = SistemaCentral.Inventario.ObtenerProductoPorId(productoSeleccionado)
                    ?? throw new ArgumentNullException("Hubo un problema descontinuando el producto");
                // Descontinuamos el producto
                productoExistente.MarcarDescontinuado(!productoExistente.Descontinuado);
                // Refrezcamos el producto
                SistemaCentral.Inventario.RefrezcarProducto(productoExistente);
                // Actualizamos el DataGridView
                ActualizarDataGrid();
                // Guardamos el inventario
                SistemaCentral.Inventario.GuardarProductos();
                // Accion
                Accion accion;
                // Agregamos la acción al historial
                if (productoExistente.Descontinuado) accion = new(cuentaEnSesion.Id, $"Descontinuó el producto {productoExistente.Nombre}", DateTime.Now);
                else accion = new(cuentaEnSesion.Id, $"Recontinuó el producto {productoExistente.Nombre}", DateTime.Now);
                // Agregamos la acción a la memoria
                SistemaCentral.Acciones.accionesEnMemoria.Add(accion);
                // Guardamos las acciones
                SistemaCentral.Acciones.GuardarAcciones();
                // Actualizamos el texto del boton
                btnDescontinuar.Text = productoExistente.Descontinuado ? "Recontinuar" : "Descontinuar";
            }
        }

        private void chbDescontinuado_CheckedChanged(object sender, EventArgs e)
        {
            // Actualizamos el DataGridView
            ActualizarDataGrid(chbDescontinuado.Checked);
        }

        private void btnDescuentos_Click(object sender, EventArgs e)
        {
            // Mostrar el form como dialogo
            DescuentosMM descuentos = new();

            // Mostrar el formulario
            descuentos.ShowDialog();

            descuentos.Dispose();
        }
    }
}
