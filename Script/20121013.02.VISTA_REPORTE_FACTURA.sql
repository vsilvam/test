USE [LQCE]
GO

/****** Object:  View [dbo].[VISTA_REPORTE_FACTURA]    Script Date: 10/13/2012 12:53:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[VISTA_REPORTE_FACTURA]
AS
SELECT     dbo.FACTURA.ID_FACTURACION, dbo.FACTURA.ID AS ID, dbo.FACTURACION.FECHA_FACTURACION, dbo.FACTURA.NOMBRE_CLIENTE, 
                      dbo.FACTURA.RUT_CLIENTE, dbo.FACTURA.DIRECCION, dbo.FACTURA.NOMBRE_COMUNA, dbo.FACTURA.NUMERO_FACTURA, dbo.FACTURA.RUT_LABORATORIO, 
                      dbo.FACTURA.DESCUENTO, dbo.FACTURA.FONO, dbo.FACTURA.GIRO, dbo.FACTURA.DETALLE, dbo.FACTURA.NETO, dbo.FACTURA.IVA, dbo.FACTURA.TOTAL,
(SELECT SUM(MONTO_COBRADO) FROM FACTURA_DETALLE
WHERE FACTURA_DETALLE.ACTIVO = 1 AND FACTURA_DETALLE.ID_FACTURA = FACTURA.ID) AS VALOR_PAGADO,
(SELECT COUNT(*) FROM
PAGO 
WHERE PAGO.ACTIVO = 1
AND EXISTS(SELECT 1 FROM PAGO_DETALLE
INNER JOIN FACTURA_DETALLE ON PAGO_DETALLE.ID_FACTURA_DETALLE = FACTURA_DETALLE.ID
WHERE PAGO_DETALLE.ACTIVO = 1
AND FACTURA_DETALLE.ACTIVO = 1
AND PAGO_DETALLE.ID_PAGO = PAGO.ID
AND FACTURA_DETALLE.ID_FACTURA = FACTURA.ID)) AS PAGOS_REGISTRADOS,
FACTURA.TOTAL - (SELECT SUM(MONTO_COBRADO) FROM FACTURA_DETALLE
WHERE FACTURA_DETALLE.ACTIVO = 1 AND FACTURA_DETALLE.ID_FACTURA = FACTURA.ID) AS SALDO_DEUDOR ,
ISNULL(FACTURA.PAGADA,0) AS PAGADA
FROM         dbo.FACTURACION INNER JOIN
                      dbo.FACTURA ON dbo.FACTURACION.ID = dbo.FACTURA.ID_FACTURACION
WHERE     (dbo.FACTURACION.ACTIVO = 1) AND (dbo.FACTURA.ACTIVO = 1)


GO

