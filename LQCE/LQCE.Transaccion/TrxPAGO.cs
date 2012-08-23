using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPAGO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPAGO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PAGO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
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

		public List<PAGO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
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

		public PAGO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
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

		public PAGO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
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
	 	
		public List<PAGO> GetByFilter(int? CLIENTEId = null, int? FECHA_PAGO = null, int? MONTO_PAGO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
                    return repositorio.GetByFilter(CLIENTEId, FECHA_PAGO, MONTO_PAGO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<PAGO> GetByFilterWithReferences(int? CLIENTEId = null, int? FECHA_PAGO = null, int? MONTO_PAGO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
                    return repositorio.GetByFilterWithReferences(CLIENTEId, FECHA_PAGO, MONTO_PAGO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CLIENTEId, int FECHA_PAGO, int MONTO_PAGO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}

					PAGO _PAGO = new PAGO();

					//properties

                    _PAGO.FECHA_PAGO = FECHA_PAGO;
                    _PAGO.MONTO_PAGO = MONTO_PAGO;
                    _PAGO.ACTIVO = true;				

					//parents
						 
                    _PAGO.CLIENTE = _objCLIENTE;
                    
					context.AddObject("PAGO",_PAGO);
                    context.SaveChanges();

					return _PAGO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CLIENTEId, int FECHA_PAGO, int MONTO_PAGO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPAGO repositorio = new RepositorioPAGO(context);
                    PAGO _PAGO = repositorio.GetById(Id);
                    if(Equals(_PAGO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO con Id =",Id.ToString()));
					}
					
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
	
					//properties

						_PAGO.FECHA_PAGO = FECHA_PAGO;
						_PAGO.MONTO_PAGO = MONTO_PAGO;
	
					//parents
					 
                    _PAGO.CLIENTE = _objCLIENTE;

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
					RepositorioPAGO repositorio = new RepositorioPAGO(context);
					PAGO _PAGO = repositorio.GetById(Id); 
					
					if(Equals(_PAGO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PAGO con Id =",Id.ToString()));
					}

					_PAGO.ACTIVO = false;

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
