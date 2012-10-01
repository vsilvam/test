using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxEXAMEN_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxEXAMEN_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<EXAMEN_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
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

		public List<EXAMEN_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
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

		public EXAMEN_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
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

		public EXAMEN_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
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
	 	
		public List<EXAMEN_DETALLE> GetByFilter(int? EXAMENId = null, int? EXAMEN1Id = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
                    return repositorio.GetByFilter(EXAMENId, EXAMEN1Id).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<EXAMEN_DETALLE> GetByFilterWithReferences(int? EXAMENId = null, int? EXAMEN1Id = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(EXAMENId, EXAMEN1Id).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int EXAMENId, int EXAMEN1Id)
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

					RepositorioEXAMEN _repositorioEXAMEN1 = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN1 = _repositorioEXAMEN1.GetById(EXAMEN1Id);
					if(Equals(_objEXAMEN1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN1 con Id =",EXAMEN1Id.ToString()));
					}

					EXAMEN_DETALLE _EXAMEN_DETALLE = new EXAMEN_DETALLE();

					//properties

                    _EXAMEN_DETALLE.ACTIVO = true;				

					//parents
						 
                    _EXAMEN_DETALLE.EXAMEN = _objEXAMEN;
                    _EXAMEN_DETALLE.EXAMEN1 = _objEXAMEN1;
                    
					context.AddObject("EXAMEN_DETALLE",_EXAMEN_DETALLE);
                    context.SaveChanges();

					return _EXAMEN_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int EXAMENId, int EXAMEN1Id)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
                    EXAMEN_DETALLE _EXAMEN_DETALLE = repositorio.GetById(Id);
                    if(Equals(_EXAMEN_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN_DETALLE con Id =",Id.ToString()));
					}
					
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}
						
					RepositorioEXAMEN _repositorioEXAMEN1 = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN1 = _repositorioEXAMEN1.GetById(EXAMEN1Id);
					if(Equals(_objEXAMEN1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN1 con Id =",EXAMEN1Id.ToString()));
					}
	
					//properties

	
					//parents
					 
                    _EXAMEN_DETALLE.EXAMEN = _objEXAMEN;
                    _EXAMEN_DETALLE.EXAMEN1 = _objEXAMEN1;

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
					RepositorioEXAMEN_DETALLE repositorio = new RepositorioEXAMEN_DETALLE(context);
					EXAMEN_DETALLE _EXAMEN_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_EXAMEN_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN_DETALLE con Id =",Id.ToString()));
					}

					_EXAMEN_DETALLE.ACTIVO = false;

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
