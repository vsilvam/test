using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION_VETERINARIA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION_VETERINARIA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PRESTACION_VETERINARIA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
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

		public List<PRESTACION_VETERINARIA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
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

		public PRESTACION_VETERINARIA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
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

		public PRESTACION_VETERINARIA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
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
	 	
		public List<PRESTACION_VETERINARIA> GetByFilter(int? ESPECIEId = null, int? RAZAId = null, string NOMBRE = "", string EDAD = "", string TELEFONO = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
                    return repositorio.GetByFilter(ESPECIEId, RAZAId, NOMBRE, EDAD, TELEFONO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<PRESTACION_VETERINARIA> GetByFilterWithReferences(int? ESPECIEId = null, int? RAZAId = null, string NOMBRE = "", string EDAD = "", string TELEFONO = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
                    return repositorio.GetByFilterWithReferences(ESPECIEId, RAZAId, NOMBRE, EDAD, TELEFONO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		        public int Add(int ESPECIEId, int RAZAId, string NOMBRE, string EDAD, string TELEFONO)
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

					RepositorioRAZA _repositorioRAZA = new RepositorioRAZA(context);
					RAZA _objRAZA = _repositorioRAZA.GetById(RAZAId);
					if(Equals(_objRAZA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA con Id =",RAZAId.ToString()));
					}

					PRESTACION_VETERINARIA _PRESTACION_VETERINARIA = new PRESTACION_VETERINARIA();

					//properties

                    _PRESTACION_VETERINARIA.NOMBRE = NOMBRE;				
                    _PRESTACION_VETERINARIA.EDAD = EDAD;				
                    _PRESTACION_VETERINARIA.TELEFONO = TELEFONO;				
                    _PRESTACION_VETERINARIA.ACTIVO = true;				

					//parents
						 
                    _PRESTACION_VETERINARIA.ESPECIE = _objESPECIE;
                    _PRESTACION_VETERINARIA.RAZA = _objRAZA;
                    
					context.AddObject("PRESTACION_VETERINARIA",_PRESTACION_VETERINARIA);
                    context.SaveChanges();

					return _PRESTACION_VETERINARIA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int ESPECIEId, int RAZAId, string NOMBRE, string EDAD, string TELEFONO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
                    PRESTACION_VETERINARIA _PRESTACION_VETERINARIA = repositorio.GetById(Id);
                    if(Equals(_PRESTACION_VETERINARIA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_VETERINARIA con Id =",Id.ToString()));
					}
					
					RepositorioESPECIE _repositorioESPECIE = new RepositorioESPECIE(context);
					ESPECIE _objESPECIE = _repositorioESPECIE.GetById(ESPECIEId);
					if(Equals(_objESPECIE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado ESPECIE con Id =",ESPECIEId.ToString()));
					}
						
					RepositorioRAZA _repositorioRAZA = new RepositorioRAZA(context);
					RAZA _objRAZA = _repositorioRAZA.GetById(RAZAId);
					if(Equals(_objRAZA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA con Id =",RAZAId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_PRESTACION_VETERINARIA.NOMBRE = NOMBRE;
					}
					if (!string.IsNullOrEmpty(EDAD))
					{
						_PRESTACION_VETERINARIA.EDAD = EDAD;
					}
					if (!string.IsNullOrEmpty(TELEFONO))
					{
						_PRESTACION_VETERINARIA.TELEFONO = TELEFONO;
					}
	
					//parents
					 
                    _PRESTACION_VETERINARIA.ESPECIE = _objESPECIE;
                    _PRESTACION_VETERINARIA.RAZA = _objRAZA;

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
					RepositorioPRESTACION_VETERINARIA repositorio = new RepositorioPRESTACION_VETERINARIA(context);
					PRESTACION_VETERINARIA _PRESTACION_VETERINARIA = repositorio.GetById(Id); 
					
					if(Equals(_PRESTACION_VETERINARIA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_VETERINARIA con Id =",Id.ToString()));
					}

					_PRESTACION_VETERINARIA.ACTIVO = false;

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
