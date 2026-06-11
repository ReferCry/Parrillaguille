using System.Data;
using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class ProductoRepository
    {
        private readonly ConexionSQL _conexion;

        public ProductoRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdProducto, Nombre, Categoria, Precio, Activo FROM Productos WHERE Activo = 1", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Precio = reader.GetDecimal(3),
                    Activo = reader.GetBoolean(4)
                });
            }
            return productos;
        }

        public List<Producto> ObtenerPorCategoria(string categoria)
        {
            var productos = new List<Producto>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdProducto, Nombre, Categoria, Precio, Activo FROM Productos WHERE Categoria = @Categoria AND Activo = 1", conn);
            cmd.Parameters.AddWithValue("@Categoria", categoria);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Precio = reader.GetDecimal(3),
                    Activo = reader.GetBoolean(4)
                });
            }
            return productos;
        }

        public Producto? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdProducto, Nombre, Categoria, Precio, Activo FROM Productos WHERE IdProducto = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Precio = reader.GetDecimal(3),
                    Activo = reader.GetBoolean(4)
                };
            }
            return null;
        }

        public Producto? ObtenerPorNombre(string nombre)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("SELECT IdProducto, Nombre, Categoria, Precio, Activo FROM Productos WHERE Nombre = @Nombre", conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Precio = reader.GetDecimal(3),
                    Activo = reader.GetBoolean(4)
                };
            }
            return null;
        }

        public void Insertar(Producto producto)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO Productos (Nombre, Categoria, Precio, Activo) VALUES (@Nombre, @Categoria, @Precio, @Activo)", conn);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);
            cmd.Parameters.AddWithValue("@Precio", producto.Precio);
            cmd.Parameters.AddWithValue("@Activo", producto.Activo);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Producto producto)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "UPDATE Productos SET Nombre = @Nombre, Categoria = @Categoria, Precio = @Precio WHERE IdProducto = @Id", conn);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);
            cmd.Parameters.AddWithValue("@Precio", producto.Precio);
            cmd.Parameters.AddWithValue("@Id", producto.IdProducto);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Productos SET Activo = 0 WHERE IdProducto = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
