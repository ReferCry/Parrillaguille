using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class Venta : Form
    {
        private readonly VentaService _ventaService;
        private readonly PedidoService _pedidoService;
        private readonly ImpresionService _impresionService;
        private readonly int _pedidoId;
        private readonly int? _mesaId;
        private readonly decimal _montoTotal;

        public Venta(int pedidoId, int? mesaId, decimal montoTotal)
        {
            InitializeComponent();

            _pedidoId = pedidoId;
            _mesaId = mesaId;
            _montoTotal = montoTotal;

            var conexion = new ConexionSQL();
            _ventaService = new VentaService(conexion);
            _pedidoService = new PedidoService(conexion);
            _impresionService = new ImpresionService();

            // Métodos de pago
            cboMetodo.Items.Clear();
            cboMetodo.Items.AddRange(new object[] { "Efectivo", "Tarjeta de Crédito", "Yape" });
            cboMetodo.SelectedIndex = 0;

            // Datos por defecto
            textBox5.Text = montoTotal.ToString("N2");
            textBox4.Text = mesaId.HasValue ? mesaId.ToString() : "Delivery";

            // Factura por defecto
            rbdFactura.Checked = true;
            txtRUC.Enabled = false;

            // Botón confirmar
            var btnConfirmar = new Button
            {
                Text = "✓ Registrar Venta",
                Location = new Point(116, 395),
                Size = new Size(200, 30),
                BackColor = Color.FromArgb(0, 153, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.Click += BtnConfirmar_Click;
            groupBox4.Controls.Add(btnConfirmar);

            // Botón imprimir ticket
            var btnImprimir = new Button
            {
                Text = "🖨 Imprimir Ticket",
                Location = new Point(330, 395),
                Size = new Size(180, 30),
                BackColor = Color.FromArgb(30, 136, 229),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnImprimir.Click += BtnImprimir_Click;
            groupBox4.Controls.Add(btnImprimir);
        }

        private void rbdFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdFactura.Checked)
            {
                txtRUC.Enabled = true;
                txtRUC.Focus();
            }
        }

        private void rboBoleta_CheckedChanged(object sender, EventArgs e)
        {
            if (rboBoleta.Checked)
            {
                txtRUC.Clear();
                txtRUC.Enabled = false;
            }
        }

        private void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            string nombres = textBox1.Text.Trim();
            string apellidos = textBox2.Text.Trim();
            string dni = textBox3.Text.Trim();
            string ruc = txtRUC.Text.Trim();
            string metodoPago = cboMetodo.SelectedItem?.ToString() ?? "Efectivo";
            string tipoComprobante = rbdFactura.Checked ? "Factura" : "Boleta";

            // Validaciones
            if (string.IsNullOrWhiteSpace(nombres))
            {
                MessageBox.Show("Ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellidos))
            {
                MessageBox.Show("Ingrese los apellidos del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8)
            {
                MessageBox.Show("Ingrese un DNI válido (8 dígitos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            if (tipoComprobante == "Factura" && (string.IsNullOrWhiteSpace(ruc) || ruc.Length != 11))
            {
                MessageBox.Show("Ingrese un RUC válido (11 dígitos) para factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRUC.Focus();
                return;
            }

            try
            {
                var venta = new VentaEntidad
                {
                    IdPedido = _pedidoId,
                    NombreCliente = nombres,
                    ApellidoCliente = apellidos,
                    DNI = dni,
                    RUC = ruc,
                    TipoComprobante = tipoComprobante,
                    MetodoPago = metodoPago,
                    MontoTotal = _montoTotal,
                    Fecha = dateTimePicker1.Value
                };

                int idVenta = _ventaService.RegistrarVenta(venta);

                // Cerrar el pedido
                if (_pedidoId > 0)
                {
                    try { _pedidoService.CerrarPedido(_pedidoId, _mesaId); }
                    catch { /* offline */ }
                }

                MessageBox.Show(
                    $"¡Venta registrada exitosamente!\n\n" +
                    $"Venta #{idVenta}\n" +
                    $"Cliente: {nombres} {apellidos}\n" +
                    $"DNI: {dni}\n" +
                    $"Comprobante: {tipoComprobante}\n" +
                    $"Método de pago: {metodoPago}\n" +
                    $"Total: S/ {_montoTotal:N2}",
                    "Venta Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Volver al menú principal
                var frm = new Form1();
                frm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnImprimir_Click(object? sender, EventArgs e)
        {
            try
            {
                var conexion = new ConexionSQL();
                var pedidoService = new PedidoService(conexion);
                var detalles = pedidoService.ObtenerDetalles(_pedidoId);

                if (detalles.Count == 0)
                {
                    MessageBox.Show("No hay detalles para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var venta = new VentaEntidad
                {
                    IdVenta = 0,
                    IdPedido = _pedidoId,
                    NombreCliente = textBox1.Text.Trim(),
                    ApellidoCliente = textBox2.Text.Trim(),
                    DNI = textBox3.Text.Trim(),
                    RUC = txtRUC.Text.Trim(),
                    TipoComprobante = rbdFactura.Checked ? "Factura" : "Boleta",
                    MetodoPago = cboMetodo.SelectedItem?.ToString() ?? "Efectivo",
                    MontoTotal = _montoTotal,
                    Fecha = DateTime.Now
                };

                string ticket = _impresionService.GenerarTicket(venta, detalles, _mesaId);
                _impresionService.GuardarTicket(ticket);

                MessageBox.Show(
                    "Ticket generado correctamente.\n\n" +
                    "El archivo 'ticket.txt' se ha guardado en la carpeta del programa.",
                    "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
    }
}
