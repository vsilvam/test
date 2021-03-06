/*
   sábado, 10 de noviembre de 201222:48:00
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
EXECUTE sp_rename N'dbo.PRESTACION_VETERINARIA.SOLICITANTE', N'Tmp_PROCEDENCIA', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.PRESTACION_VETERINARIA.Tmp_PROCEDENCIA', N'PROCEDENCIA', 'COLUMN' 
GO
ALTER TABLE dbo.PRESTACION_VETERINARIA SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PRESTACION_VETERINARIA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PRESTACION_VETERINARIA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PRESTACION_VETERINARIA', 'Object', 'CONTROL') as Contr_Per 