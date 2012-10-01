using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCONVENIO_TARIFARIO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCONVENIO_TARIFARIO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CONVENIO_TARIFARIO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
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

		public List<CONVENIO_TARIFARIO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
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

		public CONVENIO_TARIFARIO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
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

		public CONVENIO_TARIFARIO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
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
	 	
		public List<CONVENIO_TARIFARIO> GetByFilter(int? CONVENIOId = null, System.DateTime? FECHA_VIGENCIA = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
                    return repositorio.GetByFilter(CONVENIOId, FECHA_VIGENCIA).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<CONVENIO_TARIFARIO> GetByFilterWithReferences(int? CONVENIOId = null, System.DateTime? FECHA_VIGENCIA = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
                    return repositorio.GetByFilterWithReferences(CONVENIOId, FECHA_VIGENCIA).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int CONVENIOId, System.DateTime FECHA_VIGENCIA)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCONVENIO _repositorioCONVENIO = new RepositorioCONVENIO(context);
					CONVENIO _objCONVENIO = _repositorioCONVENIO.GetById(CONVENIOId);
					if(Equals(_objCONVENIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",CONVENIOId.ToString()));
					}

					CONVENIO_TARIFARIO _CONVENIO_TARIFARIO = new CONVENIO_TARIFARIO();

					//properties

                    _CONVENIO_TARIFARIO.FECHA_VIGENCIA = FECHA_VIGENCIA;
                    _CONVENIO_TARIFARIO.ACTIVO = true;				

					//parents
						 
                    _CONVENIO_TARIFARIO.CONVENIO = _objCONVENIO;
                    
					context.AddObject("CONVENIO_TARIFARIO",_CONVENIO_TARIFARIO);
                    context.SaveChanges();

					return _CONVENIO_TARIFARIO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CONVENIOId, System.DateTime FECHA_VIGENCIA)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
                    CONVENIO_TARIFARIO _CONVENIO_TARIFARIO = repositorio.GetById(Id);
                    if(Equals(_CONVENIO_TARIFARIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_TARIFARIO con Id =",Id.ToString()));
					}
					
					RepositorioCONVENIO _repositorioCONVENIO = new RepositorioCONVENIO(context);
					CONVENIO _objCONVENIO = _repositorioCONVENIO.GetById(CONVENIOId);
					if(Equals(_objCONVENIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO con Id =",CONVENIOId.ToString()));
					}
	
					//properties

						_CONVENIO_TARIFARIO.FECHA_VIGENCIA = FECHA_VIGENCIA;
	
					//parents
					 
                    _CONVENIO_TARIFARIO.CONVENIO = _objCONVENIO;

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
					RepositorioCONVENIO_TARIFARIO repositorio = new RepositorioCONVENIO_TARIFARIO(context);
					CONVENIO_TARIFARIO _CONVENIO_TARIFARIO = repositorio.GetById(Id); 
					
					if(Equals(_CONVENIO_TARIFARIO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_TARIFARIO con Id =",Id.ToString()));
					}

					_CONVENIO_TARIFARIO.ACTIVO = false;

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
