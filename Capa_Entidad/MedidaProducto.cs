namespace Capa_Entidad
{
    public class MedidaProducto
    {
        public int IdMedida { get; set; }
        public int IdProductoAlmacen { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal ValorNumerico { get; set; }
        public string UnidadBase { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
    }
}
