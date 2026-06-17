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
                @"SELECT IdMedida, IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase
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
                    UnidadBase = reader.GetString(4)
                });
            }
            return medidas;
        }

        public MedidaProducto? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT IdMedida, IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase
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
                    UnidadBase = reader.GetString(4)
                };
            }
            return null;
        }

        public void Insertar(MedidaProducto medida)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase)
                  VALUES (@IdProducto, @Nombre, @ValorNumerico, @UnidadBase)", conn);
            cmd.Parameters.AddWithValue("@IdProducto", medida.IdProductoAlmacen);
            cmd.Parameters.AddWithValue("@Nombre", medida.Nombre);
            cmd.Parameters.AddWithValue("@ValorNumerico", medida.ValorNumerico);
            cmd.Parameters.AddWithValue("@UnidadBase", medida.UnidadBase);
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
    }
}
