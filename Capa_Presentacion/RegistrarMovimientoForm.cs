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
        private decimal _precioUnitario;

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
            if (cmbCategoria.SelectedValue is not int idCategoria) return;
            var productos = _productoService.ObtenerPorCategoria(idCategoria);
            cmbProducto.DataSource = productos;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProductoAlmacen";
            cmbProducto_SelectedIndexChanged(cmbProducto, EventArgs.Empty);
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null) return;
            if (cmbProducto.SelectedValue is not int idProducto) return;
            var medidas = _medidaService.ObtenerPorProducto(idProducto);
            cmbMedida.DataSource = medidas;
            cmbMedida.DisplayMember = "Nombre";
            cmbMedida.ValueMember = "IdMedida";
            cmbMedida_SelectedIndexChanged(cmbMedida, EventArgs.Empty);
        }

        private void cmbMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMedida.SelectedValue is int idMedida && cmbMedida.DataSource is List<MedidaProducto> medidas)
            {
                var medida = medidas.FirstOrDefault(m => m.IdMedida == idMedida);
                if (medida != null)
                {
                    _precioUnitario = medida.PrecioUnitario;
                    txtPrecio.Text = medida.PrecioUnitario.ToString("N2");
                }
            }
            CalcularTotal();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPrecio.Text, out decimal precio) && precio >= 0)
                _precioUnitario = precio;
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0 && _precioUnitario > 0)
            {
                decimal total = _precioUnitario * cantidad;
                if (_tipoMovimiento == "Entrada")
                    lblTotalValor.Text = $"- S/. {total:N2}";
                else
                    lblTotalValor.Text = $"S/. {total:N2}";
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
