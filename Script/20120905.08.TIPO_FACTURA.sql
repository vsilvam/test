/*
   miércoles, 05 de septiembre de 201223:43:37
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
CREATE TABLE dbo.TIPO_FACTURA
	(
	ID int NOT NULL IDENTITY (1, 1),
	RUT_FACTURA nvarchar(50) NOT NULL,
	NOMBRE_FACTURA nvarchar(MAX) NOT NULL,
	AFECTO_IVA bit NOT NULL,
	ACTIVO bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.TIPO_FACTURA ADD CONSTRAINT
	PK_TIPO_FACTURA PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TIPO_FACTURA SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'CONTROL') as Contr_Per 