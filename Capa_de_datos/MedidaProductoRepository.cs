using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class MedidaProductoRepository
    {
        private readonly ConexionSQL _conexion;

        public MedidaProductoRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<MedidaProducto> ObtenerPorProducto(int idProducto)
        {
            var medidas = new List<MedidaProducto>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT IdMedida, IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario
                  FROM MedidasProducto
                  WHERE IdProductoAlmacen = @IdProducto
                  ORDER BY ValorNumerico DESC", conn);
            cmd.Parameters.AddWithValue("@IdProducto", idProducto);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                medidas.Add(new MedidaProducto
                {
                    IdMedida = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    Nombre = reader.GetString(2),
                    ValorNumerico = reader.GetDecimal(3),
                    UnidadBase = reader.GetString(4),
                    PrecioUnitario = reader.GetDecimal(5)
                });
            }
            return medidas;
        }

        public MedidaProducto? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT IdMedida, IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario
                  FROM MedidasProducto
                  WHERE IdMedida = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new MedidaProducto
                {
                    IdMedida = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    Nombre = reader.GetString(2),
                    ValorNumerico = reader.GetDecimal(3),
                    UnidadBase = reader.GetString(4),
                    PrecioUnitario = reader.GetDecimal(5)
                };
            }
            return null;
        }

        public void Insertar(MedidaProducto medida)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario)
                  VALUES (@IdProducto, @Nombre, @ValorNumerico, @UnidadBase, @PrecioUnitario)", conn);
            cmd.Parameters.AddWithValue("@IdProducto", medida.IdProductoAlmacen);
            cmd.Parameters.AddWithValue("@Nombre", medida.Nombre);
            cmd.Parameters.AddWithValue("@ValorNumerico", medida.ValorNumerico);
            cmd.Parameters.AddWithValue("@UnidadBase", medida.UnidadBase);
            cmd.Parameters.AddWithValue("@PrecioUnitario", medida.PrecioUnitario);
            cmd.ExecuteNonQuery();
        }

        public void EliminarPorProducto(int idProducto)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "DELETE FROM MedidasProducto WHERE IdProductoAlmacen = @IdProducto", conn);
            cmd.Parameters.AddWithValue("@IdProducto", idProducto);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "DELETE FROM MedidasProducto WHERE IdMedida = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(MedidaProducto medida)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE MedidasProducto
                  SET Nombre = @Nombre, ValorNumerico = @ValorNumerico, UnidadBase = @UnidadBase, PrecioUnitario = @PrecioUnitario
                  WHERE IdMedida = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", medida.IdMedida);
            cmd.Parameters.AddWithValue("@Nombre", medida.Nombre);
            cmd.Parameters.AddWithValue("@ValorNumerico", medida.ValorNumerico);
            cmd.Parameters.AddWithValue("@UnidadBase", medida.UnidadBase);
            cmd.Parameters.AddWithValue("@PrecioUnitario", medida.PrecioUnitario);
            cmd.ExecuteNonQuery();
        }

        public List<int> ObtenerIdsConMovimientos(int idProducto)
        {
            var ids = new List<int>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT DISTINCT m.IdMedida
                  FROM MovimientosAlmacen m
                  INNER JOIN MedidasProducto med ON m.IdMedida = med.IdMedida
                  WHERE med.IdProductoAlmacen = @IdProducto", conn);
            cmd.Parameters.AddWithValue("@IdProducto", idProducto);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }
            return ids;
        }

        public void EliminarPorIds(List<int> ids)
        {
            if (ids.Count == 0) return;
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            var parametros = string.Join(",", ids.Select((id, i) => $"@Id{i}"));
            using var cmd = new SqlCommand(
                $"DELETE FROM MedidasProducto WHERE IdMedida IN ({parametros})", conn);
            for (int i = 0; i < ids.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@Id{i}", ids[i]);
            }
            cmd.ExecuteNonQuery();
        }
    }
}
