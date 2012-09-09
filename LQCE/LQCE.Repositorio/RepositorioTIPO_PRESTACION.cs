using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioTIPO_PRESTACION
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioTIPO_PRESTACION(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public TIPO_PRESTACION GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.TIPO_PRESTACION.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public TIPO_PRESTACION GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION").Include("CARGA_PRESTACIONES_ENCABEZADO").FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.TIPO_PRESTACION select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION").Include("CARGA_PRESTACIONES_ENCABEZADO") select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetByFilter(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.TIPO_PRESTACION  select i;
			
				

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
                throw ex;
            }
		}

		public IQueryable<TIPO_PRESTACION> GetByFilterWithReferences(string NOMBRE = "")
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.TIPO_PRESTACION.Include("CLIENTE").Include("CONVENIO").Include("EXAMEN").Include("PRESTACION").Include("CARGA_PRESTACIONES_ENCABEZADO") select i;
			
				

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
                throw ex;
			}
		}
	}
}
