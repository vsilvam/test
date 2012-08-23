using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioEXAMEN_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioEXAMEN_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public EXAMEN_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.EXAMEN_DETALLE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public EXAMEN_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.EXAMEN_DETALLE.Include("EXAMEN").Include("EXAMEN1").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.EXAMEN_DETALLE  where i.ACTIVO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.EXAMEN_DETALLE.Include("EXAMEN").Include("EXAMEN1") where i.ACTIVO  select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN_DETALLE> GetByFilter(int? EXAMENId = null, int? EXAMEN1Id = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.EXAMEN_DETALLE  where i.ACTIVO  select i;

				if (EXAMENId.HasValue)
				{
				  q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
				}
				if (EXAMEN1Id.HasValue)
				{
				  q = q.Where(i => i.EXAMEN1.ID == EXAMEN1Id.Value);
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

		public IQueryable<EXAMEN_DETALLE> GetByFilterWithReferences(int? EXAMENId = null, int? EXAMEN1Id = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.EXAMEN_DETALLE.Include("EXAMEN").Include("EXAMEN1")  where i.ACTIVO select i;

				if (EXAMENId.HasValue)
				{
					q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
				}
				if (EXAMEN1Id.HasValue)
				{
					q = q.Where(i => i.EXAMEN1.ID == EXAMEN1Id.Value);
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
