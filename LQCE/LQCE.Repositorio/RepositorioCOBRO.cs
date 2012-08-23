using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCOBRO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCOBRO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public COBRO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.COBRO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public COBRO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.COBRO.Include("TIPO_COBRO").Include("NOTA_COBRO").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COBRO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COBRO  where i.ACTIVO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COBRO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COBRO.Include("TIPO_COBRO").Include("NOTA_COBRO") where i.ACTIVO  select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<COBRO> GetByFilter(int? TIPO_COBROId = null, System.DateTime? FECHA_COBRO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COBRO  where i.ACTIVO  select i;

				if (FECHA_COBRO.HasValue)
				{
				  q = q.Where(i => i.FECHA_COBRO == FECHA_COBRO.Value);
				}
				if (TIPO_COBROId.HasValue)
				{
				  q = q.Where(i => i.TIPO_COBRO.ID == TIPO_COBROId.Value);
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

		public IQueryable<COBRO> GetByFilterWithReferences(int? TIPO_COBROId = null, System.DateTime? FECHA_COBRO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.COBRO.Include("TIPO_COBRO").Include("NOTA_COBRO")  where i.ACTIVO select i;

				if (FECHA_COBRO.HasValue)
				{
					q = q.Where(i => i.FECHA_COBRO == FECHA_COBRO.Value);
				}
				if (TIPO_COBROId.HasValue)
				{
					q = q.Where(i => i.TIPO_COBRO.ID == TIPO_COBROId.Value);
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
