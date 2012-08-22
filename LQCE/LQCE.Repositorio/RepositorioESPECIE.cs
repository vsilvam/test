using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioESPECIE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioESPECIE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public ESPECIE GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.ESPECIE.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public ESPECIE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.ESPECIE.Include("PRESTACION_VETERINARIA").Include("RAZA").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<ESPECIE> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.ESPECIE select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<ESPECIE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.ESPECIE.Include("PRESTACION_VETERINARIA").Include("RAZA") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<ESPECIE> GetByFilter(string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.ESPECIE select i;

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

		public IQueryable<ESPECIE> GetByFilterWithReferences(string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.ESPECIE.Include("PRESTACION_VETERINARIA").Include("RAZA") select i;

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
