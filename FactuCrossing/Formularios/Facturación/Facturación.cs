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

namespace FactuCrossing.Formularios.Facturación
{
    public partial class Facturación : Form
    {
        List<Tuple<Producto, int>> productosFacturados = new List<Tuple<Producto, int>>() { };
        double Subtotal = 0;
        double Descuento = 0;
        double Total = 0;

        public Facturación()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);

            lblFacturador.Text = $"Facturador: {Program.nombreDeUsuario}";
            ActualizarDataGrid();
            RefrezcarTotales();
        }

        private void ActualizarDataGrid()
        {
            dgvFacturado.DataSource = null;

            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[]{ new("Nombre"), new("Proveedor"), new("Descripción"),
                new("Precio"), new("Stock"), new("Total")});

            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                Producto producto = pareja.Item1;
                int cantidad = pareja.Item2;

                dt.Rows.Add(new object[]{ producto.Nombre, producto.Proveedor, producto.Descripcion,
                    $"{producto.Precio:0.00}$", cantidad, $"{producto.Precio * cantidad:0.00}$"});
            }

            dgvFacturado.DataSource = dt;
        }

        private void RefrezcarTotales()
        {
            Subtotal = 0;
            foreach (Tuple<Producto, int> pareja in productosFacturados)
            {
                Subtotal += (double)pareja.Item1.Precio * pareja.Item2;
            }

            Total = Subtotal * (1 - Descuento);
            lblSubtotal.Text = $"Subtotal: {Subtotal:0.00}$";
            lblDescuento.Text = $"Descuento: {Descuento:0.00}%";
            lblTotal.Text = $"Total: {Total:0.00}$";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AgregarProducto frm = new();
            frm.Show();

            frm.FormClosed += delegate
            {
                this.Enabled = true;

                if (frm.productoAFacturar is not null)
                    this.procesarProducto(frm.productoAFacturar, frm.cantidadEnStock);

                ActualizarDataGrid();
                RefrezcarTotales();
            };
        }

        private void procesarProducto(Producto producto, int Cantidad)
        {
            for (int i = 0; i < productosFacturados.Count; i++)
            {
                Tuple<Producto, int> pareja = productosFacturados[i];

                if (pareja.Item1.Id == producto.Id)
                {
                    productosFacturados[i] = new Tuple<Producto, int>(producto, Cantidad);
                    return;
                }
            }
            productosFacturados.Add(new Tuple<Producto, int>(producto, Cantidad));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            EditarProducto frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }
    }
}
