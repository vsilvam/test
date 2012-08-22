using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioRAZA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioRAZA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public RAZA GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.RAZA.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public RAZA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.RAZA.Include("ESPECIE").Include("PRESTACION_VETERINARIA").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<RAZA> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.RAZA select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<RAZA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.RAZA.Include("ESPECIE").Include("PRESTACION_VETERINARIA") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<RAZA> GetByFilter(int? ESPECIEId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.RAZA select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (ESPECIEId.HasValue)
				{
				  q = q.Where(i => i.ESPECIE.ID == ESPECIEId.Value);
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

		public IQueryable<RAZA> GetByFilterWithReferences(int? ESPECIEId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.RAZA.Include("ESPECIE").Include("PRESTACION_VETERINARIA") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (ESPECIEId.HasValue)
				{
					q = q.Where(i => i.ESPECIE.ID == ESPECIEId.Value);
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