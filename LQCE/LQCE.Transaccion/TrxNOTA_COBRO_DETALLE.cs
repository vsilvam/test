using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxNOTA_COBRO_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxNOTA_COBRO_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<NOTA_COBRO_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
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

		public List<NOTA_COBRO_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
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

		public NOTA_COBRO_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
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

		public NOTA_COBRO_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
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
	 	
		public List<NOTA_COBRO_DETALLE> GetByFilter(int? NOTA_COBROId = null, int? FACTURAId = null, int? MONTO_PENDIENTE = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
                    return repositorio.GetByFilter(NOTA_COBROId, FACTURAId, MONTO_PENDIENTE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		public List<NOTA_COBRO_DETALLE> GetByFilterWithReferences(int? NOTA_COBROId = null, int? FACTURAId = null, int? MONTO_PENDIENTE = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(NOTA_COBROId, FACTURAId, MONTO_PENDIENTE).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 

		        public int Add(int NOTA_COBROId, int FACTURAId, int MONTO_PENDIENTE)
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioNOTA_COBRO _repositorioNOTA_COBRO = new RepositorioNOTA_COBRO(context);
					NOTA_COBRO _objNOTA_COBRO = _repositorioNOTA_COBRO.GetById(NOTA_COBROId);
					if(Equals(_objNOTA_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO con Id =",NOTA_COBROId.ToString()));
					}

					RepositorioFACTURA _repositorioFACTURA = new RepositorioFACTURA(context);
					FACTURA _objFACTURA = _repositorioFACTURA.GetById(FACTURAId);
					if(Equals(_objFACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",FACTURAId.ToString()));
					}

					NOTA_COBRO_DETALLE _NOTA_COBRO_DETALLE = new NOTA_COBRO_DETALLE();

					//properties

                    _NOTA_COBRO_DETALLE.MONTO_PENDIENTE = MONTO_PENDIENTE;
                    _NOTA_COBRO_DETALLE.ACTIVO = true;				

					//parents
						 
                    _NOTA_COBRO_DETALLE.NOTA_COBRO = _objNOTA_COBRO;
                    _NOTA_COBRO_DETALLE.FACTURA = _objFACTURA;
                    
					context.AddObject("NOTA_COBRO_DETALLE",_NOTA_COBRO_DETALLE);
                    context.SaveChanges();

					return _NOTA_COBRO_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int NOTA_COBROId, int FACTURAId, int MONTO_PENDIENTE)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
                    NOTA_COBRO_DETALLE _NOTA_COBRO_DETALLE = repositorio.GetById(Id);
                    if(Equals(_NOTA_COBRO_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO_DETALLE con Id =",Id.ToString()));
					}
					
					RepositorioNOTA_COBRO _repositorioNOTA_COBRO = new RepositorioNOTA_COBRO(context);
					NOTA_COBRO _objNOTA_COBRO = _repositorioNOTA_COBRO.GetById(NOTA_COBROId);
					if(Equals(_objNOTA_COBRO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO con Id =",NOTA_COBROId.ToString()));
					}
						
					RepositorioFACTURA _repositorioFACTURA = new RepositorioFACTURA(context);
					FACTURA _objFACTURA = _repositorioFACTURA.GetById(FACTURAId);
					if(Equals(_objFACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",FACTURAId.ToString()));
					}
	
					//properties

						_NOTA_COBRO_DETALLE.MONTO_PENDIENTE = MONTO_PENDIENTE;
	
					//parents
					 
                    _NOTA_COBRO_DETALLE.NOTA_COBRO = _objNOTA_COBRO;
                    _NOTA_COBRO_DETALLE.FACTURA = _objFACTURA;

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
					RepositorioNOTA_COBRO_DETALLE repositorio = new RepositorioNOTA_COBRO_DETALLE(context);
					NOTA_COBRO_DETALLE _NOTA_COBRO_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_NOTA_COBRO_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado NOTA_COBRO_DETALLE con Id =",Id.ToString()));
					}

					_NOTA_COBRO_DETALLE.ACTIVO = false;

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
