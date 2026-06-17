-- ============================================
-- Módulo Almacén - Parrillaguille
-- Tablas: CategoriasAlmacen, ProductosAlmacen, MedidasProducto, MovimientosAlmacen
-- Stock siempre en unidades (cantidad = número de unidades/items)
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

-- Tabla de Productos del Almacén (stock en unidades)
CREATE TABLE ProductosAlmacen (
    IdProductoAlmacen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    IdCategoriaAlmacen INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL DEFAULT 0,
    PrecioUnitario DECIMAL(10,2) NOT NULL DEFAULT 0,
    StockMinimo DECIMAL(10,2) NOT NULL DEFAULT 0,
    FechaUltimaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdCategoriaAlmacen) REFERENCES CategoriasAlmacen(IdCategoriaAlmacen)
);

-- Tabla de Medidas por Producto (variantes de tamaño, para referencia)
CREATE TABLE MedidasProducto (
    IdMedida INT IDENTITY(1,1) PRIMARY KEY,
    IdProductoAlmacen INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    ValorNumerico DECIMAL(10,2) NOT NULL,
    UnidadBase VARCHAR(20) NOT NULL,
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
-- Seed: Productos (stock = unidades, sin UnidadBase)
-- Los nombres ya diferencian variantes (ej: Coca Cola 2L, Coca Cola 1L)
-- ============================================

-- POLLOS Y CARNES
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Pollo entero', 1, 0, 0, 2),
    ('Pechuga de pollo', 1, 0, 0, 2),
    ('Pierna de pollo', 1, 0, 0, 2),
    ('Chuleta', 1, 0, 0, 2),
    ('Bife', 1, 0, 0, 2),
    ('Tira de cerdo', 1, 0, 0, 2),
    ('Chorizo', 1, 0, 0, 2),
    ('Costilla de res', 1, 0, 0, 2),
    ('Lomo de res', 1, 0, 0, 2);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (1, '1 Kg', 1, 'kg'), (1, '1/2 Kg', 0.5, 'kg'),
    (2, '1 Kg', 1, 'kg'), (2, '1/2 Kg', 0.5, 'kg'),
    (3, '1 Kg', 1, 'kg'), (3, '1/2 Kg', 0.5, 'kg'),
    (4, '1 Kg', 1, 'kg'), (4, '1/2 Kg', 0.5, 'kg'),
    (5, '1 Kg', 1, 'kg'), (5, '1/2 Kg', 0.5, 'kg'),
    (6, '1 Kg', 1, 'kg'), (6, '1/2 Kg', 0.5, 'kg'),
    (7, '1 Kg', 1, 'kg'), (7, '1/2 Kg', 0.5, 'kg'),
    (8, '1 Kg', 1, 'kg'), (8, '1/2 Kg', 0.5, 'kg'),
    (9, '1 Kg', 1, 'kg'), (9, '1/2 Kg', 0.5, 'kg');

-- VERDURAS Y TUBÉRCULOS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Papa blanca', 2, 0, 0, 5),
    ('Papa amarilla', 2, 0, 0, 3),
    ('Camote', 2, 0, 0, 2),
    ('Lechuga', 2, 0, 0, 2),
    ('Tomate', 2, 0, 0, 3),
    ('Cebolla', 2, 0, 0, 3),
    ('Pepino', 2, 0, 0, 2),
    ('Limón', 2, 0, 0, 2),
    ('Ajo', 2, 0, 0, 1),
    ('Ají amarillo', 2, 0, 0, 1),
    ('Culantro', 2, 0, 0, 1);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (10, '1 Kg', 1, 'kg'), (10, '2 Kg', 2, 'kg'), (10, '5 Kg', 5, 'kg'),
    (11, '1 Kg', 1, 'kg'), (11, '2 Kg', 2, 'kg'),
    (12, '1 Kg', 1, 'kg'), (12, '1/2 Kg', 0.5, 'kg'),
    (13, '1 Kg', 1, 'kg'), (13, '1/2 Kg', 0.5, 'kg'),
    (14, '1 Kg', 1, 'kg'), (14, '1/2 Kg', 0.5, 'kg'),
    (15, '1 Kg', 1, 'kg'), (15, '2 Kg', 2, 'kg'),
    (16, '1 Kg', 1, 'kg'), (16, '1/2 Kg', 0.5, 'kg'),
    (17, '1 Kg', 1, 'kg'), (17, '1/2 Kg', 0.5, 'kg'),
    (18, '1 Kg', 1, 'kg'), (18, '1/2 Kg', 0.5, 'kg'),
    (19, '1 Kg', 1, 'kg'), (19, '1/2 Kg', 0.5, 'kg'),
    (20, '1 Kg', 1, 'kg'), (20, '1/2 Kg', 0.5, 'kg');

-- BEBIDAS Y GASEOSAS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Coca Cola 2L', 3, 0, 0, 6),
    ('Coca Cola 1L', 3, 0, 0, 6),
    ('Inca Kola 2L', 3, 0, 0, 6),
    ('Inca Kola 1L', 3, 0, 0, 6),
    ('Sprite', 3, 0, 0, 6),
    ('Pepsi', 3, 0, 0, 6),
    ('Agua mineral', 3, 0, 0, 12);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (21, '2 litros', 2, 'litros'), (21, '1 litro', 1, 'litros'), (21, '1/2 litro', 0.5, 'litros'),
    (22, '1 litro', 1, 'litros'), (22, '1/2 litro', 0.5, 'litros'),
    (23, '2 litros', 2, 'litros'), (23, '1 litro', 1, 'litros'), (23, '1/2 litro', 0.5, 'litros'),
    (24, '1 litro', 1, 'litros'), (24, '1/2 litro', 0.5, 'litros'),
    (25, '2 litros', 2, 'litros'), (25, '1 litro', 1, 'litros'), (25, '1/2 litro', 0.5, 'litros'),
    (26, '2 litros', 2, 'litros'), (26, '1 litro', 1, 'litros'), (26, '1/2 litro', 0.5, 'litros'),
    (27, '1 litro', 1, 'litros'), (27, '1/2 litro', 0.5, 'litros');

-- INFUSIONES Y REFRESCOS
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Café', 4, 0, 0, 1),
    ('Manzanilla', 4, 0, 0, 1),
    ('Anís', 4, 0, 0, 1),
    ('Té', 4, 0, 0, 1),
    ('Maíz morado', 4, 0, 0, 2),
    ('Cebada', 4, 0, 0, 1),
    ('Maracuyá', 4, 0, 0, 1),
    ('Limón para infusión', 4, 0, 0, 1),
    ('Hierbaluisa', 4, 0, 0, 1);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (28, '1 Kg', 1, 'kg'), (28, '1/2 Kg', 0.5, 'kg'),
    (29, '1 Kg', 1, 'kg'), (29, '1/2 Kg', 0.5, 'kg'),
    (30, '1 Kg', 1, 'kg'), (30, '1/2 Kg', 0.5, 'kg'),
    (31, '1 Kg', 1, 'kg'), (31, '1/2 Kg', 0.5, 'kg'),
    (32, '1 Kg', 1, 'kg'), (32, '2 Kg', 2, 'kg'),
    (33, '1 Kg', 1, 'kg'), (33, '1/2 Kg', 0.5, 'kg'),
    (34, '1 Kg', 1, 'kg'), (34, '1/2 Kg', 0.5, 'kg'),
    (35, '1 Kg', 1, 'kg'), (35, '1/2 Kg', 0.5, 'kg'),
    (36, '1 Kg', 1, 'kg'), (36, '1/2 Kg', 0.5, 'kg');

-- LIMPIEZA
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Detergente', 5, 0, 0, 2),
    ('Lejía', 5, 0, 0, 2),
    ('Jabón líquido', 5, 0, 0, 2),
    ('Desinfectante', 5, 0, 0, 2),
    ('Esponjas', 5, 0, 0, 10),
    ('Guantes', 5, 0, 0, 10),
    ('Bolsas de basura', 5, 0, 0, 20),
    ('Papel toalla', 5, 0, 0, 6);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (37, '1 litro', 1, 'litros'), (37, '1/2 litro', 0.5, 'litros'),
    (38, '1 litro', 1, 'litros'), (38, '1/2 litro', 0.5, 'litros'),
    (39, '1 litro', 1, 'litros'), (39, '1/2 litro', 0.5, 'litros'),
    (40, '1 litro', 1, 'litros'), (40, '1/2 litro', 0.5, 'litros'),
    (41, '10 unidades', 10, 'unidades'), (41, '5 unidades', 5, 'unidades'),
    (42, '10 unidades', 10, 'unidades'), (42, '5 unidades', 5, 'unidades'),
    (43, '50 unidades', 50, 'unidades'), (43, '20 unidades', 20, 'unidades'),
    (44, '6 unidades', 6, 'unidades'), (44, '12 unidades', 12, 'unidades');

-- DESECHABLES
INSERT INTO ProductosAlmacen (Nombre, IdCategoriaAlmacen, Cantidad, PrecioUnitario, StockMinimo) VALUES
    ('Vasos descartables', 6, 0, 0, 50),
    ('Tapers', 6, 0, 0, 25),
    ('Bolsas', 6, 0, 0, 50),
    ('Servilletas', 6, 0, 0, 50),
    ('Cucharas descartables', 6, 0, 0, 50),
    ('Tenedores descartables', 6, 0, 0, 50);

INSERT INTO MedidasProducto (IdProductoAlmacen, Nombre, ValorNumerico, UnidadBase) VALUES
    (45, '100 unidades', 100, 'unidades'), (45, '50 unidades', 50, 'unidades'),
    (46, '50 unidades', 50, 'unidades'), (46, '25 unidades', 25, 'unidades'),
    (47, '100 unidades', 100, 'unidades'), (47, '50 unidades', 50, 'unidades'),
    (48, '100 unidades', 100, 'unidades'), (48, '50 unidades', 50, 'unidades'),
    (49, '100 unidades', 100, 'unidades'), (49, '50 unidades', 50, 'unidades'),
    (50, '100 unidades', 100, 'unidades'), (50, '50 unidades', 50, 'unidades');
