using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetById(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				return _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN").FirstOrDefault(i => i.ID == id);
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetAll()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN") select i;
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilter(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE select i;

				if (!string.IsNullOrEmpty(FICHA))
				{
				   q = q.Where(i => i.FICHA.Contains(FICHA));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(ESPECIE))
				{
				   q = q.Where(i => i.ESPECIE.Contains(ESPECIE));
				}
				if (!string.IsNullOrEmpty(RAZA))
				{
				   q = q.Where(i => i.RAZA.Contains(RAZA));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
				   q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(SEXO))
				{
				   q = q.Where(i => i.SEXO.Contains(SEXO));
				}
				if (!string.IsNullOrEmpty(SOLICITA))
				{
				   q = q.Where(i => i.SOLICITA.Contains(SOLICITA));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
				   q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(MEDICO))
				{
				   q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
				   q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (!string.IsNullOrEmpty(FECHA_RECEPCION))
				{
				   q = q.Where(i => i.FECHA_RECEPCION.Contains(FECHA_RECEPCION));
				}
				if (!string.IsNullOrEmpty(FECHA_MUESTRA))
				{
				   q = q.Where(i => i.FECHA_MUESTRA.Contains(FECHA_MUESTRA));
				}
				if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
				{
				   q = q.Where(i => i.FECHA_RESULTADOS.Contains(FECHA_RESULTADOS));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
				   q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (!string.IsNullOrEmpty(GARANTIA))
				{
				   q = q.Where(i => i.GARANTIA.Contains(GARANTIA));
				}
				if (!string.IsNullOrEmpty(PAGADO))
				{
				   q = q.Where(i => i.PAGADO.Contains(PAGADO));
				}
				if (!string.IsNullOrEmpty(TOTAL))
				{
				   q = q.Where(i => i.TOTAL.Contains(TOTAL));
				}
				if (ACTIVO.HasValue)
				{
				  q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (VALIDADO.HasValue)
				{
				  q = q.Where(i => i.VALIDADO == VALIDADO.Value);
				}
				if (ERROR.HasValue)
				{
				  q = q.Where(i => i.ERROR == ERROR.Value);
				}
				if (!string.IsNullOrEmpty(MENSAJE_ERROR))
				{
				   q = q.Where(i => i.MENSAJE_ERROR.Contains(MENSAJE_ERROR));
				}
				if (CARGA_PRESTACIONES_ENCABEZADOId.HasValue)
				{
				  q = q.Where(i => i.CARGA_PRESTACIONES_ENCABEZADO.ID == CARGA_PRESTACIONES_ENCABEZADOId.Value);
				}
				return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
		}

		public IQueryable<CARGA_PRESTACIONES_VETERINARIAS_DETALLE> GetByFilterWithReferences(int? CARGA_PRESTACIONES_ENCABEZADOId = null, bool? ACTIVO = null, bool? VALIDADO = null, bool? ERROR = null, string FICHA = "", string NOMBRE = "", string ESPECIE = "", string RAZA = "", string EDAD = "", string SEXO = "", string SOLICITA = "", string TELEFONO = "", string MEDICO = "", string PROCEDENCIA = "", string FECHA_RECEPCION = "", string FECHA_MUESTRA = "", string FECHA_RESULTADOS = "", string PENDIENTE = "", string GARANTIA = "", string PAGADO = "", string TOTAL = "", string MENSAJE_ERROR = "")
		{
			Error = string.Empty;
			try
			{
				var q = from i in _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Include("CARGA_PRESTACIONES_ENCABEZADO").Include("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN") select i;

				if (!string.IsNullOrEmpty(FICHA))
				{
					q = q.Where(i => i.FICHA.Contains(FICHA));
				}
				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(ESPECIE))
				{
					q = q.Where(i => i.ESPECIE.Contains(ESPECIE));
				}
				if (!string.IsNullOrEmpty(RAZA))
				{
					q = q.Where(i => i.RAZA.Contains(RAZA));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
					q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(SEXO))
				{
					q = q.Where(i => i.SEXO.Contains(SEXO));
				}
				if (!string.IsNullOrEmpty(SOLICITA))
				{
					q = q.Where(i => i.SOLICITA.Contains(SOLICITA));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
					q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(MEDICO))
				{
					q = q.Where(i => i.MEDICO.Contains(MEDICO));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
					q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (!string.IsNullOrEmpty(FECHA_RECEPCION))
				{
					q = q.Where(i => i.FECHA_RECEPCION.Contains(FECHA_RECEPCION));
				}
				if (!string.IsNullOrEmpty(FECHA_MUESTRA))
				{
					q = q.Where(i => i.FECHA_MUESTRA.Contains(FECHA_MUESTRA));
				}
				if (!string.IsNullOrEmpty(FECHA_RESULTADOS))
				{
					q = q.Where(i => i.FECHA_RESULTADOS.Contains(FECHA_RESULTADOS));
				}
				if (!string.IsNullOrEmpty(PENDIENTE))
				{
					q = q.Where(i => i.PENDIENTE.Contains(PENDIENTE));
				}
				if (!string.IsNullOrEmpty(GARANTIA))
				{
					q = q.Where(i => i.GARANTIA.Contains(GARANTIA));
				}
				if (!string.IsNullOrEmpty(PAGADO))
				{
					q = q.Where(i => i.PAGADO.Contains(PAGADO));
				}
				if (!string.IsNullOrEmpty(TOTAL))
				{
					q = q.Where(i => i.TOTAL.Contains(TOTAL));
				}
				if (ACTIVO.HasValue)
				{
					q = q.Where(i => i.ACTIVO == ACTIVO.Value);
				}
				if (VALIDADO.HasValue)
				{
					q = q.Where(i => i.VALIDADO == VALIDADO.Value);
				}
				if (ERROR.HasValue)
				{
					q = q.Where(i => i.ERROR == ERROR.Value);
				}
				if (!string.IsNullOrEmpty(MENSAJE_ERROR))
				{
					q = q.Where(i => i.MENSAJE_ERROR.Contains(MENSAJE_ERROR));
				}
				if (CARGA_PRESTACIONES_ENCABEZADOId.HasValue)
				{
					q = q.Where(i => i.CARGA_PRESTACIONES_ENCABEZADO.ID == CARGA_PRESTACIONES_ENCABEZADOId.Value);
				}
				return q;
			}
			catch (Exception ex)
			{
				ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
			}
		}
	}
}
