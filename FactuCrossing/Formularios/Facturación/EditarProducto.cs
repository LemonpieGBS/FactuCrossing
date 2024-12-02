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
    public partial class EditarProducto : Form
    {
        private Producto productoAEditar;
        public int nuevoStock = -1;
        public bool eliminar = false;
        public EditarProducto(Producto productoAEditar)
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            this.productoAEditar = productoAEditar;

            lblNombre.Text = productoAEditar.Nombre;
            lblCodigo.Text = productoAEditar.Proveedor;
            lblPrecio.Text = productoAEditar.Precio.ToString() + "$";
            lblStock.Text = productoAEditar.CantidadEnStock.ToString() + " unidades";

            numericUpDown1.Maximum = productoAEditar.CantidadEnStock;
            numericUpDown1.Minimum = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            eliminar = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value == 0)
            {
                MessageBox.Show("No puedes dejar el stock en 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            nuevoStock = (int) numericUpDown1.Value;
            this.Close();
        }
    }
}
