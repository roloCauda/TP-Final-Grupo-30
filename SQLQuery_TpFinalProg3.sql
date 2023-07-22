drop DATABASE BD_ecommerce

create database BD_ecommerce

-- Crear la tabla de Categorias
create TABLE Categorias (
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

CREATE TABLE FormasDeEnvio (
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
    Departamento VARCHAR(100) NULL,
    CP VARCHAR(100) NOT NULL,
    IdLocalidad INT NOT NULL,
    IdProvincia INT NOT NULL,
    FOREIGN KEY (IdLocalidad) REFERENCES Localidades(Id),
    FOREIGN KEY (IdProvincia) REFERENCES Provincias(Id)
);

CREATE TABLE Usuarios (
	Id INT PRIMARY KEY IDENTITY,
    DNI INT UNIQUE NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Contraseña VARCHAR(100) NOT NULL,
    Telefono VARCHAR(100) NULL,
    IDDomicilio INT NULL,
    TipoAcceso INT NOT NULL,
	Activo bit not null default 1,
    FOREIGN KEY (TipoAcceso) REFERENCES TipoDeAcceso(Id),
    FOREIGN KEY (IDDomicilio) REFERENCES Direcciones(Id)
);

CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY,
    IdFormaPago INT NOT NULL,
    IdCliente INT NOT NULL,
	IdFormaEnvio INT NOT NULL,
    Fecha DATE NOT NULL,
	EstadoPedido VARCHAR(100) null default 'Pendiente',
	CodigoDeTransaccion VARCHAR(100) null,
	CodigoSeguimiento VARCHAR(100) null,
	Observaciones VARCHAR(300) null,
	IdDireccion INT NULL,
    FOREIGN KEY (IdFormaPago) REFERENCES FormasDePago(Id),
    FOREIGN KEY (IdCliente) REFERENCES Usuarios(Id),
	FOREIGN KEY (IdFormaEnvio) REFERENCES FormasDeEnvio(Id),
	FOREIGN KEY (IdDireccion) REFERENCES Direcciones(Id)	
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
	FOREIGN KEY (IdCliente) REFERENCES Usuarios(Id)
);



-- Inserciones en las tablas (QUEDO VIEJO)

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

INSERT INTO Imagenes (IdArticulo, ImagenURL)
VALUES (2, 'https://intercompras.com/images/product/LEXMARK_13C1242.jpg'),
		(3,'https://2.bp.blogspot.com/-Yl9mEBVUeA0/W7RAmlmuPNI/AAAAAAAAB4o/1pBKpgFkjBse5YB-1R93pB7GQVNqN-3ZwCPcBGAYYCw/s1600/Samsung%2BSCX-6322DN.jpg'),
		(4,'https://tonerribeirao.com.br/wp-content/uploads/2019/05/impressora-semi-nova-samsung-ml-2851nd-15836-2000-23153.jpg');


INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)
VALUES ('HP664', 'Cartucho 664', 'Cartucho negro', 3, 2, 8200),
       ('T3020', 'Toner 3020', 'Toner Alternativo Para Xerox 3020 106r02773 Phaser 3020', 4, 4, 15700),
       ('R601', 'Toner 407823', 'Toner Ricoh Original Mp 601/501spf/601spf/sp5300dn (407823)', 5, 4, 6000);

INSERT INTO Imagenes (IdArticulo, ImagenURL)
VALUES (5, 'https://ar-media.hptiendaenlinea.com/catalog/product/cache/74c1057f7991b4edb2bc7bdaa94de933/F/6/F6V29AL-1_T1679641584.png'),
		(6,'https://http2.mlstatic.com/D_NQ_NP_2X_756828-MLA51162136243_082022-F.webp'),
		(7,'https://http2.mlstatic.com/D_NQ_NP_865913-MLA47049744757_082021-O.webp');

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
VALUES ('Mosconi', 1485, '1684', 3, 2),
		('Griveo', 2630, '1406', 2, 1),
		('Nazca', 3258, '1419', 1, 1);

INSERT INTO Usuarios (DNI, Nombres, Apellidos, Email, Contraseña, Telefono, IDDomicilio, TipoAcceso)
VALUES (33359541, 'Marta', 'Tripoli', 'MartaT@gmail.com', '123','43826524', 1, 3),
       (23359521, 'Catalina', 'Carod', 'Cata126@gmail.com', '123456', '1543826524', 2, 2),
       (32359042, 'Juan', 'Perez', 'admin@example.com', 'admin123', '45826544', 3, 1);

INSERT INTO FormasDeEnvio (Descripcion)
VALUES ('Retiro'),('Envio a cargo del vendedor');

INSERT INTO Pedidos (Fecha, IdFormaPago, IdFormaEnvio, IdCliente)
VALUES ('2023-06-10', 1, 1, 1),
       ('2023-06-11', 2, 2, 2);

INSERT INTO ARTICULOSxPEDIDO (IdPedido, IdArticulo, Cantidad, PrecioUnitario)
VALUES (1, 2, 2, 1000),
       (1, 3, 1, 2500),
       (2, 2, 3, 9600);

INSERT INTO Stock (IdArticulo, Cantidad)
VALUES (2, 10),
       (3, 5),
       (4, 8);

INSERT INTO Stock (IdArticulo, Cantidad)
VALUES (5, 1),
       (6, 3),
       (7, 8);

