using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxFACTURACION
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxFACTURACION()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<FACTURACION> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
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

		public List<FACTURACION> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
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

		public FACTURACION GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
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

		public FACTURACION GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
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
	 	
		public List<FACTURACION> GetByFilter(System.DateTime? FECHA_FACTURACION = null, bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
                    return repositorio.GetByFilter(FECHA_FACTURACION, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<FACTURACION> GetByFilterWithReferences(System.DateTime? FECHA_FACTURACION = null, bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
                    return repositorio.GetByFilterWithReferences(FECHA_FACTURACION, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(System.DateTime FECHA_FACTURACION)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					FACTURACION _FACTURACION = new FACTURACION();

					//properties

                    _FACTURACION.FECHA_FACTURACION = FECHA_FACTURACION;
                    _FACTURACION.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("FACTURACION",_FACTURACION);
                    context.SaveChanges();

					return _FACTURACION.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, System.DateTime FECHA_FACTURACION)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
                    FACTURACION _FACTURACION = repositorio.GetById(Id);
                    if(Equals(_FACTURACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURACION con Id =",Id.ToString()));
					}

					//properties

						_FACTURACION.FECHA_FACTURACION = FECHA_FACTURACION;
	
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
					RepositorioFACTURACION repositorio = new RepositorioFACTURACION(context);
					FACTURACION _FACTURACION = repositorio.GetById(Id); 
					
					if(Equals(_FACTURACION ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURACION con Id =",Id.ToString()));
					}

					_FACTURACION.ACTIVO = false;

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
