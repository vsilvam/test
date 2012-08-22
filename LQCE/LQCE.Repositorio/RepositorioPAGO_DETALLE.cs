using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPAGO_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPAGO_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PAGO_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PAGO_DETALLE.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public PAGO_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.PAGO_DETALLE.Include("FACTURA_DETALLE").Include("PAGO").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO_DETALLE select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO_DETALLE.Include("FACTURA_DETALLE").Include("PAGO") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<PAGO_DETALLE> GetByFilter(int? FACTURA_DETALLEId = null, int? PAGOId = null, int? MONTO = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO_DETALLE select i;

				if (MONTO.HasValue)
				{
				  q = q.Where(i => i.MONTO == MONTO.Value);
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (FACTURA_DETALLEId.HasValue)
				{
				  q = q.Where(i => i.FACTURA_DETALLE.ID == FACTURA_DETALLEId.Value);
				}
				if (PAGOId.HasValue)
				{
				  q = q.Where(i => i.PAGO.ID == PAGOId.Value);
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

		public IQueryable<PAGO_DETALLE> GetByFilterWithReferences(int? FACTURA_DETALLEId = null, int? PAGOId = null, int? MONTO = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.PAGO_DETALLE.Include("FACTURA_DETALLE").Include("PAGO") select i;

				if (MONTO.HasValue)
				{
					q = q.Where(i => i.MONTO == MONTO.Value);
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (FACTURA_DETALLEId.HasValue)
				{
					q = q.Where(i => i.FACTURA_DETALLE.ID == FACTURA_DETALLEId.Value);
				}
				if (PAGOId.HasValue)
				{
					q = q.Where(i => i.PAGO.ID == PAGOId.Value);
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
