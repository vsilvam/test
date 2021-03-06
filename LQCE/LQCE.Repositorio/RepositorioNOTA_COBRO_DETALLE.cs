using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioNOTA_COBRO_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioNOTA_COBRO_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public NOTA_COBRO_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.NOTA_COBRO_DETALLE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public NOTA_COBRO_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.NOTA_COBRO_DETALLE.Include("NOTA_COBRO").Include("FACTURA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.NOTA_COBRO_DETALLE where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.NOTA_COBRO_DETALLE.Include("NOTA_COBRO").Include("FACTURA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetByFilter(int? NOTA_COBROId = null, int? FACTURAId = null, int? MONTO_PENDIENTE = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.NOTA_COBRO_DETALLE  where i.ACTIVO  select i;
			
				

				if (MONTO_PENDIENTE.HasValue)
				{
				  q = q.Where(i => i.MONTO_PENDIENTE == MONTO_PENDIENTE.Value);
				}
				if (NOTA_COBROId.HasValue)
				{
				  q = q.Where(i => i.NOTA_COBRO.ID == NOTA_COBROId.Value);
				}
				if (FACTURAId.HasValue)
				{
				  q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
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

		public IQueryable<NOTA_COBRO_DETALLE> GetByFilterWithReferences(int? NOTA_COBROId = null, int? FACTURAId = null, int? MONTO_PENDIENTE = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.NOTA_COBRO_DETALLE.Include("NOTA_COBRO").Include("FACTURA")  where i.ACTIVO select i;
			
				

				if (MONTO_PENDIENTE.HasValue)
				{
					q = q.Where(i => i.MONTO_PENDIENTE == MONTO_PENDIENTE.Value);
				}
				if (NOTA_COBROId.HasValue)
				{
					q = q.Where(i => i.NOTA_COBRO.ID == NOTA_COBROId.Value);
				}
				if (FACTURAId.HasValue)
				{
					q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
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
