using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_ENCABEZADO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_ENCABEZADO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_ENCABEZADO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_ENCABEZADO.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CARGA_PRESTACIONES_ENCABEZADO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetByFilter(System.DateTime? FECHA_CARGA = null, int? ID_TIPO_PRESTACION = null, string ARCHIVO = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO select i;

				if (FECHA_CARGA.HasValue)
				{
				  q = q.Where(i => i.FECHA_CARGA == FECHA_CARGA.Value);
				}
				if (ID_TIPO_PRESTACION.HasValue)
				{
				  q = q.Where(i => i.ID_TIPO_PRESTACION == ID_TIPO_PRESTACION.Value);
				}
				if (!string.IsNullOrEmpty(ARCHIVO))
				{
				   q = q.Where(i => i.ARCHIVO.Contains(ARCHIVO));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetByFilterWithReferences(System.DateTime? FECHA_CARGA = null, int? ID_TIPO_PRESTACION = null, string ARCHIVO = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;

				if (FECHA_CARGA.HasValue)
				{
					q = q.Where(i => i.FECHA_CARGA == FECHA_CARGA.Value);
				}
				if (ID_TIPO_PRESTACION.HasValue)
				{
					q = q.Where(i => i.ID_TIPO_PRESTACION == ID_TIPO_PRESTACION.Value);
				}
				if (!string.IsNullOrEmpty(ARCHIVO))
				{
					q = q.Where(i => i.ARCHIVO.Contains(ARCHIVO));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
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
