using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class VentaRepository
    {
        private readonly ConexionSQL _conexion;

        public VentaRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public int Insertar(VentaEntidad venta)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha)
                  VALUES (@IdPedido, @NombreCliente, @ApellidoCliente, @DNI, @RUC, @TipoComprobante, @MetodoPago, @MontoTotal, @Fecha);
                  SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@IdPedido", venta.IdPedido);
            cmd.Parameters.AddWithValue("@NombreCliente", venta.NombreCliente);
            cmd.Parameters.AddWithValue("@ApellidoCliente", venta.ApellidoCliente);
            cmd.Parameters.AddWithValue("@DNI", venta.DNI);
            cmd.Parameters.AddWithValue("@RUC", (object)venta.RUC ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TipoComprobante", venta.TipoComprobante);
            cmd.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
            cmd.Parameters.AddWithValue("@MontoTotal", venta.MontoTotal);
            cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<VentaEntidad> ObtenerTodas()
        {
            var ventas = new List<VentaEntidad>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT IdVenta, IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha FROM Ventas ORDER BY Fecha DESC", conn);
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

        public VentaEntidad? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT IdVenta, IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha FROM Ventas WHERE IdVenta = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new VentaEntidad
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
                };
            }
            return null;
        }
    }
}
