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
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            lblMedida = new Label();
            cmbMedida = new ComboBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            lblTotal = new Label();
            lblTotalValor = new Label();
            lblObservacion = new Label();
            txtObservacion = new TextBox();
            btnRegistrar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            //
            // lblTipo
            //
            lblTipo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTipo.Location = new Point(0, 10);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(450, 30);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "ENTRADA DE STOCK";
            lblTipo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoria.Location = new Point(30, 55);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(75, 15);
            lblCategoria.TabIndex = 1;
            lblCategoria.Text = "Categoría:";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(120, 52);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(300, 23);
            cmbCategoria.TabIndex = 2;
            cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            //
            // lblProducto
            //
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProducto.Location = new Point(30, 95);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(62, 15);
            lblProducto.TabIndex = 3;
            lblProducto.Text = "Producto:";
            //
            // cmbProducto
            //
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.Location = new Point(120, 92);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(300, 23);
            cmbProducto.TabIndex = 4;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            //
            // lblMedida
            //
            lblMedida.AutoSize = true;
            lblMedida.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMedida.Location = new Point(30, 135);
            lblMedida.Name = "lblMedida";
            lblMedida.Size = new Size(53, 15);
            lblMedida.TabIndex = 5;
            lblMedida.Text = "Medida:";
            //
            // cmbMedida
            //
            cmbMedida.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedida.Location = new Point(120, 132);
            cmbMedida.Name = "cmbMedida";
            cmbMedida.Size = new Size(300, 23);
            cmbMedida.TabIndex = 6;
            cmbMedida.SelectedIndexChanged += cmbMedida_SelectedIndexChanged;
            //
            // lblPrecio
            //
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrecio.Location = new Point(30, 175);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(46, 15);
            lblPrecio.TabIndex = 5;
            lblPrecio.Text = "Precio:";
            //
            // txtPrecio
            //
            txtPrecio.Location = new Point(120, 172);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(120, 23);
            txtPrecio.TabIndex = 6;
            txtPrecio.PlaceholderText = "Se autocompleta";
            txtPrecio.TextChanged += txtPrecio_TextChanged;
            //
            // lblCantidad
            //
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCantidad.Location = new Point(30, 215);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(62, 15);
            lblCantidad.TabIndex = 7;
            lblCantidad.Text = "Cantidad:";
            //
            // txtCantidad
            //
            txtCantidad.Location = new Point(120, 212);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(120, 23);
            txtCantidad.TabIndex = 8;
            txtCantidad.PlaceholderText = "Solo unidades";
            txtCantidad.TextChanged += txtCantidad_TextChanged;
            //
            // lblTotal
            //
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(30, 255);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(46, 20);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "Total:";
            //
            // lblTotalValor
            //
            lblTotalValor.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalValor.ForeColor = Color.FromArgb(0, 100, 0);
            lblTotalValor.Location = new Point(90, 255);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(200, 20);
            lblTotalValor.TabIndex = 10;
            lblTotalValor.Text = "-";
            //
            // lblObservacion
            //
            lblObservacion.AutoSize = true;
            lblObservacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblObservacion.Location = new Point(30, 295);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(81, 15);
            lblObservacion.TabIndex = 11;
            lblObservacion.Text = "Observación:";
            //
            // txtObservacion
            //
            txtObservacion.Location = new Point(120, 292);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(300, 23);
            txtObservacion.TabIndex = 12;
            //
            // btnRegistrar
            //
            btnRegistrar.BackColor = Color.FromArgb(46, 139, 87);
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(80, 335);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(150, 35);
            btnRegistrar.TabIndex = 13;
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
            btnCancelar.Location = new Point(250, 335);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 35);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "CANCELAR";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            //
            // RegistrarMovimientoForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 385);
            Controls.Add(lblTipo);
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);
            Controls.Add(lblProducto);
            Controls.Add(cmbProducto);
            Controls.Add(lblMedida);
            Controls.Add(cmbMedida);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(lblCantidad);
            Controls.Add(txtCantidad);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalValor);
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
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblProducto;
        private ComboBox cmbProducto;
        private Label lblMedida;
        private ComboBox cmbMedida;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private Label lblCantidad;
        private TextBox txtCantidad;
        private Label lblTotal;
        private Label lblTotalValor;
        private Label lblObservacion;
        private TextBox txtObservacion;
        private Button btnRegistrar;
        private Button btnCancelar;
    }
}
