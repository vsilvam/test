using System;
using System.Data;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;

namespace LQCE.Repositorio
{
	public partial class RepositorioPAGO_DETALLE
	{
		//Contexto del entity model
        private readonly LQCEEntities _contextBd;

        #region Manejo del estado de la instancia

        /// <summary>
        /// Propiedad que contiene el error actual de la instancia de acceso a datos.
        /// </summary>
        public string Error { get; private set; }

        public RepositorioPAGO_DETALLE(LQCEEntities context)
        {
            Error = string.Empty;
            _contextBd = context;
        }

        #endregion

        #region Metodos CRUD Autogenerados - NO MODIFICAR
		

        /// <summary>
        /// Obtiene el registro desde BD en base a su key.
        /// </summary
        /// <param name="id">key.</param>
        /// <returns></returns>
        public PAGO_DETALLE GetById(int id)
        {
            Error = string.Empty;
            try
            {
                return _contextBd.PAGO_DETALLE
					.Include("FACTURA_DETALLE")				
					.Include("PAGO")				
					.FirstOrDefault(i => i.ID == id);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Busca todos los registros.
        /// </summary>
        /// <returns></returns>
        public IQueryable<PAGO_DETALLE> GetAll()
        {
            Error = string.Empty;
            try
            {
                return from i in _contextBd.PAGO_DETALLE select i;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro en BD, retornando su id.
        /// </summary>
        /// <param name="entity">Nueva entidad.</param>
        /// <returns></returns>
        public int Insert(PAGO_DETALLE entity)
        {
            Error = string.Empty;
            try
            {
                _contextBd.AddToPAGO_DETALLE(entity);
                _contextBd.SaveChanges();
                return entity.ID;
            }
            catch (NullReferenceException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return 0;
            }
            catch (UpdateException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return 0;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// Actualiza el registro en BD.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        /// <returns></returns>
        public bool Update(PAGO_DETALLE entity)
        {
            Error = string.Empty;
            try
            {
                _contextBd.ApplyPropertyChanges("PAGO_DETALLE", entity);
                return _contextBd.SaveChanges() > 0;
            }
            catch (NullReferenceException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return false;
            }
            catch (UpdateException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return false;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return false;
            }

        }

        #endregion
	}
}
