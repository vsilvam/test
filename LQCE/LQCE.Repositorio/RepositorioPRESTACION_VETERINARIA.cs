using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
	public partial class RepositorioPRESTACION_VETERINARIA
	{
		private LQCEEntities _context;
		public string Error { get; private set; }
		
		public RepositorioPRESTACION_VETERINARIA(LQCEEntities Context)
		{
			Error = string.Empty;
			this._context = Context;
		}

		public PRESTACION_VETERINARIA GetById(int id)
		{
			Error = string.Empty;
			try
			{
							return _context.PRESTACION_VETERINARIA.FirstOrDefault(i => i.ID == id && i.ACTIVO );
						}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public PRESTACION_VETERINARIA GetByIdWithReferences(int id)
		{
			Error = string.Empty;
			try
			{
				
							return _context.PRESTACION_VETERINARIA.Include("ESPECIE").Include("PRESTACION").Include("RAZA").FirstOrDefault(i => i.ID == id && i.ACTIVO );
			
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_VETERINARIA> GetAll()
		{
			Error = string.Empty;
			try
			{
				
							var q = from i in _context.PRESTACION_VETERINARIA where i.ACTIVO select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_VETERINARIA> GetAllWithReferences()
		{
			Error = string.Empty;
			try
			{
				
								var q = from i in _context.PRESTACION_VETERINARIA.Include("ESPECIE").Include("PRESTACION").Include("RAZA") where i.ACTIVO  select i;
							return q;
			}
			catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
		}

		public IQueryable<PRESTACION_VETERINARIA> GetByFilter(int? ESPECIEId = null, int? RAZAId = null, string NOMBRE = "", string EDAD = "", string TELEFONO = "", string SEXO = "", string SOLICITANTE = "", string PROCEDENCIA = "", int? FICHA_CLINICA = null)
		{
			Error = string.Empty;
			try
			{
							var q = from i in _context.PRESTACION_VETERINARIA  where i.ACTIVO  select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
				   q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
				   q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
				   q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(SEXO))
				{
				   q = q.Where(i => i.SEXO.Contains(SEXO));
				}
				if (!string.IsNullOrEmpty(SOLICITANTE))
				{
				   q = q.Where(i => i.SOLICITANTE.Contains(SOLICITANTE));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
				   q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (FICHA_CLINICA.HasValue)
				{
				  q = q.Where(i => i.FICHA_CLINICA == FICHA_CLINICA.Value);
				}
				if (ESPECIEId.HasValue)
				{
				  q = q.Where(i => i.ESPECIE.ID == ESPECIEId.Value);
				}
				if (RAZAId.HasValue)
				{
				  q = q.Where(i => i.RAZA.ID == RAZAId.Value);
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

		public IQueryable<PRESTACION_VETERINARIA> GetByFilterWithReferences(int? ESPECIEId = null, int? RAZAId = null, string NOMBRE = "", string EDAD = "", string TELEFONO = "", string SEXO = "", string SOLICITANTE = "", string PROCEDENCIA = "", int? FICHA_CLINICA = null)
		{
			Error = string.Empty;
			try
			{

							var q = from i in _context.PRESTACION_VETERINARIA.Include("ESPECIE").Include("PRESTACION").Include("RAZA")  where i.ACTIVO select i;
			
				

				if (!string.IsNullOrEmpty(NOMBRE))
				{
					q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
				}
				if (!string.IsNullOrEmpty(EDAD))
				{
					q = q.Where(i => i.EDAD.Contains(EDAD));
				}
				if (!string.IsNullOrEmpty(TELEFONO))
				{
					q = q.Where(i => i.TELEFONO.Contains(TELEFONO));
				}
				if (!string.IsNullOrEmpty(SEXO))
				{
					q = q.Where(i => i.SEXO.Contains(SEXO));
				}
				if (!string.IsNullOrEmpty(SOLICITANTE))
				{
					q = q.Where(i => i.SOLICITANTE.Contains(SOLICITANTE));
				}
				if (!string.IsNullOrEmpty(PROCEDENCIA))
				{
					q = q.Where(i => i.PROCEDENCIA.Contains(PROCEDENCIA));
				}
				if (FICHA_CLINICA.HasValue)
				{
					q = q.Where(i => i.FICHA_CLINICA == FICHA_CLINICA.Value);
				}
				if (ESPECIEId.HasValue)
				{
					q = q.Where(i => i.ESPECIE.ID == ESPECIEId.Value);
				}
				if (RAZAId.HasValue)
				{
					q = q.Where(i => i.RAZA.ID == RAZAId.Value);
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
