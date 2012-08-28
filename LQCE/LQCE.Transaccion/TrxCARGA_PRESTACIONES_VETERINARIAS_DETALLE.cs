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
	 	
		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilter(int? CARGA_PRESTACIONES_ENCABEZADOId = null, int? CARGA_PRESTACIONES_DETALLE_ESTADOId = null, int? CLIENTEId = null, int? ESPECIE1Id = null, int? GARANTIA1Id = null, int? PREVISIONId = null, int? RAZA1Id = null, System.DateTime? FECHA_ACTUALIZACION = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null)
        {
			Init();
			try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    return repositorio.GetByFilter(CARGA_PRESTACIONES_ENCABEZADOId, CARGA_PRESTACIONES_DETALLE_ESTADOId, CLIENTEId, ESPECIE1Id, GARANTIA1Id, PREVISIONId, RAZA1Id, FECHA_ACTUALIZACION, FICHA, NOMBRE, ESPECIE, RAZA, EDAD, SEXO, SOLICITA, TELEFONO, MEDICO, PROCEDENCIA, FECHA_RECEPCION, FECHA_MUESTRA, FECHA_RESULTADOS, PENDIENTE, GARANTIA, PAGADO, TOTAL, MENSAJE_ERROR, VALOR_FICHA, VALOR_FECHA_MUESTRA, VALOR_FECHA_RECEPCION, VALOR_FECHA_ENTREGA_RESULTADOS).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

		public List<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ENCABEZADOId = null, int? CARGA_PRESTACIONES_DETALLE_ESTADOId = null, int? CLIENTEId = null, int? ESPECIE1Id = null, int? GARANTIA1Id = null, int? PREVISIONId = null, int? RAZA1Id = null, System.DateTime? FECHA_ACTUALIZACION = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null)
        {
			Init();
            try
            {
                 using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    return repositorio.GetByFilterWithReferences(CARGA_PRESTACIONES_ENCABEZADOId, CARGA_PRESTACIONES_DETALLE_ESTADOId, CLIENTEId, ESPECIE1Id, GARANTIA1Id, PREVISIONId, RAZA1Id, FECHA_ACTUALIZACION, FICHA, NOMBRE, ESPECIE, RAZA, EDAD, SEXO, SOLICITA, TELEFONO, MEDICO, PROCEDENCIA, FECHA_RECEPCION, FECHA_MUESTRA, FECHA_RESULTADOS, PENDIENTE, GARANTIA, PAGADO, TOTAL, MENSAJE_ERROR, VALOR_FICHA, VALOR_FECHA_MUESTRA, VALOR_FECHA_RECEPCION, VALOR_FECHA_ENTREGA_RESULTADOS).OrderBy(i => i.ID).ToList();
                }
            }
            catch (Exception ex)
            {
				 ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        } 

        public int Add(int CARGA_PRESTACIONES_ENCABEZADOId, int CARGA_PRESTACIONES_DETALLE_ESTADOId, int CLIENTEId, int ESPECIE1Id, int GARANTIA1Id, int PREVISIONId, int RAZA1Id, System.DateTime FECHA_ACTUALIZACION, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null)
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

					RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _repositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);
					CARGA_PRESTACIONES_DETALLE_ESTADO _objCARGA_PRESTACIONES_DETALLE_ESTADO = _repositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById(CARGA_PRESTACIONES_DETALLE_ESTADOId);
					if(Equals(_objCARGA_PRESTACIONES_DETALLE_ESTADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_DETALLE_ESTADO con Id =",CARGA_PRESTACIONES_DETALLE_ESTADOId.ToString()));
					}

					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}

					RepositorioESPECIE _repositorioESPECIE1 = new RepositorioESPECIE(context);
					ESPECIE _objESPECIE1 = _repositorioESPECIE1.GetById(ESPECIE1Id);
					if(Equals(_objESPECIE1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado ESPECIE1 con Id =",ESPECIE1Id.ToString()));
					}

					RepositorioGARANTIA _repositorioGARANTIA1 = new RepositorioGARANTIA(context);
					GARANTIA _objGARANTIA1 = _repositorioGARANTIA1.GetById(GARANTIA1Id);
					if(Equals(_objGARANTIA1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado GARANTIA1 con Id =",GARANTIA1Id.ToString()));
					}

					RepositorioPREVISION _repositorioPREVISION = new RepositorioPREVISION(context);
					PREVISION _objPREVISION = _repositorioPREVISION.GetById(PREVISIONId);
					if(Equals(_objPREVISION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",PREVISIONId.ToString()));
					}

					RepositorioRAZA _repositorioRAZA1 = new RepositorioRAZA(context);
					RAZA _objRAZA1 = _repositorioRAZA1.GetById(RAZA1Id);
					if(Equals(_objRAZA1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA1 con Id =",RAZA1Id.ToString()));
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
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;				
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_ACTUALIZACION = FECHA_ACTUALIZACION;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FICHA = VALOR_FICHA;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_MUESTRA = VALOR_FECHA_MUESTRA;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_RECEPCION = VALOR_FECHA_RECEPCION;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_ENTREGA_RESULTADOS = VALOR_FECHA_ENTREGA_RESULTADOS;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ACTIVO = true;				

					//parents
						 
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_DETALLE_ESTADO = _objCARGA_PRESTACIONES_DETALLE_ESTADO;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CLIENTE = _objCLIENTE;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE1 = _objESPECIE1;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.GARANTIA1 = _objGARANTIA1;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PREVISION = _objPREVISION;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RAZA1 = _objRAZA1;
                    
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

		public void Update(int Id, int CARGA_PRESTACIONES_ENCABEZADOId, int CARGA_PRESTACIONES_DETALLE_ESTADOId, int CLIENTEId, int ESPECIE1Id, int GARANTIA1Id, int PREVISIONId, int RAZA1Id, System.DateTime FECHA_ACTUALIZACION, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null)
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
						
					RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _repositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);
					CARGA_PRESTACIONES_DETALLE_ESTADO _objCARGA_PRESTACIONES_DETALLE_ESTADO = _repositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById(CARGA_PRESTACIONES_DETALLE_ESTADOId);
					if(Equals(_objCARGA_PRESTACIONES_DETALLE_ESTADO,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CARGA_PRESTACIONES_DETALLE_ESTADO con Id =",CARGA_PRESTACIONES_DETALLE_ESTADOId.ToString()));
					}
						
					RepositorioCLIENTE _repositorioCLIENTE = new RepositorioCLIENTE(context);
					CLIENTE _objCLIENTE = _repositorioCLIENTE.GetById(CLIENTEId);
					if(Equals(_objCLIENTE,null))
					{
						throw new Exception(String.Concat("No se ha encontrado CLIENTE con Id =",CLIENTEId.ToString()));
					}
						
					RepositorioESPECIE _repositorioESPECIE1 = new RepositorioESPECIE(context);
					ESPECIE _objESPECIE1 = _repositorioESPECIE1.GetById(ESPECIE1Id);
					if(Equals(_objESPECIE1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado ESPECIE1 con Id =",ESPECIE1Id.ToString()));
					}
						
					RepositorioGARANTIA _repositorioGARANTIA1 = new RepositorioGARANTIA(context);
					GARANTIA _objGARANTIA1 = _repositorioGARANTIA1.GetById(GARANTIA1Id);
					if(Equals(_objGARANTIA1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado GARANTIA1 con Id =",GARANTIA1Id.ToString()));
					}
						
					RepositorioPREVISION _repositorioPREVISION = new RepositorioPREVISION(context);
					PREVISION _objPREVISION = _repositorioPREVISION.GetById(PREVISIONId);
					if(Equals(_objPREVISION,null))
					{
						throw new Exception(String.Concat("No se ha encontrado PREVISION con Id =",PREVISIONId.ToString()));
					}
						
					RepositorioRAZA _repositorioRAZA1 = new RepositorioRAZA(context);
					RAZA _objRAZA1 = _repositorioRAZA1.GetById(RAZA1Id);
					if(Equals(_objRAZA1,null))
					{
						throw new Exception(String.Concat("No se ha encontrado RAZA1 con Id =",RAZA1Id.ToString()));
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
					if (!string.IsNullOrEmpty(MENSAJE_ERROR))
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MENSAJE_ERROR = MENSAJE_ERROR;
					}
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FECHA_ACTUALIZACION = FECHA_ACTUALIZACION;
					if (VALOR_FICHA.HasValue)
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FICHA = VALOR_FICHA.Value;
					}
					if (VALOR_FECHA_MUESTRA.HasValue)
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_MUESTRA = VALOR_FECHA_MUESTRA.Value;
					}
					if (VALOR_FECHA_RECEPCION.HasValue)
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_RECEPCION = VALOR_FECHA_RECEPCION.Value;
					}
					if (VALOR_FECHA_ENTREGA_RESULTADOS.HasValue)
					{
						_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_ENTREGA_RESULTADOS = VALOR_FECHA_ENTREGA_RESULTADOS.Value;
					}
	
					//parents
					 
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_ENCABEZADO = _objCARGA_PRESTACIONES_ENCABEZADO;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_DETALLE_ESTADO = _objCARGA_PRESTACIONES_DETALLE_ESTADO;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CLIENTE = _objCLIENTE;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE1 = _objESPECIE1;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.GARANTIA1 = _objGARANTIA1;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PREVISION = _objPREVISION;
                    _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RAZA1 = _objRAZA1;

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
