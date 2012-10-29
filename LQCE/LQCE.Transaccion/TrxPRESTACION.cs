using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PRESTACION> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
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

		public List<PRESTACION> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
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

		public PRESTACION GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
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

		public PRESTACION GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
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
	 	
		public List<PRESTACION> GetByFilter(int? CLIENTEId = null, int? GARANTIAId = null, int? PREVISIONId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_RECEPCION = null, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
                    return repositorio.GetByFilter(CLIENTEId, GARANTIAId, PREVISIONId, TIPO_PRESTACIONId, FECHA_RECEPCION, MEDICO, RECEPCION, PENDIENTE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<PRESTACION> GetByFilterWithReferences(int? CLIENTEId = null, int? GARANTIAId = null, int? PREVISIONId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_RECEPCION = null, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
                    return repositorio.GetByFilterWithReferences(CLIENTEId, GARANTIAId, PREVISIONId, TIPO_PRESTACIONId, FECHA_RECEPCION, MEDICO, RECEPCION, PENDIENTE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int CLIENTEId, int GARANTIAId, int PREVISIONId, int TIPO_PRESTACIONId, System.DateTime FECHA_RECEPCION, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
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

					RepositorioGARANTIA _repositorioGARANTIA = new RepositorioGARANTIA(context);
					GARANTIA _objGARANTIA = _repositorioGARANTIA.GetById(GARANTIAId);
					if(Equals(_objGARANTIA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado GARANTIA con Id =",GARANTIAId.ToString()));
					}

					RepositorioPREVISION _repositorioPREVISION = new RepositorioPREVISION(context);
					PREVISION _objPREVISION = _repositorioPREVISION.GetById(PREVISIONId);
					if(Equals(_objPREVISION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",PREVISIONId.ToString()));
					}

					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}

					PRESTACION _PRESTACION = new PRESTACION();

					//properties

                    _PRESTACION.MEDICO = MEDICO;				
                    _PRESTACION.FECHA_RECEPCION = FECHA_RECEPCION;
                    _PRESTACION.RECEPCION = RECEPCION;				
                    _PRESTACION.PENDIENTE = PENDIENTE;				
                    _PRESTACION.ACTIVO = true;				

					//parents
						 
                    _PRESTACION.CLIENTE = _objCLIENTE;
                    _PRESTACION.GARANTIA = _objGARANTIA;
                    _PRESTACION.PREVISION = _objPREVISION;
                    _PRESTACION.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    
					context.AddObject("PRESTACION",_PRESTACION);
                    context.SaveChanges();

					return _PRESTACION.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CLIENTEId, int GARANTIAId, int PREVISIONId, int TIPO_PRESTACIONId, System.DateTime FECHA_RECEPCION, string MEDICO = "", string RECEPCION = "", string PENDIENTE = "")
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
                    PRESTACION _PRESTACION = repositorio.GetById(Id);
                    if(Equals(_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",Id.ToString()));
					}
					
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
						
					RepositorioGARANTIA _repositorioGARANTIA = new RepositorioGARANTIA(context);
					GARANTIA _objGARANTIA = _repositorioGARANTIA.GetById(GARANTIAId);
					if(Equals(_objGARANTIA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado GARANTIA con Id =",GARANTIAId.ToString()));
					}
						
					RepositorioPREVISION _repositorioPREVISION = new RepositorioPREVISION(context);
					PREVISION _objPREVISION = _repositorioPREVISION.GetById(PREVISIONId);
					if(Equals(_objPREVISION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",PREVISIONId.ToString()));
					}
						
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(MEDICO))
					{
						_PRESTACION.MEDICO = MEDICO;
					}
						_PRESTACION.FECHA_RECEPCION = FECHA_RECEPCION;
					if (!string.IsNullOrEmpty(RECEPCION))
					{
						_PRESTACION.RECEPCION = RECEPCION;
					}
					if (!string.IsNullOrEmpty(PENDIENTE))
					{
						_PRESTACION.PENDIENTE = PENDIENTE;
					}
	
					//parents
					 
                    _PRESTACION.CLIENTE = _objCLIENTE;
                    _PRESTACION.GARANTIA = _objGARANTIA;
                    _PRESTACION.PREVISION = _objPREVISION;
                    _PRESTACION.TIPO_PRESTACION = _objTIPO_PRESTACION;

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
					RepositorioPRESTACION repositorio = new RepositorioPRESTACION(context);
					PRESTACION _PRESTACION = repositorio.GetById(Id); 
					
					if(Equals(_PRESTACION ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",Id.ToString()));
					}

					_PRESTACION.ACTIVO = false;

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
