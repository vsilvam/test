using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxEXAMEN
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxEXAMEN()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<EXAMEN> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
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

		public List<EXAMEN> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
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

		public EXAMEN GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
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

		public EXAMEN GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
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
	 	
		public List<EXAMEN> GetByFilter(int? TIPO_PRESTACIONId = null, string CODIGO = "", string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
                    return repositorio.GetByFilter(TIPO_PRESTACIONId, CODIGO, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<EXAMEN> GetByFilterWithReferences(int? TIPO_PRESTACIONId = null, string CODIGO = "", string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
                    return repositorio.GetByFilterWithReferences(TIPO_PRESTACIONId, CODIGO, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int TIPO_PRESTACIONId, string CODIGO, string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}

					EXAMEN _EXAMEN = new EXAMEN();

					//properties

                    _EXAMEN.CODIGO = CODIGO;				
                    _EXAMEN.NOMBRE = NOMBRE;				
                    _EXAMEN.ACTIVO = true;				

					//parents
						 
                    _EXAMEN.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    
					context.AddObject("EXAMEN",_EXAMEN);
                    context.SaveChanges();

					return _EXAMEN.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int TIPO_PRESTACIONId, string CODIGO, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
                    EXAMEN _EXAMEN = repositorio.GetById(Id);
                    if(Equals(_EXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",Id.ToString()));
					}
					
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(CODIGO))
					{
						_EXAMEN.CODIGO = CODIGO;
					}
					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_EXAMEN.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _EXAMEN.TIPO_PRESTACION = _objTIPO_PRESTACION;

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
					RepositorioEXAMEN repositorio = new RepositorioEXAMEN(context);
					EXAMEN _EXAMEN = repositorio.GetById(Id); 
					
					if(Equals(_EXAMEN ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",Id.ToString()));
					}

					_EXAMEN.ACTIVO = false;

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
