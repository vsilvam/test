using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_VETERINARIAS_EXAMEN GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public CARGA_PRESTACIONES_VETERINARIAS_EXAMEN GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Include("EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Include("EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetByFilter(int? EXAMENId = null, int? CARGA_PRESTACIONES_VETERINARIAS_DETALLEId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE_EXAMEN))
				{
				   q = q.Where(i => i.NOMBRE_EXAMEN.Contains(NOMBRE_EXAMEN));
				}
				if (!string.IsNullOrEmpty(VALOR_EXAMEN))
				{
				   q = q.Where(i => i.VALOR_EXAMEN.Contains(VALOR_EXAMEN));
				}
				if (FECHA_ACTUALIZACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_ACTUALIZACION == FECHA_ACTUALIZACION.Value);
				}
				if (VALOR_VALOR_EXAMEN.HasValue)
				{
				  q = q.Where(i => i.VALOR_VALOR_EXAMEN == VALOR_VALOR_EXAMEN.Value);
				}
				if (EXAMENId.HasValue)
				{
				  q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
				}
				if (CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ID == CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.Value);
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

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetByFilterWithReferences(int? EXAMENId = null, int? CARGA_PRESTACIONES_VETERINARIAS_DETALLEId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Include("EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE_EXAMEN))
				{
					q = q.Where(i => i.NOMBRE_EXAMEN.Contains(NOMBRE_EXAMEN));
				}
				if (!string.IsNullOrEmpty(VALOR_EXAMEN))
				{
					q = q.Where(i => i.VALOR_EXAMEN.Contains(VALOR_EXAMEN));
				}
				if (FECHA_ACTUALIZACION.HasValue)
				{
					q = q.Where(i => i.FECHA_ACTUALIZACION == FECHA_ACTUALIZACION.Value);
				}
				if (VALOR_VALOR_EXAMEN.HasValue)
				{
					q = q.Where(i => i.VALOR_VALOR_EXAMEN == VALOR_VALOR_EXAMEN.Value);
				}
				if (EXAMENId.HasValue)
				{
					q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
				}
				if (CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ID == CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.Value);
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
