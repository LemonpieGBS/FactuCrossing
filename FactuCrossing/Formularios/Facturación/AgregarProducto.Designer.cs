namespace FactuCrossing.Formularios.Facturación
{
    partial class AgregarProducto
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
            lblProveedor = new Label();
            label4 = new Label();
            groupBox2 = new GroupBox();
            dgvInventario = new DataGridView();
            nudCantidad = new NumericUpDown();
            label9 = new Label();
            btnAgregar = new Button();
            btnCancelar = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
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
            lblNombre.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblNombre.Location = new Point(24, 51);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(116, 32);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "{Nombre}";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblStock);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(lblPrecio);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(lblProveedor);
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
            lblStock.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
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
            lblPrecio.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
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
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            lblProveedor.Location = new Point(24, 120);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(137, 32);
            lblProveedor.TabIndex = 3;
            lblProveedor.Text = "{Proveedor}";
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
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvInventario);
            groupBox2.Location = new Point(388, 13);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(367, 431);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Inventario";
            // 
            // dgvInventario
            // 
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventario.Location = new Point(6, 21);
            dgvInventario.Name = "dgvInventario";
            dgvInventario.ReadOnly = true;
            dgvInventario.Size = new Size(355, 404);
            dgvInventario.TabIndex = 0;
            dgvInventario.CellClick += dgvInventario_CellClick;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(15, 421);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(367, 23);
            nudCantidad.TabIndex = 9;
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
            btnAgregar.Location = new Point(566, 479);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(189, 44);
            btnAgregar.TabIndex = 20;
            btnAgregar.Text = "Agregar";
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
            btnCancelar.Size = new Size(189, 44);
            btnCancelar.TabIndex = 21;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // AgregarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 535);
            Controls.Add(btnCancelar);
            Controls.Add(btnAgregar);
            Controls.Add(label9);
            Controls.Add(nudCantidad);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "AgregarProducto";
            Text = "Agregar Producto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventario).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
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
        private Label lblProveedor;
        private Label label4;
        private GroupBox groupBox2;
        private DataGridView dgvInventario;
        private NumericUpDown nudCantidad;
        private Label label9;
        private Button btnAgregar;
        private Button btnCancelar;
    }
}