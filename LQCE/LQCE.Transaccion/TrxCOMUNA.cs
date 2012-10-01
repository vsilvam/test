using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCOMUNA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCOMUNA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<COMUNA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
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

		public List<COMUNA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
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

		public COMUNA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
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

		public COMUNA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
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
	 	
		public List<COMUNA> GetByFilter(int? REGIONId = null, string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
                    return repositorio.GetByFilter(REGIONId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<COMUNA> GetByFilterWithReferences(int? REGIONId = null, string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
                    return repositorio.GetByFilterWithReferences(REGIONId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int REGIONId, string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioREGION _repositorioREGION = new RepositorioREGION(context);
					REGION _objREGION = _repositorioREGION.GetById(REGIONId);
					if(Equals(_objREGION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado REGION con Id =",REGIONId.ToString()));
					}

					COMUNA _COMUNA = new COMUNA();

					//properties

                    _COMUNA.NOMBRE = NOMBRE;				
                    _COMUNA.ACTIVO = true;				

					//parents
						 
                    _COMUNA.REGION = _objREGION;
                    
					context.AddObject("COMUNA",_COMUNA);
                    context.SaveChanges();

					return _COMUNA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int REGIONId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
                    COMUNA _COMUNA = repositorio.GetById(Id);
                    if(Equals(_COMUNA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COMUNA con Id =",Id.ToString()));
					}
					
					RepositorioREGION _repositorioREGION = new RepositorioREGION(context);
					REGION _objREGION = _repositorioREGION.GetById(REGIONId);
					if(Equals(_objREGION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado REGION con Id =",REGIONId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_COMUNA.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _COMUNA.REGION = _objREGION;

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
					RepositorioCOMUNA repositorio = new RepositorioCOMUNA(context);
					COMUNA _COMUNA = repositorio.GetById(Id); 
					
					if(Equals(_COMUNA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COMUNA con Id =",Id.ToString()));
					}

					_COMUNA.ACTIVO = false;

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
