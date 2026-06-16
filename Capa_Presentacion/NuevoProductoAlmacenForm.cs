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
        private ProductoAlmacen? _productoEditar;

        public NuevoProductoAlmacenForm(ConexionSQL conexion, ProductoAlmacen? producto = null)
        {
            InitializeComponent();
            _conexion = conexion;
            _service = new ProductoAlmacenService(_conexion);
            _categoriaService = new CategoriaAlmacenService(_conexion);
            _productoEditar = producto;

            CargarCategorias();
            CargarUnidades();

            if (_productoEditar != null)
            {
                Text = "Editar Producto - Almacén";
                CargarDatos();
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

        private void CargarUnidades()
        {
            cmbUnidadMedida.Items.AddRange(new object[] { "kg", "g", "litros", "unidades" });
            cmbUnidadMedida.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            txtNombre.Text = _productoEditar!.Nombre;
            cmbCategoria.SelectedValue = _productoEditar.IdCategoriaAlmacen;
            txtCantidad.Text = _productoEditar.Cantidad.ToString();
            cmbUnidadMedida.Text = _productoEditar.UnidadMedida;
            txtPrecio.Text = _productoEditar.PrecioUnitario.ToString();
            txtStockMinimo.Text = _productoEditar.StockMinimo.ToString();
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
                    UnidadMedida = cmbUnidadMedida.Text,
                    PrecioUnitario = decimal.TryParse(txtPrecio.Text, out decimal precio) ? precio : 0,
                    StockMinimo = decimal.TryParse(txtStockMinimo.Text, out decimal min) ? min : 0
                };

                if (_productoEditar != null)
                    _service.ModificarProducto(producto);
                else
                    _service.AgregarProducto(producto);

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
