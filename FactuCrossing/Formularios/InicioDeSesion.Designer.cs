namespace FactuCrossing.Formularios
{
    partial class InicioDeSesion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicioDeSesion));
            pictureBox1 = new PictureBox();
            txtNombreUsuario = new TextBox();
            txtContraseña = new TextBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            label1 = new Label();
            checkBox1 = new CheckBox();
            linkOlvidaste = new LinkLabel();
            btnIniciarSesion = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(23, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(471, 162);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(17, 62);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(311, 23);
            txtNombreUsuario.TabIndex = 2;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(19, 142);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.Size = new Size(311, 23);
            txtContraseña.TabIndex = 4;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(17, 121);
            label2.Name = "label2";
            label2.Size = new Size(165, 18);
            label2.TabIndex = 3;
            label2.Text = "Contraseña";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(linkOlvidaste);
            groupBox1.Controls.Add(txtContraseña);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtNombreUsuario);
            groupBox1.Location = new Point(83, 180);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(352, 251);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inicio de Sesión";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(17, 41);
            label1.Name = "label1";
            label1.Size = new Size(165, 18);
            label1.TabIndex = 7;
            label1.Text = "Nombre de Usuario";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(236, 203);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(92, 19);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Recuérdame";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // linkOlvidaste
            // 
            linkOlvidaste.AutoSize = true;
            linkOlvidaste.Location = new Point(19, 203);
            linkOlvidaste.Name = "linkOlvidaste";
            linkOlvidaste.Size = new Size(143, 15);
            linkOlvidaste.TabIndex = 5;
            linkOlvidaste.TabStop = true;
            linkOlvidaste.Text = "¿Olvidaste tu Contraseña?";
            linkOlvidaste.LinkClicked += linkOlvidaste_LinkClicked;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.BackColor = Color.FromArgb(83, 96, 171);
            btnIniciarSesion.Cursor = Cursors.Hand;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.Location = new Point(83, 461);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(352, 50);
            btnIniciarSesion.TabIndex = 6;
            btnIniciarSesion.Text = "Iniciar Sesión";
            btnIniciarSesion.UseVisualStyleBackColor = false;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // InicioDeSesion
            // 
            AcceptButton = btnIniciarSesion;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 562);
            Controls.Add(btnIniciarSesion);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Icon = (Icon)Properties.Resources.AppIcon;
            Name = "InicioDeSesion";
            Text = "Inicio de Sesión";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox txtNombreUsuario;
        private TextBox txtContraseña;
        private Label label2;
        private GroupBox groupBox1;
        private Label label1;
        private CheckBox checkBox1;
        private LinkLabel linkOlvidaste;
        private Button btnIniciarSesion;
    }
}
