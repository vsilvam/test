UPDATE [LQCE].[dbo].[TIPO_PRESTACION]
   SET [NOMBRE_REPORTE_DETALLE_FACTURA] = 'DetalleFacturaHumana.rdlc'
 WHERE ID = 1
GO

UPDATE [LQCE].[dbo].[TIPO_PRESTACION]
   SET [NOMBRE_REPORTE_DETALLE_FACTURA] = 'DetalleFacturaVeterinaria.rdlc'
 WHERE ID = 2
GO

