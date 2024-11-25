namespace FactuCrossing.Formularios.Utilidades
{
    partial class InputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblText = new Label();
            txtInput = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblText
            // 
            lblText.Location = new Point(27, 22);
            lblText.Name = "lblText";
            lblText.Size = new Size(371, 15);
            lblText.TabIndex = 0;
            lblText.Text = "label1";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(27, 49);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(371, 23);
            txtInput.TabIndex = 8;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(83, 96, 171);
            btnOK.Cursor = Cursors.Hand;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(234, 93);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(79, 50);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(83, 96, 171);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(319, 93);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(79, 50);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 161);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtInput);
            Controls.Add(lblText);
            Name = "InputForm";
            Icon = (Icon)Properties.Resources.AppIcon;
            ShowIcon = false;
            Text = "InputForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblText;
        private TextBox txtInput;
        private Button btnOK;
        private Button btnCancel;
    }
}