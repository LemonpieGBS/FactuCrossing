namespace FactuCrossing.Formularios.Utilidades
{
    partial class DebugLog
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
            richTextBox1 = new RichTextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(10, 8);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(504, 617);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // DebugLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 635);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "DebugLog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "FactuCrossing Log";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}