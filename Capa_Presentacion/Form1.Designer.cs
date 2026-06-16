namespace Capa_Presentacion
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnAlmacen = new Button();
            btnCerrarSesion = new Button();
            SuspendLayout();
            //
            // btnAlmacen
            //
            btnAlmacen.BackColor = Color.FromArgb(200, 150, 50);
            btnAlmacen.FlatStyle = FlatStyle.Flat;
            btnAlmacen.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnAlmacen.ForeColor = Color.White;
            btnAlmacen.Location = new Point(318, 175);
            btnAlmacen.Name = "btnAlmacen";
            btnAlmacen.Size = new Size(250, 80);
            btnAlmacen.TabIndex = 0;
            btnAlmacen.Text = "ALMACÉN";
            btnAlmacen.UseVisualStyleBackColor = false;
            btnAlmacen.Click += btnAlmacen_Click;
            //
            // btnCerrarSesion
            //
            btnCerrarSesion.BackColor = Color.Transparent;
            btnCerrarSesion.BackgroundImage = (Image)resources.GetObject("button4.Image");
            btnCerrarSesion.BackgroundImageLayout = ImageLayout.Zoom;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Location = new Point(810, 10);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(60, 60);
            btnCerrarSesion.TabIndex = 1;
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(885, 450);
            Controls.Add(btnCerrarSesion);
            Controls.Add(btnAlmacen);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Parrillaguille - Sistema de Ventas";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btnAlmacen;
        private Button btnCerrarSesion;
    }
}
