using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxNOTA_COBRO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxNOTA_COBRO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<NOTA_COBRO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
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

		public List<NOTA_COBRO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
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

		public NOTA_COBRO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
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

		public NOTA_COBRO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
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
	 	
		public List<NOTA_COBRO> GetByFilter(int? CLIENTEId = null, int? COBROId = null, int? CORRELATIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
                    return repositorio.GetByFilter(CLIENTEId, COBROId, CORRELATIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<NOTA_COBRO> GetByFilterWithReferences(int? CLIENTEId = null, int? COBROId = null, int? CORRELATIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
                    return repositorio.GetByFilterWithReferences(CLIENTEId, COBROId, CORRELATIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int CLIENTEId, int COBROId, int CORRELATIVO)
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

					RepositorioCOBRO _repositorioCOBRO = new RepositorioCOBRO(context);
					COBRO _objCOBRO = _repositorioCOBRO.GetById(COBROId);
					if(Equals(_objCOBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COBRO con Id =",COBROId.ToString()));
					}

					NOTA_COBRO _NOTA_COBRO = new NOTA_COBRO();

					//properties

                    _NOTA_COBRO.CORRELATIVO = CORRELATIVO;
                    _NOTA_COBRO.ACTIVO = true;				

					//parents
						 
                    _NOTA_COBRO.CLIENTE = _objCLIENTE;
                    _NOTA_COBRO.COBRO = _objCOBRO;
                    
					context.AddObject("NOTA_COBRO",_NOTA_COBRO);
                    context.SaveChanges();

					return _NOTA_COBRO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CLIENTEId, int COBROId, int CORRELATIVO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
                    NOTA_COBRO _NOTA_COBRO = repositorio.GetById(Id);
                    if(Equals(_NOTA_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO con Id =",Id.ToString()));
					}
					
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
						
					RepositorioCOBRO _repositorioCOBRO = new RepositorioCOBRO(context);
					COBRO _objCOBRO = _repositorioCOBRO.GetById(COBROId);
					if(Equals(_objCOBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COBRO con Id =",COBROId.ToString()));
					}
	
					//properties

						_NOTA_COBRO.CORRELATIVO = CORRELATIVO;
	
					//parents
					 
                    _NOTA_COBRO.CLIENTE = _objCLIENTE;
                    _NOTA_COBRO.COBRO = _objCOBRO;

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
					RepositorioNOTA_COBRO repositorio = new RepositorioNOTA_COBRO(context);
					NOTA_COBRO _NOTA_COBRO = repositorio.GetById(Id); 
					
					if(Equals(_NOTA_COBRO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO con Id =",Id.ToString()));
					}

					_NOTA_COBRO.ACTIVO = false;

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
