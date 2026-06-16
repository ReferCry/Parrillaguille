using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class MovimientoAlmacenService
    {
        private readonly MovimientoAlmacenRepository _movimientoRepo;
        private readonly ProductoAlmacenRepository _productoRepo;

        public MovimientoAlmacenService(ConexionSQL conexion)
        {
            _movimientoRepo = new MovimientoAlmacenRepository(conexion);
            _productoRepo = new ProductoAlmacenRepository(conexion);
        }

        public List<MovimientoAlmacen> ObtenerPorFecha(DateTime fecha) => _movimientoRepo.ObtenerPorFecha(fecha);

        public List<MovimientoAlmacen> ObtenerPorRangoFechas(DateTime desde, DateTime hasta) => _movimientoRepo.ObtenerPorRangoFechas(desde, hasta);

        public List<MovimientoAlmacen> ObtenerTodos() => _movimientoRepo.ObtenerTodos();

        public void RegistrarMovimiento(MovimientoAlmacen movimiento)
        {
            if (movimiento.IdProductoAlmacen <= 0)
                throw new ArgumentException("Debe seleccionar un producto.");
            if (movimiento.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");
            if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
                throw new ArgumentException("Tipo de movimiento inválido. Use: Entrada o Salida.");

            var producto = _productoRepo.ObtenerPorId(movimiento.IdProductoAlmacen);
            if (producto == null)
                throw new ArgumentException("El producto seleccionado no existe.");

            if (movimiento.TipoMovimiento == "Salida" && producto.Cantidad < movimiento.Cantidad)
                throw new InvalidOperationException(
                    $"Stock insuficiente. Disponible: {producto.Cantidad} {producto.UnidadMedida}, solicitado: {movimiento.Cantidad}.");

            movimiento.Fecha = DateTime.Now;
            _movimientoRepo.Insertar(movimiento);
        }
    }
}
