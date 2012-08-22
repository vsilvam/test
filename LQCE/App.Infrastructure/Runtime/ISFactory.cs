using System;

namespace App.Infrastructure.Runtime
{
    public class ISFactory
    {
        /// <summary>
        /// Crea una nueva instanacia del tipo especificado
        /// </summary>
        /// <param name="typeName">Assembly.ClassName, Version, Culture, PublicKeyToken</param>
        /// <returns></returns>
        public static object GetObject(string typeName)
        {
            try
            {
                return Activator.CreateInstance(Type.GetType(typeName));
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                throw;
            }

        }
    }
}