INSERT INTO Favoritos(IdCliente, IdArticulo)
VALUES (1, 2),
		(1, 3),
       (2, 3),
	   (3, 2),
       (3, 4);

UPDATE Marcas SET ImagenURL = 'https://img.freepik.com/iconos-gratis/samsung_318-668804.jpg' where Id = 1;
UPDATE Marcas SET ImagenURL = 'https://marcas-logos.net/wp-content/uploads/2020/11/Lexmark-Logo-1991.jpg' where Id = 2;
UPDATE Marcas SET ImagenURL = 'https://vectorseek.com/wp-content/uploads/2021/01/HP-Logo-Vector-scaled.jpg' where Id = 3;
UPDATE Marcas SET ImagenURL = 'https://1000marcas.net/wp-content/uploads/2020/02/Xerox-s%C3%ADmbolo.jpg' where Id = 4;
UPDATE Marcas SET ImagenURL = 'https://www.vectorizados.com/muestras/ricoh.jpg' where Id = 5;

UPDATE Categorias SET ImagenURL = 'https://img2.freepng.es/20180314/rzw/kisspng-printer-free-content-printing-clip-art-free-microsoft-office-clipart-5aa9bacb40b896.6468117015210728432651.jpg' where Id = 1;
UPDATE Categorias SET ImagenURL = 'https://media.istockphoto.com/id/1172928330/es/vector/ilustraci%C3%B3n-plana-simple-vectorial-de-cartuchos-de-tinta-cmyk-que-consisten-en-cian-magenta.jpg?s=612x612&w=0&k=20&c=AFj0bc0l_tzHs4IDo9rV9ivyCepZVJh-0hHDxuEOrZM=' where Id = 2;
UPDATE Categorias SET ImagenURL = 'https://media.istockphoto.com/id/1449014621/es/vector/silueta-de-chip-o-componente-esquem%C3%A1tico-simple-para-microcircuitos-aislados-sobre-fondo.jpg?s=170667a&w=0&k=20&c=2xdrZp6O_7Dk9ZE4P18QJwkVlqLBfnAT7ldejXDyHOc=' where Id = 3;
UPDATE Categorias SET ImagenURL = 'https://png.pngtree.com/png-vector/20220607/ourlarge/pngtree-toner-cartridge-icon-simple-vector-png-image_4853317.png' where Id = 4;
UPDATE Categorias SET ImagenURL = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQvY4LggLlVcm40a-3o79LNugk3hk8dh63hSeGj3P7g4Dkqvpwbsla_lu56DUZam0TjFiw&usqp=CAU' where Id = 5;

-- Procedimientos almacenados
alter procedure storedListar
as
begin
	Select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.Precio, A.IdMarca, A.IdCategoria, S.Cantidad
	From ARTICULOS A, CATEGORIAS C, MARCAS M, Stock S
	Where C.Id = A.IdCategoria And M.Id = A.IdMarca And A.Id = S.IdArticulo
end

CREATE PROCEDURE storedImg
   @IdArticulo INT
AS
BEGIN
   SELECT ID, IDARTICULO, IMAGENURL
   FROM IMAGENES
   WHERE IDARTICULO = @IdArticulo;
END

alter PROCEDURE storedArticulo
   @IdArticulo INT
AS
BEGIN
   Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria, S.Cantidad
	from ARTICULOS A
	left join MARCAS M on A.IdMarca = M.Id
	left join CATEGORIAS C on A.IdCategoria = C.Id
	left join Stock S ON A.Id = S.IdArticulo
	where A.Id = @IdArticulo
END

CREATE PROCEDURE storedCategoria
   @IdCategoria INT
AS
BEGIN
   SELECT Id, Descripcion, ImagenURL
   FROM Categorias
   WHERE Id = @IdCategoria
END

CREATE PROCEDURE storedMarca
   @IdMarca INT
AS
BEGIN
   SELECT Id, Descripcion, ImagenURL
   FROM Marcas
   WHERE Id = @IdMarca
END

CREATE PROCEDURE storedFiltro
   @filtro varchar(100)
AS
BEGIN
   SELECT A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, Precio, A.IdMarca, A.IdCategoria, s.Cantidad
   FROM ARTICULOS A, CATEGORIAS C, MARCAS M, Stock S
   WHERE C.Id = A.IdCategoria AND M.Id = A.IdMarca and A.Id = s.IdArticulo
   GROUP BY A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion, C.Descripcion, Precio, A.IdMarca, A.IdCategoria, s.Cantidad
   HAVING Codigo LIKE '%' + @filtro + '%' OR Nombre LIKE '%' + @filtro + '%' OR A.Descripcion LIKE '%' + @filtro + '%' OR M.Descripcion LIKE '%' + @filtro + '%' OR C.Descripcion LIKE '%' + @filtro + '%'
END





/*PARA PROBAR*/
select * from Marcas
select * from Categorias
select * from Usuarios
select * from direcciones
select * from Pedidos
select * from FormasDeEnvio
select * from FormasDePago
select * from Favoritos
select * from Articulos INNER JOIN STOCK ON STOCK.IdArticulo = Articulos.Id
select * from stock
select * from ARTICULOSxPEDIDO


/*para saber las restricciones de una tabla*/
SELECT CONSTRAINT_NAME, CONSTRAINT_TYPE
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_SCHEMA = 'dbo'
AND TABLE_NAME = 'Favoritos';

update pedidos set  EstadoPedido = 'En Proceso' where Id = 2
update Usuarios set  IDDomicilio = 1 where Id = 2