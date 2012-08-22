using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPAGO_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPAGO_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PAGO_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetAll().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public List<PAGO_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetAllWithReferences().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public PAGO_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetById(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public PAGO_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetByIdWithReferences(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }
	 	
		public List<PAGO_DETALLE> GetByFilter(int? FACTURA_DETALLEId = null, int? PAGOId = null, int? MONTO = null, bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetByFilter(FACTURA_DETALLEId, PAGOId, MONTO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<PAGO_DETALLE> GetByFilterWithReferences(int? FACTURA_DETALLEId = null, int? PAGOId = null, int? MONTO = null, bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(FACTURA_DETALLEId, PAGOId, MONTO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int FACTURA_DETALLEId, int PAGOId, int MONTO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioFACTURA_DETALLE _repositorioFACTURA_DETALLE = new RepositorioFACTURA_DETALLE(context);
					FACTURA_DETALLE _objFACTURA_DETALLE = _repositorioFACTURA_DETALLE.GetById(FACTURA_DETALLEId);
					if(Equals(_objFACTURA_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA_DETALLE con Id =",FACTURA_DETALLEId.ToString()));
					}

					RepositorioPAGO _repositorioPAGO = new RepositorioPAGO(context);
					PAGO _objPAGO = _repositorioPAGO.GetById(PAGOId);
					if(Equals(_objPAGO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO con Id =",PAGOId.ToString()));
					}

					PAGO_DETALLE _PAGO_DETALLE = new PAGO_DETALLE();

					//properties

                    _PAGO_DETALLE.MONTO = MONTO;
                    _PAGO_DETALLE.ACTIVO = true;				

					//parents
						 
                    _PAGO_DETALLE.FACTURA_DETALLE = _objFACTURA_DETALLE;
                    _PAGO_DETALLE.PAGO = _objPAGO;
                    
					context.AddObject("PAGO_DETALLE",_PAGO_DETALLE);
                    context.SaveChanges();

					return _PAGO_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int FACTURA_DETALLEId, int PAGOId, int MONTO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
                    PAGO_DETALLE _PAGO_DETALLE = repositorio.GetById(Id);
                    if(Equals(_PAGO_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO_DETALLE con Id =",Id.ToString()));
					}
					
					RepositorioFACTURA_DETALLE _repositorioFACTURA_DETALLE = new RepositorioFACTURA_DETALLE(context);
					FACTURA_DETALLE _objFACTURA_DETALLE = _repositorioFACTURA_DETALLE.GetById(FACTURA_DETALLEId);
					if(Equals(_objFACTURA_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA_DETALLE con Id =",FACTURA_DETALLEId.ToString()));
					}
						
					RepositorioPAGO _repositorioPAGO = new RepositorioPAGO(context);
					PAGO _objPAGO = _repositorioPAGO.GetById(PAGOId);
					if(Equals(_objPAGO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO con Id =",PAGOId.ToString()));
					}
	
					//properties

						_PAGO_DETALLE.MONTO = MONTO;
	
					//parents
					 
                    _PAGO_DETALLE.FACTURA_DETALLE = _objFACTURA_DETALLE;
                    _PAGO_DETALLE.PAGO = _objPAGO;

					context.SaveChanges();
				}
			}
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                 throw ex;
			}
		}

		public void Delete (int Id)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioPAGO_DETALLE repositorio = new RepositorioPAGO_DETALLE(context);
					PAGO_DETALLE _PAGO_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_PAGO_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO_DETALLE con Id =",Id.ToString()));
					}

					_PAGO_DETALLE.ACTIVO = false;

					context.SaveChanges();
				}
			}
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                 throw ex;
			}
		}
	}
}
