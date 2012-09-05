using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_ESTADO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_ESTADO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_ESTADO GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.CARGA_PRESTACIONES_ESTADO.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CARGA_PRESTACIONES_ESTADO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CARGA_PRESTACIONES_ESTADO.Include("CARGA_PRESTACIONES_ENCABEZADO").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ESTADO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CARGA_PRESTACIONES_ESTADO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ESTADO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CARGA_PRESTACIONES_ESTADO.Include("CARGA_PRESTACIONES_ENCABEZADO") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ESTADO> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CARGA_PRESTACIONES_ESTADO  select i;
			
				

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

		public IQueryable<CARGA_PRESTACIONES_ESTADO> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CARGA_PRESTACIONES_ESTADO.Include("CARGA_PRESTACIONES_ENCABEZADO") select i;
			
				

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
