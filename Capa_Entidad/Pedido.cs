namespace Capa_Entidad
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int? IdMesa { get; set; }
        public string TipoAtencion { get; set; } = string.Empty; // "Salon" o "Delivery"
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Abierto"; // Abierto, Cerrado, Cancelado
        public decimal Total { get; set; }
    }
}
