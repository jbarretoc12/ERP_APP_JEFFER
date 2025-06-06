USE [BDPRUEBA]
GO

CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nonbre] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Clave] [varchar](100) NULL
)
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [varchar](50) NULL,
	[Marca] [varchar](50) NULL,
	[Precio] [decimal](10, 2) NULL
)


insert into Producto (Nombre,Marca,Precio) values ('teclado','Logitech','25.00')
GO
SELECT NEWID()

GO

SELECT * FROM Usuario
SELECT * FROM Producto


