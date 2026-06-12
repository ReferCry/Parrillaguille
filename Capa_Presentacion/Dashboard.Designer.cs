namespace Capa_Presentacion
{
    partial class Dashboard
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
            this.dataGridView1 = new DataGridView();
            this.dtpDesde = new DateTimePicker();
            this.dtpHasta = new DateTimePicker();
            this.lblDesde = new Label();
            this.lblHasta = new Label();
            this.btnBuscar = new Button();
            this.lblTotal = new Label();
            this.lblCantidad = new Label();
            this.lblPeriodo = new Label();
            this.btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(12, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new Size(760, 350);
            this.dataGridView1.TabIndex = 0;

            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new Font("Segoe UI", 10F);
            this.lblDesde.Location = new Point(12, 18);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new Size(49, 19);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";

            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new Font("Segoe UI", 10F);
            this.dtpDesde.Format = DateTimePickerFormat.Short;
            this.dtpDesde.Location = new Point(67, 15);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new Size(130, 25);
            this.dtpDesde.TabIndex = 2;

            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new Font("Segoe UI", 10F);
            this.lblHasta.Location = new Point(215, 18);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new Size(45, 19);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";

            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new Font("Segoe UI", 10F);
            this.dtpHasta.Format = DateTimePickerFormat.Short;
            this.dtpHasta.Location = new Point(266, 15);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new Size(130, 25);
            this.dtpHasta.TabIndex = 4;

            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = Color.FromArgb(30, 136, 229);
            this.btnBuscar.FlatStyle = FlatStyle.Flat;
            this.btnBuscar.ForeColor = Color.White;
            this.btnBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBuscar.Location = new Point(410, 13);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new Size(100, 30);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new EventHandler(this.btnBuscar_Click);

            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.DarkGreen;
            this.lblTotal.Location = new Point(12, 445);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(150, 21);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Monto Total: S/ 0.00";

            // 
            // lblCantidad
            // 
            this.lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new Font("Segoe UI", 10F);
            this.lblCantidad.Location = new Point(12, 475);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new Size(70, 19);
            this.lblCantidad.TabIndex = 7;
            this.lblCantidad.Text = "Ventas: 0";

            // 
            // lblPeriodo
            // 
            this.lblPeriodo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Font = new Font("Segoe UI", 9F);
            this.lblPeriodo.ForeColor = Color.Gray;
            this.lblPeriodo.Location = new Point(250, 448);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new Size(100, 15);
            this.lblPeriodo.TabIndex = 8;
            this.lblPeriodo.Text = "Período:";

            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnVolver.BackColor = Color.FromArgb(230, 57, 70);
            this.btnVolver.FlatStyle = FlatStyle.Flat;
            this.btnVolver.ForeColor = Color.White;
            this.btnVolver.Location = new Point(650, 445);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new Size(120, 50);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new EventHandler(this.btnVolver_Click);

            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.ClientSize = new Size(784, 511);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblPeriodo);
            this.Controls.Add(this.btnVolver);
            this.MinimumSize = new Size(600, 400);
            this.Name = "Dashboard";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Parrillaguille - Dashboard de Ventas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Label lblDesde;
        private Label lblHasta;
        private Button btnBuscar;
        private Label lblTotal;
        private Label lblCantidad;
        private Label lblPeriodo;
        private Button btnVolver;
    }
}
