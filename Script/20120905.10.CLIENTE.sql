/*
   miércoles, 05 de septiembre de 201223:45:22
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
ALTER TABLE dbo.CLIENTE
	DROP CONSTRAINT FK_CLIENTE_TIPO_PRESTACION
GO
ALTER TABLE dbo.TIPO_PRESTACION SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TIPO_PRESTACION', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CLIENTE
	DROP CONSTRAINT FK_CLIENTE_COMUNA
GO
ALTER TABLE dbo.COMUNA SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.COMUNA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.COMUNA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.COMUNA', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CLIENTE
	DROP CONSTRAINT FK_CLIENTE_CONVENIO
GO
ALTER TABLE dbo.CONVENIO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CONVENIO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CONVENIO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CONVENIO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_CLIENTE
	(
	ID int NOT NULL IDENTITY (1, 1),
	RUT nvarchar(50) NOT NULL,
	NOMBRE nvarchar(50) NOT NULL,
	ID_TIPO_PRESTACION int NOT NULL,
	ID_CONVENIO int NOT NULL,
	ID_COMUNA int NOT NULL,
	ID_TIPO_FACTURA int NOT NULL,
	ACTIVO bit NOT NULL,
	DESCUENTO int NULL,
	DIRECCION nvarchar(MAX) NULL,
	FONO nvarchar(MAX) NULL,
	GIRO nvarchar(MAX) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CLIENTE SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_CLIENTE ON
GO
IF EXISTS(SELECT * FROM dbo.CLIENTE)
	 EXEC('INSERT INTO dbo.Tmp_CLIENTE (ID, RUT, NOMBRE, ID_TIPO_PRESTACION, ID_CONVENIO, ID_COMUNA, ACTIVO, DESCUENTO, DIRECCION, FONO, GIRO)
		SELECT ID, RUT, NOMBRE, ID_TIPO_PRESTACION, ID_CONVENIO, ID_COMUNA, ACTIVO, DESCUENTO, DIRECCION, FONO, GIRO FROM dbo.CLIENTE WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_CLIENTE OFF
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE
	DROP CONSTRAINT FK_CARGA_PRESTACIONES_HUMANAS_DETALLE_CLIENTE
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE
	DROP CONSTRAINT FK_CARGA_PRESTACIONES_VETERINARIAS_DETALLE_CLIENTE
GO
ALTER TABLE dbo.PAGO
	DROP CONSTRAINT FK_PAGO_CLIENTE
GO
ALTER TABLE dbo.PRESTACION
	DROP CONSTRAINT FK_PRESTACION_CLIENTE
GO
ALTER TABLE dbo.FACTURA
	DROP CONSTRAINT FK_FACTURA_CLIENTE
GO
ALTER TABLE dbo.CLIENTE_SINONIMO
	DROP CONSTRAINT FK_CLIENTE_SINONIMO_CLIENTE
GO
DROP TABLE dbo.CLIENTE
GO
EXECUTE sp_rename N'dbo.Tmp_CLIENTE', N'CLIENTE', 'OBJECT' 
GO
ALTER TABLE dbo.CLIENTE ADD CONSTRAINT
	PK_CLIENTE PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CLIENTE ADD CONSTRAINT
	FK_CLIENTE_CONVENIO FOREIGN KEY
	(
	ID_CONVENIO
	) REFERENCES dbo.CONVENIO
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CLIENTE ADD CONSTRAINT
	FK_CLIENTE_COMUNA FOREIGN KEY
	(
	ID_COMUNA
	) REFERENCES dbo.COMUNA
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CLIENTE ADD CONSTRAINT
	FK_CLIENTE_TIPO_PRESTACION FOREIGN KEY
	(
	ID_TIPO_PRESTACION
	) REFERENCES dbo.TIPO_PRESTACION
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CLIENTE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CLIENTE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CLIENTE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CLIENTE_SINONIMO ADD CONSTRAINT
	FK_CLIENTE_SINONIMO_CLIENTE FOREIGN KEY
	(
	ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CLIENTE_SINONIMO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CLIENTE_SINONIMO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CLIENTE_SINONIMO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CLIENTE_SINONIMO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.FACTURA ADD CONSTRAINT
	FK_FACTURA_CLIENTE FOREIGN KEY
	(
	ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.FACTURA SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.FACTURA', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.FACTURA', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.FACTURA', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PRESTACION ADD CONSTRAINT
	FK_PRESTACION_CLIENTE FOREIGN KEY
	(
	ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PRESTACION SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PRESTACION', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PAGO ADD CONSTRAINT
	FK_PAGO_CLIENTE FOREIGN KEY
	(
	ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PAGO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PAGO', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PAGO', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PAGO', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE ADD CONSTRAINT
	FK_CARGA_PRESTACIONES_VETERINARIAS_DETALLE_CLIENTE FOREIGN KEY
	(
	VALOR_ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_VETERINARIAS_DETALLE', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE ADD CONSTRAINT
	FK_CARGA_PRESTACIONES_HUMANAS_DETALLE_CLIENTE FOREIGN KEY
	(
	VALOR_ID_CLIENTE
	) REFERENCES dbo.CLIENTE
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'CONTROL') as Contr_Per 