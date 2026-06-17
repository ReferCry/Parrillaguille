-- ============================================
-- Módulo Almacén - Parrillaguille
-- Tablas: CategoriasAlmacen, ProductosAlmacen, MedidasProducto, MovimientosAlmacen
-- Stock siempre en unidades (cantidad = número de unidades/items)
-- Precio vive en MedidasProducto (cada variante tiene su precio)
-- ============================================

-- Eliminar tablas en orden correcto (respetar FKs)
IF OBJECT_ID('MovimientosAlmacen', 'U') IS NOT NULL DROP TABLE MovimientosAlmacen;
IF OBJECT_ID('MedidasProducto', 'U') IS NOT NULL DROP TABLE MedidasProducto;
IF OBJECT_ID('ProductosAlmacen', 'U') IS NOT NULL DROP TABLE ProductosAlmacen;
IF OBJECT_ID('CategoriasAlmacen', 'U') IS NOT NULL DROP TABLE CategoriasAlmacen;

-- Tabla de Categorías del Almacén
CREATE TABLE CategoriasAlmacen (
    IdCategoriaAlmacen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(200) NULL
);

-- Tabla de Productos del Almacén (stock en unidades, sin precio propio)
CREATE TABLE ProductosAlmacen (
    IdProductoAlmacen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    IdCategoriaAlmacen INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL DEFAULT 0,
    StockMinimo DECIMAL(10,2) NOT NULL DEFAULT 0,
    FechaUltimaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdCategoriaAlmacen) REFERENCES CategoriasAlmacen(IdCategoriaAlmacen)
);

-- Tabla de Medidas por Producto (variantes de tamaño con precio propio)
CREATE TABLE MedidasProducto (
    IdMedida INT IDENTITY(1,1) PRIMARY KEY,
    IdProductoAlmacen INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    ValorNumerico DECIMAL(10,2) NOT NULL,
    UnidadBase VARCHAR(20) NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdProductoAlmacen) REFERENCES ProductosAlmacen(IdProductoAlmacen)
);

-- Tabla de Movimientos de Stock (entradas/salidas)
CREATE TABLE MovimientosAlmacen (
    IdMovimiento INT IDENTITY(1,1) PRIMARY KEY,
    IdProductoAlmacen INT NOT NULL,
    IdMedida INT NOT NULL,
    TipoMovimiento VARCHAR(10) NOT NULL,
    CantidadUnidades INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Observacion VARCHAR(200) NULL,
    FOREIGN KEY (IdProductoAlmacen) REFERENCES ProductosAlmacen(IdProductoAlmacen),
    FOREIGN KEY (IdMedida) REFERENCES MedidasProducto(IdMedida)
);

-- ============================================
-- Seed: Categorías (6)
-- ============================================
INSERT INTO CategoriasAlmacen (Nombre, Descripcion) VALUES
    ('Pollos y Carnes', 'Pollo, res, cerdo, chorizo, etc.'),
    ('Verduras y Tubérculos', 'Papa, camote, lechuga, tomate, etc.'),
    ('Bebidas y Gaseosas', 'Coca Cola, Inca Kola, Sprite, agua, etc.'),
    ('Infusiones y Refrescos', 'Café, manzanilla, té, maíz morado, etc.'),
    ('Limpieza', 'Detergente, lejía, desinfectante, etc.'),
    ('Desechables', 'Vasos, tapers, bolsas, servilletas, etc.');

-- ============================================
-- Seed: Productos (sin precio propio, precio en MedidasProducto)
-- Los nombres ya diferencian variantes (ej: Coca Cola 2L, Coca Cola 1L)
-- ============================================

-- POLLOS Y CARNES
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Pollo entero', 1, 0, 2),
    ('Pechuga de pollo', 1, 0, 2),
    ('Pierna de pollo', 1, 0, 2),
    ('Chuleta', 1, 0, 2),
    ('Bife', 1, 0, 2),
    ('Tira de cerdo', 1, 0, 2),
    ('Chorizo', 1, 0, 2),
    ('Costilla de res', 1, 0, 2),
    ('Lomo de res', 1, 0, 2);

