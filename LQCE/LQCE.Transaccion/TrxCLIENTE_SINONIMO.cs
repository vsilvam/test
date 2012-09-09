using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCLIENTE_SINONIMO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCLIENTE_SINONIMO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CLIENTE_SINONIMO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
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

		public List<CLIENTE_SINONIMO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
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

		public CLIENTE_SINONIMO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
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

		public CLIENTE_SINONIMO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
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
	 	
		public List<CLIENTE_SINONIMO> GetByFilter(int? CLIENTEId = null, string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
                    return repositorio.GetByFilter(CLIENTEId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<CLIENTE_SINONIMO> GetByFilterWithReferences(int? CLIENTEId = null, string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
                    return repositorio.GetByFilterWithReferences(CLIENTEId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int CLIENTEId, string NOMBRE)
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

					CLIENTE_SINONIMO _CLIENTE_SINONIMO = new CLIENTE_SINONIMO();

					//properties

                    _CLIENTE_SINONIMO.NOMBRE = NOMBRE;				
                    _CLIENTE_SINONIMO.ACTIVO = true;				

					//parents
						 
                    _CLIENTE_SINONIMO.CLIENTE = _objCLIENTE;
                    
					context.AddObject("CLIENTE_SINONIMO",_CLIENTE_SINONIMO);
                    context.SaveChanges();

					return _CLIENTE_SINONIMO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CLIENTEId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
                    CLIENTE_SINONIMO _CLIENTE_SINONIMO = repositorio.GetById(Id);
                    if(Equals(_CLIENTE_SINONIMO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE_SINONIMO con Id =",Id.ToString()));
					}
					
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CLIENTE_SINONIMO.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _CLIENTE_SINONIMO.CLIENTE = _objCLIENTE;

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
					RepositorioCLIENTE_SINONIMO repositorio = new RepositorioCLIENTE_SINONIMO(context);
					CLIENTE_SINONIMO _CLIENTE_SINONIMO = repositorio.GetById(Id); 
					
					if(Equals(_CLIENTE_SINONIMO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE_SINONIMO con Id =",Id.ToString()));
					}

					_CLIENTE_SINONIMO.ACTIVO = false;

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
