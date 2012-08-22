using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCONVENIO_TARIFARIO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCONVENIO_TARIFARIO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CONVENIO_TARIFARIO GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO_TARIFARIO.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CONVENIO_TARIFARIO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO_TARIFARIO.Include("CONVENIO").Include("CONVENIO_EXAMEN").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_TARIFARIO> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_TARIFARIO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_TARIFARIO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_TARIFARIO.Include("CONVENIO").Include("CONVENIO_EXAMEN") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_TARIFARIO> GetByFilter(int? CONVENIOId = null, System.DateTime? FECHA_VIGENCIA = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_TARIFARIO select i;

				if (FECHA_VIGENCIA.HasValue)
				{
				  q = q.Where(i => i.FECHA_VIGENCIA == FECHA_VIGENCIA.Value);
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CONVENIOId.HasValue)
				{
				  q = q.Where(i => i.CONVENIO.ID == CONVENIOId.Value);
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

		public IQueryable<CONVENIO_TARIFARIO> GetByFilterWithReferences(int? CONVENIOId = null, System.DateTime? FECHA_VIGENCIA = null, bool? ACTIVO = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_TARIFARIO.Include("CONVENIO").Include("CONVENIO_EXAMEN") select i;

				if (FECHA_VIGENCIA.HasValue)
				{
					q = q.Where(i => i.FECHA_VIGENCIA == FECHA_VIGENCIA.Value);
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (CONVENIOId.HasValue)
				{
					q = q.Where(i => i.CONVENIO.ID == CONVENIOId.Value);
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
