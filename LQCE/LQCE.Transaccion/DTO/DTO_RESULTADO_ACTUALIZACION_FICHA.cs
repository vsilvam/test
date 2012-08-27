using System.Collections.Generic;

namespace LQCE.Transaccion.DTO
{
    public class DTO_RESULTADO_ACTUALIZACION_FICHA
    {
        public bool RESULTADO { get; set; }
        public List<string> ERRORES_VALIDACION { get; set; }

        public DTO_RESULTADO_ACTUALIZACION_FICHA()
        {
            ERRORES_VALIDACION = new List<string>();
        }
    }
}
