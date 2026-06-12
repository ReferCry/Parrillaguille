using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class Dashboard : Form
    {
        private readonly DashboardService _dashboardService;

        public Dashboard()
        {
            InitializeComponent();

            var conexion = new ConexionSQL();
            _dashboardService = new DashboardService(conexion);

            dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            ConfigurarDataGridView();
            CargarDatos();
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarDatos()
        {
            try
            {
                DateTime inicio = dtpDesde.Value.Date;
                DateTime fin = dtpHasta.Value.Date.AddDays(1);

                if (fin <= inicio)
                {
                    MessageBox.Show("La fecha 'Hasta' debe ser mayor que la fecha 'Desde'.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var ventas = _dashboardService.ObtenerVentasPorPeriodo(inicio, fin);
                decimal total = _dashboardService.ObtenerTotalVentas(inicio, fin);
                int cantidad = _dashboardService.ObtenerCantidadVentas(inicio, fin);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Id", "N° Venta");
                dataGridView1.Columns.Add("Fecha", "Fecha");
                dataGridView1.Columns.Add("Cliente", "Cliente");
                dataGridView1.Columns.Add("DNI", "DNI");
                dataGridView1.Columns.Add("Tipo", "Tipo Doc.");
                dataGridView1.Columns.Add("Metodo", "Método Pago");
                dataGridView1.Columns.Add("Total", "Total (S/)");

                foreach (var v in ventas)
                {
                    dataGridView1.Rows.Add(
                        v.IdVenta,
                        v.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        $"{v.NombreCliente} {v.ApellidoCliente}",
                        v.DNI,
                        v.TipoComprobante,
                        v.MetodoPago,
                        v.MontoTotal.ToString("N2")
                    );
                }

                lblTotal.Text = $"Monto Total: S/ {total:N2}";
                lblCantidad.Text = $"Ventas: {cantidad}";
                lblPeriodo.Text = $"Período: {inicio:dd/MM/yyyy} - {fin.AddDays(-1):dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.Show();
            this.Close();
        }
    }
}
