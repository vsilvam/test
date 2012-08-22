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
                return null;
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
                return null;
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
                return null;
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
                return null;
            }
		}

		public IQueryable<FACTURACION> GetByFilter(System.DateTime? FECHA_FACTURACION = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.FACTURACION select i;

				if (FECHA_FACTURACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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

		public IQueryable<FACTURACION> GetByFilterWithReferences(System.DateTime? FECHA_FACTURACION = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.FACTURACION.Include("FACTURA") select i;

				if (FECHA_FACTURACION.HasValue)
				{
					q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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