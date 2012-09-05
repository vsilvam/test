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
							return _context.FACTURA.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public FACTURA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("NOTA_COBRO_DETALLE").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<FACTURA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.FACTURA select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<FACTURA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("NOTA_COBRO_DETALLE") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<FACTURA> GetByFilter(int? CLIENTEId = null, int? FACTURACIONId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.FACTURA  select i;
			
				

				if (CORRELATIVO.HasValue)
				{
				  q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
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
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (FACTURACIONId.HasValue)
				{
				  q = q.Where(i => i.FACTURACION.ID == FACTURACIONId.Value);
				}
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<FACTURA> GetByFilterWithReferences(int? CLIENTEId = null, int? FACTURACIONId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.FACTURA.Include("CLIENTE").Include("FACTURA_DETALLE").Include("FACTURACION").Include("NOTA_COBRO_DETALLE") select i;
			
				

				if (CORRELATIVO.HasValue)
				{
					q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
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
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (FACTURACIONId.HasValue)
				{
					q = q.Where(i => i.FACTURACION.ID == FACTURACIONId.Value);
				}
				return q;
			}
			catch (Exception ex)
			{
				ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
			}
		}
	}
}
