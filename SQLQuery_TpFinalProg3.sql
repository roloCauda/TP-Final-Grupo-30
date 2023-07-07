drop database BD_ecommerce

create database BD_ecommerce
go
use BD_ecommerce
go
-- Crear la tabla de Categorias
CREATE TABLE Categorias (
Id INT PRIMARY KEY IDENTITY,
Descripcion VARCHAR(100) NOT NULL,
ImagenURL VARCHAR(255) NULL
);
go

-- Crear la tabla de Marcas
CREATE TABLE Marcas (
Id INT PRIMARY KEY IDENTITY,
Descripcion VARCHAR(100) NOT NULL,
ImagenURL VARCHAR(255) NULL
);	
go
-- Crear la tabla de FormasDePago
CREATE TABLE FormasDePago (
Id INT PRIMARY KEY IDENTITY,
Descripcion VARCHAR(100) NOT NULL
);
go
-- Crear la tabla de Articulos
CREATE TABLE Articulos (
Id INT PRIMARY KEY IDENTITY,
Codigo VARCHAR(50) NULL,
Nombre VARCHAR(100)  NOT NULL,
Descripcion VARCHAR(255) NULL,
IdMarca INT  NOT NULL,
IdCategoria INT  NOT NULL,
Precio DECIMAL(10, 2) NULL,
FOREIGN KEY (IdMarca) REFERENCES Marcas(Id),
FOREIGN KEY (IdCategoria) REFERENCES Categorias(Id)
);
go
-- Crear la tabla de Tipo de Acceso
CREATE TABLE TipoDeAcceso (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL
);
go
-- Crear la tabla de Provincias
CREATE TABLE PROVINCIAS(
	Id INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(100) NOT NULL
);
go
-- Crear la tabla de Localidades
CREATE TABLE LOCALIDADES(
	Id INT PRIMARY KEY IDENTITY,
	IdProvincia INT	not null,
	Descripcion VARCHAR(100) NOT NULL
	FOREIGN KEY (IdProvincia) REFERENCES Provincias(Id)
);
go
-- Crear la tabla de Pedidos
CREATE TABLE DIRECCIONES(
	Id INT PRIMARY KEY IDENTITY,
	Calle VARCHAR(100) NOT NULL,
	Numero INT NOT NULL,
	Piso INT NULL,
	Departamento INT NULL,
	CP VARCHAR(100) NOT NULL,
	IdLocalidad INT NOT NULL,
	IdProvincia INT NOT NULL,
	FOREIGN KEY (IdLocalidad) REFERENCES Localidades(Id),
	FOREIGN KEY (IdProvincia) REFERENCES Provincias(Id)
);
GO
-- Crear la tabla de Usuario
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Nombres VARCHAR(100) NOT NULL,
	Apellidos VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
	Telefono VARCHAR(100) NULL,
	IDDomicilio INT NULL,
	DNI INT NOT NULL,
	TipoAcceso INT NOT NULL,
	FOREIGN KEY (TipoAcceso) REFERENCES TipoDeAcceso(Id),
	FOREIGN KEY (IDDomicilio) REFERENCES Direcciones(Id)
);
go
-- Crear la tabla de Pedidos
CREATE TABLE PEDIDOS (
Id INT PRIMARY KEY IDENTITY,
Fecha DATE NOT NULL,
IdFormaPago INT NOT NULL,
IdCliente INT NOT NULL,
FOREIGN KEY (IdFormaPago) REFERENCES FormasDePago(Id),
FOREIGN KEY (IdCliente) REFERENCES Usuarios(Id)
);
go
-- Crear la tabla de Pedidos
CREATE TABLE ARTICULOSxPEDIDO (
Id INT PRIMARY KEY IDENTITY,
IdPedido INT NOT NULL,
IdArticulo INT NOT NULL,
Cantidad INT NOT NULL,
FOREIGN KEY (IdPedido) REFERENCES PEDIDOS(Id),
FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);
go
-- Crear la tabla de Stock
CREATE TABLE Stock (
Id INT PRIMARY KEY IDENTITY,
IdArticulo INT NOT NULL,
Cantidad INT NOT NULL DEFAULT(0),
Precio DECIMAL(10, 2) NULL,
FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);
go
-- Crear la tabla de Imagenes
CREATE TABLE Imagenes (
Id INT PRIMARY KEY IDENTITY,
IdArticulo INT NOT NULL,
ImagenURL VARCHAR(255) NULL
FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id),
);
go

