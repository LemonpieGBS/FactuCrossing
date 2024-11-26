namespace FactuCrossing.Formularios.Utilidades
{
    partial class CreacionDeContraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreacionDeContraseña));
            btnCancel = new Button();
            btnOK = new Button();
            lblCreacion = new Label();
            lblNueva = new Label();
            groupBox1 = new GroupBox();
            btnMostrar = new Button();
            label3 = new Label();
            lblSeguridad = new Label();
            pgbSeguridad = new ProgressBar();
            txtConfirmar = new TextBox();
            label1 = new Label();
            txtContrasena = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(83, 96, 171);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(310, 443);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(79, 50);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(83, 96, 171);
            btnOK.Cursor = Cursors.Hand;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(225, 443);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(79, 50);
            btnOK.TabIndex = 11;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // lblCreacion
            // 
            lblCreacion.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblCreacion.Location = new Point(28, 26);
            lblCreacion.Name = "lblCreacion";
            lblCreacion.Size = new Size(361, 37);
            lblCreacion.TabIndex = 13;
            lblCreacion.Text = "Creación de Contraseña";
            lblCreacion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNueva
            // 
            lblNueva.AutoSize = true;
            lblNueva.Location = new Point(21, 35);
            lblNueva.Name = "lblNueva";
            lblNueva.Size = new Size(104, 15);
            lblNueva.TabIndex = 14;
            lblNueva.Text = "Nueva Contraseña";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnMostrar);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(lblSeguridad);
            groupBox1.Controls.Add(pgbSeguridad);
            groupBox1.Controls.Add(txtConfirmar);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtContrasena);
            groupBox1.Controls.Add(lblNueva);
            groupBox1.Location = new Point(28, 83);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(361, 354);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Input de Contraseña";
            // 
            // btnMostrar
            // 
            btnMostrar.BackColor = Color.FromArgb(83, 96, 171);
            btnMostrar.BackgroundImage = (Image)resources.GetObject("btnMostrar.BackgroundImage");
            btnMostrar.BackgroundImageLayout = ImageLayout.Zoom;
            btnMostrar.Cursor = Cursors.Hand;
            btnMostrar.FlatStyle = FlatStyle.Flat;
            btnMostrar.ForeColor = Color.White;
            btnMostrar.Location = new Point(311, 52);
            btnMostrar.Name = "btnMostrar";
            btnMostrar.Size = new Size(29, 24);
            btnMostrar.TabIndex = 16;
            btnMostrar.UseVisualStyleBackColor = false;
            btnMostrar.MouseDown += btnMostrar_MouseDown;
            btnMostrar.MouseUp += btnMostrar_MouseUp;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 134);
            label3.MaximumSize = new Size(319, 0);
            label3.Name = "label3";
            label3.Size = new Size(318, 60);
            label3.TabIndex = 20;
            label3.Text = resources.GetString("label3.Text");
            // 
            // lblSeguridad
            // 
            lblSeguridad.AutoSize = true;
            lblSeguridad.Location = new Point(21, 88);
            lblSeguridad.Name = "lblSeguridad";
            lblSeguridad.Size = new Size(153, 15);
            lblSeguridad.TabIndex = 19;
            lblSeguridad.Text = "Seguridad: No Determinada";
            // 
            // pgbSeguridad
            // 
            pgbSeguridad.ForeColor = Color.HotPink;
            pgbSeguridad.Location = new Point(21, 108);
            pgbSeguridad.MarqueeAnimationSpeed = 5;
            pgbSeguridad.Name = "pgbSeguridad";
            pgbSeguridad.Size = new Size(319, 23);
            pgbSeguridad.TabIndex = 18;
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(21, 276);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.Size = new Size(319, 23);
            txtConfirmar.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 258);
            label1.Name = "label1";
            label1.Size = new Size(124, 15);
            label1.TabIndex = 16;
            label1.Text = "Confirmar Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(21, 53);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(284, 23);
            txtContrasena.TabIndex = 15;
            txtContrasena.TextChanged += txtContrasena_TextChanged;
            // 
            // CreacionDeContraseña
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 505);
            Controls.Add(groupBox1);
            Controls.Add(lblCreacion);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Icon = Properties.Resources.AppIcon;
            Name = "CreacionDeContraseña";
            Text = "Creacion de Contraseña";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private Label lblCreacion;
        private Label lblNueva;
        private GroupBox groupBox1;
        private TextBox txtConfirmar;
        private Label label1;
        private TextBox txtContrasena;
        private Label label3;
        private Label lblSeguridad;
        private ProgressBar pgbSeguridad;
        private Button btnMostrar;
    }
}