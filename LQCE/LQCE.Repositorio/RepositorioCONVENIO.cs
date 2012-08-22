using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCONVENIO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCONVENIO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CONVENIO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CONVENIO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO.Include("CLIENTE").Include("CONVENIO_TARIFARIO").Include("TIPO_PRESTACION").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO.Include("CLIENTE").Include("CONVENIO_TARIFARIO").Include("TIPO_PRESTACION") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO> GetByFilter(int? TIPO_PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (TIPO_PRESTACIONId.HasValue)
				{
				  q = q.Where(i => i.TIPO_PRESTACION.ID == TIPO_PRESTACIONId.Value);
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

		public IQueryable<CONVENIO> GetByFilterWithReferences(int? TIPO_PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO.Include("CLIENTE").Include("CONVENIO_TARIFARIO").Include("TIPO_PRESTACION") select i;

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (TIPO_PRESTACIONId.HasValue)
				{
					q = q.Where(i => i.TIPO_PRESTACION.ID == TIPO_PRESTACIONId.Value);
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