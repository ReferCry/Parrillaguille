namespace Capa_Entidad
{
    public class ProductoAlmacen
    {
        public int IdProductoAlmacen { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdCategoriaAlmacen { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal StockMinimo { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
