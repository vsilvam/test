using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_VETERINARIAS_EXAMEN
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_VETERINARIAS_EXAMEN()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
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

		public List<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
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

		public CARGA_PRESTACIONES_VETERINARIAS_EXAMEN GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
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

		public CARGA_PRESTACIONES_VETERINARIAS_EXAMEN GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
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
	 	
		public List<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetByFilter(int? CARGA_PRESTACIONES_VETERINARIAS_DETALLEId = null, bool? ACTIVO = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
                    return repositorio.GetByFilter(CARGA_PRESTACIONES_VETERINARIAS_DETALLEId, ACTIVO, NOMBRE_EXAMEN, VALOR_EXAMEN).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> GetByFilterWithReferences(int? CARGA_PRESTACIONES_VETERINARIAS_DETALLEId = null, bool? ACTIVO = null, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
                    return repositorio.GetByFilterWithReferences(CARGA_PRESTACIONES_VETERINARIAS_DETALLEId, ACTIVO, NOMBRE_EXAMEN, VALOR_EXAMEN).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CARGA_PRESTACIONES_VETERINARIAS_DETALLEId, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "")
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _repositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
					CARGA_PRESTACIONES_VETERINARIAS_DETALLE _objCARGA_PRESTACIONES_VETERINARIAS_DETALLE = _repositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetById(CARGA_PRESTACIONES_VETERINARIAS_DETALLEId);
					if(Equals(_objCARGA_PRESTACIONES_VETERINARIAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_DETALLE con Id =",CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.ToString()));
					}

					CARGA_PRESTACIONES_VETERINARIAS_EXAMEN _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN = new CARGA_PRESTACIONES_VETERINARIAS_EXAMEN();

					//properties

                    _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.NOMBRE_EXAMEN = NOMBRE_EXAMEN;				
                    _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.VALOR_EXAMEN = VALOR_EXAMEN;				
                    _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.CARGA_PRESTACIONES_VETERINARIAS_DETALLE = _objCARGA_PRESTACIONES_VETERINARIAS_DETALLE;
                    
					context.AddObject("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN",_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CARGA_PRESTACIONES_VETERINARIAS_DETALLEId, string NOMBRE_EXAMEN = "", string VALOR_EXAMEN = "")
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
                    CARGA_PRESTACIONES_VETERINARIAS_EXAMEN _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_EXAMEN con Id =",Id.ToString()));
					}
					
					RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _repositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
					CARGA_PRESTACIONES_VETERINARIAS_DETALLE _objCARGA_PRESTACIONES_VETERINARIAS_DETALLE = _repositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetById(CARGA_PRESTACIONES_VETERINARIAS_DETALLEId);
					if(Equals(_objCARGA_PRESTACIONES_VETERINARIAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_DETALLE con Id =",CARGA_PRESTACIONES_VETERINARIAS_DETALLEId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(NOMBRE_EXAMEN))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.NOMBRE_EXAMEN = NOMBRE_EXAMEN;
					}
					if (!string.IsNullOrEmpty(VALOR_EXAMEN))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.VALOR_EXAMEN = VALOR_EXAMEN;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.CARGA_PRESTACIONES_VETERINARIAS_DETALLE = _objCARGA_PRESTACIONES_VETERINARIAS_DETALLE;

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
					RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(context);
					CARGA_PRESTACIONES_VETERINARIAS_EXAMEN _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_EXAMEN con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.ACTIVO = false;

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
