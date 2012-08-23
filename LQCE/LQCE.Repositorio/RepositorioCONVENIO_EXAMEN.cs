using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCONVENIO_EXAMEN
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCONVENIO_EXAMEN(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CONVENIO_EXAMEN GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO_EXAMEN.FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CONVENIO_EXAMEN GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CONVENIO_EXAMEN.Include("CONVENIO_TARIFARIO").Include("EXAMEN").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_EXAMEN> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_EXAMEN  where i.ACTIVO select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_EXAMEN> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_EXAMEN.Include("CONVENIO_TARIFARIO").Include("EXAMEN") where i.ACTIVO  select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CONVENIO_EXAMEN> GetByFilter(int? CONVENIO_TARIFARIOId = null, int? EXAMENId = null, int? VALOR = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_EXAMEN  where i.ACTIVO  select i;

				if (VALOR.HasValue)
				{
				  q = q.Where(i => i.VALOR == VALOR.Value);
				}
				if (CONVENIO_TARIFARIOId.HasValue)
				{
				  q = q.Where(i => i.CONVENIO_TARIFARIO.ID == CONVENIO_TARIFARIOId.Value);
				}
				if (EXAMENId.HasValue)
				{
				  q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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

		public IQueryable<CONVENIO_EXAMEN> GetByFilterWithReferences(int? CONVENIO_TARIFARIOId = null, int? EXAMENId = null, int? VALOR = null)
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CONVENIO_EXAMEN.Include("CONVENIO_TARIFARIO").Include("EXAMEN")  where i.ACTIVO select i;

				if (VALOR.HasValue)
				{
					q = q.Where(i => i.VALOR == VALOR.Value);
				}
				if (CONVENIO_TARIFARIOId.HasValue)
				{
					q = q.Where(i => i.CONVENIO_TARIFARIO.ID == CONVENIO_TARIFARIOId.Value);
				}
				if (EXAMENId.HasValue)
				{
					q = q.Where(i => i.EXAMEN.ID == EXAMENId.Value);
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
