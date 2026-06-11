using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class VentaService
    {
        private readonly VentaRepository _ventaRepo;
        private readonly PedidoRepository _pedidoRepo;

        public VentaService(ConexionSQL conexion)
        {
            _ventaRepo = new VentaRepository(conexion);
            _pedidoRepo = new PedidoRepository(conexion);
        }

        public int RegistrarVenta(VentaEntidad venta)
        {
            if (string.IsNullOrWhiteSpace(venta.NombreCliente))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            if (venta.MontoTotal <= 0)
                throw new ArgumentException("El monto total debe ser mayor a 0.");
            if (venta.TipoComprobante == "Factura" && string.IsNullOrWhiteSpace(venta.RUC))
                throw new ArgumentException("El RUC es obligatorio para facturas.");

            return _ventaRepo.Insertar(venta);
        }

        public List<VentaEntidad> ObtenerTodas() => _ventaRepo.ObtenerTodas();

        public VentaEntidad? ObtenerPorId(int id) => _ventaRepo.ObtenerPorId(id);
    }
}
