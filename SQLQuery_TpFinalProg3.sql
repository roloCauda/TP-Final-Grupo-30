create database BD_ecommerce
go
use BD_ecommerce
go
-- Crear la tabla de Categorias
CREATE TABLE Categorias (
Id INT PRIMARY KEY IDENTITY,
Descripcion VARCHAR(100) NOT NULL
);
go
-- Crear la tabla de Imagenes
CREATE TABLE Imagenes (
Id INT PRIMARY KEY IDENTITY,
IdArticulo INT NOT NULL,
ImagenURL VARCHAR(255) NULL
);
go
-- Crear la tabla de Marcas
CREATE TABLE Marcas (
Id INT PRIMARY KEY IDENTITY,
Descripcion VARCHAR(100) NOT NULL
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
IdStock INT NULL,
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
-- Crear la tabla de Usuario
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
	Usuario VARCHAR(100) UNIQUE NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
	TipoAcceso INT NOT NULL,
	FOREIGN KEY (TipoAcceso) REFERENCES TipoDeAcceso(Id)
);
go
-- Crear la tabla de Pedidos
CREATE TABLE Pedidos (
Id INT PRIMARY KEY IDENTITY,
Fecha DATE NOT NULL,
IdArticulo INT NOT NULL,
Cantidad INT NOT NULL,
IdFormaPago INT NOT NULL,
Usuario VARCHAR(100) NOT NULL,
FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id),
FOREIGN KEY (IdFormaPago) REFERENCES FormasDePago(Id),
FOREIGN KEY (Usuario) REFERENCES Usuarios(Usuario)
);
go
-- Crear la tabla de Stock
CREATE TABLE Stock (
Id INT PRIMARY KEY IDENTITY,
Cantidad INT NOT NULL DEFAULT(0),
IdArticulo INT NOT NULL,
FOREIGN KEY (IdArticulo) REFERENCES Articulos(Id)
);

-- Incersiones a tablas
INSERT INTO Categorias (Descripcion)
VALUES ('Impresoras'), ('Cartuchos'), ('Chip'), ('Tóner'), ('Repuestos');

INSERT INTO Marcas (Descripcion)
VALUES ('Samsung'), ('Lexmark'), ('HP'), ('Xerox'), ('Ricoh');

INSERT INTO FormasDePago (Descripcion)
VALUES ('Tarjeta de crédito'), ('Transferencia bancaria'), ('PayPal'), ('Efectivo'), ('Criptomonedas');

INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio, IdStock)
VALUES ('ELEC001', 'Lexmark 464', 'Impresora multifunción', 2, 1, 120000, 5),
('CLOTH001', 'Samsung 6322', 'Impresora Multifunción', 1, 1, 100000, 2),
('HOME001', 'Samsung 2851', 'Impresora doble faz', 1, 1, 60000, 3);

INSERT INTO TipoDeAcceso (Descripcion)
VALUES ('Administrador'), ('Usuario');

INSERT INTO Usuarios (Usuario, Nombre, Email, Contraseña, TipoAcceso)
VALUES ('admin', 'Administrador', 'admin@example.com', 'admin123', 1),
('user1', 'Usuario 1', 'user1@example.com', 'user123', 2),
('user2', 'Usuario 2', 'user2@example.com', 'user456', 2);

INSERT INTO Pedidos (Fecha, IdArticulo, Cantidad, IdFormaPago, Usuario)
VALUES ('2023-06-10', 1, 2, 1, 'user1'),
('2023-06-11', 3, 1, 2, 'user1'),
('2023-06-12', 2, 1, 3, 'user2');

INSERT INTO Stock (Cantidad, IdArticulo)
VALUES (10, 1),
(5, 2),
(8, 3);



-- Procedimientos almacenados
CREATE procedure storedListar
as
begin
	Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria
	From ARTICULOS A, CATEGORIAS C, MARCAS M
	Where C.Id = A.IdCategoria And M.Id = A.IdMarca
end

CREATE PROCEDURE storedImg
   @IdArticulo INT
AS
BEGIN
   SELECT ID, IDARTICULO, IMAGENURL
   FROM IMAGENES
   WHERE IDARTICULO = @IdArticulo;
END

create PROCEDURE storedArticulo
   @IdArticulo INT
AS
BEGIN
   Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria
	from ARTICULOS A
	left join MARCAS M on A.IdMarca = M.Id
	left join CATEGORIAS C on A.IdCategoria = C.Id
	where A.Id = @IdArticulo
END