-- Medidas con precios de ejemplo (cada variante tiene su precio)
INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (1, '1 Kg', 1, 'kg', 18.00), (1, '1/2 Kg', 0.5, 'kg', 10.00),
    (2, '1 Kg', 1, 'kg', 22.00), (2, '1/2 Kg', 0.5, 'kg', 12.00),
    (3, '1 Kg', 1, 'kg', 16.00), (3, '1/2 Kg', 0.5, 'kg', 9.00),
    (4, '1 Kg', 1, 'kg', 20.00), (4, '1/2 Kg', 0.5, 'kg', 11.00),
    (5, '1 Kg', 1, 'kg', 28.00), (5, '1/2 Kg', 0.5, 'kg', 15.00),
    (6, '1 Kg', 1, 'kg', 15.00), (6, '1/2 Kg', 0.5, 'kg', 8.50),
    (7, '1 Kg', 1, 'kg', 14.00), (7, '1/2 Kg', 0.5, 'kg', 8.00),
    (8, '1 Kg', 1, 'kg', 32.00), (8, '1/2 Kg', 0.5, 'kg', 18.00),
    (9, '1 Kg', 1, 'kg', 38.00), (9, '1/2 Kg', 0.5, 'kg', 21.00);

-- VERDURAS Y TUBÉRCULOS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Papa blanca', 2, 0, 5),
    ('Papa amarilla', 2, 0, 3),
    ('Camote', 2, 0, 2),
    ('Lechuga', 2, 0, 2),
    ('Tomate', 2, 0, 3),
    ('Cebolla', 2, 0, 3),
    ('Pepino', 2, 0, 2),
    ('Limón', 2, 0, 2),
    ('Ajo', 2, 0, 1),
    ('Ají amarillo', 2, 0, 1),
    ('Culantro', 2, 0, 1);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (10, '1 Kg', 1, 'kg', 4.50), (10, '2 Kg', 2, 'kg', 8.50), (10, '5 Kg', 5, 'kg', 20.00),
    (11, '1 Kg', 1, 'kg', 6.00), (11, '2 Kg', 2, 'kg', 11.50),
    (12, '1 Kg', 1, 'kg', 5.00), (12, '1/2 Kg', 0.5, 'kg', 3.00),
    (13, '1 Kg', 1, 'kg', 3.50), (13, '1/2 Kg', 0.5, 'kg', 2.00),
    (14, '1 Kg', 1, 'kg', 4.00), (14, '1/2 Kg', 0.5, 'kg', 2.50),
    (15, '1 Kg', 1, 'kg', 3.00), (15, '2 Kg', 2, 'kg', 5.50),
    (16, '1 Kg', 1, 'kg', 3.50), (16, '1/2 Kg', 0.5, 'kg', 2.00),
    (17, '1 Kg', 1, 'kg', 5.00), (17, '1/2 Kg', 0.5, 'kg', 3.00),
    (18, '1 Kg', 1, 'kg', 12.00), (18, '1/2 Kg', 0.5, 'kg', 7.00),
    (19, '1 Kg', 1, 'kg', 15.00), (19, '1/2 Kg', 0.5, 'kg', 8.50),
    (20, '1 Kg', 1, 'kg', 3.00), (20, '1/2 Kg', 0.5, 'kg', 1.80);

-- BEBIDAS Y GASEOSAS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Coca Cola', 3, 0, 6),
    ('Inca Kola', 3, 0, 6),
    ('Sprite', 3, 0, 6),
    ('Pepsi', 3, 0, 6),
    ('Agua mineral', 3, 0, 12);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (21, '2 litros', 2, 'litros', 6.50), (21, '1 litro', 1, 'litros', 4.00), (21, '1/2 litro', 0.5, 'litros', 2.50),
    (22, '2 litros', 2, 'litros', 6.50), (22, '1 litro', 1, 'litros', 4.00), (22, '1/2 litro', 0.5, 'litros', 2.50),
    (23, '2 litros', 2, 'litros', 6.00), (23, '1 litro', 1, 'litros', 3.50), (23, '1/2 litro', 0.5, 'litros', 2.00),
    (24, '2 litros', 2, 'litros', 6.00), (24, '1 litro', 1, 'litros', 3.50), (24, '1/2 litro', 0.5, 'litros', 2.00),
    (25, '1 litro', 1, 'litros', 2.00), (25, '1/2 litro', 0.5, 'litros', 1.20);

