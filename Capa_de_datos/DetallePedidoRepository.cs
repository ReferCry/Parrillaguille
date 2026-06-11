using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class DetallePedidoRepository
    {
        private readonly ConexionSQL _conexion;

        public DetallePedidoRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public void Insertar(DetallePedido detalle)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario)
                  VALUES (@IdPedido, @IdProducto, @NombreProducto, @Cantidad, @PrecioUnitario)", conn);
            cmd.Parameters.AddWithValue("@IdPedido", detalle.IdPedido);
            cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
            cmd.Parameters.AddWithValue("@NombreProducto", detalle.NombreProducto);
            cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
            cmd.ExecuteNonQuery();
        }

        public List<DetallePedido> ObtenerPorPedido(int idPedido)
        {
            var detalles = new List<DetallePedido>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT IdDetalle, IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario FROM DetallePedidos WHERE IdPedido = @IdPedido", conn);
            cmd.Parameters.AddWithValue("@IdPedido", idPedido);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detalles.Add(new DetallePedido
                {
                    IdDetalle = reader.GetInt32(0),
                    IdPedido = reader.GetInt32(1),
                    IdProducto = reader.GetInt32(2),
                    NombreProducto = reader.GetString(3),
                    Cantidad = reader.GetInt32(4),
                    PrecioUnitario = reader.GetDecimal(5)
                });
            }
            return detalles;
        }

        public void Eliminar(int idDetalle)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM DetallePedidos WHERE IdDetalle = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", idDetalle);
            cmd.ExecuteNonQuery();
        }

        public void EliminarPorPedido(int idPedido)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM DetallePedidos WHERE IdPedido = @IdPedido", conn);
            cmd.Parameters.AddWithValue("@IdPedido", idPedido);
            cmd.ExecuteNonQuery();
        }
    }
}
