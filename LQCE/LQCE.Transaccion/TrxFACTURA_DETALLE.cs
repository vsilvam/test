using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxFACTURA_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxFACTURA_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<FACTURA_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
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

		public List<FACTURA_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
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

		public FACTURA_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
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

		public FACTURA_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
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
	 	
		public List<FACTURA_DETALLE> GetByFilter(int? FACTURAId = null, int? PRESTACIONId = null, int? MONTO_TOTAL = null, int? MONTO_COBRADO = null, bool? ACTIVO = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
                    return repositorio.GetByFilter(FACTURAId, PRESTACIONId, MONTO_TOTAL, MONTO_COBRADO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<FACTURA_DETALLE> GetByFilterWithReferences(int? FACTURAId = null, int? PRESTACIONId = null, int? MONTO_TOTAL = null, int? MONTO_COBRADO = null, bool? ACTIVO = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(FACTURAId, PRESTACIONId, MONTO_TOTAL, MONTO_COBRADO, ACTIVO).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int FACTURAId, int PRESTACIONId, int MONTO_TOTAL, int MONTO_COBRADO)
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

					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}

					FACTURA_DETALLE _FACTURA_DETALLE = new FACTURA_DETALLE();

					//properties

                    _FACTURA_DETALLE.MONTO_TOTAL = MONTO_TOTAL;
                    _FACTURA_DETALLE.MONTO_COBRADO = MONTO_COBRADO;
                    _FACTURA_DETALLE.ACTIVO = true;				

					//parents
						 
                    _FACTURA_DETALLE.FACTURA = _objFACTURA;
                    _FACTURA_DETALLE.PRESTACION = _objPRESTACION;
                    
					context.AddObject("FACTURA_DETALLE",_FACTURA_DETALLE);
                    context.SaveChanges();

					return _FACTURA_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int FACTURAId, int PRESTACIONId, int MONTO_TOTAL, int MONTO_COBRADO)
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
                    FACTURA_DETALLE _FACTURA_DETALLE = repositorio.GetById(Id);
                    if(Equals(_FACTURA_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA_DETALLE con Id =",Id.ToString()));
					}
					
					RepositorioFACTURA _repositorioFACTURA = new RepositorioFACTURA(context);
					FACTURA _objFACTURA = _repositorioFACTURA.GetById(FACTURAId);
					if(Equals(_objFACTURA,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA con Id =",FACTURAId.ToString()));
					}
						
					RepositorioPRESTACION _repositorioPRESTACION = new RepositorioPRESTACION(context);
					PRESTACION _objPRESTACION = _repositorioPRESTACION.GetById(PRESTACIONId);
					if(Equals(_objPRESTACION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PRESTACION con Id =",PRESTACIONId.ToString()));
					}
	
					//properties

						_FACTURA_DETALLE.MONTO_TOTAL = MONTO_TOTAL;
						_FACTURA_DETALLE.MONTO_COBRADO = MONTO_COBRADO;
	
					//parents
					 
                    _FACTURA_DETALLE.FACTURA = _objFACTURA;
                    _FACTURA_DETALLE.PRESTACION = _objPRESTACION;

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
					RepositorioFACTURA_DETALLE repositorio = new RepositorioFACTURA_DETALLE(context);
					FACTURA_DETALLE _FACTURA_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_FACTURA_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado FACTURA_DETALLE con Id =",Id.ToString()));
					}

					_FACTURA_DETALLE.ACTIVO = false;

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
