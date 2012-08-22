using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioTIPO_PRESTACION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioTIPO_PRESTACION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public TIPO_PRESTACION GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.TIPO_PRESTACION.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public TIPO_PRESTACION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.TIPO_PRESTACION select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetByFilter(string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.TIPO_PRESTACION select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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

		public IQueryable<TIPO_PRESTACION> GetByFilterWithReferences(string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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