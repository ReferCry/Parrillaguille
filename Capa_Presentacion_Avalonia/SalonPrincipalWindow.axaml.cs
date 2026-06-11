using Avalonia.Controls;
namespace Capa_Presentacion_Avalonia;
public partial class SalonPrincipalWindow : Window
{
    public SalonPrincipalWindow() : this("Salon") { }
    public SalonPrincipalWindow(string tipoAtencion)
    {
        InitializeComponent();
        Title = tipoAtencion == "Delivery" ? "Delivery" : "Salón Principal";
    }
}
