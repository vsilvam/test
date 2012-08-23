using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_ESTADO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_ESTADO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_ESTADO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
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

		public List<CARGA_PRESTACIONES_ESTADO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
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

		public CARGA_PRESTACIONES_ESTADO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
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

		public CARGA_PRESTACIONES_ESTADO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
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
	 	
		public List<CARGA_PRESTACIONES_ESTADO> GetByFilter(string NOMBRE = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
                    return repositorio.GetByFilter(NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_ESTADO> GetByFilterWithReferences(string NOMBRE = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
                    return repositorio.GetByFilterWithReferences(NOMBRE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(string NOMBRE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					CARGA_PRESTACIONES_ESTADO _CARGA_PRESTACIONES_ESTADO = new CARGA_PRESTACIONES_ESTADO();

					//properties

                    _CARGA_PRESTACIONES_ESTADO.NOMBRE = NOMBRE;				
                    _CARGA_PRESTACIONES_ESTADO.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("CARGA_PRESTACIONES_ESTADO",_CARGA_PRESTACIONES_ESTADO);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_ESTADO.ID;
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
                    RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
                    CARGA_PRESTACIONES_ESTADO _CARGA_PRESTACIONES_ESTADO = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_ESTADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ESTADO con Id =",Id.ToString()));
					}

					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CARGA_PRESTACIONES_ESTADO.NOMBRE = NOMBRE;
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
					RepositorioCARGA_PRESTACIONES_ESTADO repositorio = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
					CARGA_PRESTACIONES_ESTADO _CARGA_PRESTACIONES_ESTADO = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_ESTADO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ESTADO con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_ESTADO.ACTIVO = false;

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
