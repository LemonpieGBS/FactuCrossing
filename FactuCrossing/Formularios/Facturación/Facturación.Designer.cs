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
            groupBox1 = new GroupBox();
            dgvFacturado = new DataGridView();
            btnAgregar = new Button();
            label1 = new Label();
            txtNombreUsuario = new TextBox();
            label2 = new Label();
            txtSede = new TextBox();
            groupBox2 = new GroupBox();
            lblFacturador = new Label();
            dtpFecha = new DateTimePicker();
            rdbSeleccionar = new RadioButton();
            rdbFechaActual = new RadioButton();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            lblTotal = new Label();
            lblDescuento = new Label();
            lblSubtotal = new Label();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacturado).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvFacturado);
            groupBox1.Location = new Point(26, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(501, 398);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos Facturados";
            // 
            // dgvFacturado
            // 
            dgvFacturado.AllowUserToAddRows = false;
            dgvFacturado.AllowUserToDeleteRows = false;
            dgvFacturado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturado.Location = new Point(6, 22);
            dgvFacturado.Name = "dgvFacturado";
            dgvFacturado.ReadOnly = true;
            dgvFacturado.Size = new Size(489, 370);
            dgvFacturado.TabIndex = 0;
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
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(19, 102);
            label2.Name = "label2";
            label2.Size = new Size(165, 18);
            label2.TabIndex = 11;
            label2.Text = "Sede/Local";
            // 
            // txtSede
            // 
            txtSede.Location = new Point(19, 123);
            txtSede.Name = "txtSede";
            txtSede.Size = new Size(279, 23);
            txtSede.TabIndex = 10;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblFacturador);
            groupBox2.Controls.Add(dtpFecha);
            groupBox2.Controls.Add(rdbSeleccionar);
            groupBox2.Controls.Add(rdbFechaActual);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtSede);
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
            lblFacturador.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblFacturador.Location = new Point(19, 289);
            lblFacturador.Name = "lblFacturador";
            lblFacturador.Size = new Size(291, 33);
            lblFacturador.TabIndex = 16;
            lblFacturador.Text = "Facturista: {Empleado}";
            // 
            // dtpFecha
            // 
            dtpFecha.Enabled = false;
            dtpFecha.Location = new Point(19, 235);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(279, 23);
            dtpFecha.TabIndex = 15;
            // 
            // rdbSeleccionar
            // 
            rdbSeleccionar.AutoSize = true;
            rdbSeleccionar.Location = new Point(19, 210);
            rdbSeleccionar.Name = "rdbSeleccionar";
            rdbSeleccionar.Size = new Size(88, 19);
            rdbSeleccionar.TabIndex = 14;
            rdbSeleccionar.Text = "Seleccionar:";
            rdbSeleccionar.UseVisualStyleBackColor = true;
            // 
            // rdbFechaActual
            // 
            rdbFechaActual.AutoSize = true;
            rdbFechaActual.Checked = true;
            rdbFechaActual.Location = new Point(19, 185);
            rdbFechaActual.Name = "rdbFechaActual";
            rdbFechaActual.Size = new Size(136, 19);
            rdbFechaActual.TabIndex = 13;
            rdbFechaActual.TabStop = true;
            rdbFechaActual.Text = "Fecha Actual: {fecha}";
            rdbFechaActual.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            // lblTotal
            // 
            lblTotal.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotal.Location = new Point(153, 63);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(202, 55);
            lblTotal.TabIndex = 18;
            lblTotal.Text = "Total: {Total}";
            lblTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDescuento
            // 
            lblDescuento.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblDescuento.ForeColor = Color.IndianRed;
            lblDescuento.Location = new Point(153, 45);
            lblDescuento.Name = "lblDescuento";
            lblDescuento.Size = new Size(202, 18);
            lblDescuento.TabIndex = 21;
            lblDescuento.Text = "Descuento: {Descuento}";
            lblDescuento.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtotal
            // 
            lblSubtotal.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtotal.Location = new Point(153, 27);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(202, 18);
            lblSubtotal.TabIndex = 17;
            lblSubtotal.Text = "Subtotal: {Subtotal}";
            lblSubtotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblDescuento);
            groupBox3.Controls.Add(lblTotal);
            groupBox3.Controls.Add(lblSubtotal);
            groupBox3.Location = new Point(26, 416);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(501, 120);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "Información de Pago";
            // 
            // Facturación
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 550);
            Controls.Add(btnAgregar);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Icon = Properties.Resources.AppIcon;
            Name = "Facturación";
            Text = "Facturación";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFacturado).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private DataGridView dgvFacturado;
        private Button btnAgregar;
        private Label label1;
        private TextBox txtNombreUsuario;
        private Label label2;
        private TextBox txtSede;
        private GroupBox groupBox2;
        private DateTimePicker dtpFecha;
        private RadioButton rdbSeleccionar;
        private RadioButton rdbFechaActual;
        private Label label3;
        private Label lblFacturador;
        private Button button1;
        private Button button2;
        private Button button4;
        private Label lblTotal;
        private Label lblDescuento;
        private Label lblSubtotal;
        private GroupBox groupBox3;
    }
}