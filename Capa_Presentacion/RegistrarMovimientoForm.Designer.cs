namespace Capa_Presentacion
{
    partial class RegistrarMovimientoForm
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
            lblTipo = new Label();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            lblObservacion = new Label();
            txtObservacion = new TextBox();
            btnRegistrar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // lblTipo
            //
            lblTipo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTipo.Location = new Point(0, 15);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(400, 30);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "ENTRADA DE STOCK";
            lblTipo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblProducto
            //
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProducto.Location = new Point(30, 65);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(62, 15);
            lblProducto.TabIndex = 1;
            lblProducto.Text = "Producto:";
            //
            // cmbProducto
            //
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.Location = new Point(110, 62);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(260, 23);
            cmbProducto.TabIndex = 2;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCantidad.Location = new Point(30, 105);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(62, 15);
            lblCantidad.TabIndex = 3;
            lblCantidad.Text = "Cantidad:";
            //
            // txtCantidad
            //
            txtCantidad.Location = new Point(110, 102);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(150, 23);
            txtCantidad.TabIndex = 4;
            //
            // lblObservacion
            //
            lblObservacion.AutoSize = true;
            lblObservacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblObservacion.Location = new Point(30, 145);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(81, 15);
            lblObservacion.TabIndex = 5;
            lblObservacion.Text = "Observación:";
            //
            // txtObservacion
            //
            txtObservacion.Location = new Point(120, 142);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(250, 23);
            txtObservacion.TabIndex = 6;
            //
            // btnRegistrar
            //
            btnRegistrar.BackColor = Color.FromArgb(46, 139, 87);
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(80, 190);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(130, 35);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "REGISTRAR";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // btnCancelar
            //
            btnCancelar.BackColor = Color.FromArgb(178, 34, 34);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(230, 190);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 35);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            //
            // RegistrarMovimientoForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 240);
            Controls.Add(lblTipo);
            Controls.Add(lblProducto);
            Controls.Add(cmbProducto);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(lblObservacion);
            Controls.Add(txtObservacion);
            Controls.Add(btnRegistrar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegistrarMovimientoForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registrar Movimiento";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTipo;
        private Label lblProducto;
        private ComboBox cmbProducto;
        private Label lblCantidad;
        private TextBox txtCantidad;
        private Label lblObservacion;
        private TextBox txtObservacion;
        private Button btnRegistrar;
        private Button btnCancelar;
    }
}
