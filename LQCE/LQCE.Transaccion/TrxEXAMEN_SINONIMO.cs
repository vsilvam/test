using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxEXAMEN_SINONIMO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxEXAMEN_SINONIMO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<EXAMEN_SINONIMO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
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

		public List<EXAMEN_SINONIMO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
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

		public EXAMEN_SINONIMO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
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

		public EXAMEN_SINONIMO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
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
	 	
		public List<EXAMEN_SINONIMO> GetByFilter(int? EXAMENId = null, string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
                    return repositorio.GetByFilter(EXAMENId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<EXAMEN_SINONIMO> GetByFilterWithReferences(int? EXAMENId = null, string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
                    return repositorio.GetByFilterWithReferences(EXAMENId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int EXAMENId, string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}

					EXAMEN_SINONIMO _EXAMEN_SINONIMO = new EXAMEN_SINONIMO();

					//properties

                    _EXAMEN_SINONIMO.NOMBRE = NOMBRE;				
                    _EXAMEN_SINONIMO.ACTIVO = true;				

					//parents
						 
                    _EXAMEN_SINONIMO.EXAMEN = _objEXAMEN;
                    
					context.AddObject("EXAMEN_SINONIMO",_EXAMEN_SINONIMO);
                    context.SaveChanges();

					return _EXAMEN_SINONIMO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int EXAMENId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
                    EXAMEN_SINONIMO _EXAMEN_SINONIMO = repositorio.GetById(Id);
                    if(Equals(_EXAMEN_SINONIMO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN_SINONIMO con Id =",Id.ToString()));
					}
					
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_EXAMEN_SINONIMO.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _EXAMEN_SINONIMO.EXAMEN = _objEXAMEN;

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
					RepositorioEXAMEN_SINONIMO repositorio = new RepositorioEXAMEN_SINONIMO(context);
					EXAMEN_SINONIMO _EXAMEN_SINONIMO = repositorio.GetById(Id); 
					
					if(Equals(_EXAMEN_SINONIMO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN_SINONIMO con Id =",Id.ToString()));
					}

					_EXAMEN_SINONIMO.ACTIVO = false;

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
