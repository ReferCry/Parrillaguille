using System.Data;
using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class Pedio2 : Form
    {
        private readonly PedidoService _pedidoService;
        private readonly ProductoService _productoService;
        private readonly string _tipoAtencion;
        private readonly int? _mesaId;
        private int? _pedidoActualId;
        private DataTable _tablaDetalles;

        // Constructor por defecto (requerido por Designer)
        public Pedio2() : this("Salon", null) { }

        // Constructor parametrizado
        public Pedio2(string tipoAtencion, int? mesaId = null)
        {
            InitializeComponent();
            _tipoAtencion = tipoAtencion;
            _mesaId = mesaId;

            var conexion = new ConexionSQL();
            _pedidoService = new PedidoService(conexion);
            _productoService = new ProductoService(conexion);

            this.Text = tipoAtencion == "Delivery"
                ? $"Pedido - Delivery"
                : $"Pedido - Salón (Mesa {mesaId})";

            ConfigurarDataGridView();
            CargarMenus();

            this.Load += Pedio2_Load;
        }

        private void Pedio2_Load(object? sender, EventArgs e)
        {
            try
            {
                var pedido = new Pedido
                {
                    IdMesa = _mesaId,
                    TipoAtencion = _tipoAtencion
                };
                _pedidoActualId = _pedidoService.AbrirPedido(pedido);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            _tablaDetalles = new DataTable();
            _tablaDetalles.Columns.Add("Producto", typeof(string));
            _tablaDetalles.Columns.Add("Cantidad", typeof(int));
            _tablaDetalles.Columns.Add("P. Unitario", typeof(decimal));
            _tablaDetalles.Columns.Add("Subtotal", typeof(decimal));
            _tablaDetalles.Columns.Add("IdProducto", typeof(int));

            dataGridView1.DataSource = _tablaDetalles;
            if (dataGridView1.Columns.Contains("IdProducto"))
                dataGridView1.Columns["IdProducto"].Visible = false;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarMenus()
        {
            var mapping = new Dictionary<string, (ComboBox combo, DomainUpDown qty)>
            {
                ["Pollos a la Brasa"] = (cboPlato, domainUpDown1),
                ["Parrillas"] = (cboParrilla, domainUpDown2),
                ["Platos a la Carta"] = (cboPlatosalacarta, domainUpDown8),
                ["Otros"] = (cbootros, domainUpDown3),
                ["Porciones y Guarniciones"] = (cboPorYguar, domainUpDown4),
                ["Gaseosas"] = (cbogaseosa, domainUpDown5),
                ["Infusiones"] = (cboInfusiones, domainUpDown6),
                ["Refrescos"] = (cboRefrescos, domainUpDown7)
            };

            foreach (var cat in MenuData.Categorias)
            {
                if (mapping.TryGetValue(cat.Key, out var m))
                {
                    m.combo.Items.Clear();
                    m.combo.Items.AddRange(cat.Value);
                    m.combo.SelectedIndex = -1;
                    m.qty.Text = "1";
                }
            }
        }

        private void AgregarDetalle(ComboBox combo, DomainUpDown qty)
        {
            if (combo.SelectedIndex < 0) return;

            int cantidad;
            if (!int.TryParse(qty.Text?.Trim(), out cantidad) || cantidad < 1)
                cantidad = 1;

            string nombre = combo.SelectedItem.ToString()!;
            decimal precio = 0;
            int idProducto = 0;

            try
            {
                var producto = _productoService.ObtenerPorNombre(nombre);
                if (producto != null)
                {
                    precio = producto.Precio;
                    idProducto = producto.IdProducto;
                }
            }
            catch { /* Si no hay BD, usar precio 0 */ }

            if (precio == 0)
            {
                MessageBox.Show(
                    $"El producto '{nombre}' no tiene precio registrado.\nRegístrelo en el Menú antes de usarlo.",
                    "Sin precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _tablaDetalles.Rows.Add(nombre, cantidad, precio, cantidad * precio, idProducto);

            // Limpiar selección
            combo.SelectedIndex = -1;
            qty.Text = "1";
        }

        // ── Eventos de botones ──

        private void button1_Click(object sender, EventArgs e) // Agregar Pedido
        {
            var combos = new (ComboBox combo, DomainUpDown qty)[]
            {
                (cboPlato, domainUpDown1),
                (cboParrilla, domainUpDown2),
                (cboPlatosalacarta, domainUpDown8),
                (cbootros, domainUpDown3),
                (cboPorYguar, domainUpDown4),
                (cbogaseosa, domainUpDown5),
                (cboInfusiones, domainUpDown6),
                (cboRefrescos, domainUpDown7)
            };

            bool algunoAgregado = false;
            foreach (var (combo, qty) in combos)
            {
                if (combo.SelectedIndex >= 0)
                {
                    AgregarDetalle(combo, qty);
                    algunoAgregado = true;
                }
            }

            if (!algunoAgregado)
            {
                MessageBox.Show("Seleccione al menos un plato o bebida antes de agregar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Eliminar Plato
        {
            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                string nombre = dataGridView1.CurrentRow.Cells["Producto"].Value?.ToString() ?? "";
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                // Guardar en BD si hay pedido abierto
                if (_pedidoActualId.HasValue)
                {
                    try
                    {
                        var detalles = _pedidoService.ObtenerDetalles(_pedidoActualId.Value);
                        var detalle = detalles.FirstOrDefault(d => d.NombreProducto == nombre);
                        if (detalle != null)
                            _pedidoService.EliminarDetalle(detalle.IdDetalle, _pedidoActualId.Value);
                    }
                    catch { /* offline mode */ }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un plato de la lista para eliminar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e) // Ir a Venta
        {
            if (_tablaDetalles.Rows.Count == 0)
            {
                MessageBox.Show("No hay platos en el pedido. Agregue al menos uno antes de facturar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _tablaDetalles.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));

            var ventaForm = new Venta(_pedidoActualId ?? 0, _mesaId, total);
            ventaForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e) // Atrás
        {
            if (_pedidoActualId.HasValue)
            {
                try
                {
                    _pedidoService.CerrarPedido(_pedidoActualId.Value, _mesaId);
                }
                catch { /* offline mode */ }
            }

            var salonForm = new SalonPrincipal(_tipoAtencion);
            salonForm.Show();
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void cboPlato_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
