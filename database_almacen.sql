-- ============================================
-- Módulo Almacén - Parrillaguille
-- Tablas: CategoriasAlmacen, ProductosAlmacen, MovimientosAlmacen
-- ============================================

-- Tabla de Categorías del Almacén
CREATE TABLE CategoriasAlmacen (
    IdCategoriaAlmacen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(200) NULL
);

-- Tabla de Productos del Almacén
CREATE TABLE ProductosAlmacen (
    IdProductoAlmacen INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    IdCategoriaAlmacen INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL DEFAULT 0,
    UnidadMedida VARCHAR(20) NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL DEFAULT 0,
    StockMinimo DECIMAL(10,2) NOT NULL DEFAULT 0,
    FechaUltimaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdCategoriaAlmacen) REFERENCES CategoriasAlmacen(IdCategoriaAlmacen)
);

-- Tabla de Movimientos de Stock (entradas/salidas)
CREATE TABLE MovimientosAlmacen (
    IdMovimiento INT IDENTITY(1,1) PRIMARY KEY,
    IdProductoAlmacen INT NOT NULL,
    TipoMovimiento VARCHAR(10) NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Observacion VARCHAR(200) NULL,
    FOREIGN KEY (IdProductoAlmacen) REFERENCES ProductosAlmacen(IdProductoAlmacen)
);

-- ============================================
-- Seed: Categorías iniciales
-- ============================================
INSERT INTO CategoriasAlmacen (Nombre, Descripcion) VALUES
    ('Carnes', 'Res, pollo, cerdo, chorizo, etc.'),
    ('Papas y Tubérculos', 'Papa, camote, yuca, etc.'),
    ('Verduras', 'Lechuga, tomate, cebolla, etc.'),
    ('Bebidas', 'Gaseosas, jugos, agua, etc.'),
    ('Granos y Cereales', 'Arroz, frejol, maíz, etc.'),
    ('Lácteos', 'Queso, mantequilla, leche, etc.'),
    ('Condimentos y Especias', 'Sal, pimienta, ají, etc.'),
    ('Aceites y Grasas', 'Aceite vegetal, manteca, etc.'),
    ('Otros', 'Productos varios');
