/*
   martes, 16 de octubre de 201213:32:11
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
CREATE TABLE dbo.NOTA_CREDITO
	(
	ID int NOT NULL IDENTITY (1, 1),
	ID_FACTURA int NOT NULL,
	FECHA_EMISION datetime NOT NULL,
	NUMERO_NOTA_CREDITO int NOT NULL,
	CORRECCION_TOTAL_PARCIAL bit NOT NULL,
	ACTIVO bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.NOTA_CREDITO ADD CONSTRAINT
	PK_NOTA_CREDITO PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.NOTA_CREDITO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.NOTA_CREDITO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.NOTA_CREDITO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.NOTA_CREDITO', 'Object', 'CONTROL') as Contr_Per 