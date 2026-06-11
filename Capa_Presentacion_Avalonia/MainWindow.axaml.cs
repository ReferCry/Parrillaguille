using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Capa_Presentacion_Avalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.FindControl<Button>("btnSalon")!.Click += (_, _) =>
        {
            new SalonPrincipalWindow("Salon").Show();
            this.Hide();
        };

        this.FindControl<Button>("btnDelivery")!.Click += (_, _) =>
        {
            new SalonPrincipalWindow("Delivery").Show();
            this.Hide();
        };

        this.FindControl<Button>("btnVentas")!.Click += (_, _) =>
        {
            try
            {
                var conexion = new Capa_de_datos.ConexionSQL();
                var service = new Capa_Logica.VentaService(conexion);
                var ventas = service.ObtenerTodas();

                string mensaje = ventas.Count == 0
                    ? "No hay ventas registradas."
                    : string.Join("\n", ventas.Take(20).Select(v =>
                        $"#{v.IdVenta} | {v.Fecha:dd/MM/yyyy HH:mm} | {v.NombreCliente} {v.ApellidoCliente} | S/{v.MontoTotal:N2}"));

                var dialog = new Window
                {
                    Title = "Ventas Registradas",
                    Width = 600, Height = 400,
                    Content = new ScrollViewer { Content = new TextBlock { Text = mensaje, Margin = new Avalonia.Thickness(20) } }
                };
                dialog.Show();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        };

        this.FindControl<Button>("btnMenu")!.Click += (_, _) =>
        {
            new MenuWindow().Show();
            this.Hide();
        };
    }

    public void ShowError(string msg)
    {
        var dialog = new Window
        {
            Title = "Error",
            Width = 300, Height = 150,
            Content = new TextBlock { Text = msg, Margin = new Avalonia.Thickness(20) }
        };
        dialog.Show();
    }
}
