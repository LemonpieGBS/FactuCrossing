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
    public partial class AplicarDescuento : Form
    {
        List<Descuento> descuentosMostrados = new List<Descuento>();
        public Descuento? descuentoAplicado = null;
        public AplicarDescuento(Producto producto)
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);

            
            // Ciclamos por los descuentos en memoria para ver cuales aplican al producto
            foreach (Descuento descuento in SistemaCentral.Descuentos.descuentosEnMemoria)
            {
                if (descuento.ProductoAplicable == -1 || descuento.ProductoAplicable == producto.Id)
                {
                    if (descuento.EstaVigente())
                    {
                        cmbDescuentos.Items.Add($"{descuento.Nombre}" +
                            $" ({(descuento.ProductoAplicable == -1 ? "General" : "Especial")}, {descuento.Porcentaje}%)");
                        descuentosMostrados.Add(descuento);
                    }
                }
            }

        }

        public AplicarDescuento()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);

            // Ciclamos por los descuentos en memoria para ver cuales aplican al producto
            foreach (Descuento descuento in SistemaCentral.Descuentos.descuentosEnMemoria)
            {
                if (descuento.ProductoAplicable == -1)
                {
                    if (descuento.EstaVigente())
                    {
                        cmbDescuentos.Items.Add($"{descuento.Nombre}" +
                            $" ({(descuento.ProductoAplicable == -1 ? "General" : "Especial")}, {descuento.Porcentaje}%)");
                        descuentosMostrados.Add(descuento);
                    }
                }
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Aplicamos el descuento y cerramos
            if (cmbDescuentos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione un descuento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            descuentoAplicado = descuentosMostrados[cmbDescuentos.SelectedIndex];

            this.Close();
        }
    }
}
