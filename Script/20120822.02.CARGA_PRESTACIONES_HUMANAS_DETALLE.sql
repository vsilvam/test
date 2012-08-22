/*
   miércoles, 22 de agosto de 201211:54:24
   Usuario: 
   Servidor: SVASQUEZ\SQLEXPRESS
   Base de datos: LQCE
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE
	(
	ID int NOT NULL IDENTITY (1, 1),
	ID_CARGA_PRESTACIONES_ENCABEZADO int NOT NULL,
	FICHA nvarchar(MAX) NULL,
	NOMBRE nvarchar(MAX) NULL,
	RUT nvarchar(MAX) NULL,
	MEDICO nvarchar(MAX) NULL,
	EDAD nvarchar(MAX) NULL,
	TELEFONO nvarchar(MAX) NULL,
	PROCEDENCIA nvarchar(MAX) NULL,
	FECHA_RECEPCION nvarchar(MAX) NULL,
	MUESTRA nvarchar(MAX) NULL,
	FECHA_RESULTADOS nvarchar(MAX) NULL,
	PREVISION nvarchar(MAX) NULL,
	GARANTIA nvarchar(MAX) NULL,
	PAGADO nvarchar(MAX) NULL,
	PENDIENTE nvarchar(MAX) NULL,
	TOTAL nvarchar(MAX) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE ADD CONSTRAINT
	PK_CARGA_PRESTACIONES_HUMANAS_DETALLE PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'CONTROL') as Contr_Per 