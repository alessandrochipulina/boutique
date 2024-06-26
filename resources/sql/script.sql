USE [master]
GO
/****** Object:  Database [BoutiqueXYZ]    Script Date: 10/04/2024 05:11:04 ******/
CREATE DATABASE [BoutiqueXYZ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BoutiqueXYZ', FILENAME = N'/var/opt/mssql/data/BoutiqueXYZ.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BoutiqueXYZ_log', FILENAME = N'/var/opt/mssql/data/BoutiqueXYZ_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BoutiqueXYZ] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BoutiqueXYZ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BoutiqueXYZ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET ARITHABORT OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BoutiqueXYZ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BoutiqueXYZ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BoutiqueXYZ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BoutiqueXYZ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET RECOVERY FULL 
GO
ALTER DATABASE [BoutiqueXYZ] SET  MULTI_USER 
GO
ALTER DATABASE [BoutiqueXYZ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BoutiqueXYZ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BoutiqueXYZ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BoutiqueXYZ] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BoutiqueXYZ] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BoutiqueXYZ] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BoutiqueXYZ', N'ON'
GO
ALTER DATABASE [BoutiqueXYZ] SET QUERY_STORE = ON
GO
ALTER DATABASE [BoutiqueXYZ] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BoutiqueXYZ]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_recepcion] [datetime] NULL,
	[fecha_despacho] [datetime] NULL,
	[fecha_entrega] [datetime] NULL,
	[codigo_usuario_creacion] [varchar](10) NULL,
	[codigo_usuario_recepcion] [varchar](10) NULL,
	[codigo_usuario_despacho] [varchar](10) NULL,
	[codigo_usuario_entrega] [varchar](10) NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoDetalle]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoDetalle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_pedido] [int] NOT NULL,
	[sku] [varchar](10) NOT NULL,
	[precioventa] [int] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_PedidoDetalle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_PedidoDetalle] UNIQUE NONCLUSTERED 
(
	[id_pedido] ASC,
	[sku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sku] [varchar](10) NOT NULL,
	[nombre] [varchar](50) NULL,
	[tipo] [varchar](50) NULL,
	[etiquetas] [varchar](100) NULL,
	[precio] [int] NULL,
	[medida] [varchar](50) NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Producto] UNIQUE NONCLUSTERED 
(
	[sku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo_rol] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Rol] UNIQUE NONCLUSTERED 
(
	[codigo_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo_usuario] [varchar](10) NOT NULL,
	[nombre] [varchar](50) NULL,
	[correo] [varchar](100) NULL,
	[telefono] [varchar](50) NULL,
	[puesto] [varchar](50) NULL,
	[codigo_rol] [int] NULL,
	[estado] [int] NULL,
	[pwd] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Usuario] UNIQUE NONCLUSTERED 
