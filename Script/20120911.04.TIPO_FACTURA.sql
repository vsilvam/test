/*
   martes, 11 de septiembre de 201210:48:15
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
ALTER TABLE dbo.TIPO_FACTURA ADD
	NOMBRE_REPORTE_FACTURA_INDIVIDUAL nvarchar(MAX) NULL
GO
ALTER TABLE dbo.TIPO_FACTURA SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TIPO_FACTURA', 'Object', 'CONTROL') as Contr_Per 