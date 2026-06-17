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
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre,
                         m.IdMedida, med.Nombre, m.TipoMovimiento,
                         m.CantidadUnidades,
                         CASE WHEN m.TipoMovimiento = 'Entrada'
                              THEN -(med.PrecioUnitario * m.CantidadUnidades)
                              ELSE (med.PrecioUnitario * m.CantidadUnidades)
                         END,
                         m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  INNER JOIN MedidasProducto med ON m.IdMedida = med.IdMedida
                  WHERE CAST(m.Fecha AS DATE) = @Fecha
                  ORDER BY m.Fecha ASC", conn);
            cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movimientos.Add(new MovimientoAlmacen
                {
                    IdMovimiento = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    IdMedida = reader.GetInt32(3),
                    NombreMedida = reader.GetString(4),
                    TipoMovimiento = reader.GetString(5),
                    CantidadUnidades = reader.GetInt32(6),
                    Total = reader.GetDecimal(7),
                    Fecha = reader.GetDateTime(8),
                    Observacion = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
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
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre,
                         m.IdMedida, med.Nombre, m.TipoMovimiento,
                         m.CantidadUnidades,
                         CASE WHEN m.TipoMovimiento = 'Entrada'
                              THEN -(med.PrecioUnitario * m.CantidadUnidades)
                              ELSE (med.PrecioUnitario * m.CantidadUnidades)
                         END,
                         m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  INNER JOIN MedidasProducto med ON m.IdMedida = med.IdMedida
                  WHERE CAST(m.Fecha AS DATE) BETWEEN @Desde AND @Hasta
                  ORDER BY m.Fecha ASC", conn);
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
                    IdMedida = reader.GetInt32(3),
                    NombreMedida = reader.GetString(4),
                    TipoMovimiento = reader.GetString(5),
                    CantidadUnidades = reader.GetInt32(6),
                    Total = reader.GetDecimal(7),
                    Fecha = reader.GetDateTime(8),
                    Observacion = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
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
                    @"INSERT INTO MovimientosAlmacen (IdProductoAlmacen, IdMedida, TipoMovimiento, CantidadUnidades, Fecha, Observacion)
                      VALUES (@IdProducto, @IdMedida, @TipoMovimiento, @CantidadUnidades, @Fecha, @Observacion)", conn, transaction);
                cmdMov.Parameters.AddWithValue("@IdProducto", movimiento.IdProductoAlmacen);
                cmdMov.Parameters.AddWithValue("@IdMedida", movimiento.IdMedida);
                cmdMov.Parameters.AddWithValue("@TipoMovimiento", movimiento.TipoMovimiento);
                cmdMov.Parameters.AddWithValue("@CantidadUnidades", movimiento.CantidadUnidades);
                cmdMov.Parameters.AddWithValue("@Fecha", movimiento.Fecha);
                cmdMov.Parameters.AddWithValue("@Observacion", (object?)movimiento.Observacion ?? DBNull.Value);
                cmdMov.ExecuteNonQuery();

                string operacion = movimiento.TipoMovimiento == "Entrada" ? "+" : "-";
                using var cmdStock = new SqlCommand(
                    $@"UPDATE ProductosAlmacen
                       SET Cantidad = Cantidad {operacion} @CantidadUnidades,
                           FechaUltimaActualizacion = GETDATE()
                       WHERE IdProductoAlmacen = @IdProducto", conn, transaction);
                cmdStock.Parameters.AddWithValue("@CantidadUnidades", movimiento.CantidadUnidades);
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
                @"SELECT m.IdMovimiento, m.IdProductoAlmacen, p.Nombre,
                         m.IdMedida, med.Nombre, m.TipoMovimiento,
                         m.CantidadUnidades,
                         CASE WHEN m.TipoMovimiento = 'Entrada'
                              THEN -(med.PrecioUnitario * m.CantidadUnidades)
                              ELSE (med.PrecioUnitario * m.CantidadUnidades)
                         END,
                         m.Fecha, m.Observacion
                  FROM MovimientosAlmacen m
                  INNER JOIN ProductosAlmacen p ON m.IdProductoAlmacen = p.IdProductoAlmacen
                  INNER JOIN MedidasProducto med ON m.IdMedida = med.IdMedida
                  ORDER BY m.Fecha ASC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                movimientos.Add(new MovimientoAlmacen
                {
                    IdMovimiento = reader.GetInt32(0),
                    IdProductoAlmacen = reader.GetInt32(1),
                    NombreProducto = reader.GetString(2),
                    IdMedida = reader.GetInt32(3),
                    NombreMedida = reader.GetString(4),
                    TipoMovimiento = reader.GetString(5),
                    CantidadUnidades = reader.GetInt32(6),
                    Total = reader.GetDecimal(7),
                    Fecha = reader.GetDateTime(8),
                    Observacion = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                });
            }
            return movimientos;
        }
    }
}
