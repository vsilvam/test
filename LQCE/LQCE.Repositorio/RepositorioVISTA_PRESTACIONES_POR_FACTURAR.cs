using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioVISTA_PRESTACIONES_POR_FACTURAR
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioVISTA_PRESTACIONES_POR_FACTURAR(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public VISTA_PRESTACIONES_POR_FACTURAR GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.VISTA_PRESTACIONES_POR_FACTURAR.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public VISTA_PRESTACIONES_POR_FACTURAR GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.VISTA_PRESTACIONES_POR_FACTURAR.FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetByFilter(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, System.DateTime? FECHA_RECEPCION = null, int? TOTAL = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR  select i;
			
				

				if (ID_CLIENTE.HasValue)
				{
				  q = q.Where(i => i.ID_CLIENTE == ID_CLIENTE.Value);
				}
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
				if (FECHA_RECEPCION.HasValue)
				{
				  q = q.Where(i => i.FECHA_RECEPCION == FECHA_RECEPCION.Value);
				}
				if (TOTAL.HasValue)
				{
				  q = q.Where(i => i.TOTAL == TOTAL.Value);
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

		public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetByFilterWithReferences(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, System.DateTime? FECHA_RECEPCION = null, int? TOTAL = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR select i;
			
				

				if (ID_CLIENTE.HasValue)
				{
					q = q.Where(i => i.ID_CLIENTE == ID_CLIENTE.Value);
				}
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
				if (FECHA_RECEPCION.HasValue)
				{
					q = q.Where(i => i.FECHA_RECEPCION == FECHA_RECEPCION.Value);
				}
				if (TOTAL.HasValue)
				{
					q = q.Where(i => i.TOTAL == TOTAL.Value);
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
