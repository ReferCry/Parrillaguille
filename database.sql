-- ============================================
-- Script de creación de base de datos
-- Parrillaguille - Sistema de Ventas
-- Base de datos: SQL Server
-- Ejecutar desde SSMS en el servidor local
-- ============================================

-- Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ParrillaguilleDB')
BEGIN
    CREATE DATABASE ParrillaguilleDB;
END
GO

USE ParrillaguilleDB;
GO

-- Eliminar tablas existentes (orden por FK)
IF OBJECT_ID('Ventas', 'U') IS NOT NULL DROP TABLE Ventas;
IF OBJECT_ID('DetallePedidos', 'U') IS NOT NULL DROP TABLE DetallePedidos;
IF OBJECT_ID('Pedidos', 'U') IS NOT NULL DROP TABLE Pedidos;
IF OBJECT_ID('Productos', 'U') IS NOT NULL DROP TABLE Productos;
IF OBJECT_ID('Categorias', 'U') IS NOT NULL DROP TABLE Categorias;
IF OBJECT_ID('Mesas', 'U') IS NOT NULL DROP TABLE Mesas;
GO

-- ============================================
-- Tabla de Mesas
-- ============================================
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
GO

-- ============================================
-- Tabla de Categorías de Productos
-- ============================================
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
GO

-- ============================================
-- Tabla de Productos (Menú)
-- ============================================
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
GO

-- ============================================
-- Tabla de Pedidos
-- ============================================
CREATE TABLE Pedidos (
    IdPedido INT IDENTITY(1,1) PRIMARY KEY,
    IdMesa INT NULL,
    TipoAtencion VARCHAR(20) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Estado VARCHAR(20) NOT NULL DEFAULT 'Abierto',
    Total DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (IdMesa) REFERENCES Mesas(IdMesa)
);
GO

-- ============================================
-- Tabla de Detalle de Pedidos
-- ============================================
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
GO

-- ============================================
-- Tabla de Ventas
-- ============================================
CREATE TABLE Ventas (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL,
    NombreCliente VARCHAR(100) NOT NULL,
    ApellidoCliente VARCHAR(100) NOT NULL,
    DNI VARCHAR(8) NOT NULL,
    RUC VARCHAR(11) NULL,
    TipoComprobante VARCHAR(20) NOT NULL DEFAULT 'Boleta',
    MetodoPago VARCHAR(50) NOT NULL DEFAULT 'Efectivo',
    MontoTotal DECIMAL(10,2) NOT NULL DEFAULT 0,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido)
);
GO

-- ============================================
-- DATOS DE PRUEBA - Pedidos, Detalles y Ventas
-- Fechas distribuidas desde 01/05/2026 hasta 12/06/2026
-- ============================================

