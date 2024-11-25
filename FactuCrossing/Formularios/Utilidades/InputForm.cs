using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuCrossing.Formularios.Utilidades
{
    public partial class InputForm : Form
    {
        public string InputtedString = "";

        public InputForm(string _titulo, string _mensaje, string _default = "")
        {
            InitializeComponent();

            this.Text = _titulo;

            // Alinear al txtInput
            lblText.AutoSize = true;
            lblText.MaximumSize = new Size(txtInput.Width, 0);
            lblText.Location = new Point(txtInput.Location.X, lblText.Location.Y);

            int label_size_viejo = lblText.Height;
            lblText.Text = _mensaje;
            int label_size_actualizado = lblText.Height - label_size_viejo;

            txtInput.Location = new Point(txtInput.Location.X, txtInput.Location.Y + label_size_actualizado);
            btnOK.Location = new Point(btnOK.Location.X, btnOK.Location.Y + label_size_actualizado);
            btnCancel.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y + label_size_actualizado);

            txtInput.Text = _default;

            this.DialogResult = DialogResult.Cancel;
        }

        public TextBox getInputBox() { return txtInput; }
        public Button getOKButton() { return btnOK; }
        public Button getCancelButton() { return btnCancel; }
        public void setValidationRule(Func<string, bool> _newFunction) { validationFunction = _newFunction; }

        Func<string, bool> validationFunction = (string _input) => { return true; };

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(validationFunction(txtInput.Text))
            {
                InputtedString = txtInput.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
