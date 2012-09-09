using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioFACTURACION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioFACTURACION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public FACTURACION GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.FACTURACION.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public FACTURACION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.FACTURACION.Include("FACTURA").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURACION> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.FACTURACION select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURACION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.FACTURACION.Include("FACTURA") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<FACTURACION> GetByFilter(System.DateTime? FECHA_FACTURACION = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.FACTURACION  select i;
			
				

				if (FECHA_FACTURACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
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

		public IQueryable<FACTURACION> GetByFilterWithReferences(System.DateTime? FECHA_FACTURACION = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.FACTURACION.Include("FACTURA") select i;
			
				

				if (FECHA_FACTURACION.HasValue)
				{
					q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
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
