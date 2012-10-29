using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_HUMANAS_EXAMEN
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_HUMANAS_EXAMEN()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
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

		public List<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
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

		public CARGA_PRESTACIONES_HUMANAS_EXAMEN GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
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

		public CARGA_PRESTACIONES_HUMANAS_EXAMEN GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
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
	 	
		public List<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetByFilter(int? EXAMENId = null, int? CARGA_PRESTACIONES_HUMANAS_DETALLEId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
                    return repositorio.GetByFilter(EXAMENId, CARGA_PRESTACIONES_HUMANAS_DETALLEId, FECHA_ACTUALIZACION, NOMBRE_EXAMEN, VALOR_EXAMEN, VALOR_VALOR_EXAMEN).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<CARGA_PRESTACIONES_HUMANAS_EXAMEN> GetByFilterWithReferences(int? EXAMENId = null, int? CARGA_PRESTACIONES_HUMANAS_DETALLEId = null, System.DateTime? FECHA_ACTUALIZACION = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
                    return repositorio.GetByFilterWithReferences(EXAMENId, CARGA_PRESTACIONES_HUMANAS_DETALLEId, FECHA_ACTUALIZACION, NOMBRE_EXAMEN, VALOR_EXAMEN, VALOR_VALOR_EXAMEN).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int EXAMENId, int CARGA_PRESTACIONES_HUMANAS_DETALLEId, System.DateTime FECHA_ACTUALIZACION, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
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

					RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _repositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
					CARGA_PRESTACIONES_HUMANAS_DETALLE _objCARGA_PRESTACIONES_HUMANAS_DETALLE = _repositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetById(CARGA_PRESTACIONES_HUMANAS_DETALLEId);
					if(Equals(_objCARGA_PRESTACIONES_HUMANAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_DETALLE con Id =",CARGA_PRESTACIONES_HUMANAS_DETALLEId.ToString()));
					}

					CARGA_PRESTACIONES_HUMANAS_EXAMEN _CARGA_PRESTACIONES_HUMANAS_EXAMEN = new CARGA_PRESTACIONES_HUMANAS_EXAMEN();

					//properties

                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.NOMBRE_EXAMEN = NOMBRE_EXAMEN;				
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.VALOR_EXAMEN = VALOR_EXAMEN;				
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.FECHA_ACTUALIZACION = FECHA_ACTUALIZACION;
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.VALOR_VALOR_EXAMEN = VALOR_VALOR_EXAMEN;
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.EXAMEN = _objEXAMEN;
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.CARGA_PRESTACIONES_HUMANAS_DETALLE = _objCARGA_PRESTACIONES_HUMANAS_DETALLE;
                    
					context.AddObject("CARGA_PRESTACIONES_HUMANAS_EXAMEN",_CARGA_PRESTACIONES_HUMANAS_EXAMEN);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_HUMANAS_EXAMEN.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int EXAMENId, int CARGA_PRESTACIONES_HUMANAS_DETALLEId, System.DateTime FECHA_ACTUALIZACION, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "", int? VALOR_VALOR_EXAMEN = null)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
                    CARGA_PRESTACIONES_HUMANAS_EXAMEN _CARGA_PRESTACIONES_HUMANAS_EXAMEN = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_HUMANAS_EXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_EXAMEN con Id =",Id.ToString()));
					}
					
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}
						
					RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _repositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
					CARGA_PRESTACIONES_HUMANAS_DETALLE _objCARGA_PRESTACIONES_HUMANAS_DETALLE = _repositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetById(CARGA_PRESTACIONES_HUMANAS_DETALLEId);
					if(Equals(_objCARGA_PRESTACIONES_HUMANAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_DETALLE con Id =",CARGA_PRESTACIONES_HUMANAS_DETALLEId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE_EXAMEN))
					{
						_CARGA_PRESTACIONES_HUMANAS_EXAMEN.NOMBRE_EXAMEN = NOMBRE_EXAMEN;
					}
					if (!string.IsNullOrEmpty(VALOR_EXAMEN))
					{
						_CARGA_PRESTACIONES_HUMANAS_EXAMEN.VALOR_EXAMEN = VALOR_EXAMEN;
					}
						_CARGA_PRESTACIONES_HUMANAS_EXAMEN.FECHA_ACTUALIZACION = FECHA_ACTUALIZACION;
					if (VALOR_VALOR_EXAMEN.HasValue)
					{
						_CARGA_PRESTACIONES_HUMANAS_EXAMEN.VALOR_VALOR_EXAMEN = VALOR_VALOR_EXAMEN.Value;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.EXAMEN = _objEXAMEN;
                    _CARGA_PRESTACIONES_HUMANAS_EXAMEN.CARGA_PRESTACIONES_HUMANAS_DETALLE = _objCARGA_PRESTACIONES_HUMANAS_DETALLE;

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
					RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_EXAMEN(context);
					CARGA_PRESTACIONES_HUMANAS_EXAMEN _CARGA_PRESTACIONES_HUMANAS_EXAMEN = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_HUMANAS_EXAMEN ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_EXAMEN con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_HUMANAS_EXAMEN.ACTIVO = false;

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