-- INFUSIONES Y REFRESCOS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Café', 4, 0, 1),
    ('Manzanilla', 4, 0, 1),
    ('Anís', 4, 0, 1),
    ('Té', 4, 0, 1),
    ('Maíz morado', 4, 0, 2),
    ('Cebada', 4, 0, 1),
    ('Maracuyá', 4, 0, 1),
    ('Limón para infusión', 4, 0, 1),
    ('Hierbaluisa', 4, 0, 1);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (26, '1 Kg', 1, 'kg', 8.00), (26, '1/2 Kg', 0.5, 'kg', 4.50),
    (27, '1 Kg', 1, 'kg', 6.00), (27, '1/2 Kg', 0.5, 'kg', 3.50),
    (28, '1 Kg', 1, 'kg', 7.00), (28, '1/2 Kg', 0.5, 'kg', 4.00),
    (29, '1 Kg', 1, 'kg', 5.00), (29, '1/2 Kg', 0.5, 'kg', 3.00),
    (30, '1 Kg', 1, 'kg', 4.00), (30, '2 Kg', 2, 'kg', 7.50),
    (31, '1 Kg', 1, 'kg', 5.50), (31, '1/2 Kg', 0.5, 'kg', 3.00),
    (32, '1 Kg', 1, 'kg', 7.00), (32, '1/2 Kg', 0.5, 'kg', 4.00),
    (33, '1 Kg', 1, 'kg', 4.00), (33, '1/2 Kg', 0.5, 'kg', 2.50),
    (34, '1 Kg', 1, 'kg', 6.00), (34, '1/2 Kg', 0.5, 'kg', 3.50);

-- LIMPIEZA
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Detergente', 5, 0, 2),
    ('Lejía', 5, 0, 2),
    ('Jabón líquido', 5, 0, 2),
    ('Desinfectante', 5, 0, 2),
    ('Esponjas', 5, 0, 10),
    ('Guantes', 5, 0, 10),
    ('Bolsas de basura', 5, 0, 20),
    ('Papel toalla', 5, 0, 6);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (35, '1 litro', 1, 'litros', 5.00), (35, '1/2 litro', 0.5, 'litros', 3.00),
    (36, '1 litro', 1, 'litros', 4.00), (36, '1/2 litro', 0.5, 'litros', 2.50),
    (37, '1 litro', 1, 'litros', 6.00), (37, '1/2 litro', 0.5, 'litros', 3.50),
    (38, '1 litro', 1, 'litros', 5.50), (38, '1/2 litro', 0.5, 'litros', 3.00),
    (39, '10 unidades', 10, 'unidades', 4.50), (39, '5 unidades', 5, 'unidades', 2.50),
    (40, '10 unidades', 10, 'unidades', 8.00), (40, '5 unidades', 5, 'unidades', 4.50),
    (41, '50 unidades', 50, 'unidades', 12.00), (41, '20 unidades', 20, 'unidades', 5.50),
    (42, '6 unidades', 6, 'unidades', 3.00), (42, '12 unidades', 12, 'unidades', 5.50);

-- DESECHABLES
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, StockMinimo) VALUES
    ('Vasos descartables', 6, 0, 50),
    ('Tapers', 6, 0, 25),
    ('Bolsas', 6, 0, 50),
    ('Servilletas', 6, 0, 50),
    ('Cucharas descartables', 6, 0, 50),
    ('Tenedores descartables', 6, 0, 50);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase, PrecioUnitario) VALUES
    (43, '100 unidades', 100, 'unidades', 15.00), (43, '50 unidades', 50, 'unidades', 8.00),
    (44, '50 unidades', 50, 'unidades', 10.00), (44, '25 unidades', 25, 'unidades', 5.50),
    (45, '100 unidades', 100, 'unidades', 8.00), (45, '50 unidades', 50, 'unidades', 4.50),
    (46, '100 unidades', 100, 'unidades', 6.00), (46, '50 unidades', 50, 'unidades', 3.50),
    (47, '100 unidades', 100, 'unidades', 10.00), (47, '50 unidades', 50, 'unidades', 5.50),
    (48, '100 unidades', 100, 'unidades', 10.00), (48, '50 unidades', 50, 'unidades', 5.50);
