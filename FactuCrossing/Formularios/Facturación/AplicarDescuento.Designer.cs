namespace FactuCrossing.Formularios.Facturación
{
    partial class AplicarDescuento
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
            label1 = new Label();
            cmbDescuentos = new ComboBox();
            btnAgregar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(141, 15);
            label1.TabIndex = 0;
            label1.Text = "Seleccione un descuento:";
            // 
            // cmbDescuentos
            // 
            cmbDescuentos.FormattingEnabled = true;
            cmbDescuentos.Location = new Point(12, 27);
            cmbDescuentos.Name = "cmbDescuentos";
            cmbDescuentos.Size = new Size(274, 23);
            cmbDescuentos.TabIndex = 1;
            cmbDescuentos.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(12, 56);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(274, 44);
            btnAgregar.TabIndex = 22;
            btnAgregar.Text = "Aplicar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // AplicarDescuento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 113);
            Controls.Add(btnAgregar);
            Controls.Add(cmbDescuentos);
            Controls.Add(label1);
            Name = "AplicarDescuento";
            Text = "Aplicar Descuento";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbDescuentos;
        private Button btnAgregar;
    }
}