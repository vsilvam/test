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
							return _context.CLIENTE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public CLIENTE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("PAGO").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("TIPO_FACTURA").Include("NOTA_COBRO").Include("PRESTACION").Include("FACTURA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CLIENTE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CLIENTE where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CLIENTE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("PAGO").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("TIPO_FACTURA").Include("NOTA_COBRO").Include("PRESTACION").Include("FACTURA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CLIENTE> GetByFilter(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, int? TIPO_FACTURAId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CLIENTE  where i.ACTIVO  select i;
			
				

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
				if (!string.IsNullOrEmpty(DIRECCION))
				{
				   q = q.Where(i => i.DIRECCION.Contains(DIRECCION));
				}
				if (!string.IsNullOrEmpty(FONO))
				{
				   q = q.Where(i => i.FONO.Contains(FONO));
				}
				if (!string.IsNullOrEmpty(GIRO))
				{
				   q = q.Where(i => i.GIRO.Contains(GIRO));
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
				if (TIPO_FACTURAId.HasValue)
				{
				  q = q.Where(i => i.TIPO_FACTURA.ID == TIPO_FACTURAId.Value);
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

		public IQueryable<CLIENTE> GetByFilterWithReferences(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, int? TIPO_FACTURAId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CLIENTE.Include("COMUNA").Include("CONVENIO").Include("CLIENTE_SINONIMO").Include("TIPO_PRESTACION").Include("PAGO").Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").Include("TIPO_FACTURA").Include("NOTA_COBRO").Include("PRESTACION").Include("FACTURA")  where i.ACTIVO select i;
			
				

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
				if (!string.IsNullOrEmpty(DIRECCION))
				{
					q = q.Where(i => i.DIRECCION.Contains(DIRECCION));
				}
				if (!string.IsNullOrEmpty(FONO))
				{
					q = q.Where(i => i.FONO.Contains(FONO));
				}
				if (!string.IsNullOrEmpty(GIRO))
				{
					q = q.Where(i => i.GIRO.Contains(GIRO));
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
				if (TIPO_FACTURAId.HasValue)
				{
					q = q.Where(i => i.TIPO_FACTURA.ID == TIPO_FACTURAId.Value);
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
