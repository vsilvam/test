using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_HUMANAS_EXAMEN GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CARGA_PRESTACIONES_HUMANAS_EXAMEN GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("EXAMEN").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("EXAMEN") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetByFilter(int? CARGA_PRESTACIONES_HUMANAS_DETALLEId = null, int? EXAMENId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN  select i;
			
				

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
				if (CARGA_PRESTACIONES_HUMANAS_DETALLEId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_HUMANAS_DETALLE.ID == CARGA_PRESTACIONES_HUMANAS_DETALLEId.Value);
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
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetByFilterWithReferences(int? CARGA_PRESTACIONES_HUMANAS_DETALLEId = null, int? EXAMENId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("EXAMEN") select i;
			
				

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
				if (CARGA_PRESTACIONES_HUMANAS_DETALLEId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_HUMANAS_DETALLE.ID == CARGA_PRESTACIONES_HUMANAS_DETALLEId.Value);
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
                return null;
			}
		}
	}
}
