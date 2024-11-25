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
            label2 = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            label5 = new Label();
            label6 = new Label();
            label3 = new Label();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            label9 = new Label();
            btnAgregar = new Button();
            btnCancelar = new Button();
            button1 = new Button();
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(24, 51);
            label2.Name = "label2";
            label2.Size = new Size(212, 32);
            label2.TabIndex = 1;
            label2.Text = "{NombreProducto}";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(15, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(367, 368);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información del Producto";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(24, 257);
            label7.Name = "label7";
            label7.Size = new Size(189, 32);
            label7.TabIndex = 7;
            label7.Text = "{Stock} unidades";
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
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(24, 190);
            label5.Name = "label5";
            label5.Size = new Size(106, 32);
            label5.TabIndex = 5;
            label5.Text = "{Precio}$";
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(24, 120);
            label3.Name = "label3";
            label3.Size = new Size(105, 32);
            label3.TabIndex = 3;
            label3.Text = "{Código}";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 105);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 2;
            label4.Text = "Código:";
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
            // button1
            // 
            button1.BackColor = Color.FromArgb(83, 96, 171);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(257, 479);
            button1.Name = "button1";
            button1.Size = new Size(125, 44);
            button1.TabIndex = 22;
            button1.Text = "Aplicar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // EditarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 535);
            Controls.Add(button1);
            Controls.Add(btnCancelar);
            Controls.Add(btnAgregar);
            Controls.Add(label9);
            Controls.Add(numericUpDown1);
            Controls.Add(groupBox1);
            Name = "EditarProducto";
            Icon = (Icon)Properties.Resources.AppIcon;
            Text = "Editar Producto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Label label7;
        private Label label8;
        private Label label5;
        private Label label6;
        private Label label3;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private Label label9;
        private Button btnAgregar;
        private Button btnCancelar;
        private Button button1;
    }
}