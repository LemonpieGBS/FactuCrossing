using FactuCrossing.Formularios.Facturación;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class MenuAdministrador : Form
    {
        public MenuAdministrador()
        {
            InitializeComponent();
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ResetearContraseñas frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }

        private void btnAdministrar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AdministrarPersonal frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Generador_de_Reportes frm = new();
            frm.Show();

            frm.FormClosed += delegate { this.Enabled = true; };
        }
    }
}
