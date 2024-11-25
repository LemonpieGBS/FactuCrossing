using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuCrossing.Formularios.Reportes
{
    public partial class GeneracionDeReportes : Form
    {
        public GeneracionDeReportes()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
        }
    }
}
