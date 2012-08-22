using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPRESTACION_EXAMEN
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPRESTACION_EXAMEN(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PRESTACION_EXAMEN GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PRESTACION_EXAMEN.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public PRESTACION_EXAMEN GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PRESTACION_EXAMEN.Include("EXAMEN").Include("PRESTACION").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_EXAMEN> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_EXAMEN select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_EXAMEN> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_EXAMEN.Include("EXAMEN").Include("PRESTACION") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PRESTACION_EXAMEN> GetByFilter(int? EXAMENId = null, int? PRESTACIONId = null, bool? ACTIVO = null, int? VALOR = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_EXAMEN select i;

				if (VALOR.HasValue)
				{
				  q = q.Where(i => i.VALOR == VALOR.Value);
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (EXAMENId.HasValue)
				{
				  q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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

		public IQueryable<PRESTACION_EXAMEN> GetByFilterWithReferences(int? EXAMENId = null, int? PRESTACIONId = null, bool? ACTIVO = null, int? VALOR = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PRESTACION_EXAMEN.Include("EXAMEN").Include("PRESTACION") select i;

				if (VALOR.HasValue)
				{
					q = q.Where(i => i.VALOR == VALOR.Value);
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (EXAMENId.HasValue)
				{
					q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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
