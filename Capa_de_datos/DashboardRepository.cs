using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class DashboardRepository
    {
        private readonly ConexionSQL _conexion;

        public DashboardRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<VentaEntidad> ObtenerVentasPorFecha(DateTime inicio, DateTime fin)
        {
            var ventas = new List<VentaEntidad>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT v.IdVenta, v.IdPedido, v.NombreCliente, v.ApellidoCliente, 
                         v.DNI, v.RUC, v.TipoComprobante, v.MetodoPago, v.MontoTotal, v.Fecha
                  FROM Ventas v
                  WHERE v.Fecha >= @Inicio AND v.Fecha < @Fin
                  ORDER BY v.Fecha DESC", conn);
            cmd.Parameters.AddWithValue("@Inicio", inicio);
            cmd.Parameters.AddWithValue("@Fin", fin);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(new VentaEntidad
                {
                    IdVenta = reader.GetInt32(0),
                    IdPedido = reader.GetInt32(1),
                    NombreCliente = reader.GetString(2),
                    ApellidoCliente = reader.GetString(3),
                    DNI = reader.GetString(4),
                    RUC = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    TipoComprobante = reader.GetString(6),
                    MetodoPago = reader.GetString(7),
                    MontoTotal = reader.GetDecimal(8),
                    Fecha = reader.GetDateTime(9)
                });
            }
            return ventas;
        }

        public decimal ObtenerTotalVentasPorPeriodo(DateTime inicio, DateTime fin)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT ISNULL(SUM(MontoTotal), 0) FROM Ventas 
                  WHERE Fecha >= @Inicio AND Fecha < @Fin", conn);
            cmd.Parameters.AddWithValue("@Inicio", inicio);
            cmd.Parameters.AddWithValue("@Fin", fin);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public int ObtenerCantidadVentasPorPeriodo(DateTime inicio, DateTime fin)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT COUNT(*) FROM Ventas 
                  WHERE Fecha >= @Inicio AND Fecha < @Fin", conn);
            cmd.Parameters.AddWithValue("@Inicio", inicio);
            cmd.Parameters.AddWithValue("@Fin", fin);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
