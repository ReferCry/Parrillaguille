using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion_Avalonia;

public partial class PedidoWindow : Window
{
    private readonly string _tipoAtencion;
    private readonly int? _numeroMesa;
    private readonly PedidoService _pedidoService;
    private readonly ProductoService _productoService;
    private int? _pedidoActualId;

    private readonly ObservableCollection<DetalleItem> _detalles = new();

    // Mapeo de categorías a ComboBox + NumericUpDown
    private readonly Dictionary<string, (ComboBox combo, NumericUpDown qty)> _mappingCategorias = new();

    public PedidoWindow() : this("Salon", 1) { }

    public PedidoWindow(string tipoAtencion, int numeroMesa)
    {
        InitializeComponent();
        _tipoAtencion = tipoAtencion;
        _numeroMesa = numeroMesa;

        Title = tipoAtencion == "Delivery"
            ? "Pedido - Delivery"
            : $"Pedido - Salón (Mesa {numeroMesa})";

        var txtTitulo = this.FindControl<TextBlock>("txtTitulo");
        if (txtTitulo != null)
            txtTitulo.Text = tipoAtencion == "Delivery"
                ? "📝 Tomar Pedido - Delivery"
                : $"📝 Tomar Pedido - Mesa {numeroMesa}";

        // Servicios
        var conexion = new ConexionSQL();
        _pedidoService = new PedidoService(conexion);
        _productoService = new ProductoService(conexion);

        // Configurar DataGrid
        dgDetalles.ItemsSource = _detalles;

        // Mapear categorías a controles
        _mappingCategorias["Pollos a la Brasa"] = (cboPollos, qtyPollos);
        _mappingCategorias["Parrillas"] = (cboParrillas, qtyParrillas);
        _mappingCategorias["Platos a la Carta"] = (cboPlatosCarta, qtyPlatosCarta);
        _mappingCategorias["Otros"] = (cboOtros, qtyOtros);
        _mappingCategorias["Porciones y Guarniciones"] = (cboPorciones, qtyPorciones);
        _mappingCategorias["Gaseosas"] = (cboGaseosas, qtyGaseosas);
        _mappingCategorias["Infusiones"] = (cboInfusiones, qtyInfusiones);
        _mappingCategorias["Refrescos"] = (cboRefrescos, qtyRefrescos);

        // Cargar menú desde BD o datos estáticos
        CargarMenus();

        // Eventos de botones
        btnAtras.Click += (_, _) => CerrarYVolver();
        btnAgregar.Click += BtnAgregar_Click;
        btnEliminar.Click += BtnEliminar_Click;
        btnVenta.Click += BtnVenta_Click;

        // Abrir pedido al cargar
        this.Opened += (_, _) => AbrirPedido();
    }

    private void CargarMenus()
    {
        foreach (var cat in _mappingCategorias)
        {
            var (combo, qty) = cat.Value;
            combo.Items.Clear();

            try
            {
                var productos = _productoService.ObtenerPorCategoria(cat.Key);
                foreach (var p in productos)
                    combo.Items.Add(p.Nombre);
            }
            catch
            {
                // Fallback: usar datos estáticos de MenuData
                if (MenuData.Categorias.TryGetValue(cat.Key, out var platos))
                {
                    foreach (var plato in platos)
                        combo.Items.Add(plato);
                }
            }

            if (combo.Items.Count > 0)
                combo.SelectedIndex = -1;

            qty.Value = 1;
        }
    }

    private void AbrirPedido()
    {
        try
        {
            var pedido = new Pedido
            {
                IdMesa = _numeroMesa,
                TipoAtencion = _tipoAtencion
            };
            _pedidoActualId = _pedidoService.AbrirPedido(pedido);
        }
        catch
        {
            // Sin BD, el pedido se trabaja localmente
        }
    }

    private void BtnAgregar_Click(object? sender, RoutedEventArgs e)
    {
        bool algunoAgregado = false;

        foreach (var cat in _mappingCategorias)
        {
            var (combo, qty) = cat.Value;
            if (combo.SelectedIndex < 0) continue;

            int cantidad = (int)(qty.Value ?? 1);
            string nombre = combo.SelectedItem?.ToString() ?? "";
            decimal precio = 0;
            int idProducto = 0;

            try
            {
                var producto = _productoService.ObtenerPorNombre(nombre);
                if (producto != null)
                {
                    precio = producto.Precio;
                    idProducto = producto.IdProducto;
                }
            }
            catch { /* Sin BD */ }

            if (precio == 0)
            {
                continue; // Saltar productos sin precio registrado
            }

            // Verificar si ya existe el producto en la lista
            var existente = _detalles.FirstOrDefault(d => d.NombreProducto == nombre);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                existente.OnPropertyChanged(nameof(DetalleItem.Subtotal));
            }
            else
            {
                var nuevo = new DetalleItem
                {
                    IdProducto = idProducto,
                    NombreProducto = nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precio
                };
                _detalles.Add(nuevo);

                // Guardar en BD solo para items nuevos
                if (_pedidoActualId.HasValue && idProducto > 0)
                {
                    try
                    {
                        _pedidoService.AgregarDetalle(_pedidoActualId.Value, new DetallePedido
                        {
                            IdProducto = idProducto,
                            NombreProducto = nombre,
                            Cantidad = cantidad,
                            PrecioUnitario = precio
                        });
                    }
                    catch { /* Sin BD */ }
                }
            }

            // Limpiar selección
            combo.SelectedIndex = -1;
            qty.Value = 1;
            algunoAgregado = true;
        }

