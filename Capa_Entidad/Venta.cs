namespace Capa_Entidad
{
    public class VentaEntidad
    {
        public int IdVenta { get; set; }
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string ApellidoCliente { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string RUC { get; set; } = string.Empty;
        public string TipoComprobante { get; set; } = "Boleta"; // Factura o Boleta
        public string MetodoPago { get; set; } = "Efectivo";
        public decimal MontoTotal { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
