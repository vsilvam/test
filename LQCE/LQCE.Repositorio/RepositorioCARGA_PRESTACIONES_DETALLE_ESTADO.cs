using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_DETALLE_ESTADO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_DETALLE_ESTADO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CARGA_PRESTACIONES_DETALLE_ESTADO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_DETALLE_ESTADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_DETALLE_ESTADO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_DETALLE_ESTADO  where i.ACTIVO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_DETALLE_ESTADO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_DETALLE_ESTADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE") where i.ACTIVO  select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_DETALLE_ESTADO> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_DETALLE_ESTADO  where i.ACTIVO  select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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

		public IQueryable<CARGA_PRESTACIONES_DETALLE_ESTADO> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_DETALLE_ESTADO.Include("CARGA_PRESTACIONES_HUMANAS_DETALLE").Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")  where i.ACTIVO select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
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
