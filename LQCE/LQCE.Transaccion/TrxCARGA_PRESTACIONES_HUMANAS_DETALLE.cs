using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_HUMANAS_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_HUMANAS_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
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

		public List<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
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

		public CARGA_PRESTACIONES_HUMANAS_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
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

		public CARGA_PRESTACIONES_HUMANAS_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
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
	 	
		public List<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetByFilter(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string MENSAJE_ERROR = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    return repositorio.GetByFilter(CARGA_PRESTACIONES_ENCABEZADOId, ACTIVO, VALIDADO, ERROR, FICHA, NOMBRE, RUT, MEDICO, EDAD, TELEFONO, PROCEDENCIA, FECHA_RECEPCION, MUESTRA, FECHA_RESULTADOS, PREVISION, GARANTIA, PAGADO, PENDIENTE, TOTAL, MENSAJE_ERROR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string MENSAJE_ERROR = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(CARGA_PRESTACIONES_ENCABEZADOId, ACTIVO, VALIDADO, ERROR, FICHA, NOMBRE, RUT, MEDICO, EDAD, TELEFONO, PROCEDENCIA, FECHA_RECEPCION, MUESTRA, FECHA_RESULTADOS, PREVISION, GARANTIA, PAGADO, PENDIENTE, TOTAL, MENSAJE_ERROR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CARGA_PRESTACIONES_ENCABEZADOId, bool VALIDADO, bool ERROR, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string MENSAJE_ERROR = "")
        {
		Init();
            try
            {
				 using (LQCEEntities context = new LQCEEntities())
				{
					RepositorioCARGA_PRESTACIONES_ENCABEZADO _repositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
					CARGA_PRESTACIONES_ENCABEZADO _objCARGA_PRESTACIONES_ENCABEZADO = _repositorioCARGA_PRESTACIONES_ENCABEZADO.GetById(CARGA_PRESTACIONES_ENCABEZADOId);
					if(Equals(_objCARGA_PRESTACIONES_ENCABEZADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ENCABEZADO con Id =",CARGA_PRESTACIONES_ENCABEZADOId.ToString()));
					}

					CARGA_PRESTACIONES_HUMANAS_DETALLE _CARGA_PRESTACIONES_HUMANAS_DETALLE = new CARGA_PRESTACIONES_HUMANAS_DETALLE();

					//properties

                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.FICHA = FICHA;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.NOMBRE = NOMBRE;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.RUT = RUT;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.MEDICO = MEDICO;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.EDAD = EDAD;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.TELEFONO = TELEFONO;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.PROCEDENCIA = PROCEDENCIA;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.FECHA_RECEPCION = FECHA_RECEPCION;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.MUESTRA = MUESTRA;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.FECHA_RESULTADOS = FECHA_RESULTADOS;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.PREVISION = PREVISION;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.GARANTIA = GARANTIA;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.PAGADO = PAGADO;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.PENDIENTE = PENDIENTE;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.TOTAL = TOTAL;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.VALIDADO = VALIDADO;
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.ERROR = ERROR;
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;				
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;
                    
					context.AddObject("CARGA_PRESTACIONES_HUMANAS_DETALLE",_CARGA_PRESTACIONES_HUMANAS_DETALLE);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_HUMANAS_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CARGA_PRESTACIONES_ENCABEZADOId, bool VALIDADO, bool ERROR, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string MENSAJE_ERROR = "")
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    CARGA_PRESTACIONES_HUMANAS_DETALLE _CARGA_PRESTACIONES_HUMANAS_DETALLE = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_HUMANAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_DETALLE con Id =",Id.ToString()));
					}
					
					RepositorioCARGA_PRESTACIONES_ENCABEZADO _repositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
					CARGA_PRESTACIONES_ENCABEZADO _objCARGA_PRESTACIONES_ENCABEZADO = _repositorioCARGA_PRESTACIONES_ENCABEZADO.GetById(CARGA_PRESTACIONES_ENCABEZADOId);
					if(Equals(_objCARGA_PRESTACIONES_ENCABEZADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_ENCABEZADO con Id =",CARGA_PRESTACIONES_ENCABEZADOId.ToString()));
					}
	
					//properties

					if (!string.IsNullOrEmpty(FICHA))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.FICHA = FICHA;
					}
					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.NOMBRE = NOMBRE;
					}
					if (!string.IsNullOrEmpty(RUT))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.RUT = RUT;
					}
					if (!string.IsNullOrEmpty(MEDICO))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.MEDICO = MEDICO;
					}
					if (!string.IsNullOrEmpty(EDAD))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.EDAD = EDAD;
					}
					if (!string.IsNullOrEmpty(TELEFONO))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.TELEFONO = TELEFONO;
					}
					if (!string.IsNullOrEmpty(PROCEDENCIA))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.PROCEDENCIA = PROCEDENCIA;
					}
					if (!string.IsNullOrEmpty(FECHA_RECEPCION))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.FECHA_RECEPCION = FECHA_RECEPCION;
					}
					if (!string.IsNullOrEmpty(MUESTRA))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.MUESTRA = MUESTRA;
					}
					if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.FECHA_RESULTADOS = FECHA_RESULTADOS;
					}
					if (!string.IsNullOrEmpty(PREVISION))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.PREVISION = PREVISION;
					}
					if (!string.IsNullOrEmpty(GARANTIA))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.GARANTIA = GARANTIA;
					}
					if (!string.IsNullOrEmpty(PAGADO))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.PAGADO = PAGADO;
					}
					if (!string.IsNullOrEmpty(PENDIENTE))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.PENDIENTE = PENDIENTE;
					}
					if (!string.IsNullOrEmpty(TOTAL))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.TOTAL = TOTAL;
					}
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.VALIDADO = VALIDADO;
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.ERROR = ERROR;
					if (!string.IsNullOrEmpty(MENSAJE_ERROR))
					{
						_CARGA_PRESTACIONES_HUMANAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_HUMANAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;

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
					RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
					CARGA_PRESTACIONES_HUMANAS_DETALLE _CARGA_PRESTACIONES_HUMANAS_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_HUMANAS_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_HUMANAS_DETALLE con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_HUMANAS_DETALLE.ACTIVO = false;

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
