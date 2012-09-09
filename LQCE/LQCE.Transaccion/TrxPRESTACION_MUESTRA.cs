using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION_MUESTRA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION_MUESTRA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PRESTACION_MUESTRA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
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

		public List<PRESTACION_MUESTRA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
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

		public PRESTACION_MUESTRA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
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

		public PRESTACION_MUESTRA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
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
	 	
		public List<PRESTACION_MUESTRA> GetByFilter(int? PRESTACIONId = null, string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
                    return repositorio.GetByFilter(PRESTACIONId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<PRESTACION_MUESTRA> GetByFilterWithReferences(int? PRESTACIONId = null, string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
                    return repositorio.GetByFilterWithReferences(PRESTACIONId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int PRESTACIONId, string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}

					PRESTACION_MUESTRA _PRESTACION_MUESTRA = new PRESTACION_MUESTRA();

					//properties

                    _PRESTACION_MUESTRA.NOMBRE = NOMBRE;				
                    _PRESTACION_MUESTRA.ACTIVO = true;				

					//parents
						 
                    _PRESTACION_MUESTRA.PRESTACION = _objPRESTACION;
                    
					context.AddObject("PRESTACION_MUESTRA",_PRESTACION_MUESTRA);
                    context.SaveChanges();

					return _PRESTACION_MUESTRA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int PRESTACIONId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
                    PRESTACION_MUESTRA _PRESTACION_MUESTRA = repositorio.GetById(Id);
                    if(Equals(_PRESTACION_MUESTRA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_MUESTRA con Id =",Id.ToString()));
					}
					
					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_PRESTACION_MUESTRA.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _PRESTACION_MUESTRA.PRESTACION = _objPRESTACION;

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
					RepositorioPRESTACION_MUESTRA repositorio = new RepositorioPRESTACION_MUESTRA(context);
					PRESTACION_MUESTRA _PRESTACION_MUESTRA = repositorio.GetById(Id); 
					
					if(Equals(_PRESTACION_MUESTRA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_MUESTRA con Id =",Id.ToString()));
					}

					_PRESTACION_MUESTRA.ACTIVO = false;

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
