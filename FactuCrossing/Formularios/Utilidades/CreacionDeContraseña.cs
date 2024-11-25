using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactuCrossing.Formularios.Utilidades
{
    public partial class CreacionDeContraseña : Form
    {
        public string Contrasena = "";
        int Seguridad = 0;

        public CreacionDeContraseña()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            lblSeguridad.Text = "Seguridad: No Determinada";
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            Contrasena = txtContrasena.Text;

            Seguridad = 0;

            Seguridad += (Contrasena.Any(char.IsUpper)) ? 1 : 0;
            Seguridad += (Contrasena.Any(char.IsLower)) ? 1 : 0;
            Seguridad += (Contrasena.Any(char.IsDigit)) ? 1 : 0;
            Seguridad += (Contrasena.Any(char.IsSymbol)
                || Contrasena.Any(char.IsPunctuation)) ? 1 : 0;

            switch (Seguridad)
            {
                case 0: lblSeguridad.Text = "Seguridad: No Determinada"; lblSeguridad.ForeColor = Color.Black; break;
                case <= 2: lblSeguridad.Text = "Seguridad: Débil"; lblSeguridad.ForeColor = Color.Red; break;
                case 3: lblSeguridad.Text = "Seguridad: Mediana"; lblSeguridad.ForeColor = Color.Orange; break;
                case 4: lblSeguridad.Text = "Seguridad: Fuerte"; lblSeguridad.ForeColor = Color.DarkGreen; break;
            }


            // Esto para evitar la putisima animacion de la barra de progreso en WinForms
            pgbSeguridad.SetProgressNoAnimation(Seguridad);
        }

        private void btnMostrar_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void btnMostrar_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtContrasena.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las dos contraseñas no coinciden!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtContrasena.Text == string.Empty)
            {
                MessageBox.Show("La contraseña no puede estar vacía!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Seguridad < 4 && MessageBox.Show("¿Está seguro de proceder con esta contraseña, francamente, insegura?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnMostrar_MouseDown(object sender, MouseEventArgs e)
        {
            txtContrasena.PasswordChar = '\0';
        }

        private void btnMostrar_MouseUp(object sender, MouseEventArgs e)
        {
            txtContrasena.PasswordChar = '*';
        }
    }
}
