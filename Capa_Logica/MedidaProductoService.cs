using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class MedidaProductoService
    {
        private readonly MedidaProductoRepository _repo;

        public MedidaProductoService(ConexionSQL conexion)
        {
            _repo = new MedidaProductoRepository(conexion);
        }

        public List<MedidaProducto> ObtenerPorProducto(int idProducto) => _repo.ObtenerPorProducto(idProducto);

        public MedidaProducto? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public void AgregarMedida(MedidaProducto medida)
        {
            if (medida.IdProductoAlmacen <= 0)
                throw new ArgumentException("ID de producto inválido.");
            if (string.IsNullOrWhiteSpace(medida.Nombre))
                throw new ArgumentException("El nombre de la medida es obligatorio.");
            if (medida.ValorNumerico <= 0)
                throw new ArgumentException("El valor numérico debe ser mayor a 0.");
            if (string.IsNullOrWhiteSpace(medida.UnidadBase))
                throw new ArgumentException("La unidad base es obligatoria.");

            _repo.Insertar(medida);
        }

        public void EliminarMedida(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID de medida inválido.");
            _repo.Eliminar(id);
        }

        public void ReemplazarMedidas(int idProducto, List<MedidaProducto> nuevasMedidas)
        {
            var viejas = _repo.ObtenerPorProducto(idProducto);
            var idsConMovimientos = _repo.ObtenerIdsConMovimientos(idProducto);

            var idsNuevos = nuevasMedidas
                .Where(m => m.IdMedida > 0)
                .Select(m => m.IdMedida)
                .ToHashSet();

            var idsAEliminar = viejas
                .Where(v => !idsNuevos.Contains(v.IdMedida) && !idsConMovimientos.Contains(v.IdMedida))
                .Select(v => v.IdMedida)
                .ToList();

            if (idsAEliminar.Count > 0)
                _repo.EliminarPorIds(idsAEliminar);

            foreach (var medida in nuevasMedidas)
            {
                medida.IdProductoAlmacen = idProducto;
                if (medida.IdMedida > 0)
                {
                    _repo.Actualizar(medida);
                }
                else
                {
                    _repo.Insertar(medida);
                }
            }
        }
    }
}
