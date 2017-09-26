CREATE TABLE [dbo].[SN_Fichadas](
	[Id] [int] NOT NULL PRIMARY KEY,
	[Controlador] [int] NOT NULL,
	[Codigo] [nvarchar](8) NOT NULL,
	[Accion] [int] NULL,
	[Consola] [int] NULL,
	[Tarjeta] [int] NULL,
	[Capturada] [datetime] NULL,
	[Fecha] [datetime] NULL,
	[Transferida] [int] NULL,
	[TReal] [int] NULL,
	[Secuencia] [int] NULL,
	[Observaciones] [nvarchar](32) NULL,
	[IdPerso] [int] NULL,
	[legajo] [int] NOT NULL,
	[Planta] [int] NOT NULL,
	[EstadoSincro] [int] NOT NULL
) ON [PRIMARY]

GO
