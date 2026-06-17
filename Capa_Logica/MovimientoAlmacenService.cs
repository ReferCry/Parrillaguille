using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class MovimientoAlmacenService
    {
        private readonly MovimientoAlmacenRepository _movimientoRepo;
        private readonly ProductoAlmacenRepository _productoRepo;
        private readonly MedidaProductoRepository _medidaRepo;

        public MovimientoAlmacenService(ConexionSQL conexion)
        {
            _movimientoRepo = new MovimientoAlmacenRepository(conexion);
            _productoRepo = new ProductoAlmacenRepository(conexion);
            _medidaRepo = new MedidaProductoRepository(conexion);
        }

        public List<MovimientoAlmacen> ObtenerPorFecha(DateTime fecha) => _movimientoRepo.ObtenerPorFecha(fecha);

        public List<MovimientoAlmacen> ObtenerPorRangoFechas(DateTime desde, DateTime hasta) => _movimientoRepo.ObtenerPorRangoFechas(desde, hasta);

        public List<MovimientoAlmacen> ObtenerTodos() => _movimientoRepo.ObtenerTodos();

        public void RegistrarMovimiento(MovimientoAlmacen movimiento)
        {
            if (movimiento.IdProductoAlmacen <= 0)
                throw new ArgumentException("Debe seleccionar un producto.");
            if (movimiento.IdMedida <= 0)
                throw new ArgumentException("Debe seleccionar una medida.");
            if (movimiento.CantidadUnidades <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");
            if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
                throw new ArgumentException("Tipo de movimiento inválido. Use: Entrada o Salida.");

            var producto = _productoRepo.ObtenerPorId(movimiento.IdProductoAlmacen);
            if (producto == null)
                throw new ArgumentException("El producto seleccionado no existe.");

            var medida = _medidaRepo.ObtenerPorId(movimiento.IdMedida);
            if (medida == null)
                throw new ArgumentException("La medida seleccionada no existe.");

            decimal totalCambio = medida.ValorNumerico * movimiento.CantidadUnidades;

            if (movimiento.TipoMovimiento == "Salida" && producto.Cantidad < totalCambio)
                throw new InvalidOperationException(
                    $"Stock insuficiente. Disponible: {producto.Cantidad} {producto.UnidadBase}, solicitado: {totalCambio} {producto.UnidadBase}.");

            movimiento.Fecha = DateTime.Now;
            _movimientoRepo.Insertar(movimiento, medida.ValorNumerico);
        }
    }
}
