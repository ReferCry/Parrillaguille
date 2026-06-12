using System.Text;
using Capa_Entidad;

namespace Capa_Logica
{
    public class ImpresionService
    {
        public string GenerarTicket(VentaEntidad venta, List<DetallePedido> detalles, int? numeroMesa = null)
        {
            var sb = new StringBuilder();
            int ancho = 32;

            sb.AppendLine(Centrar("GUILLEBULL'S", ancho));
            sb.AppendLine(Centrar("SISTEMA DE VENTAS", ancho));
            sb.AppendLine(new string('-', ancho));
            sb.AppendLine($"BOLETA N° {venta.IdVenta:D6}");
            sb.AppendLine($"Fecha: {venta.Fecha:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Hora:  {venta.Fecha:HH:mm:ss}");
            sb.AppendLine(new string('-', ancho));

            if (numeroMesa.HasValue)
                sb.AppendLine($"Mesa: {numeroMesa}");
            else
                sb.AppendLine("Atención: Delivery");

            sb.AppendLine(new string('-', ancho));
            sb.AppendLine("PRODUCTO           CANT  PRECIO  SUBTOTAL");
            sb.AppendLine(new string('-', ancho));

            foreach (var d in detalles)
            {
                string nombre = d.NombreProducto.Length > 18
                    ? d.NombreProducto.Substring(0, 18)
                    : d.NombreProducto.PadRight(18);

                sb.AppendLine($"{nombre} {d.Cantidad,4}  {d.PrecioUnitario,7:N2}  {d.Subtotal,8:N2}");
            }

            sb.AppendLine(new string('-', ancho));

            decimal subtotal = detalles.Sum(d => d.Subtotal);
            decimal igv = subtotal * 0.18m;
            decimal total = subtotal + igv;

            sb.AppendLine($"SUBTOTAL:              S/ {subtotal:N2}");
            sb.AppendLine($"IGV (18%):             S/ {igv:N2}");
            sb.AppendLine($"TOTAL:                 S/ {total:N2}");
            sb.AppendLine(new string('-', ancho));
            sb.AppendLine($"Cliente: {venta.NombreCliente} {venta.ApellidoCliente}");
            sb.AppendLine($"DNI: {venta.DNI}");

            if (!string.IsNullOrEmpty(venta.RUC))
                sb.AppendLine($"RUC: {venta.RUC}");

            sb.AppendLine($"Tipo: {venta.TipoComprobante}");
            sb.AppendLine($"Pago: {venta.MetodoPago}");
            sb.AppendLine(new string('-', ancho));
            sb.AppendLine(Centrar("¡GRACIAS POR SU COMPRA!", ancho));
            sb.AppendLine(Centrar("Vuelva pronto", ancho));

            return sb.ToString();
        }

        public void GuardarTicket(string contenido, string nombreArchivo = "ticket.txt")
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);
            File.WriteAllText(ruta, contenido, Encoding.UTF8);
        }

        private string Centrar(string texto, int ancho)
        {
            if (texto.Length >= ancho)
                return texto;
            int espacios = (ancho - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }
    }
}
