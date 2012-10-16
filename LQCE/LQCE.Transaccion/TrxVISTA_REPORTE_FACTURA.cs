using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxVISTA_REPORTE_FACTURA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxVISTA_REPORTE_FACTURA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<VISTA_REPORTE_FACTURA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
										return repositorio.GetAll().OrderBy(i => i.ID_FACTURA).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public List<VISTA_REPORTE_FACTURA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
                    					return repositorio.GetAllWithReferences().OrderBy(i => i.ID_FACTURA).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public VISTA_REPORTE_FACTURA GetById(int ID_FACTURA)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
                    return repositorio.GetById(ID_FACTURA);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public VISTA_REPORTE_FACTURA GetByIdWithReferences(int ID_FACTURA)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
                    return repositorio.GetByIdWithReferences(ID_FACTURA);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }
	 	
		public List<VISTA_REPORTE_FACTURA> GetByFilter(int? ID_FACTURACION = null, System.DateTime? FECHA_FACTURACION = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, bool? PAGADA = null, int? ID_TIPO_FACTURA = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", int? VALOR_PAGADO = null, int? PAGOS_REGISTRADOS = null, int? SALDO_DEUDOR = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
                    return repositorio.GetByFilter(ID_FACTURACION, FECHA_FACTURACION, RUT_LABORATORIO, NETO, IVA, TOTAL, PAGADA, ID_TIPO_FACTURA, NOMBRE_CLIENTE, RUT_CLIENTE, DIRECCION, NOMBRE_COMUNA, NUMERO_FACTURA, DESCUENTO, FONO, GIRO, DETALLE, VALOR_PAGADO, PAGOS_REGISTRADOS, SALDO_DEUDOR).OrderBy(i => i.ID_FACTURA).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<VISTA_REPORTE_FACTURA> GetByFilterWithReferences(int? ID_FACTURACION = null, System.DateTime? FECHA_FACTURACION = null, string RUT_LABORATORIO = "", int? NETO = null, int? IVA = null, int? TOTAL = null, bool? PAGADA = null, int? ID_TIPO_FACTURA = null, string NOMBRE_CLIENTE = "", string RUT_CLIENTE = "", string DIRECCION = "", string NOMBRE_COMUNA = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null, string FONO = "", string GIRO = "", string DETALLE = "", int? VALOR_PAGADO = null, int? PAGOS_REGISTRADOS = null, int? SALDO_DEUDOR = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA repositorio = new RepositorioVISTA_REPORTE_FACTURA(context);
                    return repositorio.GetByFilterWithReferences(ID_FACTURACION, FECHA_FACTURACION, RUT_LABORATORIO, NETO, IVA, TOTAL, PAGADA, ID_TIPO_FACTURA, NOMBRE_CLIENTE, RUT_CLIENTE, DIRECCION, NOMBRE_COMUNA, NUMERO_FACTURA, DESCUENTO, FONO, GIRO, DETALLE, VALOR_PAGADO, PAGOS_REGISTRADOS, SALDO_DEUDOR).OrderBy(i => i.ID_FACTURA).ToList();
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
