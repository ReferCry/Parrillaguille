using Capa_Entidad;

namespace Capa_Presentacion
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            cboCategoria.Items.Clear();
            foreach (var categoria in MenuData.ObtenerCategorias())
            {
                cboCategoria.Items.Add(categoria);
            }
            if (cboCategoria.Items.Count > 0)
                cboCategoria.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoria.SelectedItem == null) return;

            string categoria = cboCategoria.SelectedItem.ToString()!;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Producto", "Producto");
            dataGridView1.Columns.Add("Precio", "Precio (S/)");

            foreach (var producto in MenuData.ObtenerPlatos(categoria))
            {
                dataGridView1.Rows.Add(producto, "---");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim();
            string precioTexto = textBox2.Text.Trim();
            string categoria = cboCategoria.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingrese el nombre del producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a 0.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(categoria))
            {
                MessageBox.Show("Seleccione una categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var conexion = new Capa_de_datos.ConexionSQL();
                var service = new Capa_Logica.ProductoService(conexion);
                service.AgregarProducto(new Producto
                {
                    Nombre = nombre,
                    Categoria = categoria,
                    Precio = precio
                });

                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
                comboBox1_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dataGridView1.CurrentRow.Cells["Producto"].Value?.ToString() ?? "";

            var result = MessageBox.Show($"¿Eliminar '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                var conexion = new Capa_de_datos.ConexionSQL();
                var service = new Capa_Logica.ProductoService(conexion);
                var producto = service.ObtenerPorNombre(nombre);

                if (producto != null)
                {
                    service.EliminarProducto(producto.IdProducto);
                    MessageBox.Show("Producto eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dataGridView1.CurrentRow.Cells["Producto"].Value?.ToString() ?? "";
            string precioTexto = textBox2.Text.Trim();
            string categoria = cboCategoria.SelectedItem?.ToString() ?? "";

            if (!decimal.TryParse(precioTexto, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio válido en el campo de precio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var conexion = new Capa_de_datos.ConexionSQL();
                var service = new Capa_Logica.ProductoService(conexion);
                var producto = service.ObtenerPorNombre(nombre);

                if (producto != null)
                {
                    producto.Precio = precio;
                    if (!string.IsNullOrEmpty(categoria))
                        producto.Categoria = categoria;

                    service.ModificarProducto(producto);
                    MessageBox.Show("Producto modificado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
