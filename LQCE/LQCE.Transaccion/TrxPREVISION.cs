using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPREVISION
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPREVISION()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PREVISION> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
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

		public List<PREVISION> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
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

		public PREVISION GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
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

		public PREVISION GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
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
	 	
		public List<PREVISION> GetByFilter(string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
                    return repositorio.GetByFilter(NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<PREVISION> GetByFilterWithReferences(string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
                    return repositorio.GetByFilterWithReferences(NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					PREVISION _PREVISION = new PREVISION();

					//properties

                    _PREVISION.NOMBRE = NOMBRE;				
                    _PREVISION.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("PREVISION",_PREVISION);
                    context.SaveChanges();

					return _PREVISION.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
                    PREVISION _PREVISION = repositorio.GetById(Id);
                    if(Equals(_PREVISION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",Id.ToString()));
					}

					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_PREVISION.NOMBRE = NOMBRE;
					}
	
					//parents
					 

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
					RepositorioPREVISION repositorio = new RepositorioPREVISION(context);
					PREVISION _PREVISION = repositorio.GetById(Id); 
					
					if(Equals(_PREVISION ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",Id.ToString()));
					}

					_PREVISION.ACTIVO = false;

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
