namespace FactuCrossing.Formularios.Facturación
{
    partial class EditarProducto
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
            lblNombre = new Label();
            groupBox1 = new GroupBox();
            lblStock = new Label();
            label8 = new Label();
            lblPrecio = new Label();
            label6 = new Label();
            lblCodigo = new Label();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            label9 = new Label();
            btnAgregar = new Button();
            btnCancelar = new Button();
            btnAplicar = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 36);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre del Producto:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNombre.Location = new Point(24, 51);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(212, 32);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "{NombreProducto}";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblStock);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(lblPrecio);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(lblCodigo);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lblNombre);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(15, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(367, 368);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información del Producto";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStock.Location = new Point(24, 257);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(189, 32);
            lblStock.TabIndex = 7;
            lblStock.Text = "{Stock} unidades";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 242);
            label8.Name = "label8";
            label8.Size = new Size(103, 15);
            label8.TabIndex = 6;
            label8.Text = "Cantidad en Stock";
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrecio.Location = new Point(24, 190);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(106, 32);
            lblPrecio.TabIndex = 5;
            lblPrecio.Text = "{Precio}$";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 175);
            label6.Name = "label6";
            label6.Size = new Size(40, 15);
            label6.TabIndex = 4;
            label6.Text = "Precio";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCodigo.Location = new Point(24, 120);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(105, 32);
            lblCodigo.TabIndex = 3;
            lblCodigo.Text = "{Código}";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 105);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 2;
            label4.Text = "Proveedor:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(15, 421);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(367, 23);
            numericUpDown1.TabIndex = 9;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 403);
            label9.Name = "label9";
            label9.Size = new Size(110, 15);
            label9.TabIndex = 10;
            label9.Text = "Cantidad a Facturar";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(131, 479);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(120, 44);
            btnAgregar.TabIndex = 20;
            btnAgregar.Text = "Eliminar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(83, 96, 171);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(15, 479);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(110, 44);
            btnCancelar.TabIndex = 21;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = Color.FromArgb(83, 96, 171);
            btnAplicar.Cursor = Cursors.Hand;
            btnAplicar.FlatStyle = FlatStyle.Flat;
            btnAplicar.ForeColor = Color.White;
            btnAplicar.Location = new Point(257, 479);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(125, 44);
            btnAplicar.TabIndex = 22;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += button1_Click;
            // 
            // EditarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 535);
            Controls.Add(btnAplicar);
            Controls.Add(btnCancelar);
            Controls.Add(btnAgregar);
            Controls.Add(label9);
            Controls.Add(numericUpDown1);
            Controls.Add(groupBox1);
            Icon = Properties.Resources.AppIcon;
            Name = "EditarProducto";
            Text = "Editar Producto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblNombre;
        private GroupBox groupBox1;
        private Label lblStock;
        private Label label8;
        private Label lblPrecio;
        private Label label6;
        private Label lblCodigo;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private Label label9;
        private Button btnAgregar;
        private Button btnCancelar;
        private Button btnAplicar;
    }
}