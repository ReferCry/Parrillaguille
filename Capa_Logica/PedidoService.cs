using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly DetallePedidoRepository _detalleRepo;
        private readonly MesaRepository _mesaRepo;

        public PedidoService(ConexionSQL conexion)
        {
            _pedidoRepo = new PedidoRepository(conexion);
            _detalleRepo = new DetallePedidoRepository(conexion);
            _mesaRepo = new MesaRepository(conexion);
        }

        public int AbrirPedido(Pedido pedido)
        {
            pedido.Estado = "Abierto";
            pedido.Fecha = DateTime.Now;
            int id = _pedidoRepo.Insertar(pedido);

            if (pedido.IdMesa.HasValue)
                _mesaRepo.ActualizarEstado(pedido.IdMesa.Value, "Ocupada");

            return id;
        }

        public void AgregarDetalle(int idPedido, DetallePedido detalle)
        {
            if (detalle.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            detalle.IdPedido = idPedido;
            _detalleRepo.Insertar(detalle);
            RecalcularTotal(idPedido);
        }

        public void EliminarDetalle(int idDetalle, int idPedido)
        {
            _detalleRepo.Eliminar(idDetalle);
            RecalcularTotal(idPedido);
        }

        public List<DetallePedido> ObtenerDetalles(int idPedido)
            => _detalleRepo.ObtenerPorPedido(idPedido);

        public Pedido? ObtenerPedido(int id) => _pedidoRepo.ObtenerPorId(id);

        public List<Pedido> ObtenerPedidosAbiertos() => _pedidoRepo.ObtenerAbiertos();

        public void CerrarPedido(int idPedido, int? idMesa = null)
        {
            _pedidoRepo.CerrarPedido(idPedido);
            if (idMesa.HasValue)
                _mesaRepo.ActualizarEstado(idMesa.Value, "Libre");
        }

        private void RecalcularTotal(int idPedido)
        {
            var detalles = _detalleRepo.ObtenerPorPedido(idPedido);
            decimal total = detalles.Sum(d => d.Subtotal);
            _pedidoRepo.ActualizarTotal(idPedido, total);
        }
    }
}
