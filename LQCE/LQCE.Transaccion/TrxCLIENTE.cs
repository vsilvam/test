using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCLIENTE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCLIENTE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CLIENTE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
					                    return repositorio.GetAll().OrderBy(i => i.NOMBRE).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public List<CLIENTE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                                        return repositorio.GetAllWithReferences().OrderBy(i => i.NOMBRE).ToList();
					                }
            }
            catch (Exception ex)
           {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

		public CLIENTE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
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

		public CLIENTE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
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
	 	
		public List<CLIENTE> GetByFilter(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, int? TIPO_FACTURAId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    return repositorio.GetByFilter(COMUNAId, CONVENIOId, TIPO_PRESTACIONId, TIPO_FACTURAId, RUT, NOMBRE, DESCUENTO, DIRECCION, FONO, GIRO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<CLIENTE> GetByFilterWithReferences(int? COMUNAId = null, int? CONVENIOId = null, int? TIPO_PRESTACIONId = null, int? TIPO_FACTURAId = null, string RUT = "", string NOMBRE = "", int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    return repositorio.GetByFilterWithReferences(COMUNAId, CONVENIOId, TIPO_PRESTACIONId, TIPO_FACTURAId, RUT, NOMBRE, DESCUENTO, DIRECCION, FONO, GIRO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int COMUNAId, int CONVENIOId, int TIPO_PRESTACIONId, int TIPO_FACTURAId, string RUT, string NOMBRE, int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCOMUNA _repositorioCOMUNA = new RepositorioCOMUNA(context);
					COMUNA _objCOMUNA = _repositorioCOMUNA.GetById(COMUNAId);
					if(Equals(_objCOMUNA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COMUNA con Id =",COMUNAId.ToString()));
					}

					RepositorioCONVENIO _repositorioCONVENIO = new RepositorioCONVENIO(context);
					CONVENIO _objCONVENIO = _repositorioCONVENIO.GetById(CONVENIOId);
					if(Equals(_objCONVENIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",CONVENIOId.ToString()));
					}

					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}

					RepositorioTIPO_FACTURA _repositorioTIPO_FACTURA = new RepositorioTIPO_FACTURA(context);
					TIPO_FACTURA _objTIPO_FACTURA = _repositorioTIPO_FACTURA.GetById(TIPO_FACTURAId);
					if(Equals(_objTIPO_FACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_FACTURA con Id =",TIPO_FACTURAId.ToString()));
					}

					CLIENTE _CLIENTE = new CLIENTE();

					//properties

                    _CLIENTE.RUT = RUT;				
                    _CLIENTE.NOMBRE = NOMBRE;				
                    _CLIENTE.DESCUENTO = DESCUENTO;
                    _CLIENTE.DIRECCION = DIRECCION;				
                    _CLIENTE.FONO = FONO;				
                    _CLIENTE.GIRO = GIRO;				
                    _CLIENTE.ACTIVO = true;				

					//parents
						 
                    _CLIENTE.COMUNA = _objCOMUNA;
                    _CLIENTE.CONVENIO = _objCONVENIO;
                    _CLIENTE.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    _CLIENTE.TIPO_FACTURA = _objTIPO_FACTURA;
                    
					context.AddObject("CLIENTE",_CLIENTE);
                    context.SaveChanges();

					return _CLIENTE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int COMUNAId, int CONVENIOId, int TIPO_PRESTACIONId, int TIPO_FACTURAId, string RUT, string NOMBRE, int? DESCUENTO = null, string DIRECCION = "", string FONO = "", string GIRO = "")
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    CLIENTE _CLIENTE = repositorio.GetById(Id);
                    if(Equals(_CLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",Id.ToString()));
					}
					
					RepositorioCOMUNA _repositorioCOMUNA = new RepositorioCOMUNA(context);
					COMUNA _objCOMUNA = _repositorioCOMUNA.GetById(COMUNAId);
					if(Equals(_objCOMUNA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COMUNA con Id =",COMUNAId.ToString()));
					}
						
					RepositorioCONVENIO _repositorioCONVENIO = new RepositorioCONVENIO(context);
					CONVENIO _objCONVENIO = _repositorioCONVENIO.GetById(CONVENIOId);
					if(Equals(_objCONVENIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",CONVENIOId.ToString()));
					}
						
					RepositorioTIPO_PRESTACION _repositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
					TIPO_PRESTACION _objTIPO_PRESTACION = _repositorioTIPO_PRESTACION.GetById(TIPO_PRESTACIONId);
					if(Equals(_objTIPO_PRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_PRESTACION con Id =",TIPO_PRESTACIONId.ToString()));
					}
						
					RepositorioTIPO_FACTURA _repositorioTIPO_FACTURA = new RepositorioTIPO_FACTURA(context);
					TIPO_FACTURA _objTIPO_FACTURA = _repositorioTIPO_FACTURA.GetById(TIPO_FACTURAId);
					if(Equals(_objTIPO_FACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_FACTURA con Id =",TIPO_FACTURAId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(RUT))
					{
						_CLIENTE.RUT = RUT;
					}
					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CLIENTE.NOMBRE = NOMBRE;
					}
					if (DESCUENTO.HasValue)
					{
						_CLIENTE.DESCUENTO = DESCUENTO.Value;
					}
					if (!string.IsNullOrEmpty(DIRECCION))
					{
						_CLIENTE.DIRECCION = DIRECCION;
					}
					if (!string.IsNullOrEmpty(FONO))
					{
						_CLIENTE.FONO = FONO;
					}
					if (!string.IsNullOrEmpty(GIRO))
					{
						_CLIENTE.GIRO = GIRO;
					}
	
					//parents
					 
                    _CLIENTE.COMUNA = _objCOMUNA;
                    _CLIENTE.CONVENIO = _objCONVENIO;
                    _CLIENTE.TIPO_PRESTACION = _objTIPO_PRESTACION;
                    _CLIENTE.TIPO_FACTURA = _objTIPO_FACTURA;

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
					RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
					CLIENTE _CLIENTE = repositorio.GetById(Id); 
					
					if(Equals(_CLIENTE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",Id.ToString()));
					}

					_CLIENTE.ACTIVO = false;

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
