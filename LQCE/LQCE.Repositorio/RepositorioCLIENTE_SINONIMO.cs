using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCLIENTE_SINONIMO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCLIENTE_SINONIMO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CLIENTE_SINONIMO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CLIENTE_SINONIMO.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CLIENTE_SINONIMO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CLIENTE_SINONIMO.Include("CLIENTE").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE_SINONIMO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CLIENTE_SINONIMO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE_SINONIMO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CLIENTE_SINONIMO.Include("CLIENTE") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE_SINONIMO> GetByFilter(int? CLIENTEId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CLIENTE_SINONIMO select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
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

		public IQueryable<CLIENTE_SINONIMO> GetByFilterWithReferences(int? CLIENTEId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CLIENTE_SINONIMO.Include("CLIENTE") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
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