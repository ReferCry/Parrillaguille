namespace Capa_Presentacion
{
    partial class Almacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Almacen));
            tabControl = new TabControl();
            tabProductos = new TabPage();
            tabMovimientos = new TabPage();
            panelTop = new Panel();
            btnVolver = new Button();
            btnNuevo = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnEntrada = new Button();
            btnSalida = new Button();
            btnVerMovimientos = new Button();
            panelFiltros = new Panel();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            dgvProductos = new DataGridView();
            panelMovimientosTop = new Panel();
            lblFecha = new Label();
            dateTimePickerFecha = new DateTimePicker();
            btnCargarMovimientos = new Button();
            dgvMovimientos = new DataGridView();
            tabControl.SuspendLayout();
            tabProductos.SuspendLayout();
            tabMovimientos.SuspendLayout();
            panelTop.SuspendLayout();
            panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            panelMovimientosTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            SuspendLayout();
            //
            // tabControl
            //
            tabControl.Controls.Add(tabProductos);
            tabControl.Controls.Add(tabMovimientos);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 70);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1000, 530);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            //
            // tabProductos
            //
            tabProductos.Controls.Add(dgvProductos);
            tabProductos.Controls.Add(panelFiltros);
            tabProductos.Location = new Point(4, 29);
            tabProductos.Name = "tabProductos";
            tabProductos.Padding = new Padding(3);
            tabProductos.Size = new Size(992, 497);
            tabProductos.TabIndex = 0;
            tabProductos.Text = "Productos";
            tabProductos.UseVisualStyleBackColor = true;
            //
            // tabMovimientos
            //
            tabMovimientos.Controls.Add(dgvMovimientos);
            tabMovimientos.Controls.Add(panelMovimientosTop);
            tabMovimientos.Location = new Point(4, 29);
            tabMovimientos.Name = "tabMovimientos";
            tabMovimientos.Padding = new Padding(3);
            tabMovimientos.Size = new Size(992, 497);
            tabMovimientos.TabIndex = 1;
            tabMovimientos.Text = "Movimientos";
            tabMovimientos.UseVisualStyleBackColor = true;
            //
            // panelTop
            //
            panelTop.BackColor = Color.FromArgb(40, 40, 40);
            panelTop.Controls.Add(btnVolver);
            panelTop.Controls.Add(btnNuevo);
            panelTop.Controls.Add(btnEditar);
            panelTop.Controls.Add(btnEliminar);
            panelTop.Controls.Add(btnEntrada);
            panelTop.Controls.Add(btnSalida);
            panelTop.Controls.Add(btnVerMovimientos);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1000, 70);
            panelTop.TabIndex = 1;
            //
            // btnVolver
            //
            btnVolver.BackColor = Color.FromArgb(180, 60, 60);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(10, 15);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(90, 40);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "← VOLVER";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            //
            // btnNuevo
            //
            btnNuevo.BackColor = Color.FromArgb(46, 139, 87);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(120, 15);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(120, 40);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "+ NUEVO";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            //
            // btnEditar
            //
            btnEditar.BackColor = Color.FromArgb(70, 130, 180);
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.White;
            btnEditar.Location = new Point(250, 15);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 40);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "EDITAR";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            //
            // btnEliminar
            //
            btnEliminar.BackColor = Color.FromArgb(178, 34, 34);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(360, 15);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 40);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            //
            // btnEntrada
            //
            btnEntrada.BackColor = Color.FromArgb(34, 139, 34);
            btnEntrada.FlatStyle = FlatStyle.Flat;
            btnEntrada.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEntrada.ForeColor = Color.White;
            btnEntrada.Location = new Point(500, 15);
            btnEntrada.Name = "btnEntrada";
            btnEntrada.Size = new Size(120, 40);
            btnEntrada.TabIndex = 4;
            btnEntrada.Text = "↓ ENTRADA";
            btnEntrada.UseVisualStyleBackColor = false;
            btnEntrada.Click += btnEntrada_Click;
            //
            // btnSalida
            //
            btnSalida.BackColor = Color.FromArgb(205, 92, 92);
            btnSalida.FlatStyle = FlatStyle.Flat;
            btnSalida.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSalida.ForeColor = Color.White;
            btnSalida.Location = new Point(630, 15);
            btnSalida.Name = "btnSalida";
            btnSalida.Size = new Size(120, 40);
            btnSalida.TabIndex = 5;
            btnSalida.Text = "↑ SALIDA";
            btnSalida.UseVisualStyleBackColor = false;
            btnSalida.Click += btnSalida_Click;
            //
            // btnVerMovimientos
            //
            btnVerMovimientos.BackColor = Color.FromArgb(100, 100, 100);
            btnVerMovimientos.FlatStyle = FlatStyle.Flat;
            btnVerMovimientos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnVerMovimientos.ForeColor = Color.White;
            btnVerMovimientos.Location = new Point(780, 15);
            btnVerMovimientos.Name = "btnVerMovimientos";
            btnVerMovimientos.Size = new Size(200, 40);
            btnVerMovimientos.TabIndex = 6;
            btnVerMovimientos.Text = "VER MOVIMIENTOS";
            btnVerMovimientos.UseVisualStyleBackColor = false;
            btnVerMovimientos.Click += btnVerMovimientos_Click;
            //
            // panelFiltros
            //
            panelFiltros.BackColor = Color.FromArgb(245, 245, 245);
            panelFiltros.Controls.Add(lblCategoria);
            panelFiltros.Controls.Add(cmbCategoria);
            panelFiltros.Controls.Add(lblBuscar);
            panelFiltros.Controls.Add(txtBuscar);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(3, 3);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(986, 50);
            panelFiltros.TabIndex = 0;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoria.Location = new Point(15, 15);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(75, 15);
            lblCategoria.TabIndex = 0;
            lblCategoria.Text = "Categoría:";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(100, 12);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(200, 23);
            cmbCategoria.TabIndex = 1;
            cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            //
            // lblBuscar
            //
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBuscar.Location = new Point(330, 15);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(50, 15);
            lblBuscar.TabIndex = 2;
            lblBuscar.Text = "Buscar:";
            //
            // txtBuscar
            //
            txtBuscar.Location = new Point(390, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 23);
            txtBuscar.TabIndex = 3;
            txtBuscar.PlaceholderText = "Buscar por nombre...";
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            //
            // dgvProductos
            //
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.Location = new Point(3, 53);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersWidth = 30;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(986, 441);
            dgvProductos.TabIndex = 1;
            //
            // panelMovimientosTop
            //
            panelMovimientosTop.BackColor = Color.FromArgb(245, 245, 245);
            panelMovimientosTop.Controls.Add(lblFecha);
            panelMovimientosTop.Controls.Add(dateTimePickerFecha);
            panelMovimientosTop.Controls.Add(btnCargarMovimientos);
            panelMovimientosTop.Dock = DockStyle.Top;
            panelMovimientosTop.Location = new Point(3, 3);
            panelMovimientosTop.Name = "panelMovimientosTop";
            panelMovimientosTop.Size = new Size(986, 50);
            panelMovimientosTop.TabIndex = 0;
            //
            // lblFecha
            //
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFecha.Location = new Point(15, 15);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(45, 15);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            //
            // dateTimePickerFecha
            //
            dateTimePickerFecha.Format = DateTimePickerFormat.Short;
            dateTimePickerFecha.Location = new Point(70, 12);
            dateTimePickerFecha.Name = "dateTimePickerFecha";
            dateTimePickerFecha.Size = new Size(200, 23);
            dateTimePickerFecha.TabIndex = 1;
            //
            // btnCargarMovimientos
            //
            btnCargarMovimientos.BackColor = Color.FromArgb(70, 130, 180);
            btnCargarMovimientos.FlatStyle = FlatStyle.Flat;
            btnCargarMovimientos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCargarMovimientos.ForeColor = Color.White;
            btnCargarMovimientos.Location = new Point(290, 10);
            btnCargarMovimientos.Name = "btnCargarMovimientos";
            btnCargarMovimientos.Size = new Size(150, 30);
            btnCargarMovimientos.TabIndex = 2;
            btnCargarMovimientos.Text = "CARGAR";
            btnCargarMovimientos.UseVisualStyleBackColor = false;
            btnCargarMovimientos.Click += btnCargarMovimientos_Click;
            //
            // dgvMovimientos
            //
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovimientos.BackgroundColor = Color.White;
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Dock = DockStyle.Fill;
            dgvMovimientos.Location = new Point(3, 53);
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.RowHeadersWidth = 30;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.Size = new Size(986, 441);
            dgvMovimientos.TabIndex = 1;
            //
            // Almacen
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(tabControl);
            Controls.Add(panelTop);
            Name = "Almacen";
            Text = "Almacén - Parrillaguille";
            WindowState = FormWindowState.Maximized;
            tabControl.ResumeLayout(false);
            tabProductos.ResumeLayout(false);
            tabMovimientos.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            panelMovimientosTop.ResumeLayout(false);
            panelMovimientosTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabProductos;
        private TabPage tabMovimientos;
        private Panel panelTop;
        private Panel panelFiltros;
        private Panel panelMovimientosTop;
        private DataGridView dgvProductos;
        private DataGridView dgvMovimientos;
        private Button btnVolver;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnEntrada;
        private Button btnSalida;
        private Button btnVerMovimientos;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Label lblFecha;
        private DateTimePicker dateTimePickerFecha;
        private Button btnCargarMovimientos;
    }
}
