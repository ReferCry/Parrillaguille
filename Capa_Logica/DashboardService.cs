using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class DashboardService
    {
        private readonly DashboardRepository _repo;

        public DashboardService(ConexionSQL conexion)
        {
            _repo = new DashboardRepository(conexion);
        }

        public List<VentaEntidad> ObtenerVentasPorPeriodo(DateTime inicio, DateTime fin)
            => _repo.ObtenerVentasPorFecha(inicio, fin);

        public decimal ObtenerTotalVentas(DateTime inicio, DateTime fin)
            => _repo.ObtenerTotalVentasPorPeriodo(inicio, fin);

        public int ObtenerCantidadVentas(DateTime inicio, DateTime fin)
            => _repo.ObtenerCantidadVentasPorPeriodo(inicio, fin);
    }
}
