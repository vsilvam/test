INSERT INTO [LQCE].[dbo].[TIPO_FACTURA]
           ([RUT_FACTURA]
           ,[NOMBRE_FACTURA]
           ,[AFECTO_IVA]
           ,[NOMBRE_REPORTE_FACTURA]
           ,[ACTIVO])
     VALUES
           ('1-9', 'LQCE con IVA', 1, 'FacturaMatrizPunto_LQCE_IVA.rdlc', 1)
GO

INSERT INTO [LQCE].[dbo].[TIPO_FACTURA]
           ([RUT_FACTURA]
           ,[NOMBRE_FACTURA]
           ,[AFECTO_IVA]
           ,[NOMBRE_REPORTE_FACTURA]
           ,[ACTIVO])
     VALUES
           ('1-9', 'LQCE sin IVA', 0, 'FacturaMatrizPunto_LQCE_Exento.rdlc', 1)
GO

INSERT INTO [LQCE].[dbo].[TIPO_FACTURA]
           ([RUT_FACTURA]
           ,[NOMBRE_FACTURA]
           ,[AFECTO_IVA]
           ,[NOMBRE_REPORTE_FACTURA]
           ,[ACTIVO])
     VALUES
           ('2-7', 'Monari con IVA', 1, 'FacturaMatrizPunto_Monari_IVA.rdlc', 1)
GO

INSERT INTO [LQCE].[dbo].[TIPO_FACTURA]
           ([RUT_FACTURA]
           ,[NOMBRE_FACTURA]
           ,[AFECTO_IVA]
           ,[NOMBRE_REPORTE_FACTURA]
           ,[ACTIVO])
     VALUES
           ('2-7', 'Monari sin IVA', 0, 'FacturaMatrizPunto_Monari_Exento.rdlc', 1)
GO