using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_HUMANAS_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.CARGA_PRESTACIONES_HUMANAS_DETALLE.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public CARGA_PRESTACIONES_HUMANAS_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.CARGA_PRESTACIONES_HUMANAS_DETALLE.Include("CARGA_PRESTACIONES_DETALLE_ESTADO").Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CLIENTE").Include("GARANTIA1").Include("PREVISION1").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_DETALLE where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_DETALLE.Include("CARGA_PRESTACIONES_DETALLE_ESTADO").Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CLIENTE").Include("GARANTIA1").Include("PREVISION1").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetByFilter(int? CARGA_PRESTACIONES_DETALLE_ESTADOId = null, int? CARGA_PRESTACIONES_ENCABEZADOId = null, int? CLIENTEId = null, int? GARANTIA1Id = null, int? PREVISION1Id = null, System.DateTime? FECHA_ACTUALIZACION = null, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string RECEPCION = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null, int? VALOR_TOTAL = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_DETALLE  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(FICHA))
				{
				   q = q.Where(i => i.FICHA.Contains(FICHA));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(RUT))
				{
				   q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(MEDICO))
				{
				   q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
				   q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
				   q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
				   q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (!string.IsNullOrEmpty(FECHA_RECEPCION))
				{
				   q = q.Where(i => i.FECHA_RECEPCION.Contains(FECHA_RECEPCION));
				}
				if (!string.IsNullOrEmpty(MUESTRA))
				{
				   q = q.Where(i => i.MUESTRA.Contains(MUESTRA));
				}
				if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
				{
				   q = q.Where(i => i.FECHA_RESULTADOS.Contains(FECHA_RESULTADOS));
				}
				if (!string.IsNullOrEmpty(PREVISION))
				{
				   q = q.Where(i => i.PREVISION.Contains(PREVISION));
				}
				if (!string.IsNullOrEmpty(GARANTIA))
				{
				   q = q.Where(i => i.GARANTIA.Contains(GARANTIA));
				}
				if (!string.IsNullOrEmpty(PAGADO))
				{
				   q = q.Where(i => i.PAGADO.Contains(PAGADO));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
				   q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (!string.IsNullOrEmpty(TOTAL))
				{
				   q = q.Where(i => i.TOTAL.Contains(TOTAL));
				}
				if (!string.IsNullOrEmpty(RECEPCION))
				{
				   q = q.Where(i => i.RECEPCION.Contains(RECEPCION));
				}
				if (!string.IsNullOrEmpty(MENSAJE_ERROR))
				{
				   q = q.Where(i => i.MENSAJE_ERROR.Contains(MENSAJE_ERROR));
				}
				if (FECHA_ACTUALIZACION.HasValue)
				{
				  q = q.Where(i => i.FECHA_ACTUALIZACION == FECHA_ACTUALIZACION.Value);
				}
				if (VALOR_FICHA.HasValue)
				{
				  q = q.Where(i => i.VALOR_FICHA == VALOR_FICHA.Value);
				}
				if (VALOR_FECHA_MUESTRA.HasValue)
				{
				  q = q.Where(i => i.VALOR_FECHA_MUESTRA == VALOR_FECHA_MUESTRA.Value);
				}
				if (VALOR_FECHA_RECEPCION.HasValue)
				{
				  q = q.Where(i => i.VALOR_FECHA_RECEPCION == VALOR_FECHA_RECEPCION.Value);
				}
				if (VALOR_FECHA_ENTREGA_RESULTADOS.HasValue)
				{
				  q = q.Where(i => i.VALOR_FECHA_ENTREGA_RESULTADOS == VALOR_FECHA_ENTREGA_RESULTADOS.Value);
				}
				if (VALOR_TOTAL.HasValue)
				{
				  q = q.Where(i => i.VALOR_TOTAL == VALOR_TOTAL.Value);
				}
				if (CARGA_PRESTACIONES_DETALLE_ESTADOId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == CARGA_PRESTACIONES_DETALLE_ESTADOId.Value);
				}
				if (CARGA_PRESTACIONES_ENCABEZADOId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_ENCABEZADO.ID == CARGA_PRESTACIONES_ENCABEZADOId.Value);
				}
				if (CLIENTEId.HasValue)
				{
				  q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (GARANTIA1Id.HasValue)
				{
				  q = q.Where(i => i.GARANTIA1.ID == GARANTIA1Id.Value);
				}
				if (PREVISION1Id.HasValue)
				{
				  q = q.Where(i => i.PREVISION1.ID == PREVISION1Id.Value);
				}
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_HUMANAS_DETALLE> GetByFilterWithReferences(int? CARGA_PRESTACIONES_DETALLE_ESTADOId = null, int? CARGA_PRESTACIONES_ENCABEZADOId = null, int? CLIENTEId = null, int? GARANTIA1Id = null, int? PREVISION1Id = null, System.DateTime? FECHA_ACTUALIZACION = null, string FICHA = "", string NOMBRE = "", string RUT = "", string MEDICO = "", string EDAD = "", string TELEFONO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string MUESTRA = "", string FECHA_RESULTADOS = "", string PREVISION = "", string GARANTIA = "", string PAGADO = "", string PENDIENTE = "", string TOTAL = "", string RECEPCION = "", string MENSAJE_ERROR = "", int? VALOR_FICHA = null, DateTime? VALOR_FECHA_MUESTRA = null, DateTime? VALOR_FECHA_RECEPCION = null, DateTime? VALOR_FECHA_ENTREGA_RESULTADOS = null, int? VALOR_TOTAL = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.CARGA_PRESTACIONES_HUMANAS_DETALLE.Include("CARGA_PRESTACIONES_DETALLE_ESTADO").Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CLIENTE").Include("GARANTIA1").Include("PREVISION1").Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(FICHA))
				{
					q = q.Where(i => i.FICHA.Contains(FICHA));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(RUT))
				{
					q = q.Where(i => i.RUT.Contains(RUT));
				}
				if (!string.IsNullOrEmpty(MEDICO))
				{
					q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
					q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
					q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
					q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (!string.IsNullOrEmpty(FECHA_RECEPCION))
				{
					q = q.Where(i => i.FECHA_RECEPCION.Contains(FECHA_RECEPCION));
				}
				if (!string.IsNullOrEmpty(MUESTRA))
				{
					q = q.Where(i => i.MUESTRA.Contains(MUESTRA));
				}
				if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
				{
					q = q.Where(i => i.FECHA_RESULTADOS.Contains(FECHA_RESULTADOS));
				}
				if (!string.IsNullOrEmpty(PREVISION))
				{
					q = q.Where(i => i.PREVISION.Contains(PREVISION));
				}
				if (!string.IsNullOrEmpty(GARANTIA))
				{
					q = q.Where(i => i.GARANTIA.Contains(GARANTIA));
				}
				if (!string.IsNullOrEmpty(PAGADO))
				{
					q = q.Where(i => i.PAGADO.Contains(PAGADO));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
					q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (!string.IsNullOrEmpty(TOTAL))
				{
					q = q.Where(i => i.TOTAL.Contains(TOTAL));
				}
				if (!string.IsNullOrEmpty(RECEPCION))
				{
					q = q.Where(i => i.RECEPCION.Contains(RECEPCION));
				}
				if (!string.IsNullOrEmpty(MENSAJE_ERROR))
				{
					q = q.Where(i => i.MENSAJE_ERROR.Contains(MENSAJE_ERROR));
				}
				if (FECHA_ACTUALIZACION.HasValue)
				{
					q = q.Where(i => i.FECHA_ACTUALIZACION == FECHA_ACTUALIZACION.Value);
				}
				if (VALOR_FICHA.HasValue)
				{
					q = q.Where(i => i.VALOR_FICHA == VALOR_FICHA.Value);
				}
				if (VALOR_FECHA_MUESTRA.HasValue)
				{
					q = q.Where(i => i.VALOR_FECHA_MUESTRA == VALOR_FECHA_MUESTRA.Value);
				}
				if (VALOR_FECHA_RECEPCION.HasValue)
				{
					q = q.Where(i => i.VALOR_FECHA_RECEPCION == VALOR_FECHA_RECEPCION.Value);
				}
				if (VALOR_FECHA_ENTREGA_RESULTADOS.HasValue)
				{
					q = q.Where(i => i.VALOR_FECHA_ENTREGA_RESULTADOS == VALOR_FECHA_ENTREGA_RESULTADOS.Value);
				}
				if (VALOR_TOTAL.HasValue)
				{
					q = q.Where(i => i.VALOR_TOTAL == VALOR_TOTAL.Value);
				}
				if (CARGA_PRESTACIONES_DETALLE_ESTADOId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == CARGA_PRESTACIONES_DETALLE_ESTADOId.Value);
				}
				if (CARGA_PRESTACIONES_ENCABEZADOId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_ENCABEZADO.ID == CARGA_PRESTACIONES_ENCABEZADOId.Value);
				}
				if (CLIENTEId.HasValue)
				{
					q = q.Where(i => i.CLIENTE.ID == CLIENTEId.Value);
				}
				if (GARANTIA1Id.HasValue)
				{
					q = q.Where(i => i.GARANTIA1.ID == GARANTIA1Id.Value);
				}
				if (PREVISION1Id.HasValue)
				{
					q = q.Where(i => i.PREVISION1.ID == PREVISION1Id.Value);
				}
				return q;
			}
			catch (Exception ex)
			{
				ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
			}
		}
	}
}
