namespace FactuCrossing.Formularios.Facturación
{
    partial class Facturación
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facturación));
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            btnAgregar = new Button();
            label1 = new Label();
            txtNombreUsuario = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            groupBox2 = new GroupBox();
            lblFacturador = new Label();
            dateTimePicker1 = new DateTimePicker();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            groupBox3 = new GroupBox();
            pictureBox1 = new PictureBox();
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
            groupBox1.Location = new Point(26, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(501, 522);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos Facturados";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(489, 494);
            dataGridView1.TabIndex = 0;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(533, 354);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(316, 56);
            btnAgregar.TabIndex = 3;
            btnAgregar.Text = "Agregar Producto...";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(19, 34);
            label1.Name = "label1";
            label1.Size = new Size(165, 18);
            label1.TabIndex = 9;
            label1.Text = "Nombre de la Factura";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(19, 55);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(279, 23);
            txtNombreUsuario.TabIndex = 8;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(19, 102);
            label2.Name = "label2";
            label2.Size = new Size(165, 18);
            label2.TabIndex = 11;
            label2.Text = "Sede/Local";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(19, 123);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(279, 23);
            textBox1.TabIndex = 10;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblFacturador);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(txtNombreUsuario);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(533, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(316, 336);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Información de Factura";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // lblFacturador
            // 
            lblFacturador.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFacturador.Location = new Point(19, 289);
            lblFacturador.Name = "lblFacturador";
            lblFacturador.Size = new Size(291, 33);
            lblFacturador.TabIndex = 16;
            lblFacturador.Text = "Facturista: {Empleado}";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Location = new Point(19, 235);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(279, 23);
            dateTimePicker1.TabIndex = 15;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(19, 210);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(88, 19);
            radioButton2.TabIndex = 14;
            radioButton2.Text = "Seleccionar:";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(19, 185);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(136, 19);
            radioButton1.TabIndex = 13;
            radioButton1.TabStop = true;
            radioButton1.Text = "Fecha Actual: {fecha}";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(19, 164);
            label3.Name = "label3";
            label3.Size = new Size(165, 18);
            label3.TabIndex = 12;
            label3.Text = "Fecha";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(83, 96, 171);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(694, 416);
            button1.Name = "button1";
            button1.Size = new Size(155, 56);
            button1.TabIndex = 13;
            button1.Text = "Aplicar Descuento";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(83, 96, 171);
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(533, 416);
            button2.Name = "button2";
            button2.Size = new Size(155, 56);
            button2.TabIndex = 14;
            button2.Text = "Editar/Eliminar...";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(83, 96, 171);
            button4.Cursor = Cursors.Hand;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Location = new Point(533, 478);
            button4.Name = "button4";
            button4.Size = new Size(316, 56);
            button4.TabIndex = 19;
            button4.Text = "Facturar";
            button4.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(153, 63);
            label6.Name = "label6";
            label6.Size = new Size(202, 55);
            label6.TabIndex = 18;
            label6.Text = "Total: {Total}";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.IndianRed;
            label4.Location = new Point(153, 45);
            label4.Name = "label4";
            label4.Size = new Size(202, 18);
            label4.TabIndex = 21;
            label4.Text = "Descuento: {Descuento}";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(153, 27);
            label5.Name = "label5";
            label5.Size = new Size(202, 18);
            label5.TabIndex = 17;
            label5.Text = "Subtotal: {Subtotal}";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(26, 540);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(501, 120);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "Información de Pago";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(586, 555);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 129);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // Facturación
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 673);
            Controls.Add(btnAgregar);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Facturación";
            Text = "Facturación";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button btnAgregar;
        private Label label1;
        private TextBox txtNombreUsuario;
        private Label label2;
        private TextBox textBox1;
        private GroupBox groupBox2;
        private DateTimePicker dateTimePicker1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label3;
        private Label lblFacturador;
        private Button button1;
        private Button button2;
        private Button button4;
        private Label label6;
        private Label label4;
        private Label label5;
        private GroupBox groupBox3;
        private PictureBox pictureBox1;
    }
}