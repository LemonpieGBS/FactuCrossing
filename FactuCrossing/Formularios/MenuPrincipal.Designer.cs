namespace FactuCrossing.Formularios
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            pictureBox1 = new PictureBox();
            btnCerrarSesión = new Button();
            lblHola = new Label();
            lblTiempo = new Label();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnAdministradores = new Button();
            btnReportes = new Button();
            btnInventario = new Button();
            btnFacturación = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(896, 500);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 129);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // btnCerrarSesión
            // 
            btnCerrarSesión.Anchor = AnchorStyles.None;
            btnCerrarSesión.BackColor = Color.FromArgb(83, 96, 171);
            btnCerrarSesión.Cursor = Cursors.Hand;
            btnCerrarSesión.FlatStyle = FlatStyle.Flat;
            btnCerrarSesión.ForeColor = Color.White;
            btnCerrarSesión.Location = new Point(503, 530);
            btnCerrarSesión.Name = "btnCerrarSesión";
            btnCerrarSesión.Size = new Size(142, 40);
            btnCerrarSesión.TabIndex = 2;
            btnCerrarSesión.Text = "Cerrar Sesión";
            btnCerrarSesión.UseVisualStyleBackColor = false;
            btnCerrarSesión.Click += btnCerrarSesión_Click;
            // 
            // lblHola
            // 
            lblHola.Anchor = AnchorStyles.None;
            lblHola.Font = new Font("Segoe UI Semibold", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblHola.Location = new Point(12, 9);
            lblHola.Name = "lblHola";
            lblHola.Size = new Size(1162, 86);
            lblHola.TabIndex = 3;
            lblHola.Text = "Hola, {Nombre}";
            lblHola.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTiempo
            // 
            lblTiempo.Anchor = AnchorStyles.None;
            lblTiempo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblTiempo.Location = new Point(12, 95);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(1162, 17);
            lblTiempo.TabIndex = 4;
            lblTiempo.Text = "Sesión Iniciada: {Fecha}";
            lblTiempo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.None;
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnAdministradores);
            groupBox1.Controls.Add(btnReportes);
            groupBox1.Controls.Add(btnInventario);
            groupBox1.Controls.Add(btnFacturación);
            groupBox1.Location = new Point(154, 143);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(844, 351);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Acciones";
            // 
            // label4
            // 
            label4.Location = new Point(621, 290);
            label4.Name = "label4";
            label4.Size = new Size(192, 28);
            label4.TabIndex = 7;
            label4.Text = "Menú de Administradores";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Location = new Point(423, 290);
            label3.Name = "label3";
            label3.Size = new Size(192, 28);
            label3.TabIndex = 6;
            label3.Text = "Generación de Reportes";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(225, 290);
            label2.Name = "label2";
            label2.Size = new Size(192, 28);
            label2.TabIndex = 5;
            label2.Text = "Control de Inventario";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Location = new Point(27, 290);
            label1.Name = "label1";
            label1.Size = new Size(192, 28);
            label1.TabIndex = 4;
            label1.Text = "Facturación";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAdministradores
            // 
            btnAdministradores.BackColor = Color.FromArgb(83, 96, 171);
            btnAdministradores.BackgroundImage = (Image)resources.GetObject("btnAdministradores.BackgroundImage");
            btnAdministradores.BackgroundImageLayout = ImageLayout.Center;
            btnAdministradores.Cursor = Cursors.Hand;
            btnAdministradores.FlatStyle = FlatStyle.Flat;
            btnAdministradores.ForeColor = Color.White;
            btnAdministradores.Location = new Point(621, 34);
            btnAdministradores.Name = "btnAdministradores";
            btnAdministradores.Size = new Size(192, 240);
            btnAdministradores.TabIndex = 3;
            btnAdministradores.TextImageRelation = TextImageRelation.ImageAboveText;
            btnAdministradores.UseVisualStyleBackColor = false;
            btnAdministradores.Click += btnAdministradores_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(83, 96, 171);
            btnReportes.BackgroundImage = (Image)resources.GetObject("btnReportes.BackgroundImage");
            btnReportes.BackgroundImageLayout = ImageLayout.Center;
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(423, 34);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(192, 240);
            btnReportes.TabIndex = 2;
            btnReportes.TextImageRelation = TextImageRelation.ImageAboveText;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.FromArgb(83, 96, 171);
            btnInventario.BackgroundImage = (Image)resources.GetObject("btnInventario.BackgroundImage");
            btnInventario.BackgroundImageLayout = ImageLayout.Center;
            btnInventario.Cursor = Cursors.Hand;
            btnInventario.FlatStyle = FlatStyle.Flat;
            btnInventario.ForeColor = Color.White;
            btnInventario.Location = new Point(225, 34);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(192, 240);
            btnInventario.TabIndex = 1;
            btnInventario.TextImageRelation = TextImageRelation.ImageAboveText;
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnFacturación
            // 
            btnFacturación.BackColor = Color.FromArgb(83, 96, 171);
            btnFacturación.BackgroundImage = (Image)resources.GetObject("btnFacturación.BackgroundImage");
            btnFacturación.BackgroundImageLayout = ImageLayout.Center;
            btnFacturación.Cursor = Cursors.Hand;
            btnFacturación.FlatStyle = FlatStyle.Flat;
            btnFacturación.ForeColor = Color.White;
            btnFacturación.Location = new Point(27, 34);
            btnFacturación.Name = "btnFacturación";
            btnFacturación.Size = new Size(192, 240);
            btnFacturación.TabIndex = 0;
            btnFacturación.TextImageRelation = TextImageRelation.ImageAboveText;
            btnFacturación.UseVisualStyleBackColor = false;
            btnFacturación.Click += btnFacturación_Click;
            // 
            // MenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1186, 609);
            Controls.Add(groupBox1);
            Controls.Add(lblTiempo);
            Controls.Add(lblHola);
            Controls.Add(btnCerrarSesión);
            Controls.Add(pictureBox1);
            Icon = Properties.Resources.AppIcon;
            Name = "MenuPrincipal";
            Text = "Menu Principal";
            TransparencyKey = Color.Fuchsia;
            Activated += MenuPrincipal_Activated;
            FormClosing += MenuPrincipal_FormClosing;
            Load += MenuPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnCerrarSesión;
        private Label lblHola;
        private Label lblTiempo;
        private GroupBox groupBox1;
        private Button btnFacturación;
        private Button btnAdministradores;
        private Button btnReportes;
        private Button btnInventario;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}