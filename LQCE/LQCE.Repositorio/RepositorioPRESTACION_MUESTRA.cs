using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPRESTACION_MUESTRA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPRESTACION_MUESTRA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PRESTACION_MUESTRA GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PRESTACION_MUESTRA.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public PRESTACION_MUESTRA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PRESTACION_MUESTRA.Include("PRESTACION").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_MUESTRA> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_MUESTRA select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_MUESTRA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_MUESTRA.Include("PRESTACION") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_MUESTRA> GetByFilter(int? PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_MUESTRA select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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
                return null;
            }
		}

		public IQueryable<PRESTACION_MUESTRA> GetByFilterWithReferences(int? PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_MUESTRA.Include("PRESTACION") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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
                return null;
			}
		}
	}
}