-- PEDIDO 1 - 01/05/2026 - Salon Mesa 1
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (1, 'Salon', '2026-05-01 12:30:00', 'Cerrado', 81.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (1, 1, '1 Pollo a la Brasa', 1, 35.00),
    (1, 27, 'Gaseosa 1 Litro', 2, 6.00),
    (1, 19, 'Porción de Arroz Grande', 1, 10.00),
    (1, 38, 'Chicha Morada 1L', 1, 5.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (1, 'Juan', 'Pérez García', '45123698', 'Boleta', 'Efectivo', 81.00, '2026-05-01 12:45:00');

-- PEDIDO 2 - 03/05/2026 - Salon Mesa 3
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (3, 'Salon', '2026-05-03 13:15:00', 'Cerrado', 64.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (2, 5, 'Chuleta', 2, 18.00),
    (2, 28, 'Gaseosa Gordita', 3, 4.00),
    (2, 21, 'Ensalada Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (2, 'María', 'López Ramírez', '70258146', 'Boleta', 'Efectivo', 64.00, '2026-05-03 13:30:00');

-- PEDIDO 3 - 05/05/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-05-05 19:00:00', 'Cerrado', 97.50);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (3, 1, '1 Pollo a la Brasa', 2, 35.00),
    (3, 27, 'Gaseosa 1 Litro', 1, 6.00),
    (3, 37, 'Cebada 1L', 1, 5.00),
    (3, 19, 'Porción de Arroz Grande', 1, 10.00),
    (3, 20, 'Porción de Papa Mediana', 1, 5.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (3, 'Carlos', 'Mendoza Silva', '48963217', 'Boleta', 'Efectivo', 97.50, '2026-05-05 19:20:00');

-- PEDIDO 4 - 08/05/2026 - Salon Mesa 5
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (5, 'Salon', '2026-05-08 14:00:00', 'Cerrado', 110.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (4, 12, 'Lomo de Carne', 2, 25.00),
    (4, 6, 'Bife', 1, 22.00),
    (4, 27, 'Gaseosa 1 Litro', 2, 6.00),
    (4, 36, 'Gaseosa 2 Litros', 1, 10.00),
    (4, 22, 'Ensalada Grande', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (4, 'Roberto', 'García Torres', '15248963', '20456789012', 'Factura', 'Tarjeta de Crédito', 110.00, '2026-05-08 14:25:00');

-- PEDIDO 5 - 10/05/2026 - Salon Mesa 2
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (2, 'Salon', '2026-05-10 12:00:00', 'Cerrado', 53.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (5, 2, '1/2 Pollo', 1, 20.00),
    (5, 10, 'Porción de Chorizo', 2, 10.00),
    (5, 19, 'Porción de Arroz Personal', 1, 3.00),
    (5, 39, 'Chicha Morada 1.5L', 1, 7.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (5, 'Ana', 'Rodríguez Vargas', '70321456', 'Boleta', 'Yape', 53.00, '2026-05-10 12:20:00');

-- PEDIDO 6 - 12/05/2026 - Salon Mesa 8
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (8, 'Salon', '2026-05-12 18:30:00', 'Cerrado', 88.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (6, 14, 'Lomo Saltado', 2, 22.00),
    (6, 15, 'Chaufa de Carne', 1, 18.00),
    (6, 27, 'Gaseosa 1 Litro', 1, 6.00),
    (6, 38, 'Chicha Morada 1L', 1, 5.00),
    (6, 19, 'Porción de Arroz Grande', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (6, 'Luis', 'Fernández Castillo', '41258963', 'Boleta', 'Efectivo', 88.00, '2026-05-12 18:50:00');

-- PEDIDO 7 - 15/05/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-05-15 20:15:00', 'Cerrado', 72.50);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (7, 3, '1/4 Pollo', 2, 12.00),
    (7, 9, 'Tira de Cerdo', 1, 17.00),
    (7, 29, 'Gaseosa Personal', 2, 2.50),
    (7, 40, 'Limonada Frozen 1L', 1, 6.00),
    (7, 23, 'Unidad de Chorizo', 2, 4.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (7, 'Patricia', 'Huamán Díaz', '70145896', 'Boleta', 'Efectivo', 72.50, '2026-05-15 20:35:00');

-- PEDIDO 8 - 18/05/2026 - Salon Mesa 10
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (10, 'Salon', '2026-05-18 13:45:00', 'Cerrado', 125.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (8, 1, '1 Pollo a la Brasa', 1, 35.00),
    (8, 12, 'Lomo de Carne', 1, 25.00),
    (8, 13, 'Lomo de Pollo', 1, 20.00),
    (8, 36, 'Gaseosa 2 Litros', 2, 10.00),
    (8, 20, 'Porción de Papa Grande', 1, 8.00),
    (8, 21, 'Ensalada Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (8, 'Miguel', 'Sánchez Rojas', '20159874', '20567890123', 'Factura', 'Tarjeta de Crédito', 125.00, '2026-05-18 14:10:00');

-- PEDIDO 9 - 20/05/2026 - Salon Mesa 7
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (7, 'Salon', '2026-05-20 19:30:00', 'Cerrado', 45.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (9, 7, 'Parrilla de Pollo Pecho', 2, 16.00),
    (9, 28, 'Gaseosa Gordita', 1, 4.00),
    (9, 19, 'Porción de Arroz Personal', 1, 3.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (9, 'Elena', 'Torres Muñoz', '45896321', 'Boleta', 'Yape', 45.00, '2026-05-20 19:50:00');

-- PEDIDO 10 - 23/05/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-05-23 18:00:00', 'Cerrado', 103.50);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (10, 1, '1 Pollo a la Brasa', 1, 35.00),
    (10, 14, 'Lomo Saltado', 1, 22.00),
    (10, 16, 'Chaufa de Pollo', 1, 16.00),
    (10, 27, 'Gaseosa 1 Litro', 2, 6.00),
    (10, 19, 'Porción de Arroz Grande', 1, 10.00),
    (10, 41, 'Maracuyá 1L', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (10, 'Pedro', 'Quispe Alvarado', '46321789', 'Boleta', 'Efectivo', 103.50, '2026-05-23 18:25:00');

-- PEDIDO 11 - 25/05/2026 - Salon Mesa 4
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (4, 'Salon', '2026-05-25 12:45:00', 'Cerrado', 58.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (11, 4, '1/8 Pollo', 2, 7.00),
    (11, 8, 'Parrilla de Pollo Pierna', 1, 15.00),
    (11, 30, 'Pepsi 1 Litro', 1, 6.00),
    (11, 24, 'Porción de Arroz Personal', 2, 3.00),
    (11, 25, 'Porción de Arroz Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (11, 'Rosa', 'Mamani Condori', '70458123', 'Boleta', 'Efectivo', 58.00, '2026-05-25 13:05:00');

-- PEDIDO 12 - 28/05/2026 - Salon Mesa 6
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (6, 'Salon', '2026-05-28 20:00:00', 'Cerrado', 140.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (12, 1, '1 Pollo a la Brasa', 2, 35.00),
    (12, 5, 'Chuleta', 1, 18.00),
    (12, 6, 'Bife', 1, 22.00),
    (12, 36, 'Gaseosa 2 Litros', 1, 10.00),
    (12, 39, 'Chicha Morada 1.5L', 1, 7.00),
    (12, 22, 'Ensalada Grande', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (12, 'Fernando', 'Vargas Pinto', '10258963', '20678901234', 'Factura', 'Tarjeta de Crédito', 140.00, '2026-05-28 20:25:00');

-- PEDIDO 13 - 01/06/2026 - Salon Mesa 9
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (9, 'Salon', '2026-06-01 13:00:00', 'Cerrado', 67.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (13, 2, '1/2 Pollo', 1, 20.00),
    (13, 17, 'Tallarín Saltado de Pollo', 1, 16.00),
    (13, 27, 'Gaseosa 1 Litro', 1, 6.00),
    (13, 20, 'Porción de Papa Mediana', 1, 5.00),
    (13, 25, 'Porción de Arroz Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (13, 'Sandra', 'Cruz Mejía', '45789632', 'Boleta', 'Efectivo', 67.00, '2026-06-01 13:20:00');

-- PEDIDO 14 - 04/06/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-06-04 19:45:00', 'Cerrado', 89.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (14, 1, '1 Pollo a la Brasa', 1, 35.00),
    (14, 11, 'Parrilla con Papa Sancochada', 1, 20.00),
    (14, 38, 'Chicha Morada 1L', 2, 5.00),
    (14, 31, 'Agua Mineral', 2, 2.00),
    (14, 19, 'Porción de Arroz Grande', 1, 10.00),
    (14, 10, 'Porción de Chorizo', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (14, 'Jorge', 'Ríos Palacios', '48125963', 'Boleta', 'Yape', 89.00, '2026-06-04 20:05:00');

-- PEDIDO 15 - 07/06/2026 - Salon Mesa 11
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (11, 'Salon', '2026-06-07 14:30:00', 'Cerrado', 76.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (15, 15, 'Chaufa de Carne', 2, 18.00),
    (15, 27, 'Gaseosa 1 Litro', 1, 6.00),
    (15, 37, 'Cebada 1L', 1, 5.00),
    (15, 21, 'Ensalada Mediana', 1, 6.00),
    (15, 19, 'Porción de Arroz Grande', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (15, 'Claudia', 'Navarro Luna', '70258963', 'Boleta', 'Efectivo', 76.00, '2026-06-07 14:50:00');

-- PEDIDO 16 - 09/06/2026 - Salon Mesa 15
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (15, 'Salon', '2026-06-09 12:15:00', 'Cerrado', 115.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (16, 1, '1 Pollo a la Brasa', 1, 35.00),
    (16, 12, 'Lomo de Carne', 1, 25.00),
    (16, 5, 'Chuleta', 1, 18.00),
    (16, 36, 'Gaseosa 2 Litros', 1, 10.00),
    (16, 40, 'Limonada Frozen 1L', 1, 6.00),
    (16, 20, 'Porción de Papa Grande', 1, 8.00),
    (16, 22, 'Ensalada Grande', 1, 10.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, RUC, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (16, 'Ricardo', 'Apaza Choque', '15987456', '20789012345', 'Factura', 'Tarjeta de Crédito', 115.00, '2026-06-09 12:40:00');

-- PEDIDO 17 - 10/06/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-06-10 20:30:00', 'Cerrado', 62.50);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (17, 3, '1/4 Pollo', 3, 12.00),
    (17, 29, 'Gaseosa Personal', 2, 2.50),
    (17, 19, 'Porción de Arroz Personal', 2, 3.00),
    (17, 23, 'Unidad de Chorizo', 1, 4.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (17, 'Gladys', 'Flores Huanca', '45369871', 'Boleta', 'Efectivo', 62.50, '2026-06-10 20:50:00');

-- PEDIDO 18 - 11/06/2026 - Salon Mesa 12
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (12, 'Salon', '2026-06-11 13:30:00', 'Cerrado', 93.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (18, 14, 'Lomo Saltado', 2, 22.00),
    (18, 18, 'Tallarín Saltado de Carne', 1, 18.00),
    (18, 27, 'Gaseosa 1 Litro', 1, 6.00),
    (18, 38, 'Chicha Morada 1L', 1, 5.00),
    (18, 25, 'Porción de Arroz Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (18, 'Oscar', 'Ticona Yupanqui', '41258796', 'Boleta', 'Yape', 93.00, '2026-06-11 13:50:00');

-- PEDIDO 19 - 12/06/2026 - Salon Mesa 14
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (14, 'Salon', '2026-06-12 12:00:00', 'Cerrado', 108.00);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (19, 1, '1 Pollo a la Brasa', 1, 35.00),
    (19, 6, 'Bife', 1, 22.00),
    (19, 16, 'Chaufa de Pollo', 1, 16.00),
    (19, 36, 'Gaseosa 2 Litros', 1, 10.00),
    (19, 41, 'Maracuyá 1L', 1, 6.00),
    (19, 20, 'Porción de Papa Mediana', 1, 5.00),
    (19, 19, 'Porción de Arroz Grande', 1, 10.00),
    (19, 23, 'Unidad de Chorizo', 1, 4.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (19, 'Teresa', 'Valdivia Cárdenas', '70369852', 'Boleta', 'Efectivo', 108.00, '2026-06-12 12:30:00');

-- PEDIDO 20 - 12/06/2026 - Delivery
INSERT INTO Pedidos (IdMesa, TipoAtencion, Fecha, Estado, Total) VALUES
    (NULL, 'Delivery', '2026-06-12 19:15:00', 'Cerrado', 56.50);
INSERT INTO DetallePedidos (IdPedido, IdProducto, NombreProducto, Cantidad, PrecioUnitario) VALUES
    (20, 2, '1/2 Pollo', 1, 20.00),
    (20, 9, 'Tira de Cerdo', 1, 17.00),
    (20, 28, 'Gaseosa Gordita', 2, 4.00),
    (20, 32, 'Café', 2, 3.00),
    (20, 24, 'Porción de Arroz Personal', 1, 3.00),
    (20, 25, 'Porción de Arroz Mediana', 1, 6.00);
INSERT INTO Ventas (IdPedido, NombreCliente, ApellidoCliente, DNI, TipoComprobante, MetodoPago, MontoTotal, Fecha) VALUES
    (20, 'Victor', 'Chávez Espinoza', '46895213', 'Boleta', 'Efectivo', 56.50, '2026-06-12 19:35:00');
GO

-- ============================================
-- Verificación
-- ============================================
PRINT '============================================';
PRINT '  BASE DE DATOS ParrillaguilleDB CREADA';
PRINT '============================================';
PRINT '';
PRINT 'Tablas creadas:';
PRINT '  - Mesas (20 registros)';
PRINT '  - Categorias (8 registros)';
PRINT '  - Productos (38 registros)';
PRINT '  - Pedidos (20 registros)';
PRINT '  - DetallePedidos (~70 registros)';
PRINT '  - Ventas (20 registros)';
PRINT '';
PRINT 'Rango de fechas de ventas: 01/05/2026 - 12/06/2026';
PRINT 'Distribución: 15 Salon + 5 Delivery';
PRINT 'Métodos de pago: Efectivo, Tarjeta de Crédito, Yape';
PRINT 'Comprobantes: Boleta, Factura';
GO
