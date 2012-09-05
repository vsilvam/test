USE [LQCE]
GO


CREATE VIEW [dbo].[VISTA_PRESTACIONES_POR_FACTURAR]
AS
SELECT     dbo.PRESTACION.ID, dbo.PRESTACION.ID_CLIENTE, dbo.CLIENTE.RUT, dbo.CLIENTE.NOMBRE, ISNULL(dbo.CLIENTE.DESCUENTO, 0) AS DESCUENTO, 
                      dbo.PRESTACION.FECHA_RECEPCION, ISNULL(PE.TOTAL, 0) AS TOTAL
FROM         dbo.PRESTACION INNER JOIN
                          (SELECT     NUMERO_FICHA, SUM(VALOR) AS TOTAL
                            FROM          dbo.PRESTACION_EXAMEN
                            WHERE      (ACTIVO = 1)
                            GROUP BY NUMERO_FICHA) AS PE ON dbo.PRESTACION.ID = PE.NUMERO_FICHA INNER JOIN
                      dbo.CLIENTE ON dbo.PRESTACION.ID_CLIENTE = dbo.CLIENTE.ID
WHERE     (dbo.PRESTACION.ACTIVO = 1) AND (dbo.CLIENTE.ACTIVO = 1) AND (NOT EXISTS
                          (SELECT     1 AS Expr1
                            FROM          dbo.FACTURA_DETALLE
                            WHERE      (ACTIVO = 1) AND (NUMERO_FICHA = dbo.PRESTACION.ID)))

GO
