using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioEXAMEN_SINONIMO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioEXAMEN_SINONIMO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public EXAMEN_SINONIMO GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.EXAMEN_SINONIMO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public EXAMEN_SINONIMO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.EXAMEN_SINONIMO.Include("EXAMEN").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<EXAMEN_SINONIMO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.EXAMEN_SINONIMO where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<EXAMEN_SINONIMO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.EXAMEN_SINONIMO.Include("EXAMEN") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<EXAMEN_SINONIMO> GetByFilter(int? EXAMENId = null, string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.EXAMEN_SINONIMO  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (EXAMENId.HasValue)
				{
				  q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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

		public IQueryable<EXAMEN_SINONIMO> GetByFilterWithReferences(int? EXAMENId = null, string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.EXAMEN_SINONIMO.Include("EXAMEN")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (EXAMENId.HasValue)
				{
					q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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
