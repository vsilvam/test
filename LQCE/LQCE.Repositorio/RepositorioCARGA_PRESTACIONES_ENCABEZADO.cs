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
							return _context.CARGA_PRESTACIONES_ENCABEZADO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public CARGA_PRESTACIONES_ENCABEZADO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_ESTADO").Include("TIPO_PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_ESTADO").Include("TIPO_PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetByFilter(int? CARGA_PRESTACIONES_ESTADOId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_CARGA = null, string ARCHIVO = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO  where i.ACTIVO  select i;
			
				

				if (FECHA_CARGA.HasValue)
				{
				  q = q.Where(i => i.FECHA_CARGA == FECHA_CARGA.Value);
				}
				if (!string.IsNullOrEmpty(ARCHIVO))
				{
				   q = q.Where(i => i.ARCHIVO.Contains(ARCHIVO));
				}
				if (CARGA_PRESTACIONES_ESTADOId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_ESTADO.ID == CARGA_PRESTACIONES_ESTADOId.Value);
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
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_ENCABEZADO> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ESTADOId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_CARGA = null, string ARCHIVO = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CARGA_PRESTACIONES_ENCABEZADO.Include("CARGA_PRESTACIONES_ESTADO").Include("TIPO_PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")  where i.ACTIVO select i;
			
				

				if (FECHA_CARGA.HasValue)
				{
					q = q.Where(i => i.FECHA_CARGA == FECHA_CARGA.Value);
				}
				if (!string.IsNullOrEmpty(ARCHIVO))
				{
					q = q.Where(i => i.ARCHIVO.Contains(ARCHIVO));
				}
				if (CARGA_PRESTACIONES_ESTADOId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_ESTADO.ID == CARGA_PRESTACIONES_ESTADOId.Value);
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
                throw ex;
			}
		}
	}
}
