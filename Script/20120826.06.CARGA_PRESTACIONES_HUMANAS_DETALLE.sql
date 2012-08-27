/*
   domingo, 26 de agosto de 201213:30:54
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
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE
	DROP CONSTRAINT FK_CARGA_PRESTACIONES_HUMANAS_DETALLE_CARGA_PRESTACIONES_ENCABEZADO
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_ENCABEZADO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_ENCABEZADO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_ENCABEZADO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_ENCABEZADO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE
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
	TOTAL nvarchar(MAX) NULL,
	ACTIVO bit NOT NULL,
	ID_CARGA_PRESTACIONES_DETALLE_ESTADO int NOT NULL,
	MENSAJE_ERROR nvarchar(MAX) NULL,
	FECHA_ACTUALIZACION datetime NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE ON
GO
IF EXISTS(SELECT * FROM dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE)
	 EXEC('INSERT INTO dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE (ID, ID_CARGA_PRESTACIONES_ENCABEZADO, FICHA, NOMBRE, RUT, MEDICO, EDAD, TELEFONO, PROCEDENCIA, FECHA_RECEPCION, MUESTRA, FECHA_RESULTADOS, PREVISION, GARANTIA, PAGADO, PENDIENTE, TOTAL, ACTIVO, ID_CARGA_PRESTACIONES_DETALLE_ESTADO, MENSAJE_ERROR, FECHA_ACTUALIZACION)
		SELECT ID, ID_CARGA_PRESTACIONES_ENCABEZADO, FICHA, NOMBRE, RUT, MEDICO, EDAD, TELEFONO, PROCEDENCIA, FECHA_RECEPCION, MUESTRA, FECHA_RESULTADOS, PREVISION, GARANTIA, PAGADO, PENDIENTE, TOTAL, ACTIVO, CONVERT(int, ID_CARGA_PRESTACIONES_DETALLE_ESTADO), MENSAJE_ERROR, FECHA_ACTUALIZACION FROM dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE OFF
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN
	DROP CONSTRAINT FK_CARGA_PRESTACIONES_HUMANAS_EXAMEN_CARGA_PRESTACIONES_HUMANAS_DETALLE
GO
DROP TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE
GO
EXECUTE sp_rename N'dbo.Tmp_CARGA_PRESTACIONES_HUMANAS_DETALLE', N'CARGA_PRESTACIONES_HUMANAS_DETALLE', 'OBJECT' 
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE ADD CONSTRAINT
	PK_CARGA_PRESTACIONES_HUMANAS_DETALLE PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE ADD CONSTRAINT
	FK_CARGA_PRESTACIONES_HUMANAS_DETALLE_CARGA_PRESTACIONES_ENCABEZADO FOREIGN KEY
	(
	ID_CARGA_PRESTACIONES_ENCABEZADO
	) REFERENCES dbo.CARGA_PRESTACIONES_ENCABEZADO
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN ADD CONSTRAINT
	FK_CARGA_PRESTACIONES_HUMANAS_EXAMEN_CARGA_PRESTACIONES_HUMANAS_DETALLE FOREIGN KEY
	(
	ID_CARGA_PRESTACIONES_HUMANAS_DETALLE
	) REFERENCES dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_EXAMEN', 'Object', 'CONTROL') as Contr_Per 