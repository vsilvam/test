USE [LQCE]
GO

/****** Object:  View [dbo].[VISTA_PRESTACIONES_POR_FACTURAR]    Script Date: 11/20/2012 13:24:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VISTA_PRESTACIONES_POR_FACTURAR]
AS
SELECT     dbo.PRESTACION.ID, dbo.PRESTACION.ID_CLIENTE, dbo.CLIENTE.RUT, dbo.CLIENTE.NOMBRE, ISNULL(dbo.CLIENTE.DESCUENTO, 0) AS DESCUENTO, 
                      dbo.PRESTACION.FECHA_RECEPCION, ISNULL(PE.TOTAL, 0) AS TOTAL
FROM         dbo.PRESTACION INNER JOIN
                          (SELECT     NUMERO_FICHA, SUM(VALOR) AS TOTAL
                            FROM          dbo.PRESTACION_EXAMEN
                            WHERE      (ACTIVO = 1)
                            GROUP BY NUMERO_FICHA) AS PE ON dbo.PRESTACION.ID = PE.NUMERO_FICHA INNER JOIN
                      dbo.CLIENTE ON dbo.PRESTACION.ID_CLIENTE = dbo.CLIENTE.ID INNER JOIN
                      dbo.TIPO_FACTURA ON dbo.CLIENTE.ID_TIPO_FACTURA = dbo.TIPO_FACTURA.ID
WHERE     (dbo.PRESTACION.ACTIVO = 1) AND (dbo.CLIENTE.ACTIVO = 1) AND (NOT EXISTS
                          (SELECT     1 AS Expr1
                            FROM          dbo.FACTURA_DETALLE INNER JOIN
                                                   dbo.FACTURA ON dbo.FACTURA_DETALLE.ID_FACTURA = dbo.FACTURA.ID INNER JOIN
                                                   dbo.FACTURACION ON dbo.FACTURA.ID_FACTURACION = dbo.FACTURACION.ID
                            WHERE      (dbo.FACTURA.ACTIVO = 1) AND (dbo.FACTURACION.ACTIVO = 1) AND (dbo.FACTURA_DETALLE.ACTIVO = 1) AND 
                                                   (dbo.FACTURA_DETALLE.NUMERO_FICHA = dbo.PRESTACION.ID))) AND (isnull(dbo.TIPO_FACTURA.NOMBRE_FACTURA,'') != N'')

GO


