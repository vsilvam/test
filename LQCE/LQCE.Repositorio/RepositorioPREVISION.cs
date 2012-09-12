using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPREVISION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPREVISION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PREVISION GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.PREVISION.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public PREVISION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.PREVISION.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PREVISION> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.PREVISION where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PREVISION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.PREVISION.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PREVISION> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.PREVISION  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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

		public IQueryable<PREVISION> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.PREVISION.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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
