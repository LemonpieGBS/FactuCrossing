namespace FactuCrossing.Formularios.Inventario
{
    partial class Inventario
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
            groupBox1 = new GroupBox();
            dgvInventario = new DataGridView();
            groupBox2 = new GroupBox();
            txtProveedor = new ComboBox();
            label4 = new Label();
            txtPrecio = new TextBox();
            rtxtDescripcion = new RichTextBox();
            label3 = new Label();
            label2 = new Label();
            nudStock = new NumericUpDown();
            txtNombre = new TextBox();
            label8 = new Label();
            label1 = new Label();
            label6 = new Label();
            btnAgregar = new Button();
            btnEditar = new Button();
            btnDescontinuar = new Button();
            strSeleccion = new StatusStrip();
            strLabel = new ToolStripStatusLabel();
            chbDescontinuado = new CheckBox();
            btnDescuentos = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            strSeleccion.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvInventario);
            groupBox1.Location = new Point(321, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(617, 503);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inventario";
            // 
            // dgvInventario
            // 
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventario.Location = new Point(6, 22);
            dgvInventario.Name = "dgvInventario";
            dgvInventario.ReadOnly = true;
            dgvInventario.Size = new Size(605, 475);
            dgvInventario.TabIndex = 0;
            dgvInventario.CellDoubleClick += dgvInventario_CellDoubleClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtProveedor);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(txtPrecio);
            groupBox2.Controls.Add(rtxtDescripcion);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(nudStock);
            groupBox2.Controls.Add(txtNombre);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(303, 391);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Información del Producto";
            // 
            // txtProveedor
            // 
            txtProveedor.FormattingEnabled = true;
            txtProveedor.Location = new Point(15, 112);
            txtProveedor.Name = "txtProveedor";
            txtProveedor.Size = new Size(271, 23);
            txtProveedor.TabIndex = 21;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.Window;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(266, 169);
            label4.Name = "label4";
            label4.Size = new Size(20, 23);
            label4.TabIndex = 20;
            label4.Text = "$";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(15, 169);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(245, 23);
            txtPrecio.TabIndex = 19;
            // 
            // rtxtDescripcion
            // 
            rtxtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            rtxtDescripcion.Location = new Point(15, 282);
            rtxtDescripcion.Name = "rtxtDescripcion";
            rtxtDescripcion.Size = new Size(271, 90);
            rtxtDescripcion.TabIndex = 18;
            rtxtDescripcion.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 264);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 17;
            label3.Text = "Descripción:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 94);
            label2.Name = "label2";
            label2.Size = new Size(135, 15);
            label2.TabIndex = 15;
            label2.Text = "Proveedor del Producto:";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(15, 224);
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(271, 23);
            nudStock.TabIndex = 14;
            nudStock.ThousandsSeparator = true;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(15, 53);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(271, 23);
            txtNombre.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 206);
            label8.Name = "label8";
            label8.Size = new Size(106, 15);
            label8.TabIndex = 10;
            label8.Text = "Cantidad en Stock:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 35);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 7;
            label1.Text = "Nombre del Producto:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 151);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 9;
            label6.Text = "Precio:";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(12, 421);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(303, 44);
            btnAgregar.TabIndex = 23;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.FromArgb(83, 96, 171);
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(12, 471);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(303, 44);
            btnEditar.TabIndex = 24;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnDescontinuar
            // 
            btnDescontinuar.BackColor = Color.FromArgb(83, 96, 171);
            btnDescontinuar.Cursor = Cursors.Hand;
            btnDescontinuar.FlatStyle = FlatStyle.Flat;
            btnDescontinuar.ForeColor = Color.White;
            btnDescontinuar.Location = new Point(12, 521);
            btnDescontinuar.Name = "btnDescontinuar";
            btnDescontinuar.Size = new Size(303, 44);
            btnDescontinuar.TabIndex = 25;
            btnDescontinuar.Text = "Descontinuar / Recontinuar";
            btnDescontinuar.UseVisualStyleBackColor = false;
            btnDescontinuar.Click += btnDescontinuar_Click;
            // 
            // strSeleccion
            // 
            strSeleccion.Items.AddRange(new ToolStripItem[] { strLabel });
            strSeleccion.Location = new Point(0, 572);
            strSeleccion.Name = "strSeleccion";
            strSeleccion.Size = new Size(950, 22);
            strSeleccion.TabIndex = 26;
            strSeleccion.Text = "statusStrip1";
            // 
            // strLabel
            // 
            strLabel.Name = "strLabel";
            strLabel.Size = new Size(118, 17);
            strLabel.Text = "toolStripStatusLabel1";
            // 
            // chbDescontinuado
            // 
            chbDescontinuado.AutoSize = true;
            chbDescontinuado.Location = new Point(321, 521);
            chbDescontinuado.Name = "chbDescontinuado";
            chbDescontinuado.Size = new Size(155, 19);
            chbDescontinuado.TabIndex = 27;
            chbDescontinuado.Text = "Mostrar Descontinuados";
            chbDescontinuado.UseVisualStyleBackColor = true;
            chbDescontinuado.CheckedChanged += chbDescontinuado_CheckedChanged;
            // 
            // btnDescuentos
            // 
            btnDescuentos.BackColor = Color.FromArgb(83, 96, 171);
            btnDescuentos.Cursor = Cursors.Hand;
            btnDescuentos.FlatStyle = FlatStyle.Flat;
            btnDescuentos.ForeColor = Color.White;
            btnDescuentos.Location = new Point(770, 521);
            btnDescuentos.Name = "btnDescuentos";
            btnDescuentos.Size = new Size(162, 44);
            btnDescuentos.TabIndex = 28;
            btnDescuentos.Text = "Menu de Descuentos...";
            btnDescuentos.UseVisualStyleBackColor = false;
            btnDescuentos.Click += btnDescuentos_Click;
            // 
            // Inventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 594);
            Controls.Add(btnDescuentos);
            Controls.Add(btnDescontinuar);
            Controls.Add(chbDescontinuado);
            Controls.Add(strSeleccion);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = Properties.Resources.AppIcon;
            Name = "Inventario";
            Text = "Inventario";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventario).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            strSeleccion.ResumeLayout(false);
            strSeleccion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dgvInventario;
        private GroupBox groupBox2;
        private NumericUpDown nudStock;
        private TextBox txtNombre;
        private Label label8;
        private Label label1;
        private Label label6;
        private Button btnAgregar;
        private Button btnEditar;
        private Button btnDescontinuar;
        private Label label3;
        private Label label2;
        private RichTextBox rtxtDescripcion;
        private TextBox txtPrecio;
        private Label label4;
        private StatusStrip strSeleccion;
        private ComboBox txtProveedor;
        private ToolStripStatusLabel strLabel;
        private CheckBox chbDescontinuado;
        private Button btnDescuentos;
    }
}