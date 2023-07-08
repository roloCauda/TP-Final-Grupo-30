drop DATABASE BD_ecommerce

create database BD_ecommerce

use BD_ecommerce

-- Crear la tabla de Categorias
CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL,
    ImagenURL VARCHAR(255) NULL
);

CREATE TABLE Marcas (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL,
    ImagenURL VARCHAR(255) NULL
);

CREATE TABLE FormasDePago (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL
);

CREATE TABLE Articulos (
    Id INT PRIMARY KEY IDENTITY,
    Codigo VARCHAR(50) NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    IdMarca INT NOT NULL,
    IdCategoria INT NOT NULL,
    Precio DECIMAL(10, 2) NULL CHECK (Precio >= 0),
    PrecioDescuento DECIMAL(10, 2) NULL CHECK (PrecioDescuento >= 0),
    FOREIGN KEY (IdMarca) REFERENCES Marcas(Id),
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(Id)
);

CREATE TABLE TipoDeAcceso (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL
);

CREATE TABLE Provincias (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(100) NOT NULL
);

CREATE TABLE Localidades (
    Id INT PRIMARY KEY IDENTITY,
    IdProvincia INT NOT NULL,
    Descripcion VARCHAR(100) NOT NULL,
    FOREIGN KEY (IdProvincia) REFERENCES Provincias(Id)
);

CREATE TABLE Direcciones (
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

CREATE TABLE Usuarios (
    DNI INT PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
    Telefono VARCHAR(100) NULL,
    IDDomicilio INT NULL,
    TipoAcceso INT NOT NULL,
    FOREIGN KEY (TipoAcceso) REFERENCES TipoDeAcceso(Id),
    FOREIGN KEY (IDDomicilio) REFERENCES Direcciones(Id)
);

CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY,
    IdFormaPago INT NOT NULL,
    IdCliente INT NOT NULL,
    Fecha DATE NOT NULL,
    FOREIGN KEY (IdFormaPago) REFERENCES FormasDePago(Id),
    FOREIGN KEY (IdCliente) REFERENCES Usuarios(DNI)
);

CREATE TABLE ARTICULOSxPEDIDO (
    IdPedido INT NOT NULL,
    IdArticulo INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY(IdPedido, IdArticulo),
    FOREIGN KEY (IdPedido) REFERENCES Pedidos(Id),
    FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);

CREATE TABLE Stock (
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    IdArticulo INT NOT NULL,
    Cantidad INT NOT NULL DEFAULT(0),
    Precio DECIMAL(10, 2) NULL CHECK (Precio >= 0),
    FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);

CREATE TABLE Imagenes (
    Id INT PRIMARY KEY IDENTITY,
    IdArticulo INT NOT NULL,
    ImagenURL VARCHAR(255) NULL,
    FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);

CREATE TABLE Favoritos (
	IdCliente INT NOT NULL,
	IdArticulo INT NOT NULL,
	PRIMARY KEY(IdCliente, IdArticulo),
	FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id),
	FOREIGN KEY (IdCliente) REFERENCES Usuarios(DNI)
);
-- Inserciones en las tablas

INSERT INTO Categorias (Descripcion)
VALUES ('Impresoras'), ('Cartuchos'), ('Chip'), ('Tóner'), ('Repuestos');

INSERT INTO Marcas (Descripcion)
VALUES ('Samsung'), ('Lexmark'), ('HP'), ('Xerox'), ('Ricoh');

INSERT INTO FormasDePago (Descripcion)
VALUES ('Tarjeta de crédito'), ('Transferencia bancaria'), ('PayPal'), ('Efectivo'), ('Criptomonedas');

INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)
VALUES ('ELEC001', 'Lexmark 464', 'Impresora multifunción', 2, 1, 120000),
       ('CLOTH001', 'Samsung 6322', 'Impresora Multifunción', 1, 1, 100000),
       ('HOME001', 'Samsung 2851', 'Impresora doble faz', 1, 1, 60000);

INSERT INTO TipoDeAcceso (Descripcion)
VALUES ('Administrador'), ('Empleado'), ('Cliente');

INSERT INTO Provincias (Descripcion)
VALUES ('Buenos Aires'), ('La Pampa');

INSERT INTO Localidades (IdProvincia, Descripcion)
VALUES (1, 'CABA'),
       (1, 'Azul'),
       (1, 'Zarate'),
       (2, 'Ataliva Roca');

INSERT INTO Direcciones (Calle, Numero, CP, IdLocalidad, IdProvincia)
VALUES ('Nazca', 3258, '1419', 1, 1);

INSERT INTO Usuarios (DNI, Nombres, Apellidos, Email, Contraseña, Telefono, IDDomicilio, TipoAcceso)
VALUES (33359541, 'Marta', 'Tripoli', 'MartaT@gmail.com', '123', '43826524', 1, 3),
       (23359521, 'Catalina', 'Carod', 'Cata126@gmail.com', '123456', '1543826524', NULL, 2),
       (32359042, 'Juan', 'Perez', 'admin@example.com', 'admin123', '45826544', 1, 1);

INSERT INTO Pedidos (Fecha, IdFormaPago, IdCliente)
VALUES ('2023-06-10', 1, 32359042),
       ('2023-06-11', 2, 23359521);

INSERT INTO ARTICULOSxPEDIDO (IdPedido, IdArticulo, Cantidad, PrecioUnitario)
VALUES (1, 1, 2, 1000),
       (1, 3, 1, 2500),
       (2, 1, 3, 9600);

INSERT INTO Stock (IdArticulo, Cantidad, Precio)
VALUES (1, 10, 5200),
       (2, 5, 710),
       (3, 8, 950.5);
GO
-- Procedimientos almacenados
CREATE procedure storedListar
as
begin
	Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, PrecioDescuento, A.IdMarca, A.IdCategoria
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

CREATE PROCEDURE storedArticulo
   @IdArticulo INT
AS
BEGIN
   Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, PrecioDescuento, A.IdMarca, A.IdCategoria
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
GO

/*CREATE VIEW vw_PEDIDOSxCLIENTE AS
SELECT P.Id AS 'NUMERO DE PEDIDO',
	A.Descripcion AS 'DESCRIPCION',
	AP.Cantidad AS 'CANTIDAD',
	AP.PrecioUnitario AS 'PRECIO UNITARIO',
	SUM(AP.PrecioUnitario * AP.Cantidad) AS 'SUBTOTAL'
FROM Pedidos P
INNER JOIN Usuarios U ON P.IdCliente = U.DNI
INNER JOIN ARTICULOSxPEDIDO AP ON P.Id = AP.IdPedido
INNER JOIN Articulos A ON A.Id = AP.IdArticulo
GROUP BY P.Id, A.Descripcion, AP.Cantidad, AP.PrecioUnitario;*/

ALTER TABLE Direcciones
ALTER COLUMN Departamento VARCHAR(100) NULL;
ALTER TABLE Usuarios
ALTER COLUMN Email VARCHAR(100) NOT NULL;

select * from Usuarios