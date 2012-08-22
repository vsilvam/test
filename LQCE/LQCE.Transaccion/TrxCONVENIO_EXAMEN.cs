using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCONVENIO_EXAMEN
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCONVENIO_EXAMEN()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CONVENIO_EXAMEN> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
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

		public List<CONVENIO_EXAMEN> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
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

		public CONVENIO_EXAMEN GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
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

		public CONVENIO_EXAMEN GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
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
	 	
		public List<CONVENIO_EXAMEN> GetByFilter(int? CONVENIO_TARIFARIOId = null, int? EXAMENId = null, int? VALOR = null, bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
                    return repositorio.GetByFilter(CONVENIO_TARIFARIOId, EXAMENId, VALOR, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CONVENIO_EXAMEN> GetByFilterWithReferences(int? CONVENIO_TARIFARIOId = null, int? EXAMENId = null, int? VALOR = null, bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
                    return repositorio.GetByFilterWithReferences(CONVENIO_TARIFARIOId, EXAMENId, VALOR, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CONVENIO_TARIFARIOId, int EXAMENId, int VALOR)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCONVENIO_TARIFARIO _repositorioCONVENIO_TARIFARIO = new RepositorioCONVENIO_TARIFARIO(context);
					CONVENIO_TARIFARIO _objCONVENIO_TARIFARIO = _repositorioCONVENIO_TARIFARIO.GetById(CONVENIO_TARIFARIOId);
					if(Equals(_objCONVENIO_TARIFARIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_TARIFARIO con Id =",CONVENIO_TARIFARIOId.ToString()));
					}

					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}

					CONVENIO_EXAMEN _CONVENIO_EXAMEN = new CONVENIO_EXAMEN();

					//properties

                    _CONVENIO_EXAMEN.VALOR = VALOR;
                    _CONVENIO_EXAMEN.ACTIVO = true;				

					//parents
						 
                    _CONVENIO_EXAMEN.CONVENIO_TARIFARIO = _objCONVENIO_TARIFARIO;
                    _CONVENIO_EXAMEN.EXAMEN = _objEXAMEN;
                    
					context.AddObject("CONVENIO_EXAMEN",_CONVENIO_EXAMEN);
                    context.SaveChanges();

					return _CONVENIO_EXAMEN.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CONVENIO_TARIFARIOId, int EXAMENId, int VALOR)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
                    CONVENIO_EXAMEN _CONVENIO_EXAMEN = repositorio.GetById(Id);
                    if(Equals(_CONVENIO_EXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_EXAMEN con Id =",Id.ToString()));
					}
					
					RepositorioCONVENIO_TARIFARIO _repositorioCONVENIO_TARIFARIO = new RepositorioCONVENIO_TARIFARIO(context);
					CONVENIO_TARIFARIO _objCONVENIO_TARIFARIO = _repositorioCONVENIO_TARIFARIO.GetById(CONVENIO_TARIFARIOId);
					if(Equals(_objCONVENIO_TARIFARIO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_TARIFARIO con Id =",CONVENIO_TARIFARIOId.ToString()));
					}
						
					RepositorioEXAMEN _repositorioEXAMEN = new RepositorioEXAMEN(context);
					EXAMEN _objEXAMEN = _repositorioEXAMEN.GetById(EXAMENId);
					if(Equals(_objEXAMEN,null))
					{
						throw new Exception(String.Concat("No se ha encontrado EXAMEN con Id =",EXAMENId.ToString()));
					}
	
					//properties

						_CONVENIO_EXAMEN.VALOR = VALOR;
	
					//parents
					 
                    _CONVENIO_EXAMEN.CONVENIO_TARIFARIO = _objCONVENIO_TARIFARIO;
                    _CONVENIO_EXAMEN.EXAMEN = _objEXAMEN;

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
					RepositorioCONVENIO_EXAMEN repositorio = new RepositorioCONVENIO_EXAMEN(context);
					CONVENIO_EXAMEN _CONVENIO_EXAMEN = repositorio.GetById(Id); 
					
					if(Equals(_CONVENIO_EXAMEN ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CONVENIO_EXAMEN con Id =",Id.ToString()));
					}

					_CONVENIO_EXAMEN.ACTIVO = false;

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
