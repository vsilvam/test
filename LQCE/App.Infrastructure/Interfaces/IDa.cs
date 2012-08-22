using System.Collections.Generic;

namespace App.Infrastructure.Interfaces
{
    public interface IDa
    {
        /// <summary>
        /// Propiedad que contiene el error actual de la instancia de acceso a datos.
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Inserta un nuevo registro en BD, retornando su id.
        /// </summary>
        /// <param name="dto">Dto concreto a persistir.</param>
        /// <returns></returns>
        int Insert(object dto);

        /// <summary>
        /// Actualiza el registro en BD, retornando el estado de la operación.
        /// </summary>
        /// <param name="dto">Dto concreto a actualizar.</param>
        /// <returns></returns>
        bool Update(object dto);

        /// <summary>
        /// Obtiene el registro desde BD en base a su id, retornado un dto concreto.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns></returns>
        object GetById(int id);

        /// <summary>
        /// Busca todos los registros que coinciden con los campos del dto de búsqueda, retornado una lista de dto específicos.
        /// </summary>
        /// <param name="dto">Dto de la clase concreta  a buscar.</param>
        /// <returns></returns>
        IList<object> Find(object dto);
    }
}
