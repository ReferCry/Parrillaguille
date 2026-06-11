using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class MesaRepository
    {
        private readonly ConexionSQL _conexion;

        public MesaRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<Mesa> ObtenerTodas()
        {
            var mesas = new List<Mesa>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdMesa, Numero, Estado FROM Mesas ORDER BY Numero", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                mesas.Add(new Mesa
                {
                    IdMesa = reader.GetInt32(0),
                    Numero = reader.GetInt32(1),
                    Estado = reader.GetString(2)
                });
            }
            return mesas;
        }

        public void ActualizarEstado(int idMesa, string estado)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Mesas SET Estado = @Estado WHERE IdMesa = @Id", conn);
            cmd.Parameters.AddWithValue("@Estado", estado);
            cmd.Parameters.AddWithValue("@Id", idMesa);
            cmd.ExecuteNonQuery();
        }
    }
}
