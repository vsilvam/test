using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioTIPO_COBRO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioTIPO_COBRO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public TIPO_COBRO GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.TIPO_COBRO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public TIPO_COBRO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.TIPO_COBRO.Include("COBRO").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_COBRO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.TIPO_COBRO where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_COBRO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.TIPO_COBRO.Include("COBRO") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_COBRO> GetByFilter(string NOMBRE = "", string REPORTE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.TIPO_COBRO  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(REPORTE))
				{
				   q = q.Where(i => i.REPORTE.Contains(REPORTE));
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

		public IQueryable<TIPO_COBRO> GetByFilterWithReferences(string NOMBRE = "", string REPORTE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.TIPO_COBRO.Include("COBRO")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(REPORTE))
				{
					q = q.Where(i => i.REPORTE.Contains(REPORTE));
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
