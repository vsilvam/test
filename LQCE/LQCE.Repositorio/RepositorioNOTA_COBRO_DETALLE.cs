using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioNOTA_COBRO_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioNOTA_COBRO_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public NOTA_COBRO_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.NOTA_COBRO_DETALLE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public NOTA_COBRO_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.NOTA_COBRO_DETALLE.Include("FACTURA").Include("NOTA_COBRO").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.NOTA_COBRO_DETALLE  where i.ACTIVO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.NOTA_COBRO_DETALLE.Include("FACTURA").Include("NOTA_COBRO") where i.ACTIVO  select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<NOTA_COBRO_DETALLE> GetByFilter(int? FACTURAId = null, int? NOTA_COBROId = null, int? MONTO_PENDIENTE = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.NOTA_COBRO_DETALLE  where i.ACTIVO  select i;

				if (MONTO_PENDIENTE.HasValue)
				{
				  q = q.Where(i => i.MONTO_PENDIENTE == MONTO_PENDIENTE.Value);
				}
				if (FACTURAId.HasValue)
				{
				  q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
				}
				if (NOTA_COBROId.HasValue)
				{
				  q = q.Where(i => i.NOTA_COBRO.ID == NOTA_COBROId.Value);
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

		public IQueryable<NOTA_COBRO_DETALLE> GetByFilterWithReferences(int? FACTURAId = null, int? NOTA_COBROId = null, int? MONTO_PENDIENTE = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.NOTA_COBRO_DETALLE.Include("FACTURA").Include("NOTA_COBRO")  where i.ACTIVO select i;

				if (MONTO_PENDIENTE.HasValue)
				{
					q = q.Where(i => i.MONTO_PENDIENTE == MONTO_PENDIENTE.Value);
				}
				if (FACTURAId.HasValue)
				{
					q = q.Where(i => i.FACTURA.ID == FACTURAId.Value);
				}
				if (NOTA_COBROId.HasValue)
				{
					q = q.Where(i => i.NOTA_COBRO.ID == NOTA_COBROId.Value);
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
