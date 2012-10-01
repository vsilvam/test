using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_ENCABEZADO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_ENCABEZADO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_ENCABEZADO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
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

		public List<CARGA_PRESTACIONES_ENCABEZADO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
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

		public CARGA_PRESTACIONES_ENCABEZADO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
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

		public CARGA_PRESTACIONES_ENCABEZADO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
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
	 	
		public List<CARGA_PRESTACIONES_ENCABEZADO> GetByFilter(int? CARGA_PRESTACIONES_ESTADOId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_CARGA = null, string ARCHIVO = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    return repositorio.GetByFilter(CARGA_PRESTACIONES_ESTADOId, TIPO_PRESTACIONId, FECHA_CARGA, ARCHIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<CARGA_PRESTACIONES_ENCABEZADO> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ESTADOId = null, int? TIPO_PRESTACIONId = null, System.DateTime? FECHA_CARGA = null, string ARCHIVO = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    return repositorio.GetByFilterWithReferences(CARGA_PRESTACIONES_ESTADOId, TIPO_PRESTACIONId, FECHA_CARGA, ARCHIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int CARGA_PRESTACIONES_ESTADOId, int TIPO_PRESTACIONId, System.DateTime FECHA_CARGA, string ARCHIVO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCARGA_PRESTACIONES_ESTADO _repositorioCARGA_PRESTACIONES_ESTADO = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
					CARGA_PRESTACIONES_ESTADO _objCARGA_PRESTACIONES_ESTADO = _repositorioCARGA_PRESTACIONES_ESTADO.GetById(CARGA_PRESTACIONES_ESTADOId);
					if(Equals(_objCARGA_PRESTACIONES_ESTADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ESTADO con Id =",CARGA_PRESTACIONES_ESTADOId.ToString()));
					}

					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}

					CARGA_PRESTACIONES_ENCABEZADO _CARGA_PRESTACIONES_ENCABEZADO = new CARGA_PRESTACIONES_ENCABEZADO();

					//properties

                    _CARGA_PRESTACIONES_ENCABEZADO.FECHA_CARGA = FECHA_CARGA;
                    _CARGA_PRESTACIONES_ENCABEZADO.ARCHIVO = ARCHIVO;				
                    _CARGA_PRESTACIONES_ENCABEZADO.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO = _objCARGA_PRESTACIONES_ESTADO;
                    _CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    
					context.AddObject("CARGA_PRESTACIONES_ENCABEZADO",_CARGA_PRESTACIONES_ENCABEZADO);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_ENCABEZADO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CARGA_PRESTACIONES_ESTADOId, int TIPO_PRESTACIONId, System.DateTime FECHA_CARGA, string ARCHIVO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    CARGA_PRESTACIONES_ENCABEZADO _CARGA_PRESTACIONES_ENCABEZADO = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_ENCABEZADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ENCABEZADO con Id =",Id.ToString()));
					}
					
					RepositorioCARGA_PRESTACIONES_ESTADO _repositorioCARGA_PRESTACIONES_ESTADO = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
					CARGA_PRESTACIONES_ESTADO _objCARGA_PRESTACIONES_ESTADO = _repositorioCARGA_PRESTACIONES_ESTADO.GetById(CARGA_PRESTACIONES_ESTADOId);
					if(Equals(_objCARGA_PRESTACIONES_ESTADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ESTADO con Id =",CARGA_PRESTACIONES_ESTADOId.ToString()));
					}
						
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}
	
					//properties

						_CARGA_PRESTACIONES_ENCABEZADO.FECHA_CARGA = FECHA_CARGA;
					if (!string.IsNullOrEmpty(ARCHIVO))
					{
						_CARGA_PRESTACIONES_ENCABEZADO.ARCHIVO = ARCHIVO;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO = _objCARGA_PRESTACIONES_ESTADO;
                    _CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION = _objTIPO_PRESTACION;

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
					RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
					CARGA_PRESTACIONES_ENCABEZADO _CARGA_PRESTACIONES_ENCABEZADO = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_ENCABEZADO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ENCABEZADO con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_ENCABEZADO.ACTIVO = false;

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
