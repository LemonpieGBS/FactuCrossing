namespace FactuCrossing.Formularios.Administrador
{
    partial class AdministrarPersonal
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
            dgvPersonal = new DataGridView();
            btnAgregar = new Button();
            button2 = new Button();
            btnDeshabilitar = new Button();
            groupBox2 = new GroupBox();
            cmbAcceso = new ComboBox();
            label2 = new Label();
            txtNombreUsuario = new TextBox();
            label1 = new Label();
            txtNombre = new TextBox();
            label4 = new Label();
            chbHabilitada = new CheckBox();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonal).BeginInit();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvPersonal);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(390, 407);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Lista de Personal";
            // 
            // dgvPersonal
            // 
            dgvPersonal.AllowUserToAddRows = false;
            dgvPersonal.AllowUserToDeleteRows = false;
            dgvPersonal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonal.Location = new Point(6, 19);
            dgvPersonal.Name = "dgvPersonal";
            dgvPersonal.ReadOnly = true;
            dgvPersonal.Size = new Size(378, 382);
            dgvPersonal.TabIndex = 0;
            dgvPersonal.CellDoubleClick += dgvPersonal_CellDoubleClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(408, 286);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(249, 40);
            btnAgregar.TabIndex = 14;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(83, 96, 171);
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(408, 332);
            button2.Name = "button2";
            button2.Size = new Size(249, 40);
            button2.TabIndex = 15;
            button2.Text = "Editar";
            button2.UseVisualStyleBackColor = false;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.BackColor = Color.FromArgb(83, 96, 171);
            btnDeshabilitar.Cursor = Cursors.Hand;
            btnDeshabilitar.FlatStyle = FlatStyle.Flat;
            btnDeshabilitar.ForeColor = Color.White;
            btnDeshabilitar.Location = new Point(408, 378);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(249, 40);
            btnDeshabilitar.TabIndex = 16;
            btnDeshabilitar.Text = "Habilitar/Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = false;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbAcceso);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtNombreUsuario);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(txtNombre);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(408, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(249, 268);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos de Personal";
            // 
            // cmbAcceso
            // 
            cmbAcceso.FlatStyle = FlatStyle.System;
            cmbAcceso.FormattingEnabled = true;
            cmbAcceso.Items.AddRange(new object[] { "Facturista", "Gestor de Inventario", "Analista", "Administrador", "Gerente" });
            cmbAcceso.Location = new Point(6, 170);
            cmbAcceso.Name = "cmbAcceso";
            cmbAcceso.Size = new Size(237, 23);
            cmbAcceso.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 152);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 22;
            label2.Text = "Nivel de Acceso:";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(6, 111);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(237, 23);
            txtNombreUsuario.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 18;
            label1.Text = "Nombre del Empleado";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(6, 52);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(237, 23);
            txtNombre.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 93);
            label4.Name = "label4";
            label4.Size = new Size(113, 15);
            label4.TabIndex = 19;
            label4.Text = "Nombre de Usuario:";
            // 
            // chbHabilitada
            // 
            chbHabilitada.AutoSize = true;
            chbHabilitada.Location = new Point(12, 446);
            chbHabilitada.Name = "chbHabilitada";
            chbHabilitada.Size = new Size(192, 19);
            chbHabilitada.TabIndex = 18;
            chbHabilitada.Text = "Mostrar Cuentas Deshabilitadas";
            chbHabilitada.UseVisualStyleBackColor = true;
            chbHabilitada.CheckedChanged += chbHabilitada_CheckedChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 476);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(672, 22);
            statusStrip1.TabIndex = 19;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(175, 17);
            statusLabel.Text = "Ningun empleado seleccionado";
            // 
            // AdministrarPersonal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 498);
            Controls.Add(statusStrip1);
            Controls.Add(chbHabilitada);
            Controls.Add(groupBox2);
            Controls.Add(btnDeshabilitar);
            Controls.Add(button2);
            Controls.Add(btnAgregar);
            Controls.Add(groupBox1);
            Name = "AdministrarPersonal";
            Text = "AdministrarPersonal";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPersonal).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dgvPersonal;
        private Button btnAgregar;
        private Button button2;
        private Button btnDeshabilitar;
        private GroupBox groupBox2;
        private TextBox txtNombreUsuario;
        private Label label1;
        private TextBox txtNombre;
        private Label label4;
        private ComboBox cmbAcceso;
        private Label label2;
        private CheckBox chbHabilitada;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
    }
}