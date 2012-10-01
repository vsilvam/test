using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxVISTA_FACTURAS_POR_NOTIFICAR
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxVISTA_FACTURAS_POR_NOTIFICAR()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<VISTA_FACTURAS_POR_NOTIFICAR> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
					                    return repositorio.GetAll().OrderBy(i => i.NOMBRE).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public List<VISTA_FACTURAS_POR_NOTIFICAR> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
                                        return repositorio.GetAllWithReferences().OrderBy(i => i.NOMBRE).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public VISTA_FACTURAS_POR_NOTIFICAR GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
                    return repositorio.GetById(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public VISTA_FACTURAS_POR_NOTIFICAR GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
                    return repositorio.GetByIdWithReferences(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }
	 	
		public List<VISTA_FACTURAS_POR_NOTIFICAR> GetByFilter(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", System.DateTime? FECHA_FACTURACION = null, int? CONTADOR_NOTAS_COBRO = null, int? NUMERO_FACTURA = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
                    return repositorio.GetByFilter(ID_CLIENTE, RUT, NOMBRE, FECHA_FACTURACION, CONTADOR_NOTAS_COBRO, NUMERO_FACTURA).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<VISTA_FACTURAS_POR_NOTIFICAR> GetByFilterWithReferences(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", System.DateTime? FECHA_FACTURACION = null, int? CONTADOR_NOTAS_COBRO = null, int? NUMERO_FACTURA = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_FACTURAS_POR_NOTIFICAR repositorio = new RepositorioVISTA_FACTURAS_POR_NOTIFICAR(context);
                    return repositorio.GetByFilterWithReferences(ID_CLIENTE, RUT, NOMBRE, FECHA_FACTURACION, CONTADOR_NOTAS_COBRO, NUMERO_FACTURA).OrderBy(i => i.ID).ToList();
                }
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
