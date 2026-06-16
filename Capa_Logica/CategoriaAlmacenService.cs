using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class CategoriaAlmacenService
    {
        private readonly CategoriaAlmacenRepository _repo;

        public CategoriaAlmacenService(ConexionSQL conexion)
        {
            _repo = new CategoriaAlmacenRepository(conexion);
        }

        public List<CategoriaAlmacen> ObtenerTodas() => _repo.ObtenerTodas();

        public CategoriaAlmacen? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public void AgregarCategoria(CategoriaAlmacen categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            _repo.Insertar(categoria);
        }

        public void ModificarCategoria(CategoriaAlmacen categoria)
        {
            if (categoria.IdCategoriaAlmacen <= 0)
                throw new ArgumentException("ID de categoría inválido.");
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            _repo.Actualizar(categoria);
        }

        public void EliminarCategoria(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID de categoría inválido.");

            if (_repo.TieneProductosAsociados(id))
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos asociados.");

            _repo.Eliminar(id);
        }
    }
}
