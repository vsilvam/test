using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioFACTURA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioFACTURA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public FACTURA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.FACTURA.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public FACTURA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("TIPO_FACTURA").Include("NOTA_COBRO_DETALLE").Include("NOTA_CREDITO").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.FACTURA where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("TIPO_FACTURA").Include("NOTA_COBRO_DETALLE").Include("NOTA_CREDITO") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA> GetByFilter(int? CLIENTEId = null, int? FACTURACIONId = null, int? TIPO_FACTURAId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", bool? PAGADA = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.FACTURA  where i.ACTIVO  select i;
			
				

				if (CORRELATIVO.HasValue)
				{
				  q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
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
				if (PAGADA.HasValue)
				{
				  q = q.Where(i => i.PAGADA == PAGADA.Value);
				}
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (FACTURACIONId.HasValue)
				{
				  q = q.Where(i => i.FACTURACION.ID == FACTURACIONId.Value);
				}
				if (TIPO_FACTURAId.HasValue)
				{
				  q = q.Where(i => i.TIPO_FACTURA.ID == TIPO_FACTURAId.Value);
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

		public IQueryable<FACTURA> GetByFilterWithReferences(int? CLIENTEId = null, int? FACTURACIONId = null, int? TIPO_FACTURAId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", bool? PAGADA = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("TIPO_FACTURA").Include("NOTA_COBRO_DETALLE").Include("NOTA_CREDITO")  where i.ACTIVO select i;
			
				

				if (CORRELATIVO.HasValue)
				{
					q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
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
				if (PAGADA.HasValue)
				{
					q = q.Where(i => i.PAGADA == PAGADA.Value);
				}
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (FACTURACIONId.HasValue)
				{
					q = q.Where(i => i.FACTURACION.ID == FACTURACIONId.Value);
				}
				if (TIPO_FACTURAId.HasValue)
				{
					q = q.Where(i => i.TIPO_FACTURA.ID == TIPO_FACTURAId.Value);
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
