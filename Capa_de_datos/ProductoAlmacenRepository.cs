using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class ProductoAlmacenRepository
    {
        private readonly ConexionSQL _conexion;

        public ProductoAlmacenRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<ProductoAlmacen> ObtenerTodos()
        {
            var productos = new List<ProductoAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT p.IdProductoAlmacen, p.Nombre, p.IdCategoriaAlmacen, c.Nombre,
                         p.Cantidad, p.UnidadBase, p.PrecioUnitario, p.StockMinimo,
                         p.FechaUltimaActualizacion, p.Activo
                  FROM ProductosAlmacen p
                  INNER JOIN CategoriasAlmacen c ON p.IdCategoriaAlmacen = c.IdCategoriaAlmacen
                  WHERE p.Activo = 1
                  ORDER BY c.Nombre, p.Nombre", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new ProductoAlmacen
                {
                    IdProductoAlmacen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    IdCategoriaAlmacen = reader.GetInt32(2),
                    NombreCategoria = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    UnidadBase = reader.GetString(5),
                    PrecioUnitario = reader.GetDecimal(6),
                    StockMinimo = reader.GetDecimal(7),
                    FechaUltimaActualizacion = reader.GetDateTime(8),
                    Activo = reader.GetBoolean(9)
                });
            }
            return productos;
        }

        public ProductoAlmacen? ObtenerPorId(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT p.IdProductoAlmacen, p.Nombre, p.IdCategoriaAlmacen, c.Nombre,
                         p.Cantidad, p.UnidadBase, p.PrecioUnitario, p.StockMinimo,
                         p.FechaUltimaActualizacion, p.Activo
                  FROM ProductosAlmacen p
                  INNER JOIN CategoriasAlmacen c ON p.IdCategoriaAlmacen = c.IdCategoriaAlmacen
                  WHERE p.IdProductoAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ProductoAlmacen
                {
                    IdProductoAlmacen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    IdCategoriaAlmacen = reader.GetInt32(2),
                    NombreCategoria = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    UnidadBase = reader.GetString(5),
                    PrecioUnitario = reader.GetDecimal(6),
                    StockMinimo = reader.GetDecimal(7),
                    FechaUltimaActualizacion = reader.GetDateTime(8),
                    Activo = reader.GetBoolean(9)
                };
            }
            return null;
        }

        public List<ProductoAlmacen> ObtenerPorCategoria(int idCategoria)
        {
            var productos = new List<ProductoAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT p.IdProductoAlmacen, p.Nombre, p.IdCategoriaAlmacen, c.Nombre,
                         p.Cantidad, p.UnidadBase, p.PrecioUnitario, p.StockMinimo,
                         p.FechaUltimaActualizacion, p.Activo
                  FROM ProductosAlmacen p
                  INNER JOIN CategoriasAlmacen c ON p.IdCategoriaAlmacen = c.IdCategoriaAlmacen
                  WHERE p.IdCategoriaAlmacen = @IdCategoria AND p.Activo = 1
                  ORDER BY p.Nombre", conn);
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new ProductoAlmacen
                {
                    IdProductoAlmacen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    IdCategoriaAlmacen = reader.GetInt32(2),
                    NombreCategoria = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    UnidadBase = reader.GetString(5),
                    PrecioUnitario = reader.GetDecimal(6),
                    StockMinimo = reader.GetDecimal(7),
                    FechaUltimaActualizacion = reader.GetDateTime(8),
                    Activo = reader.GetBoolean(9)
                });
            }
            return productos;
        }

        public void Insertar(ProductoAlmacen producto)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO ProductosAlmacen
                  (Nombre, IdCategoriaAlmacen, Cantidad, UnidadBase, PrecioUnitario, StockMinimo, FechaUltimaActualizacion, Activo)
                  VALUES
                  (@Nombre, @IdCategoriaAlmacen, @Cantidad, @UnidadBase, @PrecioUnitario, @StockMinimo, @FechaUltimaActualizacion, @Activo)", conn);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@IdCategoriaAlmacen", producto.IdCategoriaAlmacen);
            cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
            cmd.Parameters.AddWithValue("@UnidadBase", producto.UnidadBase);
            cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@FechaUltimaActualizacion", producto.FechaUltimaActualizacion);
            cmd.Parameters.AddWithValue("@Activo", producto.Activo);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(ProductoAlmacen producto)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE ProductosAlmacen SET
                  Nombre = @Nombre, IdCategoriaAlmacen = @IdCategoriaAlmacen, Cantidad = @Cantidad,
                  UnidadBase = @UnidadBase, PrecioUnitario = @PrecioUnitario, StockMinimo = @StockMinimo,
                  FechaUltimaActualizacion = @FechaUltimaActualizacion
                  WHERE IdProductoAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@IdCategoriaAlmacen", producto.IdCategoriaAlmacen);
            cmd.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
            cmd.Parameters.AddWithValue("@UnidadBase", producto.UnidadBase);
            cmd.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@FechaUltimaActualizacion", producto.FechaUltimaActualizacion);
            cmd.Parameters.AddWithValue("@Id", producto.IdProductoAlmacen);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarCantidad(int idProducto, decimal nuevaCantidad)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE ProductosAlmacen SET Cantidad = @Cantidad, FechaUltimaActualizacion = GETDATE()
                  WHERE IdProductoAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Cantidad", nuevaCantidad);
            cmd.Parameters.AddWithValue("@Id", idProducto);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                "UPDATE ProductosAlmacen SET Activo = 0 WHERE IdProductoAlmacen = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
