using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPRESTACION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPRESTACION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PRESTACION GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.PRESTACION.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public PRESTACION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.PRESTACION.Include("CLIENTE").Include("FACTURA_DETALLE").Include("GARANTIA").Include("PRESTACION_EXAMEN").Include("PRESTACION_HUMANA").Include("PRESTACION_MUESTRA").Include("PREVISION").Include("TIPO_PRESTACION").Include("PRESTACION_VETERINARIA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.PRESTACION where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.PRESTACION.Include("CLIENTE").Include("FACTURA_DETALLE").Include("GARANTIA").Include("PRESTACION_EXAMEN").Include("PRESTACION_HUMANA").Include("PRESTACION_MUESTRA").Include("PREVISION").Include("TIPO_PRESTACION").Include("PRESTACION_VETERINARIA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION> GetByFilter(int? CLIENTEId = null, int? GARANTIAId = null, int? PREVISIONId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_RECEPCION = null, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.PRESTACION  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(MEDICO))
				{
				   q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (FECHA_RECEPCION.HasValue)
				{
				  q = q.Where(i => i.FECHA_RECEPCION == FECHA_RECEPCION.Value);
				}
				if (!string.IsNullOrEmpty(RECEPCION))
				{
				   q = q.Where(i => i.RECEPCION.Contains(RECEPCION));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
				   q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (GARANTIAId.HasValue)
				{
				  q = q.Where(i => i.GARANTIA.ID == GARANTIAId.Value);
				}
				if (PREVISIONId.HasValue)
				{
				  q = q.Where(i => i.PREVISION.ID == PREVISIONId.Value);
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

		public IQueryable<PRESTACION> GetByFilterWithReferences(int? CLIENTEId = null, int? GARANTIAId = null, int? PREVISIONId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_RECEPCION = null, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.PRESTACION.Include("CLIENTE").Include("FACTURA_DETALLE").Include("GARANTIA").Include("PRESTACION_EXAMEN").Include("PRESTACION_HUMANA").Include("PRESTACION_MUESTRA").Include("PREVISION").Include("TIPO_PRESTACION").Include("PRESTACION_VETERINARIA")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(MEDICO))
				{
					q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (FECHA_RECEPCION.HasValue)
				{
					q = q.Where(i => i.FECHA_RECEPCION == FECHA_RECEPCION.Value);
				}
				if (!string.IsNullOrEmpty(RECEPCION))
				{
					q = q.Where(i => i.RECEPCION.Contains(RECEPCION));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
					q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (GARANTIAId.HasValue)
				{
					q = q.Where(i => i.GARANTIA.ID == GARANTIAId.Value);
				}
				if (PREVISIONId.HasValue)
				{
					q = q.Where(i => i.PREVISION.ID == PREVISIONId.Value);
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
