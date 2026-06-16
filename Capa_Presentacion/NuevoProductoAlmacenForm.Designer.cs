namespace Capa_Presentacion
{
    partial class NuevoProductoAlmacenForm
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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            lblUnidadMedida = new Label();
            cmbUnidadMedida = new ComboBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblStockMinimo = new Label();
            txtStockMinimo = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(30, 25);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(58, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(120, 22);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(300, 23);
            txtNombre.TabIndex = 1;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoria.Location = new Point(30, 65);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(75, 15);
            lblCategoria.TabIndex = 2;
            lblCategoria.Text = "Categoría:";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(120, 62);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(300, 23);
            cmbCategoria.TabIndex = 3;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCantidad.Location = new Point(30, 105);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(62, 15);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad:";
            //
            // txtCantidad
            //
            txtCantidad.Location = new Point(120, 102);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(150, 23);
            txtCantidad.TabIndex = 5;
            //
            // lblUnidadMedida
            //
            lblUnidadMedida.AutoSize = true;
            lblUnidadMedida.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUnidadMedida.Location = new Point(30, 145);
            lblUnidadMedida.Name = "lblUnidadMedida";
            lblUnidadMedida.Size = new Size(96, 15);
            lblUnidadMedida.TabIndex = 6;
            lblUnidadMedida.Text = "Unidad Medida:";
            //
            // cmbUnidadMedida
            //
            cmbUnidadMedida.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnidadMedida.Location = new Point(140, 142);
            cmbUnidadMedida.Name = "cmbUnidadMedida";
            cmbUnidadMedida.Size = new Size(150, 23);
            cmbUnidadMedida.TabIndex = 7;
            //
            // lblPrecio
            //
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrecio.Location = new Point(30, 185);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(92, 15);
            lblPrecio.TabIndex = 8;
            lblPrecio.Text = "Precio Unitario:";
            //
            // txtPrecio
            //
            txtPrecio.Location = new Point(140, 182);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(150, 23);
            txtPrecio.TabIndex = 9;
            //
            // lblStockMinimo
            //
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStockMinimo.Location = new Point(30, 225);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(89, 15);
            lblStockMinimo.TabIndex = 10;
            lblStockMinimo.Text = "Stock Mínimo:";
            //
            // txtStockMinimo
            //
            txtStockMinimo.Location = new Point(140, 222);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(150, 23);
            txtStockMinimo.TabIndex = 11;
            //
            // btnGuardar
            //
            btnGuardar.BackColor = Color.FromArgb(46, 139, 87);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(120, 270);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnCancelar
            //
            btnCancelar.BackColor = Color.FromArgb(178, 34, 34);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(260, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 35);
            btnCancelar.TabIndex = 13;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            //
            // NuevoProductoAlmacenForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 320);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(lblUnidadMedida);
            Controls.Add(cmbUnidadMedida);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(lblStockMinimo);
            Controls.Add(txtStockMinimo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NuevoProductoAlmacenForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo Producto - Almacén";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblCantidad;
        private TextBox txtCantidad;
        private Label lblUnidadMedida;
        private ComboBox cmbUnidadMedida;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private Label lblStockMinimo;
        private TextBox txtStockMinimo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
