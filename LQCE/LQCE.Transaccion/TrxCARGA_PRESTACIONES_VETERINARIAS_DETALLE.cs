using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
	public partial class TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE
	{
		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE()
		{
		     Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetAll()
        {
			Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
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

		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetAllWithReferences()
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
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

		public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetById(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
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

		public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetByIdWithReferences(int ID)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
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
	 	
		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilter(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    return repositorio.GetByFilter(CARGA_PRESTACIONES_ENCABEZADOId, ACTIVO, VALIDADO, ERROR, FICHA, NOMBRE, ESPECIE, RAZA, EDAD, SEXO, SOLICITA, TELEFONO, MEDICO, PROCEDENCIA, FECHA_RECEPCION, FECHA_MUESTRA, FECHA_RESULTADOS, PENDIENTE, GARANTIA, PAGADO, TOTAL, MENSAJE_ERROR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(CARGA_PRESTACIONES_ENCABEZADOId, ACTIVO, VALIDADO, ERROR, FICHA, NOMBRE, ESPECIE, RAZA, EDAD, SEXO, SOLICITA, TELEFONO, MEDICO, PROCEDENCIA, FECHA_RECEPCION, FECHA_MUESTRA, FECHA_RESULTADOS, PENDIENTE, GARANTIA, PAGADO, TOTAL, MENSAJE_ERROR).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CARGA_PRESTACIONES_ENCABEZADOId, bool VALIDADO, bool ERROR, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
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

					CARGA_PRESTACIONES_VETERINARIAS_DETALLE _CARGA_PRESTACIONES_VETERINARIAS_DETALLE = new CARGA_PRESTACIONES_VETERINARIAS_DETALLE();

					//properties

                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FICHA = FICHA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.NOMBRE = NOMBRE;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE = ESPECIE;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RAZA = RAZA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.EDAD = EDAD;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SEXO = SEXO;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SOLICITA = SOLICITA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.TELEFONO = TELEFONO;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MEDICO = MEDICO;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PROCEDENCIA = PROCEDENCIA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_RECEPCION = FECHA_RECEPCION;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_MUESTRA = FECHA_MUESTRA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_RESULTADOS = FECHA_RESULTADOS;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PENDIENTE = PENDIENTE;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.GARANTIA = GARANTIA;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PAGADO = PAGADO;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.TOTAL = TOTAL;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALIDADO = VALIDADO;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ERROR = ERROR;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;
                    
					context.AddObject("CARGA_PRESTACIONES_VETERINARIAS_DETALLE",_CARGA_PRESTACIONES_VETERINARIAS_DETALLE);
                    context.SaveChanges();

					return _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ID;
                }
            }
			catch(Exception ex)
			{
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
        }

		public void Update(int Id, int CARGA_PRESTACIONES_ENCABEZADOId, bool VALIDADO, bool ERROR, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
		{
		Init();
			try
			{
				 using (LQCEEntities context = new LQCEEntities())
				{
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    CARGA_PRESTACIONES_VETERINARIAS_DETALLE _CARGA_PRESTACIONES_VETERINARIAS_DETALLE = repositorio.GetById(Id);
                    if(Equals(_CARGA_PRESTACIONES_VETERINARIAS_DETALLE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_DETALLE con Id =",Id.ToString()));
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
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FICHA = FICHA;
					}
					if (!string.IsNullOrEmpty(NOMBRE))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.NOMBRE = NOMBRE;
					}
					if (!string.IsNullOrEmpty(ESPECIE))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE = ESPECIE;
					}
					if (!string.IsNullOrEmpty(RAZA))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RAZA = RAZA;
					}
					if (!string.IsNullOrEmpty(EDAD))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.EDAD = EDAD;
					}
					if (!string.IsNullOrEmpty(SEXO))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SEXO = SEXO;
					}
					if (!string.IsNullOrEmpty(SOLICITA))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SOLICITA = SOLICITA;
					}
					if (!string.IsNullOrEmpty(TELEFONO))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.TELEFONO = TELEFONO;
					}
					if (!string.IsNullOrEmpty(MEDICO))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MEDICO = MEDICO;
					}
					if (!string.IsNullOrEmpty(PROCEDENCIA))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PROCEDENCIA = PROCEDENCIA;
					}
					if (!string.IsNullOrEmpty(FECHA_RECEPCION))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_RECEPCION = FECHA_RECEPCION;
					}
					if (!string.IsNullOrEmpty(FECHA_MUESTRA))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_MUESTRA = FECHA_MUESTRA;
					}
					if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_RESULTADOS = FECHA_RESULTADOS;
					}
					if (!string.IsNullOrEmpty(PENDIENTE))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PENDIENTE = PENDIENTE;
					}
					if (!string.IsNullOrEmpty(GARANTIA))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.GARANTIA = GARANTIA;
					}
					if (!string.IsNullOrEmpty(PAGADO))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PAGADO = PAGADO;
					}
					if (!string.IsNullOrEmpty(TOTAL))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.TOTAL = TOTAL;
					}
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALIDADO = VALIDADO;
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ERROR = ERROR;
					if (!string.IsNullOrEmpty(MENSAJE_ERROR))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;

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
					RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
					CARGA_PRESTACIONES_VETERINARIAS_DETALLE _CARGA_PRESTACIONES_VETERINARIAS_DETALLE = repositorio.GetById(Id); 
					
					if(Equals(_CARGA_PRESTACIONES_VETERINARIAS_DETALLE ,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_VETERINARIAS_DETALLE con Id =",Id.ToString()));
					}

					_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ACTIVO = false;

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
