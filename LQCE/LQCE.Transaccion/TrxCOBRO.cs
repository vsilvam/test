using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCOBRO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCOBRO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<COBRO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
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

		public List<COBRO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
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

		public COBRO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
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

		public COBRO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
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
	 	
		public List<COBRO> GetByFilter(int? TIPO_COBROId = null, System.DateTime? FECHA_COBRO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
                    return repositorio.GetByFilter(TIPO_COBROId, FECHA_COBRO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<COBRO> GetByFilterWithReferences(int? TIPO_COBROId = null, System.DateTime? FECHA_COBRO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
                    return repositorio.GetByFilterWithReferences(TIPO_COBROId, FECHA_COBRO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int TIPO_COBROId, System.DateTime FECHA_COBRO)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioTIPO_COBRO _repositorioTIPO_COBRO = new RepositorioTIPO_COBRO(context);
					TIPO_COBRO _objTIPO_COBRO = _repositorioTIPO_COBRO.GetById(TIPO_COBROId);
					if(Equals(_objTIPO_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_COBRO con Id =",TIPO_COBROId.ToString()));
					}

					COBRO _COBRO = new COBRO();

					//properties

                    _COBRO.FECHA_COBRO = FECHA_COBRO;
                    _COBRO.ACTIVO = true;				

					//parents
						 
                    _COBRO.TIPO_COBRO = _objTIPO_COBRO;
                    
					context.AddObject("COBRO",_COBRO);
                    context.SaveChanges();

					return _COBRO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int TIPO_COBROId, System.DateTime FECHA_COBRO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
                    COBRO _COBRO = repositorio.GetById(Id);
                    if(Equals(_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COBRO con Id =",Id.ToString()));
					}
					
					RepositorioTIPO_COBRO _repositorioTIPO_COBRO = new RepositorioTIPO_COBRO(context);
					TIPO_COBRO _objTIPO_COBRO = _repositorioTIPO_COBRO.GetById(TIPO_COBROId);
					if(Equals(_objTIPO_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado TIPO_COBRO con Id =",TIPO_COBROId.ToString()));
					}
	
					//properties

						_COBRO.FECHA_COBRO = FECHA_COBRO;
	
					//parents
					 
                    _COBRO.TIPO_COBRO = _objTIPO_COBRO;

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
					RepositorioCOBRO repositorio = new RepositorioCOBRO(context);
					COBRO _COBRO = repositorio.GetById(Id); 
					
					if(Equals(_COBRO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado COBRO con Id =",Id.ToString()));
					}

					_COBRO.ACTIVO = false;

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
