using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION_HUMANA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION_HUMANA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<PRESTACION_HUMANA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
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

		public List<PRESTACION_HUMANA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
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

		public PRESTACION_HUMANA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
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

		public PRESTACION_HUMANA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
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
	 	
		public List<PRESTACION_HUMANA> GetByFilter(string NOMBRE = "", string RUT = "", string EDAD = "", string TELEFONO = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
                    return repositorio.GetByFilter(NOMBRE, RUT, EDAD, TELEFONO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<PRESTACION_HUMANA> GetByFilterWithReferences(string NOMBRE = "", string RUT = "", string EDAD = "", string TELEFONO = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
                    return repositorio.GetByFilterWithReferences(NOMBRE, RUT, EDAD, TELEFONO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(string NOMBRE, string RUT, string EDAD, string TELEFONO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					PRESTACION_HUMANA _PRESTACION_HUMANA = new PRESTACION_HUMANA();

					//properties

                    _PRESTACION_HUMANA.NOMBRE = NOMBRE;				
                    _PRESTACION_HUMANA.RUT = RUT;				
                    _PRESTACION_HUMANA.EDAD = EDAD;				
                    _PRESTACION_HUMANA.TELEFONO = TELEFONO;				
                    _PRESTACION_HUMANA.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("PRESTACION_HUMANA",_PRESTACION_HUMANA);
                    context.SaveChanges();

					return _PRESTACION_HUMANA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, string NOMBRE, string RUT, string EDAD, string TELEFONO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
                    PRESTACION_HUMANA _PRESTACION_HUMANA = repositorio.GetById(Id);
                    if(Equals(_PRESTACION_HUMANA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_HUMANA con Id =",Id.ToString()));
					}

					//properties

					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_PRESTACION_HUMANA.NOMBRE = NOMBRE;
					}
					if (!string.IsNullOrEmpty(RUT))
					{
						_PRESTACION_HUMANA.RUT = RUT;
					}
					if (!string.IsNullOrEmpty(EDAD))
					{
						_PRESTACION_HUMANA.EDAD = EDAD;
					}
					if (!string.IsNullOrEmpty(TELEFONO))
					{
						_PRESTACION_HUMANA.TELEFONO = TELEFONO;
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
					RepositorioPRESTACION_HUMANA repositorio = new RepositorioPRESTACION_HUMANA(context);
					PRESTACION_HUMANA _PRESTACION_HUMANA = repositorio.GetById(Id); 
					
					if(Equals(_PRESTACION_HUMANA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION_HUMANA con Id =",Id.ToString()));
					}

					_PRESTACION_HUMANA.ACTIVO = false;

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
