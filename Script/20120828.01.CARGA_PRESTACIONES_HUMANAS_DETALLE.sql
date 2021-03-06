/*
   martes, 28 de agosto de 201211:29:07
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
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE ADD
	VALOR_ID_CLIENTE int NULL,
	VALOR_FECHA_MUESTRA datetime NULL,
	VALOR_FECHA_RECEPCION datetime NULL,
	VALOR_ID_PREVISION int NULL,
	VALOR_ID_GARANTIA int NULL,
	VALOR_FECHA_ENTREGA_RESULTADOS datetime NULL
GO
ALTER TABLE dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CARGA_PRESTACIONES_HUMANAS_DETALLE', 'Object', 'CONTROL') as Contr_Per 