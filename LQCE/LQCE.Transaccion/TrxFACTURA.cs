using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxFACTURA
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxFACTURA()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<FACTURA> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
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

		public List<FACTURA> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
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

		public FACTURA GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
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

		public FACTURA GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
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
	 	
		public List<FACTURA> GetByFilter(int? CLIENTEId = null, int? FACTURACIONId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
                    return repositorio.GetByFilter(CLIENTEId, FACTURACIONId, CORRELATIVO, RUT_LABORATORIO, NUMERO_FACTURA, DESCUENTO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<FACTURA> GetByFilterWithReferences(int? CLIENTEId = null, int? FACTURACIONId = null, int? CORRELATIVO = null, string RUT_LABORATORIO = "", int? NUMERO_FACTURA = null, int? DESCUENTO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
                    return repositorio.GetByFilterWithReferences(CLIENTEId, FACTURACIONId, CORRELATIVO, RUT_LABORATORIO, NUMERO_FACTURA, DESCUENTO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		        public int Add(int CLIENTEId, int FACTURACIONId, int CORRELATIVO, string RUT_LABORATORIO, int? NUMERO_FACTURA = null, int? DESCUENTO = null)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}

					RepositorioFACTURACION _repositorioFACTURACION = new RepositorioFACTURACION(context);
					FACTURACION _objFACTURACION = _repositorioFACTURACION.GetById(FACTURACIONId);
					if(Equals(_objFACTURACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURACION con Id =",FACTURACIONId.ToString()));
					}

					FACTURA _FACTURA = new FACTURA();

					//properties

                    _FACTURA.CORRELATIVO = CORRELATIVO;
                    _FACTURA.NUMERO_FACTURA = NUMERO_FACTURA;
                    _FACTURA.RUT_LABORATORIO = RUT_LABORATORIO;				
                    _FACTURA.DESCUENTO = DESCUENTO;
                    _FACTURA.ACTIVO = true;				

					//parents
						 
                    _FACTURA.CLIENTE = _objCLIENTE;
                    _FACTURA.FACTURACION = _objFACTURACION;
                    
					context.AddObject("FACTURA",_FACTURA);
                    context.SaveChanges();

					return _FACTURA.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CLIENTEId, int FACTURACIONId, int CORRELATIVO, string RUT_LABORATORIO, int? NUMERO_FACTURA = null, int? DESCUENTO = null)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
                    FACTURA _FACTURA = repositorio.GetById(Id);
                    if(Equals(_FACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",Id.ToString()));
					}
					
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
						
					RepositorioFACTURACION _repositorioFACTURACION = new RepositorioFACTURACION(context);
					FACTURACION _objFACTURACION = _repositorioFACTURACION.GetById(FACTURACIONId);
					if(Equals(_objFACTURACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURACION con Id =",FACTURACIONId.ToString()));
					}
	
					//properties

						_FACTURA.CORRELATIVO = CORRELATIVO;
					if (NUMERO_FACTURA.HasValue)
					{
						_FACTURA.NUMERO_FACTURA = NUMERO_FACTURA.Value;
					}
					if (!string.IsNullOrEmpty(RUT_LABORATORIO))
					{
						_FACTURA.RUT_LABORATORIO = RUT_LABORATORIO;
					}
					if (DESCUENTO.HasValue)
					{
						_FACTURA.DESCUENTO = DESCUENTO.Value;
					}
	
					//parents
					 
                    _FACTURA.CLIENTE = _objCLIENTE;
                    _FACTURA.FACTURACION = _objFACTURACION;

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
					RepositorioFACTURA repositorio = new RepositorioFACTURA(context);
					FACTURA _FACTURA = repositorio.GetById(Id); 
					
					if(Equals(_FACTURA ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",Id.ToString()));
					}

					_FACTURA.ACTIVO = false;

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
