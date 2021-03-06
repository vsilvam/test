/*
   domingo, 28 de octubre de 201222:36:07
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
ALTER TABLE dbo.PRESTACION_HUMANA
	DROP CONSTRAINT FK_PRESTACION_HUMANA_PRESTACION
GO
ALTER TABLE dbo.PRESTACION SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_PRESTACION_HUMANA
	(
	ID int NOT NULL,
	NOMBRE nvarchar(MAX) NOT NULL,
	RUT nvarchar(50) NULL,
	EDAD nvarchar(50) NULL,
	TELEFONO nvarchar(50) NULL,
	ACTIVO bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PRESTACION_HUMANA SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.PRESTACION_HUMANA)
	 EXEC('INSERT INTO dbo.Tmp_PRESTACION_HUMANA (ID, NOMBRE, RUT, EDAD, TELEFONO, ACTIVO)
		SELECT ID, NOMBRE, RUT, EDAD, TELEFONO, ACTIVO FROM dbo.PRESTACION_HUMANA WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.PRESTACION_HUMANA
GO
EXECUTE sp_rename N'dbo.Tmp_PRESTACION_HUMANA', N'PRESTACION_HUMANA', 'OBJECT' 
GO
ALTER TABLE dbo.PRESTACION_HUMANA ADD CONSTRAINT
	PK_PRESTACION_HUMANA PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PRESTACION_HUMANA ADD CONSTRAINT
	FK_PRESTACION_HUMANA_PRESTACION FOREIGN KEY
	(
	ID
	) REFERENCES dbo.PRESTACION
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PRESTACION_HUMANA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PRESTACION_HUMANA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PRESTACION_HUMANA', 'Object', 'CONTROL') as Contr_Per 