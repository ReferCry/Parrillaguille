namespace Capa_Entidad
{
    public static class MenuData
    {
        public static readonly Dictionary<string, string[]> Categorias = new()
        {
            ["Pollos a la Brasa"] = new[]
            {
                "1 Pollo a la Brasa",
                "1/2 Pollo",
                "1/4 Pollo",
                "1/8 Pollo"
            },
            ["Parrillas"] = new[]
            {
                "Chuleta",
                "Bife",
                "Parrilla de Pollo Pecho",
                "Parrilla de Pollo Pierna",
                "Tira de Cerdo",
                "Porción de Chorizo",
                "Parrilla con Papa Sancochada"
            },
            ["Platos a la Carta"] = new[]
            {
                "Lomo de Carne",
                "Lomo de Pollo",
                "Lomo Saltado",
                "Chaufa de Carne",
                "Chaufa de Pollo",
                "Tallarín Saltado de Pollo",
                "Tallarín Saltado de Carne"
            },
            ["Otros"] = new[]
            {
                "Mostrito 1/8"
            },
            ["Porciones y Guarniciones"] = new[]
            {
                "Porción de Arroz Personal",
                "Porción de Arroz Mediana",
                "Porción de Arroz Grande",
                "Porción de Papa Mediana",
                "Porción de Papa Grande",
                "Ensalada Mediana",
                "Ensalada Grande",
                "Unidad de Chorizo"
            },
            ["Gaseosas"] = new[]
            {
                "Gaseosa 2 Litros",
                "Gaseosa 1 Litro",
                "Gaseosa Gordita",
                "Gaseosa Personal",
                "Pepsi 1 Litro",
                "Agua Mineral"
            },
            ["Infusiones"] = new[]
            {
                "Café",
                "Infusiones"
            },
            ["Refrescos"] = new[]
            {
                "Chicha Morada 1L",
                "Chicha Morada 1.5L",
                "Cebada 1L",
                "Cebada 1.5L",
                "Limonada Frozen 1L",
                "Maracuyá 1L"
            }
        };

        public static string[] ObtenerCategorias() => Categorias.Keys.ToArray();
        public static string[] ObtenerPlatos(string categoria) =>
            Categorias.TryGetValue(categoria, out var platos) ? platos : Array.Empty<string>();
    }
}
