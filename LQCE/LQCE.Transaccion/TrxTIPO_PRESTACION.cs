using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxTIPO_PRESTACION
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxTIPO_PRESTACION()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<TIPO_PRESTACION> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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

		public List<TIPO_PRESTACION> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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

		public TIPO_PRESTACION GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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

		public TIPO_PRESTACION GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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
	 	
		public List<TIPO_PRESTACION> GetByFilter(string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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

		public List<TIPO_PRESTACION> GetByFilterWithReferences(string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
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
					TIPO_PRESTACION _TIPO_PRESTACION = new TIPO_PRESTACION();

					//properties

                    _TIPO_PRESTACION.NOMBRE = NOMBRE;				
                    _TIPO_PRESTACION.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("TIPO_PRESTACION",_TIPO_PRESTACION);
                    context.SaveChanges();

					return _TIPO_PRESTACION.ID;
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
                    RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
                    TIPO_PRESTACION _TIPO_PRESTACION = repositorio.GetById(Id);
                    if(Equals(_TIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",Id.ToString()));
					}

					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_TIPO_PRESTACION.NOMBRE = NOMBRE;
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
					RepositorioTIPO_PRESTACION repositorio = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _TIPO_PRESTACION = repositorio.GetById(Id); 
					
					if(Equals(_TIPO_PRESTACION ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",Id.ToString()));
					}

					_TIPO_PRESTACION.ACTIVO = false;

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
