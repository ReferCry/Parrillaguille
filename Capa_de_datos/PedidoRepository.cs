using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class PedidoRepository
    {
        private readonly ConexionSQL _conexion;

        public PedidoRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public int Insertar(Pedido pedido)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total)
                  VALUES (@IdMesa, @TipoAtencion, @Fecha, @Estado, @Total);
                  SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@IdMesa", pedido.IdMesa ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@TipoAtencion", pedido.TipoAtencion);
            cmd.Parameters.AddWithValue("@Fecha", pedido.Fecha);
            cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
            cmd.Parameters.AddWithValue("@Total", pedido.Total);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void ActualizarTotal(int idPedido, decimal total)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Pedidos SET Total = @Total WHERE IdPedido = @Id", conn);
            cmd.Parameters.AddWithValue("@Total", total);
            cmd.Parameters.AddWithValue("@Id", idPedido);
            cmd.ExecuteNonQuery();
        }

        public void CerrarPedido(int idPedido)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Pedidos SET Estado = 'Cerrado' WHERE IdPedido = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", idPedido);
            cmd.ExecuteNonQuery();
        }

        public Pedido? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT IdPedido, IdMesa, TipoAtencion, Fecha, Estado, Total FROM Pedidos WHERE IdPedido = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Pedido
                {
                    IdPedido = reader.GetInt32(0),
                    IdMesa = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    TipoAtencion = reader.GetString(2),
                    Fecha = reader.GetDateTime(3),
                    Estado = reader.GetString(4),
                    Total = reader.GetDecimal(5)
                };
            }
            return null;
        }

        public List<Pedido> ObtenerAbiertos()
        {
            var pedidos = new List<Pedido>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT IdPedido, IdMesa, TipoAtencion, Fecha, Estado, Total FROM Pedidos WHERE Estado = 'Abierto' ORDER BY Fecha DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pedidos.Add(new Pedido
                {
                    IdPedido = reader.GetInt32(0),
                    IdMesa = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    TipoAtencion = reader.GetString(2),
                    Fecha = reader.GetDateTime(3),
                    Estado = reader.GetString(4),
                    Total = reader.GetDecimal(5)
                });
            }
            return pedidos;
        }
    }
}
