namespace FactuCrossing.Formularios.Administrador
{
    partial class MenuAdministrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuAdministrador));
            groupBox2 = new GroupBox();
            btnGenerar = new Button();
            btnAdministrar = new Button();
            btnResetear = new Button();
            lblMes = new Label();
            groupBox3 = new GroupBox();
            lblPersonal = new Label();
            label5 = new Label();
            lblAccesos = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            dateTimePicker1 = new DateTimePicker();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            btnDerecha = new Button();
            btnIzquierda = new Button();
            dgvAccesos = new DataGridView();
            groupBox4 = new GroupBox();
            label3 = new Label();
            label1 = new Label();
            radioButton7 = new RadioButton();
            dtpFecha2 = new DateTimePicker();
            dtpFecha1 = new DateTimePicker();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            dtpEspecifica = new DateTimePicker();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox5 = new GroupBox();
            cmbPersonal = new ComboBox();
            radioButton9 = new RadioButton();
            radioButton8 = new RadioButton();
            groupBox6 = new GroupBox();
            radioButton10 = new RadioButton();
            radioButton11 = new RadioButton();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAccesos).BeginInit();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnGenerar);
            groupBox2.Controls.Add(btnAdministrar);
            groupBox2.Controls.Add(btnResetear);
            groupBox2.Location = new Point(12, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(349, 244);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Opciones de Administrador";
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.FromArgb(83, 96, 171);
            btnGenerar.Cursor = Cursors.Hand;
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.ForeColor = Color.White;
            btnGenerar.Location = new Point(22, 153);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(296, 40);
            btnGenerar.TabIndex = 13;
            btnGenerar.Text = "Generar Reporte...";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnAdministrar
            // 
            btnAdministrar.BackColor = Color.FromArgb(83, 96, 171);
            btnAdministrar.Cursor = Cursors.Hand;
            btnAdministrar.FlatStyle = FlatStyle.Flat;
            btnAdministrar.ForeColor = Color.White;
            btnAdministrar.Location = new Point(22, 107);
            btnAdministrar.Name = "btnAdministrar";
            btnAdministrar.Size = new Size(296, 40);
            btnAdministrar.TabIndex = 12;
            btnAdministrar.Text = "Administrar Personal...";
            btnAdministrar.UseVisualStyleBackColor = false;
            btnAdministrar.Click += btnAdministrar_Click;
            // 
            // btnResetear
            // 
            btnResetear.BackColor = Color.FromArgb(83, 96, 171);
            btnResetear.Cursor = Cursors.Hand;
            btnResetear.FlatStyle = FlatStyle.Flat;
            btnResetear.ForeColor = Color.White;
            btnResetear.Location = new Point(22, 61);
            btnResetear.Name = "btnResetear";
            btnResetear.Size = new Size(296, 40);
            btnResetear.TabIndex = 11;
            btnResetear.Text = "Resetear Contraseña(s)...";
            btnResetear.UseVisualStyleBackColor = false;
            btnResetear.Click += btnResetear_Click;
            // 
            // lblMes
            // 
            lblMes.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMes.Location = new Point(54, 273);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(263, 40);
            lblMes.TabIndex = 8;
            lblMes.Text = "Noviembre 2024";
            lblMes.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblPersonal);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(lblAccesos);
            groupBox3.Controls.Add(label2);
            groupBox3.Location = new Point(11, 330);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(349, 136);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Datos del Mes";
            // 
            // lblPersonal
            // 
            lblPersonal.Font = new Font("Segoe UI", 24F);
            lblPersonal.Location = new Point(188, 58);
            lblPersonal.Name = "lblPersonal";
            lblPersonal.Size = new Size(144, 48);
            lblPersonal.TabIndex = 5;
            lblPersonal.Text = "12";
            lblPersonal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Location = new Point(188, 31);
            label5.Name = "label5";
            label5.Size = new Size(115, 27);
            label5.TabIndex = 4;
            label5.Text = "Empleados";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAccesos
            // 
            lblAccesos.Font = new Font("Segoe UI", 27.75F);
            lblAccesos.Location = new Point(23, 58);
            lblAccesos.Name = "lblAccesos";
            lblAccesos.Size = new Size(175, 48);
            lblAccesos.TabIndex = 3;
            lblAccesos.Text = "65";
            lblAccesos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(23, 31);
            label2.Name = "label2";
            label2.Size = new Size(115, 27);
            label2.TabIndex = 2;
            label2.Text = "Accesos";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 461);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 129);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Location = new Point(-568, 150);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(279, 23);
            dateTimePicker1.TabIndex = 26;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(-568, 125);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(88, 19);
            radioButton2.TabIndex = 25;
            radioButton2.Text = "Seleccionar:";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(-568, 100);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(136, 19);
            radioButton1.TabIndex = 24;
            radioButton1.TabStop = true;
            radioButton1.Text = "Fecha Actual: {fecha}";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnDerecha
            // 
            btnDerecha.BackColor = Color.FromArgb(83, 96, 171);
            btnDerecha.Cursor = Cursors.Hand;
            btnDerecha.FlatStyle = FlatStyle.Flat;
            btnDerecha.ForeColor = Color.White;
            btnDerecha.Location = new Point(323, 274);
            btnDerecha.Name = "btnDerecha";
            btnDerecha.Size = new Size(37, 40);
            btnDerecha.TabIndex = 9;
            btnDerecha.Text = ">";
            btnDerecha.UseVisualStyleBackColor = false;
            btnDerecha.Click += btnDerecha_Click;
            // 
            // btnIzquierda
            // 
            btnIzquierda.BackColor = Color.FromArgb(83, 96, 171);
            btnIzquierda.Cursor = Cursors.Hand;
            btnIzquierda.FlatStyle = FlatStyle.Flat;
            btnIzquierda.ForeColor = Color.White;
            btnIzquierda.Location = new Point(11, 274);
            btnIzquierda.Name = "btnIzquierda";
            btnIzquierda.Size = new Size(37, 40);
            btnIzquierda.TabIndex = 7;
            btnIzquierda.Text = "<";
            btnIzquierda.UseVisualStyleBackColor = false;
            btnIzquierda.Click += btnIzquierda_Click;
            // 
            // dgvAccesos
            // 
            dgvAccesos.AllowUserToAddRows = false;
            dgvAccesos.AllowUserToDeleteRows = false;
            dgvAccesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccesos.Location = new Point(6, 22);
            dgvAccesos.Name = "dgvAccesos";
            dgvAccesos.ReadOnly = true;
            dgvAccesos.Size = new Size(469, 414);
            dgvAccesos.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(radioButton7);
            groupBox4.Controls.Add(dtpFecha2);
            groupBox4.Controls.Add(dtpFecha1);
            groupBox4.Controls.Add(radioButton6);
            groupBox4.Controls.Add(radioButton5);
            groupBox4.Controls.Add(dtpEspecifica);
            groupBox4.Controls.Add(radioButton4);
            groupBox4.Controls.Add(radioButton3);
            groupBox4.Location = new Point(854, 24);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(218, 255);
            groupBox4.TabIndex = 30;
            groupBox4.TabStop = false;
            groupBox4.Text = "Opciones de Muestreo";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 186);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 9;
            label3.Text = "A:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 157);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 8;
            label1.Text = "De:";
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(15, 212);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(73, 19);
            radioButton7.TabIndex = 7;
            radioButton7.Text = "Histórico";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton7_CheckedChanged;
            // 
            // dtpFecha2
            // 
            dtpFecha2.Enabled = false;
            dtpFecha2.Location = new Point(48, 180);
            dtpFecha2.Name = "dtpFecha2";
            dtpFecha2.Size = new Size(153, 23);
            dtpFecha2.TabIndex = 6;
            dtpFecha2.ValueChanged += dtpFecha2_ValueChanged;
            // 
            // dtpFecha1
            // 
            dtpFecha1.Enabled = false;
            dtpFecha1.Location = new Point(48, 151);
            dtpFecha1.Name = "dtpFecha1";
            dtpFecha1.Size = new Size(153, 23);
            dtpFecha1.TabIndex = 5;
            dtpFecha1.ValueChanged += dtpFecha1_ValueChanged;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(18, 126);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(114, 19);
            radioButton6.TabIndex = 4;
            radioButton6.Text = "Rango de Fechas";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(18, 72);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(111, 19);
            radioButton5.TabIndex = 3;
            radioButton5.Text = "Fecha Específica";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged_1;
            // 
            // dtpEspecifica
            // 
            dtpEspecifica.Enabled = false;
            dtpEspecifica.Location = new Point(18, 97);
            dtpEspecifica.Name = "dtpEspecifica";
            dtpEspecifica.Size = new Size(183, 23);
            dtpEspecifica.TabIndex = 2;
            dtpEspecifica.ValueChanged += dtpEspecifica_ValueChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Checked = true;
            radioButton4.Location = new Point(18, 22);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(47, 19);
            radioButton4.TabIndex = 1;
            radioButton4.TabStop = true;
            radioButton4.Text = "Hoy";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(18, 47);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(120, 19);
            radioButton3.TabIndex = 0;
            radioButton3.Text = "Mes Seleccionado";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvAccesos);
            groupBox1.Location = new Point(367, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(481, 442);
            groupBox1.TabIndex = 28;
            groupBox1.TabStop = false;
            groupBox1.Text = "Logs";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(cmbPersonal);
            groupBox5.Controls.Add(radioButton9);
            groupBox5.Controls.Add(radioButton8);
            groupBox5.Location = new Point(367, 466);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(481, 65);
            groupBox5.TabIndex = 33;
            groupBox5.TabStop = false;
            groupBox5.Text = "Personal";
            // 
            // cmbPersonal
            // 
            cmbPersonal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPersonal.FormattingEnabled = true;
            cmbPersonal.Location = new Point(318, 25);
            cmbPersonal.Name = "cmbPersonal";
            cmbPersonal.Size = new Size(157, 23);
            cmbPersonal.TabIndex = 2;
            cmbPersonal.SelectedIndexChanged += cmbPersonal_SelectedIndexChanged;
            // 
            // radioButton9
            // 
            radioButton9.AutoSize = true;
            radioButton9.Location = new Point(234, 26);
            radioButton9.Name = "radioButton9";
            radioButton9.Size = new Size(81, 19);
            radioButton9.TabIndex = 1;
            radioButton9.Text = "Empleado:";
            radioButton9.UseVisualStyleBackColor = true;
            radioButton9.CheckedChanged += radioButton9_CheckedChanged;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Checked = true;
            radioButton8.Location = new Point(18, 26);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(111, 19);
            radioButton8.TabIndex = 0;
            radioButton8.TabStop = true;
            radioButton8.Text = "Todo el Personal";
            radioButton8.UseVisualStyleBackColor = true;
            radioButton8.CheckedChanged += radioButton8_CheckedChanged;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(radioButton10);
            groupBox6.Controls.Add(radioButton11);
            groupBox6.Location = new Point(854, 285);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(218, 84);
            groupBox6.TabIndex = 34;
            groupBox6.TabStop = false;
            groupBox6.Text = "Orden";
            // 
            // radioButton10
            // 
            radioButton10.AutoSize = true;
            radioButton10.Location = new Point(18, 51);
            radioButton10.Name = "radioButton10";
            radioButton10.Size = new Size(91, 19);
            radioButton10.TabIndex = 1;
            radioButton10.Text = "Cronológico";
            radioButton10.UseVisualStyleBackColor = true;
            radioButton10.CheckedChanged += radioButton10_CheckedChanged;
            // 
            // radioButton11
            // 
            radioButton11.AutoSize = true;
            radioButton11.Checked = true;
            radioButton11.Location = new Point(18, 26);
            radioButton11.Name = "radioButton11";
            radioButton11.Size = new Size(74, 19);
            radioButton11.TabIndex = 0;
            radioButton11.TabStop = true;
            radioButton11.Text = "Resumen";
            radioButton11.UseVisualStyleBackColor = true;
            radioButton11.CheckedChanged += radioButton11_CheckedChanged;
            // 
            // MenuAdministrador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 570);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(groupBox3);
            Controls.Add(btnDerecha);
            Controls.Add(lblMes);
            Controls.Add(btnIzquierda);
            Controls.Add(groupBox2);
            Controls.Add(pictureBox1);
            Icon = Properties.Resources.AppIcon;
            Name = "MenuAdministrador";
            Text = "Menu Administrador";
            Load += MenuAdministrador_Load;
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAccesos).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox2;
        private Label lblMes;
        private Button btnGenerar;
        private Button btnAdministrar;
        private Button btnResetear;
        private GroupBox groupBox3;
        private Label lblPersonal;
        private Label label5;
        private Label lblAccesos;
        private Label label2;
        private PictureBox pictureBox1;
        private DateTimePicker dateTimePicker1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button btnDerecha;
        private Button btnIzquierda;
        private DataGridView dgvAccesos;
        private GroupBox groupBox4;
        private Label label3;
        private Label label1;
        private RadioButton radioButton7;
        private DateTimePicker dtpFecha2;
        private DateTimePicker dtpFecha1;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private DateTimePicker dtpEspecifica;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private GroupBox groupBox1;
        private GroupBox groupBox5;
        private ComboBox cmbPersonal;
        private RadioButton radioButton9;
        private RadioButton radioButton8;
        private GroupBox groupBox6;
        private RadioButton radioButton10;
        private RadioButton radioButton11;
    }
}