-- Incersiones a tablas
INSERT INTO Categorias (Descripcion)
VALUES ('Impresoras'), ('Cartuchos'), ('Chip'), ('Tóner'), ('Repuestos');
GO

INSERT INTO Marcas (Descripcion)
VALUES ('Samsung'), ('Lexmark'), ('HP'), ('Xerox'), ('Ricoh');
GO

INSERT INTO FormasDePago (Descripcion)
VALUES ('Tarjeta de crédito'), ('Transferencia bancaria'), ('PayPal'), ('Efectivo'), ('Criptomonedas');
GO

INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)
VALUES ('ELEC001', 'Lexmark 464', 'Impresora multifunción', 2, 1, 120000),
('CLOTH001', 'Samsung 6322', 'Impresora Multifunción', 1, 1, 100000),
('HOME001', 'Samsung 2851', 'Impresora doble faz', 1, 1, 60000);
GO

INSERT INTO TipoDeAcceso (Descripcion)
VALUES ('Administrador'), ('Empleado'), ('Cliente');
GO

INSERT INTO PROVINCIAS (Descripcion)
VALUES ('Buenos Aires'), ('La Pampa');
GO

INSERT INTO LOCALIDADES (IdProvincia, Descripcion)
VALUES (1, 'CABA'),
(1, 'Azul'),(1, 'Zarate'),(2, 'Ataliva Roca');
GO

INSERT INTO DIRECCIONES (Calle, Numero, CP, IdLocalidad, IdProvincia)
VALUES ('Nazca', 3258, 1419, 1, 1);
GO

INSERT INTO Usuarios (Nombres, Apellidos, Email, Contraseña, Telefono, DNI, TipoAcceso)
VALUES ('Marta', 'Tripoli', 'MartaT@gmail.com', '123', '43826524', 33359541, 3),
('Catalina', 'Carod', 'Cata126@gmail.com', '123456', '1543826524', 23359521, 2);
GO

INSERT INTO Usuarios (Nombres, Apellidos, Email, Contraseña, Telefono, IDDomicilio, DNI, TipoAcceso)
VALUES ('Juan', 'Perez', 'admin@example.com', 'admin123', '45826544', 1, 32359042, 1);
GO

INSERT INTO Pedidos (Fecha, IdFormaPago, IdCliente)
VALUES ('2023-06-10', 1, 4),
('2023-06-11', 2, 4);
GO

INSERT INTO ARTICULOSxPEDIDO (IdPedido, IdArticulo, Cantidad)
VALUES (1, 1, 2),(1, 3, 1), (2, 1, 3);
GO

INSERT INTO Stock (Cantidad, IdArticulo, Precio)
VALUES (10, 1, 5200),
(5, 2, 710),
(8, 3, 950.5);
GO
-- Procedimientos almacenados
CREATE procedure storedListar
as
begin
	Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria
	From ARTICULOS A, CATEGORIAS C, MARCAS M
	Where C.Id = A.IdCategoria And M.Id = A.IdMarca
end
GO

CREATE PROCEDURE storedImg
   @IdArticulo INT
AS
BEGIN
   SELECT ID, IDARTICULO, IMAGENURL
   FROM IMAGENES
   WHERE IDARTICULO = @IdArticulo;
END
GO

create PROCEDURE storedArticulo
   @IdArticulo INT
AS
BEGIN
   Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, PrecioOferta, A.IdMarca, A.IdCategoria
	from ARTICULOS A
	left join MARCAS M on A.IdMarca = M.Id
	left join CATEGORIAS C on A.IdCategoria = C.Id
	where A.Id = @IdArticulo
END
GO

CREATE PROCEDURE storedCategoria
   @IdCategoria INT
AS
BEGIN
   SELECT Id, Descripcion, ImagenURL
   FROM Categorias
   WHERE Id = @IdCategoria
END
GO

CREATE PROCEDURE storedMarca
   @IdMarca INT
AS
BEGIN
   SELECT Id, Descripcion, ImagenURL
   FROM Marcas
   WHERE Id = @IdMarca
END