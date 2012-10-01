using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION_EXAMEN
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION_EXAMEN()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PRESTACION_EXAMEN> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
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

		public List<PRESTACION_EXAMEN> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
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

		public PRESTACION_EXAMEN GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
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

		public PRESTACION_EXAMEN GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
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
	 	
		public List<PRESTACION_EXAMEN> GetByFilter(int? EXAMENId = null, int? PRESTACIONId = null, int? VALOR = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
                    return repositorio.GetByFilter(EXAMENId, PRESTACIONId, VALOR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<PRESTACION_EXAMEN> GetByFilterWithReferences(int? EXAMENId = null, int? PRESTACIONId = null, int? VALOR = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
                    return repositorio.GetByFilterWithReferences(EXAMENId, PRESTACIONId, VALOR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int EXAMENId, int PRESTACIONId, int? VALOR = null)
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

					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}

					PRESTACION_EXAMEN _PRESTACION_EXAMEN = new PRESTACION_EXAMEN();

					//properties

                    _PRESTACION_EXAMEN.VALOR = VALOR;
                    _PRESTACION_EXAMEN.ACTIVO = true;				

					//parents
						 
                    _PRESTACION_EXAMEN.EXAMEN = _objEXAMEN;
                    _PRESTACION_EXAMEN.PRESTACION = _objPRESTACION;
                    
					context.AddObject("PRESTACION_EXAMEN",_PRESTACION_EXAMEN);
                    context.SaveChanges();

					return _PRESTACION_EXAMEN.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int EXAMENId, int PRESTACIONId, int? VALOR = null)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
                    PRESTACION_EXAMEN _PRESTACION_EXAMEN = repositorio.GetById(Id);
                    if(Equals(_PRESTACION_EXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_EXAMEN con Id =",Id.ToString()));
					}
					
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}
						
					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}
	
					//properties

					if (VALOR.HasValue)
					{
						_PRESTACION_EXAMEN.VALOR = VALOR.Value;
					}
	
					//parents
					 
                    _PRESTACION_EXAMEN.EXAMEN = _objEXAMEN;
                    _PRESTACION_EXAMEN.PRESTACION = _objPRESTACION;

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
					RepositorioPRESTACION_EXAMEN repositorio = new RepositorioPRESTACION_EXAMEN(context);
					PRESTACION_EXAMEN _PRESTACION_EXAMEN = repositorio.GetById(Id); 
					
					if(Equals(_PRESTACION_EXAMEN ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_EXAMEN con Id =",Id.ToString()));
					}

					_PRESTACION_EXAMEN.ACTIVO = false;

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
