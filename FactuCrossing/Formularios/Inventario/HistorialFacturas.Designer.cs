namespace FactuCrossing.Formularios.Inventario
{
    partial class HistorialFacturas
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
            Fac = new GroupBox();
            dgvFactuas = new DataGridView();
            txtBuscar = new TextBox();
            label1 = new Label();
            dgvFactura = new DataGridView();
            Factura = new GroupBox();
            btnAgregar = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            Fac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFactuas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactura).BeginInit();
            Factura.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Fac
            // 
            Fac.Controls.Add(dgvFactuas);
            Fac.Location = new Point(12, 10);
            Fac.Name = "Fac";
            Fac.Size = new Size(373, 444);
            Fac.TabIndex = 1;
            Fac.TabStop = false;
            Fac.Text = "Facturas";
            // 
            // dgvFactuas
            // 
            dgvFactuas.AllowUserToAddRows = false;
            dgvFactuas.AllowUserToDeleteRows = false;
            dgvFactuas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFactuas.Location = new Point(6, 22);
            dgvFactuas.Name = "dgvFactuas";
            dgvFactuas.ReadOnly = true;
            dgvFactuas.Size = new Size(361, 416);
            dgvFactuas.TabIndex = 0;
            dgvFactuas.CellDoubleClick += dgvFactuas_CellDoubleClick;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(111, 460);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(274, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(60, 463);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 3;
            label1.Text = "Buscar:";
            // 
            // dgvFactura
            // 
            dgvFactura.AllowUserToAddRows = false;
            dgvFactura.AllowUserToDeleteRows = false;
            dgvFactura.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFactura.Location = new Point(6, 22);
            dgvFactura.Name = "dgvFactura";
            dgvFactura.ReadOnly = true;
            dgvFactura.Size = new Size(434, 414);
            dgvFactura.TabIndex = 0;
            // 
            // Factura
            // 
            Factura.Controls.Add(dgvFactura);
            Factura.Location = new Point(391, 12);
            Factura.Name = "Factura";
            Factura.Size = new Size(446, 442);
            Factura.TabIndex = 4;
            Factura.TabStop = false;
            Factura.Text = "Información de Factura";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(83, 96, 171);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.Location = new Point(391, 463);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(446, 56);
            btnAgregar.TabIndex = 5;
            btnAgregar.Text = "Exportar...";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 530);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(850, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // HistorialFacturas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 552);
            Controls.Add(statusStrip1);
            Controls.Add(btnAgregar);
            Controls.Add(Factura);
            Controls.Add(label1);
            Controls.Add(txtBuscar);
            Controls.Add(Fac);
            Name = "HistorialFacturas";
            Text = "HistorialFacturas";
            Fac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFactuas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFactura).EndInit();
            Factura.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Fac;
        private DataGridView dgvFactuas;
        private TextBox txtBuscar;
        private Label label1;
        private DataGridView dgvFactura;
        private GroupBox Factura;
        private Button btnAgregar;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}