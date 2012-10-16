using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxNOTA_CREDITO
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxNOTA_CREDITO()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<NOTA_CREDITO> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
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

		public List<NOTA_CREDITO> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
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

		public NOTA_CREDITO GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
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

		public NOTA_CREDITO GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
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
	 	
		public List<NOTA_CREDITO> GetByFilter(int? FACTURAId = null, System.DateTime? FECHA_EMISION = null, int? NUMERO_NOTA_CREDITO = null, bool? CORRECCION_TOTAL_PARCIAL = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
                    return repositorio.GetByFilter(FACTURAId, FECHA_EMISION, NUMERO_NOTA_CREDITO, CORRECCION_TOTAL_PARCIAL).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<NOTA_CREDITO> GetByFilterWithReferences(int? FACTURAId = null, System.DateTime? FECHA_EMISION = null, int? NUMERO_NOTA_CREDITO = null, bool? CORRECCION_TOTAL_PARCIAL = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
                    return repositorio.GetByFilterWithReferences(FACTURAId, FECHA_EMISION, NUMERO_NOTA_CREDITO, CORRECCION_TOTAL_PARCIAL).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int FACTURAId, System.DateTime FECHA_EMISION, int NUMERO_NOTA_CREDITO, bool CORRECCION_TOTAL_PARCIAL)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioFACTURA _repositorioFACTURA = new RepositorioFACTURA(context);
					FACTURA _objFACTURA = _repositorioFACTURA.GetById(FACTURAId);
					if(Equals(_objFACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",FACTURAId.ToString()));
					}

					NOTA_CREDITO _NOTA_CREDITO = new NOTA_CREDITO();

					//properties

                    _NOTA_CREDITO.FECHA_EMISION = FECHA_EMISION;
                    _NOTA_CREDITO.NUMERO_NOTA_CREDITO = NUMERO_NOTA_CREDITO;
                    _NOTA_CREDITO.CORRECCION_TOTAL_PARCIAL = CORRECCION_TOTAL_PARCIAL;
                    _NOTA_CREDITO.ACTIVO = true;				

					//parents
						 
                    _NOTA_CREDITO.FACTURA = _objFACTURA;
                    
					context.AddObject("NOTA_CREDITO",_NOTA_CREDITO);
                    context.SaveChanges();

					return _NOTA_CREDITO.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int FACTURAId, System.DateTime FECHA_EMISION, int NUMERO_NOTA_CREDITO, bool CORRECCION_TOTAL_PARCIAL)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
                    NOTA_CREDITO _NOTA_CREDITO = repositorio.GetById(Id);
                    if(Equals(_NOTA_CREDITO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_CREDITO con Id =",Id.ToString()));
					}
					
					RepositorioFACTURA _repositorioFACTURA = new RepositorioFACTURA(context);
					FACTURA _objFACTURA = _repositorioFACTURA.GetById(FACTURAId);
					if(Equals(_objFACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",FACTURAId.ToString()));
					}
	
					//properties

						_NOTA_CREDITO.FECHA_EMISION = FECHA_EMISION;
						_NOTA_CREDITO.NUMERO_NOTA_CREDITO = NUMERO_NOTA_CREDITO;
						_NOTA_CREDITO.CORRECCION_TOTAL_PARCIAL = CORRECCION_TOTAL_PARCIAL;
	
					//parents
					 
                    _NOTA_CREDITO.FACTURA = _objFACTURA;

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
					RepositorioNOTA_CREDITO repositorio = new RepositorioNOTA_CREDITO(context);
					NOTA_CREDITO _NOTA_CREDITO = repositorio.GetById(Id); 
					
					if(Equals(_NOTA_CREDITO ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_CREDITO con Id =",Id.ToString()));
					}

					_NOTA_CREDITO.ACTIVO = false;

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
