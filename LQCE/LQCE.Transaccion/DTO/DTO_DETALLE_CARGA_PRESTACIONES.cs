using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
   public  class DTO_DETALLE_CARGA_PRESTACIONES
    {
       public int ID { get; set; }
       public int ID_TIPO_PRESTACION { get; set; }
       public string NUMERO_FICHA { get; set; }
       public string NOMBRE { get; set; }
       public int ID_ESTADO_DETALLE { get; set; }
       public string NOMBRE_ESTADO_DETALLE { get; set; }
       public string PROCEDENCIA { get; set; }
       public string FECHA_RECEPCION { get; set; }
    }
}
