using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioGARANTIA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioGARANTIA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public GARANTIA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.GARANTIA.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public GARANTIA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.GARANTIA.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<GARANTIA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.GARANTIA select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<GARANTIA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.GARANTIA.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<GARANTIA> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.GARANTIA  select i;
			
				

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
                return null;
            }
		}

		public IQueryable<GARANTIA> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.GARANTIA.Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;
			
				

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
                return null;
			}
		}
	}
}
