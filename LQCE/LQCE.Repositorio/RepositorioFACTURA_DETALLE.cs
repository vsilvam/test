using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioFACTURA_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioFACTURA_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public FACTURA_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.FACTURA_DETALLE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public FACTURA_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.FACTURA_DETALLE.Include("FACTURA").Include("PRESTACION").Include("PAGO_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.FACTURA_DETALLE where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.FACTURA_DETALLE.Include("FACTURA").Include("PRESTACION").Include("PAGO_DETALLE") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURA_DETALLE> GetByFilter(int? FACTURAId = null, int? PRESTACIONId = null, int? MONTO_TOTAL = null, int? MONTO_COBRADO = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.FACTURA_DETALLE  where i.ACTIVO  select i;
			
				

				if (MONTO_TOTAL.HasValue)
				{
				  q = q.Where(i => i.MONTO_TOTAL == MONTO_TOTAL.Value);
				}
				if (MONTO_COBRADO.HasValue)
				{
				  q = q.Where(i => i.MONTO_COBRADO == MONTO_COBRADO.Value);
				}
				if (FACTURAId.HasValue)
				{
				  q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
				}
				if (PRESTACIONId.HasValue)
				{
				  q = q.Where(i => i.PRESTACION.ID == PRESTACIONId.Value);
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

		public IQueryable<FACTURA_DETALLE> GetByFilterWithReferences(int? FACTURAId = null, int? PRESTACIONId = null, int? MONTO_TOTAL = null, int? MONTO_COBRADO = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.FACTURA_DETALLE.Include("FACTURA").Include("PRESTACION").Include("PAGO_DETALLE")  where i.ACTIVO select i;
			
				

				if (MONTO_TOTAL.HasValue)
				{
					q = q.Where(i => i.MONTO_TOTAL == MONTO_TOTAL.Value);
				}
				if (MONTO_COBRADO.HasValue)
				{
					q = q.Where(i => i.MONTO_COBRADO == MONTO_COBRADO.Value);
				}
				if (FACTURAId.HasValue)
				{
					q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
				}
				if (PRESTACIONId.HasValue)
				{
					q = q.Where(i => i.PRESTACION.ID == PRESTACIONId.Value);
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
