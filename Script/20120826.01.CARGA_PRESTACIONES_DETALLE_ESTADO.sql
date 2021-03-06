/*
   domingo, 26 de agosto de 201213:01:06
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
CREATE TABLE dbo.CARGA_PRESTACIONES_DETALLE_ESTADO
	(
	ID int NOT NULL IDENTITY (1, 1),
	NOMBRE nvarchar(50) NOT NULL,
	ACTIVO bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_DETALLE_ESTADO ADD CONSTRAINT
	PK_CARGA_PRESTACIONES_DETALLE_ESTADO PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CARGA_PRESTACIONES_DETALLE_ESTADO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_DETALLE_ESTADO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_DETALLE_ESTADO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_DETALLE_ESTADO', 'Object', 'CONTROL') as Contr_Per 