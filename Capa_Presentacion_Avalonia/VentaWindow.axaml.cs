using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion_Avalonia;

public partial class VentaWindow : Window
{
    private readonly int _pedidoId;
    private readonly int? _mesaId;
    private readonly decimal _montoTotal;
    private readonly VentaService _ventaService;

    public VentaWindow() : this(0, null, 0) { }

    public VentaWindow(int pedidoId, int? mesaId, decimal montoTotal)
    {
        InitializeComponent();
        _pedidoId = pedidoId;
        _mesaId = mesaId;
        _montoTotal = montoTotal;

        var conexion = new ConexionSQL();
        _ventaService = new VentaService(conexion);

        // Configurar campos
        txtMesa.Text = mesaId.HasValue ? $"Mesa {mesaId}" : "Delivery";
        txtMontoTotal.Text = $"S/ {montoTotal:N2}";
        dpFecha.SelectedDate = DateTimeOffset.Now;

        // Métodos de pago
        cboMetodoPago.Items.Clear();
        cboMetodoPago.Items.Add("Efectivo");
        cboMetodoPago.Items.Add("Tarjeta de Crédito");
        cboMetodoPago.Items.Add("Yape");
        cboMetodoPago.SelectedIndex = 0;

        // Radio buttons: Factura habilita RUC, Boleta lo deshabilita
        rbFactura.IsCheckedChanged += (_, _) =>
        {
            if (rbFactura.IsChecked == true)
            {
                txtRUC.IsEnabled = true;
                txtRUC.Focus();
            }
        };

        rbBoleta.IsCheckedChanged += (_, _) =>
        {
            if (rbBoleta.IsChecked == true)
            {
                txtRUC.Clear();
                txtRUC.IsEnabled = false;
            }
        };

        // Estado inicial: Boleta por defecto (RUC deshabilitado)
        rbBoleta.IsChecked = true;

        // Eventos de botones
        btnAtras.Click += (_, _) => Close();
        btnRegistrar.Click += BtnRegistrar_Click;
    }

    private void BtnRegistrar_Click(object? sender, RoutedEventArgs e)
    {
        string nombres = txtNombres.Text?.Trim() ?? "";
        string apellidos = txtApellidos.Text?.Trim() ?? "";
        string dni = txtDNI.Text?.Trim() ?? "";
        string ruc = txtRUC.Text?.Trim() ?? "";
        string metodoPago = cboMetodoPago.SelectedItem?.ToString() ?? "Efectivo";
        string tipoComprobante = rbFactura.IsChecked == true ? "Factura" : "Boleta";
        var fecha = dpFecha.SelectedDate?.DateTime ?? DateTime.Now;

        // Validaciones
        if (string.IsNullOrWhiteSpace(nombres))
        {
            MostrarAviso("Ingrese el nombre del cliente.");
            txtNombres.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(apellidos))
        {
            MostrarAviso("Ingrese los apellidos del cliente.");
            txtApellidos.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8)
        {
            MostrarAviso("Ingrese un DNI válido (8 dígitos).");
            txtDNI.Focus();
            return;
        }

        if (tipoComprobante == "Factura" && (string.IsNullOrWhiteSpace(ruc) || ruc.Length != 11))
        {
            MostrarAviso("Ingrese un RUC válido (11 dígitos) para factura.");
            txtRUC.Focus();
            return;
        }

        try
        {
            var venta = new VentaEntidad
            {
                IdPedido = _pedidoId,
                NombreCliente = nombres,
                ApellidoCliente = apellidos,
                DNI = dni,
                RUC = ruc,
                TipoComprobante = tipoComprobante,
                MetodoPago = metodoPago,
                MontoTotal = _montoTotal,
                Fecha = fecha
            };

            int idVenta = _ventaService.RegistrarVenta(venta);

            // Mostrar confirmación
            MostrarExito(
                $"¡Venta registrada exitosamente!\n\n" +
                $"Venta #{idVenta}\n" +
                $"Cliente: {nombres} {apellidos}\n" +
                $"DNI: {dni}\n" +
                $"Comprobante: {tipoComprobante}\n" +
                $"Método de pago: {metodoPago}\n" +
                $"Total: S/ {_montoTotal:N2}");
        }
        catch (Exception ex)
        {
            MostrarError($"Error al registrar venta: {ex.Message}");
        }
    }

    private void MostrarAviso(string mensaje) => MostrarDialogo("Aviso", mensaje, "#FB8C00", cerrarEnExito: false);
    private void MostrarExito(string mensaje) => MostrarDialogo("Éxito", mensaje, "#43A047", cerrarEnExito: true);
    private void MostrarError(string mensaje) => MostrarDialogo("Error", mensaje, "#E53935", cerrarEnExito: false);

    private void MostrarDialogo(string titulo, string mensaje, string colorHex, bool cerrarEnExito)
    {
        Window? dialog = null;

        var btnAceptar = new Button
        {
            Content = "Aceptar",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            Padding = new Avalonia.Thickness(20, 6),
            Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse(colorHex)),
            Foreground = Avalonia.Media.Brushes.White,
            CornerRadius = new Avalonia.CornerRadius(6)
        };
        btnAceptar.Click += (_, _) =>
        {
            dialog?.Close();
            if (cerrarEnExito) Close();
        };

        dialog = new Window
        {
            Title = titulo,
            Width = 380, Height = 200,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new StackPanel
            {
                Margin = new Avalonia.Thickness(20),
                Spacing = 16,
                Children =
                {
                    new TextBlock { Text = mensaje, TextWrapping = Avalonia.Media.TextWrapping.Wrap, FontSize = 14 },
                    btnAceptar
                }
            }
        };

        dialog.ShowDialog(this);
    }
}
