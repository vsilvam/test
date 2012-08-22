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
                return null;
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
                return null;
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
                return null;
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
                return null;
            }
        }
	 	
		public List<CARGA_PRESTACIONES_ENCABEZADO> GetByFilter(System.DateTime? FECHA_CARGA = null, int? ID_TIPO_PRESTACION = null, string ARCHIVO = "", bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    return repositorio.GetByFilter(FECHA_CARGA, ID_TIPO_PRESTACION, ARCHIVO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_ENCABEZADO> GetByFilterWithReferences(System.DateTime? FECHA_CARGA = null, int? ID_TIPO_PRESTACION = null, string ARCHIVO = "", bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    return repositorio.GetByFilterWithReferences(FECHA_CARGA, ID_TIPO_PRESTACION, ARCHIVO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(System.DateTime FECHA_CARGA, int ID_TIPO_PRESTACION, string ARCHIVO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					CARGA_PRESTACIONES_ENCABEZADO _CARGA_PRESTACIONES_ENCABEZADO = new CARGA_PRESTACIONES_ENCABEZADO();

					//properties

                    _CARGA_PRESTACIONES_ENCABEZADO.FECHA_CARGA = FECHA_CARGA;
                    _CARGA_PRESTACIONES_ENCABEZADO.ID_TIPO_PRESTACION = ID_TIPO_PRESTACION;
                    _CARGA_PRESTACIONES_ENCABEZADO.ARCHIVO = ARCHIVO;				
                    _CARGA_PRESTACIONES_ENCABEZADO.ACTIVO = true;				

					//parents
						 
                    
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

		public void Update(int Id, System.DateTime FECHA_CARGA, int ID_TIPO_PRESTACION, string ARCHIVO)
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

					//properties

						_CARGA_PRESTACIONES_ENCABEZADO.FECHA_CARGA = FECHA_CARGA;
						_CARGA_PRESTACIONES_ENCABEZADO.ID_TIPO_PRESTACION = ID_TIPO_PRESTACION;
					if (!string.IsNullOrEmpty(ARCHIVO))
					{
						_CARGA_PRESTACIONES_ENCABEZADO.ARCHIVO = ARCHIVO;
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
