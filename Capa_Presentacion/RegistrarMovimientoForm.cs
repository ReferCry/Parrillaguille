using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class RegistrarMovimientoForm : Form
    {
        private readonly ConexionSQL _conexion;
        private readonly MovimientoAlmacenService _service;
        private readonly CategoriaAlmacenService _categoriaService;
        private readonly ProductoAlmacenService _productoService;
        private readonly MedidaProductoService _medidaService;
        private readonly string _tipoMovimiento;

        public RegistrarMovimientoForm(ConexionSQL conexion, string tipoMovimiento)
        {
            InitializeComponent();
            _conexion = conexion;
            _service = new MovimientoAlmacenService(_conexion);
            _categoriaService = new CategoriaAlmacenService(_conexion);
            _productoService = new ProductoAlmacenService(_conexion);
            _medidaService = new MedidaProductoService(_conexion);
            _tipoMovimiento = tipoMovimiento;

            Text = tipoMovimiento == "Entrada" ? "Registrar Entrada - Almacén" : "Registrar Salida - Almacén";
            lblTipo.Text = tipoMovimiento == "Entrada" ? "ENTRADA DE STOCK" : "SALIDA DE STOCK";
            lblTipo.ForeColor = tipoMovimiento == "Entrada" ? Color.ForestGreen : Color.Firebrick;

            CargarCategorias();
            CalcularTotal();
        }

        private void CargarCategorias()
        {
            var categorias = _categoriaService.ObtenerTodas();
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoriaAlmacen";
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedValue == null) return;
            int idCategoria = (int)cmbCategoria.SelectedValue;
            var productos = _productoService.ObtenerPorCategoria(idCategoria);
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProductoAlmacen";
            cmbProducto_SelectedIndexChanged(cmbProducto, EventArgs.Empty);
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null) return;
            int idProducto = (int)cmbProducto.SelectedValue;
            var medidas = _medidaService.ObtenerPorProducto(idProducto);
            cmbMedida.DataSource = medidas;
            cmbMedida.DisplayMember = "Nombre";
            cmbMedida.ValueMember = "IdMedida";
            CalcularTotal();
        }

        private void cmbMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (cmbMedida.SelectedItem is MedidaProducto medida &&
                int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                decimal total = medida.ValorNumerico * cantidad;
                lblTotalValor.Text = $"{total} {medida.UnidadBase}";
            }
            else
            {
                lblTotalValor.Text = "-";
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmbProducto.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmbMedida.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una medida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var movimiento = new MovimientoAlmacen
                {
                    IdProductoAlmacen = (int)cmbProducto.SelectedValue,
                    IdMedida = (int)cmbMedida.SelectedValue,
                    TipoMovimiento = _tipoMovimiento,
                    CantidadUnidades = cantidad,
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
