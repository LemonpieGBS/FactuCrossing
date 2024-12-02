namespace FactuCrossing.Formularios.Inventario
{
    partial class Descuentos
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            cmbTipoDescuento = new ComboBox();
            label3 = new Label();
            dtpFechaInicio = new DateTimePicker();
            label4 = new Label();
            dtpFechaFin = new DateTimePicker();
            label5 = new Label();
            txtPorcentaje = new TextBox();
            label6 = new Label();
            txtX = new TextBox();
            label7 = new Label();
            txtY = new TextBox();
            label8 = new Label();
            txtMaximoProductos = new TextBox();
            label9 = new Label();
            dgvDescuentos = new DataGridView();
            btnAgregar = new Button();
            groupBox1 = new GroupBox();
            cmbProductos = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvDescuentos).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(140, 14);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(233, 23);
            txtNombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 47);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 2;
            label2.Text = "Tipo Descuento:";
            // 
            // cmbTipoDescuento
            // 
            cmbTipoDescuento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoDescuento.FormattingEnabled = true;
            cmbTipoDescuento.Items.AddRange(new object[] { "Porcentaje", "X por Y" });
            cmbTipoDescuento.Location = new Point(140, 44);
            cmbTipoDescuento.Margin = new Padding(4, 3, 4, 3);
            cmbTipoDescuento.Name = "cmbTipoDescuento";
            cmbTipoDescuento.Size = new Size(233, 23);
            cmbTipoDescuento.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 78);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 4;
            label3.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(140, 75);
            dtpFechaInicio.Margin = new Padding(4, 3, 4, 3);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(233, 23);
            dtpFechaInicio.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 108);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 6;
            label4.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(140, 105);
            dtpFechaFin.Margin = new Padding(4, 3, 4, 3);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(233, 23);
            dtpFechaFin.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 138);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 8;
            label5.Text = "Porcentaje:";
            // 
            // txtPorcentaje
            // 
            txtPorcentaje.Location = new Point(140, 135);
            txtPorcentaje.Margin = new Padding(4, 3, 4, 3);
            txtPorcentaje.Name = "txtPorcentaje";
            txtPorcentaje.Size = new Size(233, 23);
            txtPorcentaje.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 168);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 10;
            label6.Text = "X:";
            // 
            // txtX
            // 
            txtX.Location = new Point(140, 165);
            txtX.Margin = new Padding(4, 3, 4, 3);
            txtX.Name = "txtX";
            txtX.Size = new Size(233, 23);
            txtX.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 198);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(17, 15);
            label7.TabIndex = 12;
            label7.Text = "Y:";
            // 
            // txtY
            // 
            txtY.Location = new Point(140, 195);
            txtY.Margin = new Padding(4, 3, 4, 3);
            txtY.Name = "txtY";
            txtY.Size = new Size(233, 23);
            txtY.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 228);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(111, 15);
            label8.TabIndex = 14;
            label8.Text = "Máximo Productos:";
            // 
            // txtMaximoProductos
            // 
            txtMaximoProductos.Location = new Point(140, 225);
            txtMaximoProductos.Margin = new Padding(4, 3, 4, 3);
            txtMaximoProductos.Name = "txtMaximoProductos";
            txtMaximoProductos.Size = new Size(233, 23);
            txtMaximoProductos.TabIndex = 15;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(14, 258);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(121, 15);
            label9.TabIndex = 16;
            label9.Text = "Productos Aplicables:";
            // 
            // dgvDescuentos
            // 
            dgvDescuentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDescuentos.Location = new Point(6, 22);
            dgvDescuentos.Name = "dgvDescuentos";
            dgvDescuentos.Size = new Size(373, 286);
            dgvDescuentos.TabIndex = 19;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(140, 284);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(233, 44);
            btnAgregar.TabIndex = 21;
            btnAgregar.Text = "Guardar";
            btnAgregar.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvDescuentos);
            groupBox1.Location = new Point(380, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(385, 314);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "Descuentos Existentes";
            // 
            // cmbProductos
            // 
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(140, 254);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(233, 23);
            cmbProductos.TabIndex = 23;
            // 
            // Descuentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(777, 354);
            Controls.Add(cmbProductos);
            Controls.Add(groupBox1);
            Controls.Add(btnAgregar);
            Controls.Add(label9);
            Controls.Add(txtMaximoProductos);
            Controls.Add(label8);
            Controls.Add(txtY);
            Controls.Add(label7);
            Controls.Add(txtX);
            Controls.Add(label6);
            Controls.Add(txtPorcentaje);
            Controls.Add(label5);
            Controls.Add(dtpFechaFin);
            Controls.Add(label4);
            Controls.Add(dtpFechaInicio);
            Controls.Add(label3);
            Controls.Add(cmbTipoDescuento);
            Controls.Add(label2);
            Controls.Add(txtNombre);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Descuentos";
            Text = "Crear Descuento";
            ((System.ComponentModel.ISupportInitialize)dgvDescuentos).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTipoDescuento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaximoProductos;
        private System.Windows.Forms.Label label9;
        private DataGridView dgvDescuentos;
        private Button btnAgregar;
        private GroupBox groupBox1;
        private ComboBox cmbProductos;
    }
}