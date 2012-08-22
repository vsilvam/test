using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCONVENIO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCONVENIO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CONVENIO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetAll().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public List<CONVENIO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetAllWithReferences().OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public CONVENIO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetById(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

		public CONVENIO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetByIdWithReferences(ID);
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }
	 	
		public List<CONVENIO> GetByFilter(int? TIPO_PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetByFilter(TIPO_PRESTACIONId, NOMBRE, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CONVENIO> GetByFilterWithReferences(int? TIPO_PRESTACIONId = null, string NOMBRE = "", bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    return repositorio.GetByFilterWithReferences(TIPO_PRESTACIONId, NOMBRE, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int TIPO_PRESTACIONId, string NOMBRE)
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

					CONVENIO _CONVENIO = new CONVENIO();

					//properties

                    _CONVENIO.NOMBRE = NOMBRE;				
                    _CONVENIO.ACTIVO = true;				

					//parents
						 
                    _CONVENIO.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    
					context.AddObject("CONVENIO",_CONVENIO);
                    context.SaveChanges();

					return _CONVENIO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int TIPO_PRESTACIONId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
                    CONVENIO _CONVENIO = repositorio.GetById(Id);
                    if(Equals(_CONVENIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",Id.ToString()));
					}
					
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CONVENIO.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _CONVENIO.TIPO_PRESTACION = _objTIPO_PRESTACION;

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
					RepositorioCONVENIO repositorio = new RepositorioCONVENIO(context);
					CONVENIO _CONVENIO = repositorio.GetById(Id); 
					
					if(Equals(_CONVENIO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",Id.ToString()));
					}

					_CONVENIO.ACTIVO = false;

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
