namespace Capa_Entidad
{
    public class MovimientoAlmacen
    {
        public int IdMovimiento { get; set; }
        public int IdProductoAlmacen { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string TipoMovimiento { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; } = string.Empty;
    }
}
