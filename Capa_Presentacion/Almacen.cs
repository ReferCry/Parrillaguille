using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class Almacen : Form
    {
        private readonly ConexionSQL _conexion;
        private readonly ProductoAlmacenService _productoService;
        private readonly CategoriaAlmacenService _categoriaService;
        private readonly MovimientoAlmacenService _movimientoService;

        public Almacen()
        {
            InitializeComponent();
            _conexion = new ConexionSQL();
            _productoService = new ProductoAlmacenService(_conexion);
            _categoriaService = new CategoriaAlmacenService(_conexion);
            _movimientoService = new MovimientoAlmacenService(_conexion);

            CargarCategorias();
            CargarProductos();
            dateTimePickerFecha.Value = DateTime.Today;
        }

        private void CargarCategorias()
        {
            var categorias = _categoriaService.ObtenerTodas();
            categorias.Insert(0, new Capa_Entidad.CategoriaAlmacen { IdCategoriaAlmacen = 0, Nombre = "Todas" });
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoriaAlmacen";
        }

        private void CargarProductos()
        {
            var productos = _productoService.ObtenerTodos();
            var filtroCategoria = (int)cmbCategoria.SelectedValue;
            var filtroBusqueda = txtBuscar.Text.Trim().ToLower();

            if (filtroCategoria > 0)
                productos = productos.Where(p => p.IdCategoriaAlmacen == filtroCategoria).ToList();

            if (!string.IsNullOrEmpty(filtroBusqueda))
                productos = productos.Where(p => p.Nombre.ToLower().Contains(filtroBusqueda)).ToList();

            dgvProductos.DataSource = productos.Select(p => new
            {
                p.IdProductoAlmacen,
                p.Nombre,
                Categoria = p.NombreCategoria,
                Stock = $"{p.Cantidad} {p.UnidadBase}",
                Precio = p.PrecioUnitario,
                StockMínimo = $"{p.StockMinimo} {p.UnidadBase}",
                ÚltimaActualización = p.FechaUltimaActualizacion.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            dgvProductos.Columns["IdProductoAlmacen"].Visible = false;

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                string stockStr = row.Cells["Stock"].Value?.ToString() ?? "0";
                string minStr = row.Cells["StockMínimo"].Value?.ToString() ?? "0";
                decimal stock = decimal.TryParse(stockStr.Split(' ')[0], out decimal s) ? s : 0;
                decimal min = decimal.TryParse(minStr.Split(' ')[0], out decimal m) ? m : 0;
                if (stock <= min)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }
        }

        private void CargarMovimientos()
        {
            var fecha = dateTimePickerFecha.Value;
            var movimientos = _movimientoService.ObtenerPorFecha(fecha);

            dgvMovimientos.DataSource = movimientos.Select(m => new
            {
                m.IdMovimiento,
                Producto = m.NombreProducto,
                Medida = m.NombreMedida,
                Tipo = m.TipoMovimiento,
                Unidades = m.CantidadUnidades,
                Total = $"{m.Total} {(m.NombreMedida.Contains("litro") ? "litros" : m.NombreMedida.Contains("Kg") ? "kg" : "unidades")}",
                Hora = m.Fecha.ToString("HH:mm"),
                m.Observacion
            }).ToList();

            dgvMovimientos.Columns["IdMovimiento"].Visible = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using var form = new NuevoProductoAlmacenForm(_conexion);
            if (form.ShowDialog() == DialogResult.OK)
                CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProductoAlmacen"].Value);
            var producto = _productoService.ObtenerPorId(id);
            if (producto == null) return;

            using var form = new NuevoProductoAlmacenForm(_conexion, producto);
            if (form.ShowDialog() == DialogResult.OK)
                CargarProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProductoAlmacen"].Value);
            string nombre = dgvProductos.SelectedRows[0].Cells["Nombre"].Value.ToString()!;

            var result = MessageBox.Show(
                $"¿Está seguro de eliminar el producto '{nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _productoService.EliminarProducto(id);
                CargarProductos();
            }
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            using var form = new RegistrarMovimientoForm(_conexion, "Entrada");
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
                if (tabControl.SelectedIndex == 1)
                    CargarMovimientos();
            }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            using var form = new RegistrarMovimientoForm(_conexion, "Salida");
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarProductos();
                if (tabControl.SelectedIndex == 1)
                    CargarMovimientos();
            }
        }

        private void btnVerMovimientos_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            CargarMovimientos();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            var form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
                CargarMovimientos();
        }

        private void btnCargarMovimientos_Click(object sender, EventArgs e)
        {
            CargarMovimientos();
        }
    }
}
