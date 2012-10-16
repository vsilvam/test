using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioVISTA_REPORTE_FACTURA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioVISTA_REPORTE_FACTURA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public VISTA_REPORTE_FACTURA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.VISTA_REPORTE_FACTURA.FirstOrDefault(i => i.ID_FACTURA == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public VISTA_REPORTE_FACTURA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.VISTA_REPORTE_FACTURA.FirstOrDefault(i => i.ID_FACTURA == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_REPORTE_FACTURA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.VISTA_REPORTE_FACTURA select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_REPORTE_FACTURA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.VISTA_REPORTE_FACTURA select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_REPORTE_FACTURA> GetByFilter(int? ID_FACTURACION = null, System.DateTime? FECHA_FACTURACION = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, bool? PAGADA = null, int? ID_TIPO_FACTURA = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", int? VALOR_PAGADO = null, int? PAGOS_REGISTRADOS = null, int? SALDO_DEUDOR = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.VISTA_REPORTE_FACTURA  select i;
			
				

				if (ID_FACTURACION.HasValue)
				{
				  q = q.Where(i => i.ID_FACTURACION == ID_FACTURACION.Value);
				}
				if (FECHA_FACTURACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (!string.IsNullOrEmpty(NOMBRE_CLIENTE))
				{
				   q = q.Where(i => i.NOMBRE_CLIENTE.Contains(NOMBRE_CLIENTE));
				}
				if (!string.IsNullOrEmpty(RUT_CLIENTE))
				{
				   q = q.Where(i => i.RUT_CLIENTE.Contains(RUT_CLIENTE));
				}
				if (!string.IsNullOrEmpty(DIRECCION))
				{
				   q = q.Where(i => i.DIRECCION.Contains(DIRECCION));
				}
				if (!string.IsNullOrEmpty(NOMBRE_COMUNA))
				{
				   q = q.Where(i => i.NOMBRE_COMUNA.Contains(NOMBRE_COMUNA));
				}
				if (NUMERO_FACTURA.HasValue)
				{
				  q = q.Where(i => i.NUMERO_FACTURA == NUMERO_FACTURA.Value);
				}
				if (!string.IsNullOrEmpty(RUT_LABORATORIO))
				{
				   q = q.Where(i => i.RUT_LABORATORIO.Contains(RUT_LABORATORIO));
				}
				if (DESCUENTO.HasValue)
				{
				  q = q.Where(i => i.DESCUENTO == DESCUENTO.Value);
				}
				if (!string.IsNullOrEmpty(FONO))
				{
				   q = q.Where(i => i.FONO.Contains(FONO));
				}
				if (!string.IsNullOrEmpty(GIRO))
				{
				   q = q.Where(i => i.GIRO.Contains(GIRO));
				}
				if (!string.IsNullOrEmpty(DETALLE))
				{
				   q = q.Where(i => i.DETALLE.Contains(DETALLE));
				}
				if (NETO.HasValue)
				{
				  q = q.Where(i => i.NETO == NETO.Value);
				}
				if (IVA.HasValue)
				{
				  q = q.Where(i => i.IVA == IVA.Value);
				}
				if (TOTAL.HasValue)
				{
				  q = q.Where(i => i.TOTAL == TOTAL.Value);
				}
				if (VALOR_PAGADO.HasValue)
				{
				  q = q.Where(i => i.VALOR_PAGADO == VALOR_PAGADO.Value);
				}
				if (PAGOS_REGISTRADOS.HasValue)
				{
				  q = q.Where(i => i.PAGOS_REGISTRADOS == PAGOS_REGISTRADOS.Value);
				}
				if (SALDO_DEUDOR.HasValue)
				{
				  q = q.Where(i => i.SALDO_DEUDOR == SALDO_DEUDOR.Value);
				}
				if (PAGADA.HasValue)
				{
				  q = q.Where(i => i.PAGADA == PAGADA.Value);
				}
				if (ID_TIPO_FACTURA.HasValue)
				{
				  q = q.Where(i => i.ID_TIPO_FACTURA == ID_TIPO_FACTURA.Value);
				}
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_REPORTE_FACTURA> GetByFilterWithReferences(int? ID_FACTURACION = null, System.DateTime? FECHA_FACTURACION = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, bool? PAGADA = null, int? ID_TIPO_FACTURA = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", int? VALOR_PAGADO = null, int? PAGOS_REGISTRADOS = null, int? SALDO_DEUDOR = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.VISTA_REPORTE_FACTURA select i;
			
				

				if (ID_FACTURACION.HasValue)
				{
					q = q.Where(i => i.ID_FACTURACION == ID_FACTURACION.Value);
				}
				if (FECHA_FACTURACION.HasValue)
				{
					q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (!string.IsNullOrEmpty(NOMBRE_CLIENTE))
				{
					q = q.Where(i => i.NOMBRE_CLIENTE.Contains(NOMBRE_CLIENTE));
				}
				if (!string.IsNullOrEmpty(RUT_CLIENTE))
				{
					q = q.Where(i => i.RUT_CLIENTE.Contains(RUT_CLIENTE));
				}
				if (!string.IsNullOrEmpty(DIRECCION))
				{
					q = q.Where(i => i.DIRECCION.Contains(DIRECCION));
				}
				if (!string.IsNullOrEmpty(NOMBRE_COMUNA))
				{
					q = q.Where(i => i.NOMBRE_COMUNA.Contains(NOMBRE_COMUNA));
				}
				if (NUMERO_FACTURA.HasValue)
				{
					q = q.Where(i => i.NUMERO_FACTURA == NUMERO_FACTURA.Value);
				}
				if (!string.IsNullOrEmpty(RUT_LABORATORIO))
				{
					q = q.Where(i => i.RUT_LABORATORIO.Contains(RUT_LABORATORIO));
				}
				if (DESCUENTO.HasValue)
				{
					q = q.Where(i => i.DESCUENTO == DESCUENTO.Value);
				}
				if (!string.IsNullOrEmpty(FONO))
				{
					q = q.Where(i => i.FONO.Contains(FONO));
				}
				if (!string.IsNullOrEmpty(GIRO))
				{
					q = q.Where(i => i.GIRO.Contains(GIRO));
				}
				if (!string.IsNullOrEmpty(DETALLE))
				{
					q = q.Where(i => i.DETALLE.Contains(DETALLE));
				}
				if (NETO.HasValue)
				{
					q = q.Where(i => i.NETO == NETO.Value);
				}
				if (IVA.HasValue)
				{
					q = q.Where(i => i.IVA == IVA.Value);
				}
				if (TOTAL.HasValue)
				{
					q = q.Where(i => i.TOTAL == TOTAL.Value);
				}
				if (VALOR_PAGADO.HasValue)
				{
					q = q.Where(i => i.VALOR_PAGADO == VALOR_PAGADO.Value);
				}
				if (PAGOS_REGISTRADOS.HasValue)
				{
					q = q.Where(i => i.PAGOS_REGISTRADOS == PAGOS_REGISTRADOS.Value);
				}
				if (SALDO_DEUDOR.HasValue)
				{
					q = q.Where(i => i.SALDO_DEUDOR == SALDO_DEUDOR.Value);
				}
				if (PAGADA.HasValue)
				{
					q = q.Where(i => i.PAGADA == PAGADA.Value);
				}
				if (ID_TIPO_FACTURA.HasValue)
				{
					q = q.Where(i => i.ID_TIPO_FACTURA == ID_TIPO_FACTURA.Value);
				}
				return q;
			}
			catch (Exception ex)
			{
				ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
		}
	}
}
