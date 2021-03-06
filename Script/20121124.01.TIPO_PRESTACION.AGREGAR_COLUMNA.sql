/*
   sábado, 24 de noviembre de 201223:02:01
   User: 
   Server: VP\SQLEXPRESS2008R2
   Database: LQCE
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
ALTER TABLE dbo.TIPO_PRESTACION ADD
	NOMBRE_REPORTE_DETALLE_FACTURA nvarchar(MAX) NULL
GO
ALTER TABLE dbo.TIPO_PRESTACION SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'CONTROL') as Contr_Per 