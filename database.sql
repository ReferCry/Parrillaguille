-- ============================================
-- Script de creación de base de datos
-- Parrillaguille - Sistema de Ventas
-- Base de datos: SQL Server
-- ============================================

-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ParrillaguilleDB')
BEGIN
    CREATE DATABASE ParrillaguilleDB;
END
GO

USE ParrillaguilleDB;
GO

-- ============================================
-- Tabla de Mesas
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Mesas' AND xtype='U')
BEGIN
    CREATE TABLE Mesas (
        IdMesa INT IDENTITY(1,1) PRIMARY KEY,
        Numero INT NOT NULL UNIQUE,
        Estado VARCHAR(20) NOT NULL DEFAULT 'Libre'
    );

    -- Insertar 20 mesas
    DECLARE @i INT = 1;
    WHILE @i <= 20
    BEGIN
        INSERT INTO Mesas (Numero, Estado) VALUES (@i, 'Libre');
        SET @i = @i + 1;
    END
END
GO

-- ============================================
-- Tabla de Categorías de Productos
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categorias' AND xtype='U')
BEGIN
    CREATE TABLE Categorias (
        IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(100) NOT NULL UNIQUE
    );

    INSERT INTO Categorias (Nombre) VALUES
        ('Pollos a la Brasa'),
        ('Parrillas'),
        ('Platos a la Carta'),
        ('Otros'),
        ('Porciones y Guarniciones'),
        ('Gaseosas'),
        ('Infusiones'),
        ('Refrescos');
END
GO

-- ============================================
-- Tabla de Productos (Menú)
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Productos' AND xtype='U')
BEGIN
    CREATE TABLE Productos (
        IdProducto INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(200) NOT NULL,
        Categoria VARCHAR(100) NOT NULL,
        Precio DECIMAL(10,2) NOT NULL DEFAULT 0,
        Activo BIT NOT NULL DEFAULT 1
    );

    -- POLLOS A LA BRASA
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('1 Pollo a la Brasa', 'Pollos a la Brasa', 35.00),
        ('1/2 Pollo', 'Pollos a la Brasa', 20.00),
        ('1/4 Pollo', 'Pollos a la Brasa', 12.00),
        ('1/8 Pollo', 'Pollos a la Brasa', 7.00);

    -- PARRILLAS
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Chuleta', 'Parrillas', 18.00),
        ('Bife', 'Parrillas', 22.00),
        ('Parrilla de Pollo Pecho', 'Parrillas', 16.00),
        ('Parrilla de Pollo Pierna', 'Parrillas', 15.00),
        ('Tira de Cerdo', 'Parrillas', 17.00),
        ('Porción de Chorizo', 'Parrillas', 10.00),
        ('Parrilla con Papa Sancochada', 'Parrillas', 20.00);

    -- PLATOS A LA CARTA
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Lomo de Carne', 'Platos a la Carta', 25.00),
        ('Lomo de Pollo', 'Platos a la Carta', 20.00),
        ('Lomo Saltado', 'Platos a la Carta', 22.00),
        ('Chaufa de Carne', 'Platos a la Carta', 18.00),
        ('Chaufa de Pollo', 'Platos a la Carta', 16.00),
        ('Tallarín Saltado de Pollo', 'Platos a la Carta', 16.00),
        ('Tallarín Saltado de Carne', 'Platos a la Carta', 18.00);

    -- OTROS
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Mostrito 1/8', 'Otros', 8.00);

    -- PORCIONES Y GUARNICIONES
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Porción de Arroz Personal', 'Porciones y Guarniciones', 3.00),
        ('Porción de Arroz Mediana', 'Porciones y Guarniciones', 6.00),
        ('Porción de Arroz Grande', 'Porciones y Guarniciones', 10.00),
        ('Porción de Papa Mediana', 'Porciones y Guarniciones', 5.00),
        ('Porción de Papa Grande', 'Porciones y Guarniciones', 8.00),
        ('Ensalada Mediana', 'Porciones y Guarniciones', 6.00),
        ('Ensalada Grande', 'Porciones y Guarniciones', 10.00),
        ('Unidad de Chorizo', 'Porciones y Guarniciones', 4.00);

    -- GASEOSAS
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Gaseosa 2 Litros', 'Gaseosas', 10.00),
        ('Gaseosa 1 Litro', 'Gaseosas', 6.00),
        ('Gaseosa Gordita', 'Gaseosas', 4.00),
        ('Gaseosa Personal', 'Gaseosas', 2.50),
        ('Pepsi 1 Litro', 'Gaseosas', 6.00),
        ('Agua Mineral', 'Gaseosas', 2.00);

    -- INFUSIONES
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Café', 'Infusiones', 3.00),
        ('Infusiones', 'Infusiones', 3.00);

    -- REFRESCOS
    INSERT INTO Productos (Nombre, Categoria, Precio) VALUES
        ('Chicha Morada 1L', 'Refrescos', 5.00),
        ('Chicha Morada 1.5L', 'Refrescos', 7.00),
        ('Cebada 1L', 'Refrescos', 5.00),
        ('Cebada 1.5L', 'Refrescos', 7.00),
        ('Limonada Frozen 1L', 'Refrescos', 6.00),
        ('Maracuyá 1L', 'Refrescos', 6.00);
END
GO

-- ============================================
-- Tabla de Pedidos
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pedidos' AND xtype='U')
BEGIN
    CREATE TABLE Pedidos (
        IdPedido INT IDENTITY(1,1) PRIMARY KEY,
        IdMesa INT NULL,
        TipoAtencion VARCHAR(20) NOT NULL, -- 'Salon' o 'Delivery'
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        Estado VARCHAR(20) NOT NULL DEFAULT 'Abierto', -- Abierto, Cerrado, Cancelado
        Total DECIMAL(10,2) NOT NULL DEFAULT 0,
        FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
    );
END
GO

-- ============================================
-- Tabla de Detalle de Pedidos
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DetallePedidos' AND xtype='U')
BEGIN
    CREATE TABLE DetallePedidos (
        IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
        IdPedido INT NOT NULL,
        IdProducto INT NOT NULL,
        NombreProducto VARCHAR(200) NOT NULL,
        Cantidad INT NOT NULL DEFAULT 1,
        PrecioUnitario DECIMAL(10,2) NOT NULL DEFAULT 0,
        FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido),
        FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
    );
END
GO

-- ============================================
-- Tabla de Ventas
-- ============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Ventas' AND xtype='U')
BEGIN
    CREATE TABLE Ventas (
        IdVenta INT IDENTITY(1,1) PRIMARY KEY,
        IdPedido INT NOT NULL,
        NombreCliente VARCHAR(100) NOT NULL,
        ApellidoCliente VARCHAR(100) NOT NULL,
        DNI VARCHAR(8) NOT NULL,
        RUC VARCHAR(11) NULL,
        TipoComprobante VARCHAR(20) NOT NULL DEFAULT 'Boleta', -- Factura o Boleta
        MetodoPago VARCHAR(50) NOT NULL DEFAULT 'Efectivo',
        MontoTotal DECIMAL(10,2) NOT NULL DEFAULT 0,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido)
    );
END
GO

PRINT '✅ Base de datos ParrillaguilleDB creada correctamente.';
PRINT '✅ Tablas: Mesas, Categorias, Productos, Pedidos, DetallePedidos, Ventas';
PRINT '✅ Datos iniciales del menú insertados.';
GO