        if (!algunoAgregado)
        {
            MostrarAviso("Seleccione al menos un plato o bebida antes de agregar.");
            return;
        }

        ActualizarTotal();
    }

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
    {
        if (dgDetalles.SelectedItem is DetalleItem item)
        {
            // Eliminar de BD si existe
            if (_pedidoActualId.HasValue && item.IdDetalle > 0)
            {
                try
                {
                    _pedidoService.EliminarDetalle(item.IdDetalle, _pedidoActualId.Value);
                }
                catch { /* Sin BD */ }
            }

            _detalles.Remove(item);
            ActualizarTotal();
        }
        else
        {
            MostrarAviso("Seleccione un plato de la lista para eliminar.");
        }
    }

    private void BtnVenta_Click(object? sender, RoutedEventArgs e)
    {
        if (_detalles.Count == 0)
        {
            MostrarAviso("No hay platos en el pedido. Agregue al menos uno antes de facturar.");
            return;
        }

        decimal total = _detalles.Sum(d => d.Subtotal);

        var ventaWindow = new VentaWindow(_pedidoActualId ?? 0, _numeroMesa, total);
        ventaWindow.Closed += (_, _) =>
        {
            // Cerrar pedido al registrar venta
            if (_pedidoActualId.HasValue)
            {
                try { _pedidoService.CerrarPedido(_pedidoActualId.Value, _numeroMesa); }
                catch { /* Sin BD */ }
            }
            Close();
        };
        ventaWindow.Show();
        this.Hide();
    }

    private void ActualizarTotal()
    {
        decimal total = _detalles.Sum(d => d.Subtotal);
        txtTotal.Text = total.ToString("N2");

        // Actualizar resumen
        if (_detalles.Count == 0)
        {
            txtResumen.Text = "Sin items agregados";
        }
        else
        {
            var lineas = _detalles.Select(d => $"• {d.NombreProducto} x{d.Cantidad} = S/{d.Subtotal:N2}");
            txtResumen.Text = string.Join("\n", lineas);
        }
    }

    private void CerrarYVolver()
    {
        if (_pedidoActualId.HasValue)
        {
            try { _pedidoService.CerrarPedido(_pedidoActualId.Value, _numeroMesa); }
            catch { /* Sin BD */ }
        }
        Close();
    }

    private void MostrarAviso(string mensaje)
    {
        Window? dialog = null;

        var btnAceptar = new Button
        {
            Content = "Aceptar",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            Padding = new Avalonia.Thickness(20, 6),
            Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#1E88E5")),
            Foreground = Avalonia.Media.Brushes.White,
            CornerRadius = new Avalonia.CornerRadius(6)
        };
        btnAceptar.Click += (_, _) => dialog?.Close();

        dialog = new Window
        {
            Title = "Aviso",
            Width = 350, Height = 150,
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

/// <summary>
/// Modelo para el DataGrid de detalles del pedido
/// </summary>
public class DetalleItem : System.ComponentModel.INotifyPropertyChanged
{
    public int IdDetalle { get; set; }
    public int IdProducto { get; set; }

    private string _nombreProducto = string.Empty;
    public string NombreProducto
    {
        get => _nombreProducto;
        set { _nombreProducto = value; OnPropertyChanged(nameof(NombreProducto)); }
    }

    private int _cantidad;
    public int Cantidad
    {
        get => _cantidad;
        set { _cantidad = value; OnPropertyChanged(nameof(Cantidad)); OnPropertyChanged(nameof(Subtotal)); }
    }

    private decimal _precioUnitario;
    public decimal PrecioUnitario
    {
        get => _precioUnitario;
        set { _precioUnitario = value; OnPropertyChanged(nameof(PrecioUnitario)); OnPropertyChanged(nameof(Subtotal)); }
    }

    public decimal Subtotal => Cantidad * PrecioUnitario;

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
