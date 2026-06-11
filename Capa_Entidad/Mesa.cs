namespace Capa_Entidad
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } = "Libre"; // Libre, Ocupada, Reservada
    }
}
