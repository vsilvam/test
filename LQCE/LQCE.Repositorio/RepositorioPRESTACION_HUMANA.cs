using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPRESTACION_HUMANA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPRESTACION_HUMANA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PRESTACION_HUMANA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.PRESTACION_HUMANA.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public PRESTACION_HUMANA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.PRESTACION_HUMANA.Include("PRESTACION").Include("PRESTACION_1").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_HUMANA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.PRESTACION_HUMANA where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_HUMANA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.PRESTACION_HUMANA.Include("PRESTACION").Include("PRESTACION_1") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_HUMANA> GetByFilter(string NOMBRE = "", string RUT = "", string EDAD = "", string TELEFONO = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.PRESTACION_HUMANA  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(RUT))
				{
				   q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
				   q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
				   q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
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

		public IQueryable<PRESTACION_HUMANA> GetByFilterWithReferences(string NOMBRE = "", string RUT = "", string EDAD = "", string TELEFONO = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.PRESTACION_HUMANA.Include("PRESTACION").Include("PRESTACION_1")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(RUT))
				{
					q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
					q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
					q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
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
