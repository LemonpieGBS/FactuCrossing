﻿namespace FactuCrossing.Formularios.Administrador
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
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox2 = new GroupBox();
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            groupBox3 = new GroupBox();
            label4 = new Label();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnResetear = new Button();
            btnAdministrar = new Button();
            btnGenerar = new Button();
            pictureBox1 = new PictureBox();
            dateTimePicker1 = new DateTimePicker();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(367, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(421, 553);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Lista de Accesos";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(409, 525);
            dataGridView1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnGenerar);
            groupBox2.Controls.Add(btnAdministrar);
            groupBox2.Controls.Add(btnResetear);
            groupBox2.Location = new Point(12, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(349, 202);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Opciones de Administrador";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(83, 96, 171);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(12, 255);
            button1.Name = "button1";
            button1.Size = new Size(37, 40);
            button1.TabIndex = 7;
            button1.Text = "<";
            button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(55, 246);
            label1.Name = "label1";
            label1.Size = new Size(263, 40);
            label1.TabIndex = 8;
            label1.Text = "Noviembre 2024";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(83, 96, 171);
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(324, 255);
            button2.Name = "button2";
            button2.Size = new Size(37, 40);
            button2.TabIndex = 9;
            button2.Text = ">";
            button2.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label2);
            groupBox3.Location = new Point(12, 311);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(349, 136);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Datos del Mes";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(188, 58);
            label4.Name = "label4";
            label4.Size = new Size(144, 48);
            label4.TabIndex = 5;
            label4.Text = "12";
            label4.TextAlign = ContentAlignment.MiddleLeft;
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
            // label3
            // 
            label3.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(23, 58);
            label3.Name = "label3";
            label3.Size = new Size(175, 48);
            label3.TabIndex = 3;
            label3.Text = "65";
            label3.TextAlign = ContentAlignment.MiddleLeft;
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
            // btnResetear
            // 
            btnResetear.BackColor = Color.FromArgb(83, 96, 171);
            btnResetear.Cursor = Cursors.Hand;
            btnResetear.FlatStyle = FlatStyle.Flat;
            btnResetear.ForeColor = Color.White;
            btnResetear.Location = new Point(26, 39);
            btnResetear.Name = "btnResetear";
            btnResetear.Size = new Size(296, 40);
            btnResetear.TabIndex = 11;
            btnResetear.Text = "Resetear Contraseña(s)...";
            btnResetear.UseVisualStyleBackColor = false;
            btnResetear.Click += btnResetear_Click;
            // 
            // btnAdministrar
            // 
            btnAdministrar.BackColor = Color.FromArgb(83, 96, 171);
            btnAdministrar.Cursor = Cursors.Hand;
            btnAdministrar.FlatStyle = FlatStyle.Flat;
            btnAdministrar.ForeColor = Color.White;
            btnAdministrar.Location = new Point(26, 85);
            btnAdministrar.Name = "btnAdministrar";
            btnAdministrar.Size = new Size(296, 40);
            btnAdministrar.TabIndex = 12;
            btnAdministrar.Text = "Administrar Personal...";
            btnAdministrar.UseVisualStyleBackColor = false;
            btnAdministrar.Click += btnAdministrar_Click;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.FromArgb(83, 96, 171);
            btnGenerar.Cursor = Cursors.Hand;
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.ForeColor = Color.White;
            btnGenerar.Location = new Point(26, 131);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(296, 40);
            btnGenerar.TabIndex = 13;
            btnGenerar.Text = "Generar Reporte...";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 470);
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
            // MenuAdministrador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 589);
            Controls.Add(pictureBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(groupBox3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuAdministrador";
            Text = "Menu Administrador";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private GroupBox groupBox2;
        private Button button1;
        private Label label1;
        private Button button2;
        private Button btnGenerar;
        private Button btnAdministrar;
        private Button btnResetear;
        private GroupBox groupBox3;
        private Label label4;
        private Label label5;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private DateTimePicker dateTimePicker1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
}