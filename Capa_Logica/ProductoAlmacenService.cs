using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class ProductoAlmacenService
    {
        private readonly ProductoAlmacenRepository _repo;

        public ProductoAlmacenService(ConexionSQL conexion)
        {
            _repo = new ProductoAlmacenRepository(conexion);
        }

        public List<ProductoAlmacen> ObtenerTodos() => _repo.ObtenerTodos();

        public ProductoAlmacen? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public List<ProductoAlmacen> ObtenerPorCategoria(int idCategoria) => _repo.ObtenerPorCategoria(idCategoria);

        public void AgregarProducto(ProductoAlmacen producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.IdCategoriaAlmacen <= 0)
                throw new ArgumentException("Debe seleccionar una categoría.");
            if (producto.Cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");
            if (producto.PrecioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");
            producto.FechaUltimaActualizacion = DateTime.Now;
            _repo.Insertar(producto);
        }

        public void ModificarProducto(ProductoAlmacen producto)
        {
            if (producto.IdProductoAlmacen <= 0)
                throw new ArgumentException("ID de producto inválido.");
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.IdCategoriaAlmacen <= 0)
                throw new ArgumentException("Debe seleccionar una categoría.");
            if (producto.Cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");
            if (producto.PrecioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            producto.FechaUltimaActualizacion = DateTime.Now;
            _repo.Actualizar(producto);
        }

        public void EliminarProducto(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID de producto inválido.");

            _repo.Eliminar(id);
        }
    }
}
