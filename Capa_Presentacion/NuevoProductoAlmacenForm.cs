using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class NuevoProductoAlmacenForm : Form
    {
        private readonly ConexionSQL _conexion;
        private readonly ProductoAlmacenService _service;
        private readonly CategoriaAlmacenService _categoriaService;
        private readonly MedidaProductoService _medidaService;
        private ProductoAlmacen? _productoEditar;
        private readonly List<MedidaProducto> _medidasNuevas = new();

        public NuevoProductoAlmacenForm(ConexionSQL conexion, ProductoAlmacen? producto = null)
        {
            InitializeComponent();
            _conexion = conexion;
            _service = new ProductoAlmacenService(_conexion);
            _categoriaService = new CategoriaAlmacenService(_conexion);
            _medidaService = new MedidaProductoService(_conexion);
            _productoEditar = producto;

            CargarCategorias();
            CargarUnidadesMedida();

            if (_productoEditar != null)
            {
                Text = "Editar Producto - Almacén";
                CargarDatos();
                CargarMedidas();
            }
            else
            {
                Text = "Nuevo Producto - Almacén";
            }
        }

        private void CargarCategorias()
        {
            var categorias = _categoriaService.ObtenerTodas();
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoriaAlmacen";
        }

        private void CargarUnidadesMedida()
        {
            cmbMedidaUnidadBase.Items.AddRange(new object[] { "kg", "litros", "unidades" });
            cmbMedidaUnidadBase.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            txtNombre.Text = _productoEditar!.Nombre;
            cmbCategoria.SelectedValue = _productoEditar.IdCategoriaAlmacen;
            txtCantidad.Text = _productoEditar.Cantidad.ToString();
            txtStockMinimo.Text = _productoEditar.StockMinimo.ToString();
        }

        private void CargarMedidas()
        {
            if (_productoEditar == null) return;
            var medidas = _medidaService.ObtenerPorProducto(_productoEditar.IdProductoAlmacen);
            _medidasNuevas.Clear();
            lstMedidas.Items.Clear();
            foreach (var m in medidas)
            {
                _medidasNuevas.Add(m);
                lstMedidas.Items.Add($"{m.Nombre} ({m.ValorNumerico} {m.UnidadBase}) - S/. {m.PrecioUnitario:N2}");
            }
        }

        private void btnAgregarMedida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedidaNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la medida (ej: 2 litros).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!decimal.TryParse(txtMedidaValor.Text, out decimal valor) || valor <= 0)
            {
                MessageBox.Show("Ingrese un valor numérico válido mayor a 0.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!decimal.TryParse(txtMedidaPrecio.Text, out decimal precio) || precio < 0)
            {
                MessageBox.Show("Ingrese un precio válido (>= 0).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var medida = new MedidaProducto
            {
                Nombre = txtMedidaNombre.Text.Trim(),
                ValorNumerico = valor,
                UnidadBase = cmbMedidaUnidadBase.Text,
                PrecioUnitario = precio
            };

            _medidasNuevas.Add(medida);
            lstMedidas.Items.Add($"{medida.Nombre} ({medida.ValorNumerico} {medida.UnidadBase}) - S/. {medida.PrecioUnitario:N2}");

            txtMedidaNombre.Text = "";
            txtMedidaValor.Text = "";
            txtMedidaPrecio.Text = "";
        }

        private void btnQuitarMedida_Click(object sender, EventArgs e)
        {
            if (lstMedidas.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una medida para quitar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _medidasNuevas.RemoveAt(lstMedidas.SelectedIndex);
            lstMedidas.Items.RemoveAt(lstMedidas.SelectedIndex);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = new ProductoAlmacen
                {
                    IdProductoAlmacen = _productoEditar?.IdProductoAlmacen ?? 0,
                    Nombre = txtNombre.Text.Trim(),
                    IdCategoriaAlmacen = (int)cmbCategoria.SelectedValue!,
                    Cantidad = decimal.TryParse(txtCantidad.Text, out decimal cant) ? cant : 0,
                    StockMinimo = decimal.TryParse(txtStockMinimo.Text, out decimal min) ? min : 0
                };

                if (_productoEditar != null)
                    _service.ModificarProducto(producto);
                else
                    _service.AgregarProducto(producto);

                if (_medidasNuevas.Count > 0)
                {
                    int idProducto = _productoEditar?.IdProductoAlmacen ?? ObtenerUltimoId();
                    _medidaService.ReemplazarMedidas(idProducto, _medidasNuevas);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerUltimoId()
        {
            var productos = _service.ObtenerTodos();
            return productos.Max(p => p.IdProductoAlmacen);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
