using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioVISTA_FACTURAS_POR_NOTIFICAR
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioVISTA_FACTURAS_POR_NOTIFICAR(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public VISTA_FACTURAS_POR_NOTIFICAR GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.VISTA_FACTURAS_POR_NOTIFICAR.FirstOrDefault(i => i.ID == id);
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public VISTA_FACTURAS_POR_NOTIFICAR GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.VISTA_FACTURAS_POR_NOTIFICAR.FirstOrDefault(i => i.ID == id);
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<VISTA_FACTURAS_POR_NOTIFICAR> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.VISTA_FACTURAS_POR_NOTIFICAR select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<VISTA_FACTURAS_POR_NOTIFICAR> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.VISTA_FACTURAS_POR_NOTIFICAR select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<VISTA_FACTURAS_POR_NOTIFICAR> GetByFilter(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", System.DateTime? FECHA_FACTURACION = null, int? CONTADOR_NOTAS_COBRO = null, int? NUMERO_FACTURA = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.VISTA_FACTURAS_POR_NOTIFICAR  select i;
			
				

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
				if (FECHA_FACTURACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (NUMERO_FACTURA.HasValue)
				{
				  q = q.Where(i => i.NUMERO_FACTURA == NUMERO_FACTURA.Value);
				}
				if (CONTADOR_NOTAS_COBRO.HasValue)
				{
				  q = q.Where(i => i.CONTADOR_NOTAS_COBRO == CONTADOR_NOTAS_COBRO.Value);
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

		public IQueryable<VISTA_FACTURAS_POR_NOTIFICAR> GetByFilterWithReferences(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", System.DateTime? FECHA_FACTURACION = null, int? CONTADOR_NOTAS_COBRO = null, int? NUMERO_FACTURA = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.VISTA_FACTURAS_POR_NOTIFICAR select i;
			
				

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
				if (FECHA_FACTURACION.HasValue)
				{
					q = q.Where(i => i.FECHA_FACTURACION == FECHA_FACTURACION.Value);
				}
				if (NUMERO_FACTURA.HasValue)
				{
					q = q.Where(i => i.NUMERO_FACTURA == NUMERO_FACTURA.Value);
				}
				if (CONTADOR_NOTAS_COBRO.HasValue)
				{
					q = q.Where(i => i.CONTADOR_NOTAS_COBRO == CONTADOR_NOTAS_COBRO.Value);
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
