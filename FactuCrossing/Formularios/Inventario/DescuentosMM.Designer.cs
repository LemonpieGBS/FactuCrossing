namespace FactuCrossing.Formularios.Inventario
{
    partial class DescuentosMM
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
            btnAgregar = new Button();
            cmbProductos = new ComboBox();
            groupBox1 = new GroupBox();
            chkPermanente = new CheckBox();
            dtpFin = new DateTimePicker();
            label6 = new Label();
            dtpInicio = new DateTimePicker();
            label5 = new Label();
            label4 = new Label();
            nudPorcentaje = new NumericUpDown();
            label3 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            label1 = new Label();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            dgvDescuentos = new DataGridView();
            Gb = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPorcentaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDescuentos).BeginInit();
            Gb.SuspendLayout();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(160, 394);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(189, 44);
            btnAgregar.TabIndex = 21;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // cmbProductos
            // 
            cmbProductos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductos.Enabled = false;
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(164, 187);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(153, 23);
            cmbProductos.TabIndex = 22;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkPermanente);
            groupBox1.Controls.Add(dtpFin);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(dtpInicio);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(nudPorcentaje);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(cmbProductos);
            groupBox1.Location = new Point(12, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(337, 375);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Descuento";
            // 
            // chkPermanente
            // 
            chkPermanente.AutoSize = true;
            chkPermanente.Location = new Point(29, 341);
            chkPermanente.Name = "chkPermanente";
            chkPermanente.Size = new Size(90, 19);
            chkPermanente.TabIndex = 35;
            chkPermanente.Text = "Permanente";
            chkPermanente.UseVisualStyleBackColor = true;
            chkPermanente.CheckedChanged += chkPermanente_CheckedChanged;
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(29, 303);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(288, 23);
            dtpFin.TabIndex = 34;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(29, 285);
            label6.Name = "label6";
            label6.Size = new Size(66, 15);
            label6.TabIndex = 33;
            label6.Text = "Fecha Final";
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(29, 248);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(288, 23);
            dtpInicio.TabIndex = 32;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 230);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 31;
            label5.Text = "Fecha de Inicio";
            // 
            // label4
            // 
            label4.BackColor = SystemColors.Window;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(297, 104);
            label4.Name = "label4";
            label4.Size = new Size(20, 23);
            label4.TabIndex = 30;
            label4.Text = "%";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nudPorcentaje
            // 
            nudPorcentaje.Location = new Point(29, 104);
            nudPorcentaje.Name = "nudPorcentaje";
            nudPorcentaje.Size = new Size(262, 23);
            nudPorcentaje.TabIndex = 29;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 86);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 28;
            label3.Text = "Porcentaje";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(29, 48);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(288, 23);
            txtNombre.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 30);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 26;
            label2.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 145);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 25;
            label1.Text = "Aplicabilidad";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(29, 188);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(129, 19);
            radioButton2.TabIndex = 24;
            radioButton2.Text = "Producto Aplicable:";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(29, 163);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(131, 19);
            radioButton1.TabIndex = 23;
            radioButton1.TabStop = true;
            radioButton1.Text = "Todos los Productos";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // dgvDescuentos
            // 
            dgvDescuentos.AllowUserToAddRows = false;
            dgvDescuentos.AllowUserToDeleteRows = false;
            dgvDescuentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDescuentos.Location = new Point(6, 22);
            dgvDescuentos.Name = "dgvDescuentos";
            dgvDescuentos.ReadOnly = true;
            dgvDescuentos.Size = new Size(396, 347);
            dgvDescuentos.TabIndex = 35;
            dgvDescuentos.CellDoubleClick += dgvDescuentos_CellDoubleClick;
            // 
            // Gb
            // 
            Gb.Controls.Add(dgvDescuentos);
            Gb.Location = new Point(366, 13);
            Gb.Name = "Gb";
            Gb.Size = new Size(408, 375);
            Gb.TabIndex = 36;
            Gb.TabStop = false;
            Gb.Text = "Descuentos";
            // 
            // DescuentosMM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 450);
            Controls.Add(Gb);
            Controls.Add(groupBox1);
            Controls.Add(btnAgregar);
            Name = "DescuentosMM";
            Text = "Menú Descuentos";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPorcentaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDescuentos).EndInit();
            Gb.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnAgregar;
        private ComboBox cmbProductos;
        private GroupBox groupBox1;
        private NumericUpDown nudPorcentaje;
        private Label label3;
        private TextBox txtNombre;
        private Label label2;
        private Label label1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private DateTimePicker dtpFin;
        private Label label6;
        private DateTimePicker dtpInicio;
        private Label label5;
        private Label label4;
        private DataGridView dgvDescuentos;
        private GroupBox Gb;
        private CheckBox chkPermanente;
    }
}