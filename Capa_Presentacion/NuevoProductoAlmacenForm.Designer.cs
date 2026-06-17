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
            lblStockMinimo = new Label();
            txtStockMinimo = new TextBox();
            grpMedidas = new GroupBox();
            lstMedidas = new ListBox();
            lblMedidaNombre = new Label();
            txtMedidaNombre = new TextBox();
            lblMedidaValor = new Label();
            txtMedidaValor = new TextBox();
            lblMedidaUnidad = new Label();
            cmbMedidaUnidadBase = new ComboBox();
            lblMedidaPrecio = new Label();
            txtMedidaPrecio = new TextBox();
            btnAgregarMedida = new Button();
            btnQuitarMedida = new Button();
            btnGuardar = new Button();
            btnCancelar = new Button();
            grpMedidas.SuspendLayout();
            SuspendLayout();
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.Location = new Point(20, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(58, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(110, 17);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(320, 23);
            txtNombre.TabIndex = 1;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoria.Location = new Point(20, 55);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(75, 15);
            lblCategoria.TabIndex = 2;
            lblCategoria.Text = "Categoría:";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(110, 52);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(320, 23);
            cmbCategoria.TabIndex = 3;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCantidad.Location = new Point(20, 90);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(62, 15);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad:";
            //
            // txtCantidad
            //
            txtCantidad.Location = new Point(110, 87);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(320, 23);
            txtCantidad.TabIndex = 5;
            //
            // lblStockMinimo
            //
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStockMinimo.Location = new Point(250, 90);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(89, 15);
            lblStockMinimo.TabIndex = 6;
            lblStockMinimo.Text = "Stock Mínimo:";
            //
            // txtStockMinimo
            //
            txtStockMinimo.Location = new Point(350, 87);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(80, 23);
            txtStockMinimo.TabIndex = 7;
            //
            // grpMedidas
            //
            grpMedidas.Controls.Add(lstMedidas);
            grpMedidas.Controls.Add(lblMedidaNombre);
            grpMedidas.Controls.Add(txtMedidaNombre);
            grpMedidas.Controls.Add(lblMedidaValor);
            grpMedidas.Controls.Add(txtMedidaValor);
            grpMedidas.Controls.Add(lblMedidaUnidad);
            grpMedidas.Controls.Add(cmbMedidaUnidadBase);
            grpMedidas.Controls.Add(lblMedidaPrecio);
            grpMedidas.Controls.Add(txtMedidaPrecio);
            grpMedidas.Controls.Add(btnAgregarMedida);
            grpMedidas.Controls.Add(btnQuitarMedida);
            grpMedidas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpMedidas.Location = new Point(20, 120);
            grpMedidas.Name = "grpMedidas";
            grpMedidas.Size = new Size(410, 240);
            grpMedidas.TabIndex = 8;
            grpMedidas.TabStop = false;
            grpMedidas.Text = "Medidas del Producto (cada medida tiene su precio)";
            //
            // lstMedidas
            //
            lstMedidas.Font = new Font("Segoe UI", 9F);
            lstMedidas.FormattingEnabled = true;
            lstMedidas.ItemHeight = 15;
            lstMedidas.Location = new Point(10, 25);
            lstMedidas.Name = "lstMedidas";
            lstMedidas.Size = new Size(220, 154);
            lstMedidas.TabIndex = 0;
            //
            // lblMedidaNombre
            //
            lblMedidaNombre.AutoSize = true;
            lblMedidaNombre.Font = new Font("Segoe UI", 8F);
            lblMedidaNombre.Location = new Point(245, 25);
            lblMedidaNombre.Name = "lblMedidaNombre";
            lblMedidaNombre.Size = new Size(47, 13);
            lblMedidaNombre.TabIndex = 1;
            lblMedidaNombre.Text = "Nombre:";
            //
            // txtMedidaNombre
            //
            txtMedidaNombre.Font = new Font("Segoe UI", 9F);
            txtMedidaNombre.Location = new Point(245, 42);
            txtMedidaNombre.Name = "txtMedidaNombre";
            txtMedidaNombre.Size = new Size(150, 23);
            txtMedidaNombre.TabIndex = 2;
            txtMedidaNombre.PlaceholderText = "2 litros";
            //
            // lblMedidaValor
            //
            lblMedidaValor.AutoSize = true;
            lblMedidaValor.Font = new Font("Segoe UI", 8F);
            lblMedidaValor.Location = new Point(245, 72);
            lblMedidaValor.Name = "lblMedidaValor";
            lblMedidaValor.Size = new Size(34, 13);
            lblMedidaValor.TabIndex = 3;
            lblMedidaValor.Text = "Valor:";
            //
            // txtMedidaValor
            //
            txtMedidaValor.Font = new Font("Segoe UI", 9F);
            txtMedidaValor.Location = new Point(285, 69);
            txtMedidaValor.Name = "txtMedidaValor";
            txtMedidaValor.Size = new Size(110, 23);
            txtMedidaValor.TabIndex = 4;
            txtMedidaValor.PlaceholderText = "2";
            //
            // lblMedidaUnidad
            //
            lblMedidaUnidad.AutoSize = true;
            lblMedidaUnidad.Font = new Font("Segoe UI", 8F);
            lblMedidaUnidad.Location = new Point(245, 97);
            lblMedidaUnidad.Name = "lblMedidaUnidad";
            lblMedidaUnidad.Size = new Size(40, 13);
            lblMedidaUnidad.TabIndex = 5;
            lblMedidaUnidad.Text = "Unidad:";
            //
            // cmbMedidaUnidadBase
            //
            cmbMedidaUnidadBase.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedidaUnidadBase.Font = new Font("Segoe UI", 9F);
            cmbMedidaUnidadBase.Location = new Point(285, 94);
            cmbMedidaUnidadBase.Name = "cmbMedidaUnidadBase";
            cmbMedidaUnidadBase.Size = new Size(110, 23);
            cmbMedidaUnidadBase.TabIndex = 6;
            //
            // lblMedidaPrecio
            //
            lblMedidaPrecio.AutoSize = true;
            lblMedidaPrecio.Font = new Font("Segoe UI", 8F);
            lblMedidaPrecio.Location = new Point(245, 122);
            lblMedidaPrecio.Name = "lblMedidaPrecio";
            lblMedidaPrecio.Size = new Size(41, 13);
            lblMedidaPrecio.TabIndex = 7;
            lblMedidaPrecio.Text = "Precio:";
            //
            // txtMedidaPrecio
            //
            txtMedidaPrecio.Font = new Font("Segoe UI", 9F);
            txtMedidaPrecio.Location = new Point(285, 119);
            txtMedidaPrecio.Name = "txtMedidaPrecio";
            txtMedidaPrecio.Size = new Size(110, 23);
            txtMedidaPrecio.TabIndex = 8;
            txtMedidaPrecio.PlaceholderText = "6.00";
            //
            // btnAgregarMedida
            //
            btnAgregarMedida.BackColor = Color.FromArgb(46, 139, 87);
            btnAgregarMedida.FlatStyle = FlatStyle.Flat;
            btnAgregarMedida.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnAgregarMedida.ForeColor = Color.White;
            btnAgregarMedida.Location = new Point(245, 148);
            btnAgregarMedida.Name = "btnAgregarMedida";
            btnAgregarMedida.Size = new Size(80, 30);
            btnAgregarMedida.TabIndex = 9;
            btnAgregarMedida.Text = "+ AGREGAR";
            btnAgregarMedida.UseVisualStyleBackColor = false;
            btnAgregarMedida.Click += btnAgregarMedida_Click;
            //
            // btnQuitarMedida
            //
            btnQuitarMedida.BackColor = Color.FromArgb(178, 34, 34);
            btnQuitarMedida.FlatStyle = FlatStyle.Flat;
            btnQuitarMedida.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnQuitarMedida.ForeColor = Color.White;
            btnQuitarMedida.Location = new Point(335, 148);
            btnQuitarMedida.Name = "btnQuitarMedida";
            btnQuitarMedida.Size = new Size(60, 30);
            btnQuitarMedida.TabIndex = 10;
            btnQuitarMedida.Text = "QUITAR";
            btnQuitarMedida.UseVisualStyleBackColor = false;
            btnQuitarMedida.Click += btnQuitarMedida_Click;
            //
            // btnGuardar
            //
            btnGuardar.BackColor = Color.FromArgb(46, 139, 87);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(110, 375);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 9;
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
            btnCancelar.Location = new Point(250, 375);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 35);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            //
            // NuevoProductoAlmacenForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 425);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(lblStockMinimo);
            Controls.Add(txtStockMinimo);
            Controls.Add(grpMedidas);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NuevoProductoAlmacenForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo Producto - Almacén";
            grpMedidas.ResumeLayout(false);
            grpMedidas.PerformLayout();
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
        private Label lblStockMinimo;
        private TextBox txtStockMinimo;
        private GroupBox grpMedidas;
        private ListBox lstMedidas;
        private Label lblMedidaNombre;
        private TextBox txtMedidaNombre;
        private Label lblMedidaValor;
        private TextBox txtMedidaValor;
        private Label lblMedidaUnidad;
        private ComboBox cmbMedidaUnidadBase;
        private Label lblMedidaPrecio;
        private TextBox txtMedidaPrecio;
        private Button btnAgregarMedida;
        private Button btnQuitarMedida;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
