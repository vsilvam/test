using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCLIENTE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCLIENTE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CLIENTE GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.CLIENTE.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CLIENTE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("FACTURA").Include("PAGO").Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CLIENTE select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("FACTURA").Include("PAGO").Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CLIENTE> GetByFilter(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CLIENTE  select i;
			
				

				if (!string.IsNullOrEmpty(RUT))
				{
				   q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (DESCUENTO.HasValue)
				{
				  q = q.Where(i => i.DESCUENTO == DESCUENTO.Value);
				}
				if (COMUNAId.HasValue)
				{
				  q = q.Where(i => i.COMUNA.ID == COMUNAId.Value);
				}
				if (CONVENIOId.HasValue)
				{
				  q = q.Where(i => i.CONVENIO.ID == CONVENIOId.Value);
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

		public IQueryable<CLIENTE> GetByFilterWithReferences(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("FACTURA").Include("PAGO").Include("PRESTACION").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") select i;
			
				

				if (!string.IsNullOrEmpty(RUT))
				{
					q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (DESCUENTO.HasValue)
				{
					q = q.Where(i => i.DESCUENTO == DESCUENTO.Value);
				}
				if (COMUNAId.HasValue)
				{
					q = q.Where(i => i.COMUNA.ID == COMUNAId.Value);
				}
				if (CONVENIOId.HasValue)
				{
					q = q.Where(i => i.CONVENIO.ID == CONVENIOId.Value);
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
