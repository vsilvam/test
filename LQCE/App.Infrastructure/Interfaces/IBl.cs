using System.Collections.Generic;

namespace App.Infrastructure.Interfaces
{
    public interface IBl
    {
        /// <summary>
        /// Propiedad que contiene el error actual de la instancia de acceso a datos.
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Propiedad que indica si el método actual se ejecuto satisfactoriamente.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Realiza la creación o actualización de un registro según su id único.
        /// </summary>
        /// <param name="dto">Dto a persistir.</param>
        /// <returns></returns>
        int Save(object dto);

        /// <summary>
        /// Obtiene un registro en base a su id único, retornando un dto específico.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns></returns>
        object GetById(int id);

        /// <summary>
        /// Elimina un registro en base a su id.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Busca todos los registros que coinciden con los campos del dto de búsqueda, retornando una lista de dto específicos.
        /// </summary>
        /// <param name="dto">Dto de la clase concreta  a buscar.</param>
        /// <returns></returns>
        IList<object> Find(object dto);
    }
}
