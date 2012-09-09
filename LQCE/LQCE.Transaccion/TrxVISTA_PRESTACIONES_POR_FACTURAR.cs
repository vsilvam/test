using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxVISTA_PRESTACIONES_POR_FACTURAR
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxVISTA_PRESTACIONES_POR_FACTURAR()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<VISTA_PRESTACIONES_POR_FACTURAR> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
                    return repositorio.GetAll().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public List<VISTA_PRESTACIONES_POR_FACTURAR> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
                    return repositorio.GetAllWithReferences().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public VISTA_PRESTACIONES_POR_FACTURAR GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
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

		public VISTA_PRESTACIONES_POR_FACTURAR GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
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
	 	
		public List<VISTA_PRESTACIONES_POR_FACTURAR> GetByFilter(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, System.DateTime? FECHA_RECEPCION = null, int? TOTAL = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
                    return repositorio.GetByFilter(ID_CLIENTE, RUT, NOMBRE, DESCUENTO, FECHA_RECEPCION, TOTAL).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<VISTA_PRESTACIONES_POR_FACTURAR> GetByFilterWithReferences(int? ID_CLIENTE = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, System.DateTime? FECHA_RECEPCION = null, int? TOTAL = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_PRESTACIONES_POR_FACTURAR repositorio = new RepositorioVISTA_PRESTACIONES_POR_FACTURAR(context);
                    return repositorio.GetByFilterWithReferences(ID_CLIENTE, RUT, NOMBRE, DESCUENTO, FECHA_RECEPCION, TOTAL).OrderBy(i => i.ID).ToList();
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
