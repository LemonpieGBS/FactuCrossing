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
        // Variables para controlar la contraseña y la seguridad de la misma
        public string Contrasena = "";
        // Seguridad de la contraseña que se determina algoritmicamente en
        // txtContrasena_TextChanged
        int Seguridad = 0;

        // Constructor del form en donde se establecen algunas cosas
        public CreacionDeContraseña()
        {
            // Inicializar componente de WinForms
            InitializeComponent();
            // Si la fuente principal existe, aplicarla a todos los controles en el form
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Hacer que el label diga que la seguridad está no determinada
            lblSeguridad.Text = "Seguridad: No Determinada";
            // El DialogResult es Cancel por default
            this.DialogResult = DialogResult.Cancel;
            // Establecer la seguridad máxima y editar el atributo de la barra de progreso
            int MaximumSecurity = 35;
            pgbSeguridad.Maximum = MaximumSecurity;
        }

        // Este evento se ejecuta cada vez que se altera el texto en txtContrasena
        // Se usa para reevaluar la seguridad de la contraseña
        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            Contrasena = txtContrasena.Text;
            // Empezamos con seguridad en 0
            Seguridad = 0;
            // Dependiendo de si cumple ciertos parametros, asignamos un puntaje
            // (Siendo el máximo 35)
            Seguridad += (Contrasena.Length > 10) ? 10 : 0;
            Seguridad += (Contrasena.Any(char.IsUpper)) ? 5 : 0;
            Seguridad += (Contrasena.Any(char.IsLower)) ? 5 : 0;
            Seguridad += (Contrasena.Any(char.IsDigit)) ? 5 : 0;
            Seguridad += (Contrasena.Any(char.IsSymbol)
                || Contrasena.Any(char.IsPunctuation)) ? 10 : 0;
            // Un switch aqui listito para asignar un color y texto al label
            // para indicarle al usuario que tan segura es su contraseña
            switch (Seguridad)
            {
                case 0: lblSeguridad.Text = "Seguridad: No Determinada"; lblSeguridad.ForeColor = Color.Black; break;
                case <15: lblSeguridad.Text = "Seguridad: Débil"; lblSeguridad.ForeColor = Color.Red; break;
                case <25: lblSeguridad.Text = "Seguridad: Mediana"; lblSeguridad.ForeColor = Color.Orange; break;
                case 35: lblSeguridad.Text = "Seguridad: Fuerte"; lblSeguridad.ForeColor = Color.DarkGreen; break;
            }
            // Esto para evitar la putisima animacion de la barra de progreso en WinForms
            pgbSeguridad.SetProgressNoAnimation(Seguridad);
        }

        // Si cliquea el boton cancelar, se cancela LMAOOoo
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Aquí el evento por si el boton OK se cliquea
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Si hay whitespace en la contraseña entonces vamos a MATAR al usuario
            if (txtContrasena.Text.Any(char.IsWhiteSpace))
            {
                // Mostrar un mensajito para que el usuario sepa porque hubo un error
                MessageBox.Show("La contraseña no puede tener espacios!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si las contraseñas en el campo contraseña y el campo confirmar contraseña
            // no son iguales, entonces se mandará un error
            else if (txtContrasena.Text != txtConfirmar.Text)
            {
                // Mostrar un mensajito para que el usuario sepa porque hubo un error
                MessageBox.Show("Las dos contraseñas no coinciden!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si el campo txtContrasena esta vacío, mostrar otro error
            else if (txtContrasena.Text == string.Empty)
            {
                // Mostrar un mensajito para que el usuario sepa porque hubo un error
                MessageBox.Show("La contraseña no puede estar vacía!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si ninguna de las dos condiciones se cumplen, la contraseña es válida
            else
            {
                // Si la seguridad es baja se manda un mensaje para que el usuario confirme
                // si quiere usar esa contraseña, si la respuesta no es 'Si' se sale de la función
                if (Seguridad < 25 && MessageBox.Show("¿Está seguro de proceder con esta contraseña, francamente, insegura?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                // En el caso de llegar a esta parte de código, la contraseña es válida y todo
                // está bien
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Si el mouse está clickando btnMostrar, el PasswordChar va a ser nulo
        // es decir, el texto no se va a enmascarar
        private void btnMostrar_MouseDown(object sender, MouseEventArgs e)
        {
            txtContrasena.PasswordChar = '\0';
        }

        // Si el mouse no está clickando btnMostrar, el PasswordChar va a ser *
        // es decir, el texto se enmascara con asteriscos
        private void btnMostrar_MouseUp(object sender, MouseEventArgs e)
        {
            txtContrasena.PasswordChar = '*';
        }
    }
}
