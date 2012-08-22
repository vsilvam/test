using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCOMUNA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCOMUNA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public COMUNA GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.COMUNA.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public COMUNA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.COMUNA.Include("CLIENTE").Include("REGION").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COMUNA> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COMUNA select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COMUNA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COMUNA.Include("CLIENTE").Include("REGION") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COMUNA> GetByFilter(int? REGIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COMUNA select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (REGIONId.HasValue)
				{
				  q = q.Where(i => i.REGION.ID == REGIONId.Value);
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

		public IQueryable<COMUNA> GetByFilterWithReferences(int? REGIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COMUNA.Include("CLIENTE").Include("REGION") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (REGIONId.HasValue)
				{
					q = q.Where(i => i.REGION.ID == REGIONId.Value);
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
