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
							return _context.ESPECIE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public ESPECIE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.ESPECIE.Include("RAZA").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("PRESTACION_VETERINARIA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<ESPECIE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.ESPECIE where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<ESPECIE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.ESPECIE.Include("RAZA").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("PRESTACION_VETERINARIA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<ESPECIE> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.ESPECIE  where i.ACTIVO  select i;
			
				

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

		public IQueryable<ESPECIE> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.ESPECIE.Include("RAZA").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("PRESTACION_VETERINARIA")  where i.ACTIVO select i;
			
				

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
