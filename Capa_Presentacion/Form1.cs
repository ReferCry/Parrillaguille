namespace Capa_Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Parrillaguille - Sistema de Ventas";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SalonPrincipal frm = new SalonPrincipal("Salon");
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SalonPrincipal frm = new SalonPrincipal("Delivery");
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var conexion = new Capa_de_datos.ConexionSQL();
                var service = new Capa_Logica.VentaService(conexion);
                var ventas = service.ObtenerTodas();

                if (ventas.Count == 0)
                {
                    MessageBox.Show("No hay ventas registradas.", "Detalles de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string mensaje = "=== Ventas Registradas ===\n\n";
                foreach (var v in ventas.Take(20))
                {
                    mensaje += $"#{v.IdVenta} | {v.Fecha:dd/MM/yyyy HH:mm} | {v.NombreCliente} {v.ApellidoCliente} | S/{v.MontoTotal:N2} | {v.TipoComprobante} | {v.MetodoPago}\n";
                }

                MessageBox.Show(mensaje, "Detalles de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            frm.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
