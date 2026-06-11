using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo;

        public ProductoService(ConexionSQL conexion)
        {
            _repo = new ProductoRepository(conexion);
        }

        public List<Producto> ObtenerTodos() => _repo.ObtenerTodos();

        public List<Producto> ObtenerPorCategoria(string categoria) => _repo.ObtenerPorCategoria(categoria);

        public Producto? ObtenerPorNombre(string nombre) => _repo.ObtenerPorNombre(nombre);

        public void AgregarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");

            _repo.Insertar(producto);
        }

        public void ModificarProducto(Producto producto)
        {
            if (producto.IdProducto <= 0)
                throw new ArgumentException("ID de producto inválido.");
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");

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
