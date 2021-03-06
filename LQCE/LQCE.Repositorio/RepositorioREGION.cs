using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioREGION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioREGION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public REGION GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.REGION.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public REGION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.REGION.Include("COMUNA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<REGION> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.REGION where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<REGION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.REGION.Include("COMUNA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<REGION> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.REGION  where i.ACTIVO  select i;
			
				

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

		public IQueryable<REGION> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.REGION.Include("COMUNA")  where i.ACTIVO select i;
			
				

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
