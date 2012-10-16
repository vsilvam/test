using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioNOTA_CREDITO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioNOTA_CREDITO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public NOTA_CREDITO GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.NOTA_CREDITO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public NOTA_CREDITO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.NOTA_CREDITO.Include("FACTURA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_CREDITO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.NOTA_CREDITO where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_CREDITO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.NOTA_CREDITO.Include("FACTURA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_CREDITO> GetByFilter(int? FACTURAId = null, System.DateTime? FECHA_EMISION = null, int? NUMERO_NOTA_CREDITO = null, bool? CORRECCION_TOTAL_PARCIAL = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.NOTA_CREDITO  where i.ACTIVO  select i;
			
				

				if (FECHA_EMISION.HasValue)
				{
				  q = q.Where(i => i.FECHA_EMISION == FECHA_EMISION.Value);
				}
				if (NUMERO_NOTA_CREDITO.HasValue)
				{
				  q = q.Where(i => i.NUMERO_NOTA_CREDITO == NUMERO_NOTA_CREDITO.Value);
				}
				if (CORRECCION_TOTAL_PARCIAL.HasValue)
				{
				  q = q.Where(i => i.CORRECCION_TOTAL_PARCIAL == CORRECCION_TOTAL_PARCIAL.Value);
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

		public IQueryable<NOTA_CREDITO> GetByFilterWithReferences(int? FACTURAId = null, System.DateTime? FECHA_EMISION = null, int? NUMERO_NOTA_CREDITO = null, bool? CORRECCION_TOTAL_PARCIAL = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.NOTA_CREDITO.Include("FACTURA")  where i.ACTIVO select i;
			
				

				if (FECHA_EMISION.HasValue)
				{
					q = q.Where(i => i.FECHA_EMISION == FECHA_EMISION.Value);
				}
				if (NUMERO_NOTA_CREDITO.HasValue)
				{
					q = q.Where(i => i.NUMERO_NOTA_CREDITO == NUMERO_NOTA_CREDITO.Value);
				}
				if (CORRECCION_TOTAL_PARCIAL.HasValue)
				{
					q = q.Where(i => i.CORRECCION_TOTAL_PARCIAL == CORRECCION_TOTAL_PARCIAL.Value);
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
