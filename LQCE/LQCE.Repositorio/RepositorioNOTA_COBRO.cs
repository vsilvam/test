using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioNOTA_COBRO
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioNOTA_COBRO(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public NOTA_COBRO GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.NOTA_COBRO.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public NOTA_COBRO GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.NOTA_COBRO.Include("COBRO").Include("NOTA_COBRO_DETALLE").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.NOTA_COBRO where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.NOTA_COBRO.Include("COBRO").Include("NOTA_COBRO_DETALLE") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<NOTA_COBRO> GetByFilter(int? COBROId = null, int? CORRELATIVO = null, int? ID_CLIENTE = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.NOTA_COBRO  where i.ACTIVO  select i;
			
				

				if (CORRELATIVO.HasValue)
				{
				  q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
				}
				if (ID_CLIENTE.HasValue)
				{
				  q = q.Where(i => i.ID_CLIENTE == ID_CLIENTE.Value);
				}
				if (COBROId.HasValue)
				{
				  q = q.Where(i => i.COBRO.ID == COBROId.Value);
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

		public IQueryable<NOTA_COBRO> GetByFilterWithReferences(int? COBROId = null, int? CORRELATIVO = null, int? ID_CLIENTE = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.NOTA_COBRO.Include("COBRO").Include("NOTA_COBRO_DETALLE")  where i.ACTIVO select i;
			
				

				if (CORRELATIVO.HasValue)
				{
					q = q.Where(i => i.CORRELATIVO == CORRELATIVO.Value);
				}
				if (ID_CLIENTE.HasValue)
				{
					q = q.Where(i => i.ID_CLIENTE == ID_CLIENTE.Value);
				}
				if (COBROId.HasValue)
				{
					q = q.Where(i => i.COBRO.ID == COBROId.Value);
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
