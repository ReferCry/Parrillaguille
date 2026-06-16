using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class CategoriaAlmacenRepository
    {
        private readonly ConexionSQL _conexion;

        public CategoriaAlmacenRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<CategoriaAlmacen> ObtenerTodas()
        {
            var categorias = new List<CategoriaAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdCategoriaAlmacen, Nombre, Descripcion FROM CategoriasAlmacen ORDER BY Nombre", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categorias.Add(new CategoriaAlmacen
                {
                    IdCategoriaAlmacen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                });
            }
            return categorias;
        }

        public CategoriaAlmacen? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdCategoriaAlmacen, Nombre, Descripcion FROM CategoriasAlmacen WHERE IdCategoriaAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new CategoriaAlmacen
                {
                    IdCategoriaAlmacen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                };
            }
            return null;
        }

        public void Insertar(CategoriaAlmacen categoria)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO CategoriasAlmacen (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)", conn);
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)categoria.Descripcion ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(CategoriaAlmacen categoria)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "UPDATE CategoriasAlmacen SET Nombre = @Nombre, Descripcion = @Descripcion WHERE IdCategoriaAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", (object?)categoria.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", categoria.IdCategoriaAlmacen);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM CategoriasAlmacen WHERE IdCategoriaAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public bool TieneProductosAsociados(int idCategoria)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM ProductosAlmacen WHERE IdCategoriaAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", idCategoria);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
