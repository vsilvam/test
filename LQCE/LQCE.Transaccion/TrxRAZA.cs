using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxRAZA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxRAZA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<RAZA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
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

		public List<RAZA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
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

		public RAZA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
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

		public RAZA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
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
	 	
		public List<RAZA> GetByFilter(int? ESPECIEId = null, string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
                    return repositorio.GetByFilter(ESPECIEId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<RAZA> GetByFilterWithReferences(int? ESPECIEId = null, string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
                    return repositorio.GetByFilterWithReferences(ESPECIEId, NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int ESPECIEId, string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioESPECIE _repositorioESPECIE = new RepositorioESPECIE(context);
					ESPECIE _objESPECIE = _repositorioESPECIE.GetById(ESPECIEId);
					if(Equals(_objESPECIE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado ESPECIE con Id =",ESPECIEId.ToString()));
					}

					RAZA _RAZA = new RAZA();

					//properties

                    _RAZA.NOMBRE = NOMBRE;				
                    _RAZA.ACTIVO = true;				

					//parents
						 
                    _RAZA.ESPECIE = _objESPECIE;
                    
					context.AddObject("RAZA",_RAZA);
                    context.SaveChanges();

					return _RAZA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int ESPECIEId, string NOMBRE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioRAZA repositorio = new RepositorioRAZA(context);
                    RAZA _RAZA = repositorio.GetById(Id);
                    if(Equals(_RAZA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA con Id =",Id.ToString()));
					}
					
					RepositorioESPECIE _repositorioESPECIE = new RepositorioESPECIE(context);
					ESPECIE _objESPECIE = _repositorioESPECIE.GetById(ESPECIEId);
					if(Equals(_objESPECIE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado ESPECIE con Id =",ESPECIEId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_RAZA.NOMBRE = NOMBRE;
					}
	
					//parents
					 
                    _RAZA.ESPECIE = _objESPECIE;

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
					RepositorioRAZA repositorio = new RepositorioRAZA(context);
					RAZA _RAZA = repositorio.GetById(Id); 
					
					if(Equals(_RAZA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA con Id =",Id.ToString()));
					}

					_RAZA.ACTIVO = false;

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
