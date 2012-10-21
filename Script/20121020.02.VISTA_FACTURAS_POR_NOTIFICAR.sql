USE [LQCE]
GO

/****** Object:  View [dbo].[VISTA_FACTURAS_POR_NOTIFICAR]    Script Date: 10/20/2012 22:38:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VISTA_FACTURAS_POR_NOTIFICAR]
AS
SELECT     dbo.FACTURA.ID, dbo.FACTURA.ID_CLIENTE, dbo.CLIENTE.RUT, dbo.CLIENTE.NOMBRE, dbo.FACTURACION.FECHA_FACTURACION, 
                      dbo.FACTURA.NUMERO_FACTURA,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.NOTA_COBRO_DETALLE
                            WHERE      (ID_FACTURA = dbo.FACTURA.ID)
                            AND NOTA_COBRO_DETALLE.ACTIVO = 1
                            ) AS CONTADOR_NOTAS_COBRO
FROM         dbo.FACTURA INNER JOIN
                      dbo.FACTURACION ON dbo.FACTURA.ID_FACTURACION = dbo.FACTURACION.ID INNER JOIN
                      dbo.CLIENTE ON dbo.FACTURA.ID_CLIENTE = dbo.CLIENTE.ID
WHERE     (dbo.FACTURA.ACTIVO = 1) AND (dbo.FACTURACION.ACTIVO = 1) AND (dbo.CLIENTE.ACTIVO = 1)
AND NUMERO_FACTURA IS NOT NULL
GO


