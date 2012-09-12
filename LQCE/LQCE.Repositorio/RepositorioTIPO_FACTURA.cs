using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioTIPO_FACTURA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioTIPO_FACTURA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public TIPO_FACTURA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.TIPO_FACTURA.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public TIPO_FACTURA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.TIPO_FACTURA.Include("CLIENTE").Include("FACTURA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_FACTURA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.TIPO_FACTURA where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_FACTURA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.TIPO_FACTURA.Include("CLIENTE").Include("FACTURA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_FACTURA> GetByFilter(string RUT_FACTURA = "", string NOMBRE_FACTURA = "", bool? AFECTO_IVA = null, string NOMBRE_REPORTE_FACTURA = "", string NOMBRE_REPORTE_FACTURA_INDIVIDUAL = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.TIPO_FACTURA  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(RUT_FACTURA))
				{
				   q = q.Where(i => i.RUT_FACTURA.Contains(RUT_FACTURA));
				}
				if (!string.IsNullOrEmpty(NOMBRE_FACTURA))
				{
				   q = q.Where(i => i.NOMBRE_FACTURA.Contains(NOMBRE_FACTURA));
				}
				if (AFECTO_IVA.HasValue)
				{
				  q = q.Where(i => i.AFECTO_IVA == AFECTO_IVA.Value);
				}
				if (!string.IsNullOrEmpty(NOMBRE_REPORTE_FACTURA))
				{
				   q = q.Where(i => i.NOMBRE_REPORTE_FACTURA.Contains(NOMBRE_REPORTE_FACTURA));
				}
				if (!string.IsNullOrEmpty(NOMBRE_REPORTE_FACTURA_INDIVIDUAL))
				{
				   q = q.Where(i => i.NOMBRE_REPORTE_FACTURA_INDIVIDUAL.Contains(NOMBRE_REPORTE_FACTURA_INDIVIDUAL));
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

		public IQueryable<TIPO_FACTURA> GetByFilterWithReferences(string RUT_FACTURA = "", string NOMBRE_FACTURA = "", bool? AFECTO_IVA = null, string NOMBRE_REPORTE_FACTURA = "", string NOMBRE_REPORTE_FACTURA_INDIVIDUAL = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.TIPO_FACTURA.Include("CLIENTE").Include("FACTURA")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(RUT_FACTURA))
				{
					q = q.Where(i => i.RUT_FACTURA.Contains(RUT_FACTURA));
				}
				if (!string.IsNullOrEmpty(NOMBRE_FACTURA))
				{
					q = q.Where(i => i.NOMBRE_FACTURA.Contains(NOMBRE_FACTURA));
				}
				if (AFECTO_IVA.HasValue)
				{
					q = q.Where(i => i.AFECTO_IVA == AFECTO_IVA.Value);
				}
				if (!string.IsNullOrEmpty(NOMBRE_REPORTE_FACTURA))
				{
					q = q.Where(i => i.NOMBRE_REPORTE_FACTURA.Contains(NOMBRE_REPORTE_FACTURA));
				}
				if (!string.IsNullOrEmpty(NOMBRE_REPORTE_FACTURA_INDIVIDUAL))
				{
					q = q.Where(i => i.NOMBRE_REPORTE_FACTURA_INDIVIDUAL.Contains(NOMBRE_REPORTE_FACTURA_INDIVIDUAL));
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
