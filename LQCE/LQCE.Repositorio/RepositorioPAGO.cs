using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPAGO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPAGO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PAGO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PAGO.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public PAGO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PAGO.Include("CLIENTE").Include("PAGO_DETALLE").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO.Include("CLIENTE").Include("PAGO_DETALLE") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO> GetByFilter(int? CLIENTEId = null, int? FECHA_PAGO = null, int? MONTO_PAGO = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO select i;

				if (FECHA_PAGO.HasValue)
				{
				  q = q.Where(i => i.FECHA_PAGO == FECHA_PAGO.Value);
				}
				if (MONTO_PAGO.HasValue)
				{
				  q = q.Where(i => i.MONTO_PAGO == MONTO_PAGO.Value);
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
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

		public IQueryable<PAGO> GetByFilterWithReferences(int? CLIENTEId = null, int? FECHA_PAGO = null, int? MONTO_PAGO = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO.Include("CLIENTE").Include("PAGO_DETALLE") select i;

				if (FECHA_PAGO.HasValue)
				{
					q = q.Where(i => i.FECHA_PAGO == FECHA_PAGO.Value);
				}
				if (MONTO_PAGO.HasValue)
				{
					q = q.Where(i => i.MONTO_PAGO == MONTO_PAGO.Value);
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
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