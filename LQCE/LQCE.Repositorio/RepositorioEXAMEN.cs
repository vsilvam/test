using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioEXAMEN
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioEXAMEN(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public EXAMEN GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.EXAMEN.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public EXAMEN GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.EXAMEN.Include("CONVENIO_EXAMEN").Include("EXAMEN_DETALLE").Include("EXAMEN_DETALLE1").Include("EXAMEN_SINONIMO").Include("TIPO_PRESTACION").Include("PRESTACION_EXAMEN").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.EXAMEN select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.EXAMEN.Include("CONVENIO_EXAMEN").Include("EXAMEN_DETALLE").Include("EXAMEN_DETALLE1").Include("EXAMEN_SINONIMO").Include("TIPO_PRESTACION").Include("PRESTACION_EXAMEN").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<EXAMEN> GetByFilter(int? TIPO_PRESTACIONId = null, string CODIGO = "", string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.EXAMEN  select i;
			
				

				if (!string.IsNullOrEmpty(CODIGO))
				{
				   q = q.Where(i => i.CODIGO.Contains(CODIGO));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (TIPO_PRESTACIONId.HasValue)
				{
				  q = q.Where(i => i.TIPO_PRESTACION.ID == TIPO_PRESTACIONId.Value);
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

		public IQueryable<EXAMEN> GetByFilterWithReferences(int? TIPO_PRESTACIONId = null, string CODIGO = "", string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.EXAMEN.Include("CONVENIO_EXAMEN").Include("EXAMEN_DETALLE").Include("EXAMEN_DETALLE1").Include("EXAMEN_SINONIMO").Include("TIPO_PRESTACION").Include("PRESTACION_EXAMEN").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN") select i;
			
				

				if (!string.IsNullOrEmpty(CODIGO))
				{
					q = q.Where(i => i.CODIGO.Contains(CODIGO));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (TIPO_PRESTACIONId.HasValue)
				{
					q = q.Where(i => i.TIPO_PRESTACION.ID == TIPO_PRESTACIONId.Value);
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
