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
        public Facturación()
        {
            InitializeComponent();
            lblFacturador.Text = $"Facturador: {Program.nombreDeUsuario}";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AgregarProducto frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
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