(
	[codigo_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF_Pedido_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[PedidoDetalle] ADD  CONSTRAINT [DF_PedidoDetalle_precioventa]  DEFAULT ((0)) FOR [precioventa]
GO
ALTER TABLE [dbo].[PedidoDetalle] ADD  CONSTRAINT [DF_PedidoDetalle_cantidad]  DEFAULT ((1)) FOR [cantidad]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_puesto]  DEFAULT ('VENDEDOR') FOR [puesto]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_id_rol]  DEFAULT ((1)) FOR [codigo_rol]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[PedidoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidoDetalle_Pedido] FOREIGN KEY([id_pedido])
REFERENCES [dbo].[Pedido] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PedidoDetalle] CHECK CONSTRAINT [FK_PedidoDetalle_Pedido]
GO
ALTER TABLE [dbo].[PedidoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidoDetalle_Producto] FOREIGN KEY([sku])
REFERENCES [dbo].[Producto] ([sku])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[PedidoDetalle] CHECK CONSTRAINT [FK_PedidoDetalle_Producto]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([codigo_rol])
REFERENCES [dbo].[Rol] ([codigo_rol])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
/****** Object:  StoredProcedure [dbo].[ResetAll]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ResetAll]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM PedidoDetalle;
	DELETE FROM Pedido;
	DELETE FROM Producto;
	DELETE FROM Usuario;
	DELETE FROM Rol;

	-- ----------------------
	-- 
	-- ROLES 
	--
	-- ----------------------

	INSERT INTO Rol ( codigo_rol, nombre, descripcion) VALUES( 1, 'VENDEDOR', 'Ingresa los pedidos al sistema');
	INSERT INTO Rol ( codigo_rol, nombre, descripcion) VALUES( 2, 'ENCARGADO','Procesa los pedidos');
	INSERT INTO Rol ( codigo_rol, nombre, descripcion) VALUES( 3, 'DELIVERY', 'Programa la entrega');
	INSERT INTO Rol ( codigo_rol, nombre, descripcion) VALUES( 4, 'REPARTIDOR','Entrega los pedidos');

	-- ----------------------
	-- 
	-- USUARIOS 
	--
	-- ----------------------

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0001', 'Juan Perez', 'jperez@boutiquexyz.com', '977173845', 'VENDEDOR JUNIOR', 1, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0002', 'Maria Gomez', 'mgomez@boutiquexyz.com', '976113325', 'VENDEDOR JUNIOR', 1, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0003', 'Luis Acosta', 'lacosta@boutiquexyz.com', '988453662', 'VENDEDOR JUNIOR', 1, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0004', 'Jaime Redondo', 'jredondo@boutiquexyz.com', '955463318', 'VENDEDOR SENIOR', 1, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0005', 'Maya Potter', 'mpotter@boutiquexyz.com', '988231109', 'VENDEDOR SENIOR', 1, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0006', 'Lucia Galois', 'lgalois@boutiquexyz.com', '976224317', 'ADMINISTRADOR', 2, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0007', 'Alessandro Altobelli', 'aaltobelli@boutiquexyz.com', '989341234', 'ADMINISTRADOR', 2, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0008', 'Luciana Volta', 'lvolta@boutiquexyz.com', '997354936', 'PROGRAMADOR DELIVERY', 3, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0009', 'Ricardo Flores', 'rflores@boutiquexyz.com', '978665342', 'PROGRAMADOR DELIVERY', 3, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0010', 'Enrique Borrego', 'eborrego@boutiquexyz.com', '988784532', 'REPARTIDOR JUNIOR', 4, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0011', 'Julio Guerrero', 'jguerrero@boutiquexyz.com', '977192645', 'REPARTIDOR JUNIOR', 4, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0012', 'Andrea Riso', 'ariso@boutiquexyz.com', '988663735', 'REPARTIDOR JUNIOR', 4, '123' );

	INSERT INTO Usuario( codigo_usuario, nombre, correo, telefono, puesto, codigo_rol, pwd )
	VALUES ( 'XYZ0013', 'Oswaldo Mercado', 'omercado@boutiquexyz.com', '976561488', 'REPARTIDOR SENIOR', 4, '123' );

	-- ----------------------
	-- 
	-- PRODUCTOS 
	--
	-- ----------------------

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006001', 'DIOR SAUVAGE 300', 'PERFUME HOMBRE', 'TABACO, VAINILLA, NARANJA', 200, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006002', 'DIOR SAUVAGE 150', 'PERFUME HOMBRE', 'TABACO, VAINILLA, NARANJA', 130, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006003', 'DIOR SAUVAGE 100', 'PERFUME HOMBRE', 'TABACO, VAINILLA, NARANJA', 100, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006005', 'CALVIN KLEIN CK ONE 200', 'PERFUME HOMBRE', 'COCO, VAINILLA, MELON', 130, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006006', 'CALVIN KLEIN CK ONE 150', 'PERFUME HOMBRE', 'COCO, VAINILLA, MELON', 100, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006010', 'CHANEL BLEU DE CHANEL 50', 'PERFUME HOMBRE', 'ORQUIDEA, NARANJA, CAFE', 75, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006013', 'CHANEL BLEU DE CHANEL 100', 'PERFUME HOMBRE', 'ORQUIDEA, NARANJA, CAFE', 130, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006015', 'CHANEL BLEU DE CHANEL 150', 'PERFUME HOMBRE', 'ORQUIDEA, NARANJA, CAFE', 150, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006020', 'YVES SAINT LAURENT EAU DE TOILETTE 140', 'PERFUME HOMBRE', 'TABACO, RON, MADERA', 350, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006023', 'YVES SAINT LAURENT EAU DE TOILETTE 90', 'PERFUME HOMBRE', 'TABACO, RON, MADERA', 250, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006034', 'HUGO BOSS EAU DE TOILETTE 110', 'PERFUME HOMBRE', 'LIMON, NARANJA, MADERA', 170, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006037', 'HUGO BOSS EAU DE TOILETTE 180', 'PERFUME HOMBRE', 'LIMON, NARANJA, MADERA', 270, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006038', 'HUGO BOSS EAU DE TOILETTE 180', 'PERFUME HOMBRE', 'LIMON, NARANJA, MADERA', 270, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006450', 'CAROLINA HERRERA EAU DE PARFUM 80', 'PERFUME MUJER', 'MELON, LIMON, CAFE', 280, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006453', 'CAROLINA HERRERA EAU DE PARFUM 160', 'PERFUME MUJER', 'MELON, LIMON, CAFE', 500, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006470', 'VERSACE EROS POUR FEMME 100', 'PERFUME MUJER', 'ROSAS, ARANDANO', 300, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0006490', 'VERSACE EROS POUR FEMME 150', 'PERFUME MUJER', 'ROSAS, ARANDANO', 400, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0007091', 'GUESS BELLA VITA 100', 'PERFUME MUJER', 'MANDARINA, CLAVO, TRIGO', 360, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0007093', 'GUESS BELLA VITA 80', 'PERFUME MUJER', 'MANDARINA, CLAVO, TRIGO', 290, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0007565', 'HOLISTER RUSH FOR HER 100', 'PERFUME MUJER', 'ROSAS, MIEL, CANDAMO', 280, 'ml' );

	INSERT INTO Producto( sku, nombre, tipo, etiquetas, precio, medida )
	VALUES (  'SKU0007566', 'HOLISTER RUSH FOR HER 180', 'PERFUME MUJER', 'ROSAS, MIEL, CANDAMO', 380, 'ml' );

	-- ----------------------
	-- 
	-- VENTAS 
	--
	-- ----------------------

	DECLARE @id_pedido INT;

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0003' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006005', 130, 2 );
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0007091', 360, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0004' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0007565', 280, 3 );
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0007566', 380, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0004' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0007565', 280, 2 );
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006015', 150, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0004' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006001', 200, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0001' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006034', 170, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0001' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006006', 100, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0001' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006023', 250, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0001' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006005', 130, 1 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0002' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006001', 200, 2 );

	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( 'XYZ0002' );
	SET @id_pedido = @@IDENTITY;
	INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
	VALUES ( @id_pedido, 'SKU0006450', 280, 3 );

END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_AGREGAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_AGREGAR]( 
	@id_pedido int,
	@sku varchar(10), 
	@precioventa int = 0, 
	@cantidad int = 1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS(
		SELECT * FROM Pedido
		WHERE @id_pedido = id ) BEGIN

		IF( @precioventa <= 0 ) BEGIN
			SELECT @precioventa = precio FROM Producto
			WHERE @sku = sku
		END

		-- Insert statements for procedure here
		INSERT INTO PedidoDetalle( id_pedido, sku, precioventa, cantidad )
		VALUES ( @id_pedido, @sku, @precioventa, @cantidad );

		SELECT @@IDENTITY AS result, 'OK' AS mensaje
		RETURN @@IDENTITY
	END
	ELSE BEGIN
		SELECT 0 AS result, 'No existe el pedido' AS mensaje
		RETURN 0
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_ATENDER]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_ATENDER]( 
	@id_pedido int,
	@codigo_usuario varchar(10) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @estado INT = 0;

	SELECT	@estado = estado FROM Pedido
	WHERE	id = @id_pedido AND
			estado > 0

	IF @estado >= 4 BEGIN
		SELECT 0 AS result, 'El pedido ya fue atendido' AS mensaje
		RETURN 0
	END

	IF @estado = 0 BEGIN
		SELECT 0 AS result, 'El pedido no existe' AS mensaje
		RETURN 0
	END

	IF NOT EXISTS (
		SELECT * FROM Usuario
		WHERE @codigo_usuario = codigo_usuario
	)	BEGIN
		SELECT 2 AS result, 'El usuario no existe' AS mensaje
		RETURN 2
	END

	SET @estado = @estado + 1;

	IF( @estado = 2 ) BEGIN
		UPDATE Pedido 
		SET estado = @estado,
		fecha_recepcion = GETDATE(),
		codigo_usuario_recepcion = @codigo_usuario
		WHERE @id_pedido = id 
	END

	IF( @estado = 3 ) BEGIN
		UPDATE Pedido
		SET estado = @estado,
		fecha_despacho = GETDATE(),
		codigo_usuario_despacho = @codigo_usuario
		WHERE @id_pedido = id 
	END

	IF( @estado = 4 ) BEGIN
		UPDATE Pedido
		SET estado = @estado,
		fecha_entrega = GETDATE(),
		codigo_usuario_entrega = @codigo_usuario
		WHERE @id_pedido = id 
	END

	SELECT @estado AS result, 'OK' AS mensaje
	RETURN @estado

END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_BUSCAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_BUSCAR]
	(
		@texto VARCHAR(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @texto = '%' + UPPER(@texto) + '%'

    -- Insert statements for procedure here
	SELECT P.id as id_pedido, PD.sku, PP.nombre, PD.precioventa, PD.cantidad, PP.etiquetas,
		CASE P.estado 
			WHEN 1 THEN 'CREADO'		
			WHEN 2 THEN 'RECEPCIONADO'
			WHEN 3 THEN 'DESPACHADO'
			WHEN 4 THEN 'ENTREGADO'
		END	
		AS estado,
		CASE P.estado 
			WHEN 1 THEN P.codigo_usuario_creacion 		
			WHEN 2 THEN P.codigo_usuario_recepcion
			WHEN 4 THEN P.codigo_usuario_entrega
			WHEN 3 THEN P.codigo_usuario_despacho
		END	
		AS usuario,
		CASE P.estado 
			WHEN 1 THEN P.fecha_creacion 		
			WHEN 2 THEN P.fecha_recepcion
			WHEN 4 THEN P.fecha_entrega
			WHEN 3 THEN P.fecha_despacho
		END	
		AS fecha
	FROM Pedido P
	INNER JOIN PedidoDetalle PD ON P.id = PD.id_pedido AND P.estado > 0
	INNER JOIN Producto PP ON PD.sku = PP.sku
	LEFT JOIN Usuario U_1 ON P.codigo_usuario_creacion  = U_1.codigo_usuario 
	LEFT JOIN Usuario U_2 ON P.codigo_usuario_recepcion = U_2.codigo_usuario 
	LEFT JOIN Usuario U_4 ON P.codigo_usuario_entrega   = U_4.codigo_usuario 
	LEFT JOIN Usuario U_3 ON P.codigo_usuario_despacho  = U_3.codigo_usuario 
	WHERE 
		PD.sku LIKE @texto OR
		PP.nombre LIKE @texto   OR
		PP.tipo LIKE @texto   OR
		PP.etiquetas LIKE @texto  

END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_CANCELAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_CANCELAR]( @id_pedido int )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Pedido
	SET estado = 0
	WHERE @id_pedido = id

	SELECT @@ROWCOUNT as result
	RETURN @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_CREAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_CREAR]( @codigo_usuario varchar(10) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Pedido( codigo_usuario_creacion ) VALUES( @codigo_usuario );
	SELECT @@IDENTITY as result
	RETURN @@IDENTITY
END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_LISTAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_LISTAR]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.id as id_pedido, PD.sku, PP.nombre, PD.precioventa, PD.cantidad, 
		CASE P.estado 
			WHEN 1 THEN 'CREADO'		
			WHEN 2 THEN 'RECEPCIONADO'
			WHEN 3 THEN 'DESPACHADO' 
			WHEN 4 THEN 'ENTREGADO'
		END	
		AS estado,
		CASE P.estado 
			WHEN 1 THEN P.codigo_usuario_creacion 		
			WHEN 2 THEN P.codigo_usuario_recepcion
			WHEN 3 THEN P.codigo_usuario_despacho
			WHEN 4 THEN P.codigo_usuario_entrega
		END	
		AS usuario,
		CASE P.estado 
			WHEN 1 THEN P.fecha_creacion 		
			WHEN 2 THEN P.fecha_recepcion
			WHEN 3 THEN P.fecha_despacho
			WHEN 4 THEN P.fecha_entrega
		END	
		AS fecha
	FROM Pedido P
	INNER JOIN PedidoDetalle PD ON P.id = PD.id_pedido
	INNER JOIN Producto PP ON PD.sku = PP.sku
	LEFT JOIN Usuario U_1 ON P.codigo_usuario_creacion  = U_1.codigo_usuario 
	LEFT JOIN Usuario U_2 ON P.codigo_usuario_recepcion = U_2.codigo_usuario 
	LEFT JOIN Usuario U_3 ON P.codigo_usuario_despacho  = U_3.codigo_usuario 
	LEFT JOIN Usuario U_4 ON P.codigo_usuario_entrega   = U_4.codigo_usuario
	WHERE P.estado > 0


END
GO
/****** Object:  StoredProcedure [dbo].[USP_PEDIDO_RECUPERAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PEDIDO_RECUPERAR]( @id_pedido int )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Pedido
	SET estado = 1
	WHERE @id_pedido = id

	SELECT @@ROWCOUNT as result
	RETURN @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[USP_PRODUCTO_BUSCAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PRODUCTO_BUSCAR]
	(
		@texto VARCHAR(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @texto = '%' + UPPER(@texto) + '%'

    -- Insert statements for procedure here
	SELECT P.sku, P.nombre, P.medida, P.tipo, P.precio, P.etiquetas		
	FROM Producto P
	WHERE 
		P.nombre LIKE @texto OR
		P.sku LIKE @texto OR
		P.tipo LIKE @texto OR
		P.etiquetas LIKE @texto 
END

GO
/****** Object:  StoredProcedure [dbo].[USP_PRODUCTO_LISTAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PRODUCTO_LISTAR]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.sku, P.nombre, P.medida, P.tipo, P.precio, P.etiquetas		
	FROM Producto P

END
GO
/****** Object:  StoredProcedure [dbo].[USP_USUARIO_BUSCAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_USUARIO_BUSCAR]
(
	@texto VARCHAR(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @texto = '%' + UPPER(@texto) + '%'

    -- Insert statements for procedure here
	SELECT U.codigo_usuario as usuario, R.nombre as rol, U.puesto, U.nombre,  U.telefono, U.correo
	FROM Usuario U
	INNER JOIN Rol R ON U.codigo_rol = R.codigo_rol
	WHERE 
		U.nombre LIKE @texto OR
		U.codigo_usuario LIKE @texto OR
		U.nombre LIKE @texto OR
		U.puesto LIKE @texto OR
		R.nombre LIKE @texto
END
GO
/****** Object:  StoredProcedure [dbo].[USP_USUARIO_LISTAR]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_USUARIO_LISTAR]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT U.codigo_usuario as usuario, R.nombre as rol, U.puesto, U.nombre,  U.telefono, U.correo
	FROM Usuario U
	INNER JOIN Rol R ON U.codigo_rol = R.codigo_rol

END
GO
/****** Object:  StoredProcedure [dbo].[USP_USUARIO_LOGIN]    Script Date: 10/04/2024 05:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_USUARIO_LOGIN]( 
	@codigo VARCHAR(10),
	@pwd VARCHAR(100) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @estado INT = 0;

	SELECT	codigo_usuario as usuario, nombre, correo, telefono, puesto FROM Usuario
	WHERE	codigo_usuario = @codigo AND
			pwd = @pwd AND
			estado > 0

	RETURN	1
END
GO
USE [master]
GO
ALTER DATABASE [BoutiqueXYZ] SET  READ_WRITE 
GO
