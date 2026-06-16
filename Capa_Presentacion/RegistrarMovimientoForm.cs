using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class RegistrarMovimientoForm : Form
    {
        private readonly ConexionSQL _conexion;
        private readonly MovimientoAlmacenService _service;
        private readonly ProductoAlmacenService _productoService;
        private readonly string _tipoMovimiento;

        public RegistrarMovimientoForm(ConexionSQL conexion, string tipoMovimiento)
        {
            InitializeComponent();
            _conexion = conexion;
            _service = new MovimientoAlmacenService(_conexion);
            _productoService = new ProductoAlmacenService(_conexion);
            _tipoMovimiento = tipoMovimiento;

            Text = tipoMovimiento == "Entrada" ? "Registrar Entrada - Almacén" : "Registrar Salida - Almacén";
            lblTipo.Text = tipoMovimiento == "Entrada" ? "ENTRADA DE STOCK" : "SALIDA DE STOCK";
            lblTipo.ForeColor = tipoMovimiento == "Entrada" ? Color.ForestGreen : Color.Firebrick;

            CargarProductos();
        }

        private void CargarProductos()
        {
            var productos = _productoService.ObtenerTodos();
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProductoAlmacen";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var movimiento = new MovimientoAlmacen
                {
                    IdProductoAlmacen = (int)cmbProducto.SelectedValue,
                    TipoMovimiento = _tipoMovimiento,
                    Cantidad = cantidad,
                    Observacion = txtObservacion.Text.Trim()
                };

                _service.RegistrarMovimiento(movimiento);
                MessageBox.Show(
                    $"{_tipoMovimiento} registrada exitosamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
