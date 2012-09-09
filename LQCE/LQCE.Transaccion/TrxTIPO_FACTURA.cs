using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxTIPO_FACTURA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxTIPO_FACTURA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<TIPO_FACTURA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
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

		public List<TIPO_FACTURA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
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

		public TIPO_FACTURA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
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

		public TIPO_FACTURA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
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
	 	
		public List<TIPO_FACTURA> GetByFilter(string RUT_FACTURA = "", string NOMBRE_FACTURA = "", bool? AFECTO_IVA = null, string NOMBRE_REPORTE_FACTURA = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
                    return repositorio.GetByFilter(RUT_FACTURA, NOMBRE_FACTURA, AFECTO_IVA, NOMBRE_REPORTE_FACTURA).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<TIPO_FACTURA> GetByFilterWithReferences(string RUT_FACTURA = "", string NOMBRE_FACTURA = "", bool? AFECTO_IVA = null, string NOMBRE_REPORTE_FACTURA = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
                    return repositorio.GetByFilterWithReferences(RUT_FACTURA, NOMBRE_FACTURA, AFECTO_IVA, NOMBRE_REPORTE_FACTURA).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(string RUT_FACTURA, string NOMBRE_FACTURA, bool AFECTO_IVA, string NOMBRE_REPORTE_FACTURA)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					TIPO_FACTURA _TIPO_FACTURA = new TIPO_FACTURA();

					//properties

                    _TIPO_FACTURA.RUT_FACTURA = RUT_FACTURA;				
                    _TIPO_FACTURA.NOMBRE_FACTURA = NOMBRE_FACTURA;				
                    _TIPO_FACTURA.AFECTO_IVA = AFECTO_IVA;
                    _TIPO_FACTURA.NOMBRE_REPORTE_FACTURA = NOMBRE_REPORTE_FACTURA;				
                    _TIPO_FACTURA.ACTIVO = true;				

					//parents
						 
                    
					context.AddObject("TIPO_FACTURA",_TIPO_FACTURA);
                    context.SaveChanges();

					return _TIPO_FACTURA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, string RUT_FACTURA, string NOMBRE_FACTURA, bool AFECTO_IVA, string NOMBRE_REPORTE_FACTURA)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
                    TIPO_FACTURA _TIPO_FACTURA = repositorio.GetById(Id);
                    if(Equals(_TIPO_FACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_FACTURA con Id =",Id.ToString()));
					}

					//properties

					if (!string.IsNullOrEmpty(RUT_FACTURA))
					{
						_TIPO_FACTURA.RUT_FACTURA = RUT_FACTURA;
					}
					if (!string.IsNullOrEmpty(NOMBRE_FACTURA))
					{
						_TIPO_FACTURA.NOMBRE_FACTURA = NOMBRE_FACTURA;
					}
						_TIPO_FACTURA.AFECTO_IVA = AFECTO_IVA;
					if (!string.IsNullOrEmpty(NOMBRE_REPORTE_FACTURA))
					{
						_TIPO_FACTURA.NOMBRE_REPORTE_FACTURA = NOMBRE_REPORTE_FACTURA;
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
					RepositorioTIPO_FACTURA repositorio = new RepositorioTIPO_FACTURA(context);
					TIPO_FACTURA _TIPO_FACTURA = repositorio.GetById(Id); 
					
					if(Equals(_TIPO_FACTURA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_FACTURA con Id =",Id.ToString()));
					}

					_TIPO_FACTURA.ACTIVO = false;

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
