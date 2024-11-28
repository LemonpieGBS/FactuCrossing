using FactuCrossing.Estructuras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuCrossing.Formularios.Inventario
{
    public partial class Inventario : Form
    {
        /// <summary>
        /// Constructor de la clase Inventario
        /// </summary>
        public Inventario()
        {
            // Inicializamos los componentes
            InitializeComponent();
            // Si la fuente del programa está cargada, la aplicamos
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el DataGridView
            ActualizarDataGrid();
        }

        /// <summary>
        /// Actualiza el DataGridView con los productos en memoria
        /// </summary>
        public void ActualizarDataGrid()
        {
            // Limpiamos el DataGridView
            dgvInventario.DataSource = null;
            // Creamos un DataTable
            DataTable dt = new();
            // Agregamos las columnas al DataTable
            dt.Columns.AddRange(new DataColumn[]{ new("ID"), new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Stock"), new("Fecha de Ingreso")});
            // Por cada producto en la lista de productos en memoria
            foreach (Producto producto in SistemaCentral.Inventario.productosEnMemoria)
            {
                // Agregamos una fila al DataTable con los datos del producto
                dt.Rows.Add(new object[]{ producto.Id, producto.Nombre, producto.Proveedor, producto.Descripcion,
                    $"{producto.Precio:0.00}$", producto.CantidadEnStock, producto.FechaIngreso.ToString("yyyy-MM-dd")});
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
            // Agregamos el producto a la lista de productos
            SistemaCentral.Inventario.productosEnMemoria.Add(nuevoProducto);
            // Actualizamos el DataGridView
            ActualizarDataGrid();
            // Guardamos el inventario
            SistemaCentral.Inventario.GuardarProductos();
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
    }
}
