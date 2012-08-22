using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Repositorio;
using LQCE.Modelo;
using LQCE.Transaccion.DTO;

namespace LQCE.Transaccion
{
	public partial class TrxPRESTACION
	{
		#region Manejo del estado de la instancia

		/// <summary>
		/// Propiedad que contiene el error actual de la instancia de negocio.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Propiedad que indica si el metodo actual se ejecuto satisfactoriamente.
		/// </summary>
		public bool Success { get; private set; }

		public TrxPRESTACION()
		{
		    Init();
		}

		private void Init()
		{
		    Error = string.Empty;
		    Success = false;
		}

		#endregion

		#region Metodos Autogenerados
		
			/// <summary>
	      /// Obtiene un registro en base a su key.
	      /// </summary>
	      /// <param name="id">key.</param>
	      /// <returns></returns>
		public PRESTACION GetById(int id)
		{
			Init();

			using (var context = new LQCEEntities())
			{
				var dato = new RepositorioPRESTACION(context);
				var entity = dato.GetById(id);

				//Se procesa el resultado de la operacion.
				Error = dato.Error;
				Success = entity != null;

				return entity;
			}
		}

	  	/// <summary>
      /// Busca todos los registros activos.
      /// </summary>
      /// <returns></returns>
      public IList<PRESTACION> GetAll()
      {
          Init();

          using (var context = new LQCEEntities())
          {
              var dato = new RepositorioPRESTACION(context);
              var q = dato.GetAll();
              q = q.Where(i => i.ACTIVO);

            try
            {
              //Se procesa el resultado de la operacion.
              var list = q.ToList();
              Error = dato.Error;
              Success = true;

              return list;
            }
            catch (ArgumentNullException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
          }
      }

      /// <summary>
      /// Busca todos los registros que coinciden con los campos del dto de busqueda.
      /// </summary>
      /// <param name="dto">Dto con parametros de busqueda.</param>
      /// <returns></returns>
      public IList<PRESTACION> Find(DTO_PRESTACION dto)
      {
          Init();

          using (var context = new LQCEEntities())
          {
              var dato = new RepositorioPRESTACION(context);
              var q = dato.GetAll();
              if (dto != null)
              {
					if (dto.ID != null)
						q = q.Where(i => i.ID  == dto.ID);		
					if (dto.MEDICO != null)
						q = q.Where(i => i.MEDICO.Contains(dto.MEDICO));					
					if (dto.FECHA_MUESTRA != null)
						q = q.Where(i => i.FECHA_MUESTRA  == dto.FECHA_MUESTRA);		
					if (dto.FECHA_RECEPCION != null)
						q = q.Where(i => i.FECHA_RECEPCION  == dto.FECHA_RECEPCION);		
					if (dto.FECHA_ENTREGA_RESULTADOS != null)
						q = q.Where(i => i.FECHA_ENTREGA_RESULTADOS  == dto.FECHA_ENTREGA_RESULTADOS);		
					if (dto.ACTIVO != null)
						q = q.Where(i => i.ACTIVO  == dto.ACTIVO);		
					if (dto.ID_CLIENTE != null)
						q = q.Where(i => i.CLIENTE.ID == dto.ID_CLIENTE);				
					if (dto.ID_GARANTIA != null)
						q = q.Where(i => i.GARANTIA.ID == dto.ID_GARANTIA);				
					if (dto.ID_PREVISION != null)
						q = q.Where(i => i.PREVISION.ID == dto.ID_PREVISION);				
					if (dto.ID_TIPO_PRESTACION != null)
						q = q.Where(i => i.TIPO_PRESTACION.ID == dto.ID_TIPO_PRESTACION);				
              }

            try
            {
              //Se procesa el resultado de la operacion.
              var list = q.ToList();
              Error = dato.Error;
              Success = true;

              return list;
            }
            catch (ArgumentNullException ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
          }
      }

      /// <summary>
      /// Crea o actualiza un registro en la base de datos dependiendo de su key.
      /// </summary>
      /// <param name="entity">Entidad a persistir.</param>
      /// <returns></returns>
      public int Save(PRESTACION entity)
      {
          Init();

          if (entity == null)
          {
              Error = "ArgumentNullException. La entidad a persistir 'PRESTACION' no puede ser nula.";
              ISException.RegisterExcepcion(Error);
              return 0;
          }

          using (var context = new LQCEEntities())
          {
              var dato = new RepositorioPRESTACION(context);
              var oldEntity = dato.GetById(entity.ID);
              //Dependiendo de su key, el registro se crea o actualiza.
              if (oldEntity == null)
              {
                  entity.ACTIVO = true;
                  var id = dato.Insert(entity);
                  Success = id > 0;
                  Error = dato.Error;
                  return id;
              }

              oldEntity.MEDICO = entity.MEDICO;				
              oldEntity.FECHA_MUESTRA = entity.FECHA_MUESTRA;				
              oldEntity.FECHA_RECEPCION = entity.FECHA_RECEPCION;				
              oldEntity.FECHA_ENTREGA_RESULTADOS = entity.FECHA_ENTREGA_RESULTADOS;				
              Success = dato.Update(oldEntity);
              Error = dato.Error;
              return Success ? oldEntity.ID : 0;
          }
      }

      /// <summary>
      /// Elimina un registro en base a su key.
      /// </summary>
      /// <param name="id">key.</param>
      /// <returns></returns>
      public bool Delete(int id)
      {
          Init();

          using (var context = new LQCEEntities())
          {
              var dato = new RepositorioPRESTACION(context);
              var entity = dato.GetById(id);

              //Se procesa el resultado de la operacion.
              if (entity == null)
              {
                  Error = String.Format("Registro '{0}' en 'PRESTACION' no encontrado. {1}", id, dato.Error);
                  ISException.RegisterExcepcion(Error);
                  return false;
              }

              //Eliminacion logica.
               entity.ACTIVO = false;
              //Se procesa el resultado de la operacion.
              Success = dato.Update(entity);
              Error = dato.Error;

              return Success;
          }
      }

      #endregion 	
	}
}
