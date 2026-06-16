using Microsoft.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_datos
{
    public class MovimientoAlmacenRepository
    {
        private readonly ConexionSQL _conexion;

        public MovimientoAlmacenRepository(ConexionSQL conexion)
        {
            _conexion = conexion;
        }

        public List<MovimientoAlmacen> ObtenerPorFecha(DateTime fecha)
        {
            var movimientos = new List<MovimientoAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre, m.TipoMovimiento,
                         m.Cantidad, m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  WHERE CAST(m.Fecha AS DATE) = @Fecha
                  ORDER BY m.Fecha DESC", conn);
            cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movimientos.Add(new MovimientoAlmacen
                {
                    IdMovimiento = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    TipoMovimiento = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    Fecha = reader.GetDateTime(5),
                    Observacion = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            return movimientos;
        }

        public List<MovimientoAlmacen> ObtenerPorRangoFechas(DateTime desde, DateTime hasta)
        {
            var movimientos = new List<MovimientoAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre, m.TipoMovimiento,
                         m.Cantidad, m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  WHERE CAST(m.Fecha AS DATE) BETWEEN @Desde AND @Hasta
                  ORDER BY m.Fecha DESC", conn);
            cmd.Parameters.AddWithValue("@Desde", desde.Date);
            cmd.Parameters.AddWithValue("@Hasta", hasta.Date);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movimientos.Add(new MovimientoAlmacen
                {
                    IdMovimiento = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    TipoMovimiento = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    Fecha = reader.GetDateTime(5),
                    Observacion = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            return movimientos;
        }

        public void Insertar(MovimientoAlmacen movimiento)
        {
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmdMov = new SqlCommand(
                    @"INSERT INTO MovimientosAlmacen (IdProductoAlmacen, TipoMovimiento, Cantidad, Fecha, Observacion)
                      VALUES (@IdProducto, @TipoMovimiento, @Cantidad, @Fecha, @Observacion)", conn, transaction);
                cmdMov.Parameters.AddWithValue("@IdProducto", movimiento.IdProductoAlmacen);
                cmdMov.Parameters.AddWithValue("@TipoMovimiento", movimiento.TipoMovimiento);
                cmdMov.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
                cmdMov.Parameters.AddWithValue("@Fecha", movimiento.Fecha);
                cmdMov.Parameters.AddWithValue("@Observacion", (object?)movimiento.Observacion ?? DBNull.Value);
                cmdMov.ExecuteNonQuery();

                string operacion = movimiento.TipoMovimiento == "Entrada" ? "+" : "-";
                using var cmdStock = new SqlCommand(
                    $@"UPDATE ProductosAlmacen
                       SET Cantidad = Cantidad {operacion} @Cantidad,
                           FechaUltimaActualizacion = GETDATE()
                       WHERE IdProductoAlmacen = @IdProducto", conn, transaction);
                cmdStock.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
                cmdStock.Parameters.AddWithValue("@IdProducto", movimiento.IdProductoAlmacen);
                cmdStock.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<MovimientoAlmacen> ObtenerTodos()
        {
            var movimientos = new List<MovimientoAlmacen>();
            using var conn = _conexion.ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre, m.TipoMovimiento,
                         m.Cantidad, m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  ORDER BY m.Fecha DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movimientos.Add(new MovimientoAlmacen
                {
                    IdMovimiento = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    TipoMovimiento = reader.GetString(3),
                    Cantidad = reader.GetDecimal(4),
                    Fecha = reader.GetDateTime(5),
                    Observacion = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            return movimientos;
        }
    }
}
