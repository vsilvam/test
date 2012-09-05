using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTO_RESUMEN_PRESTACIONES_FACTURAR
    {
        public int ID_CLIENTE { get; set; }
        public string RUT_CLIENTE {get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public int CANTIDAD_PRESTACIONES { get; set; }
        public int TOTAL_PRESTACIONES { get; set; }
        public int DESCUENTO { get; set; }
        public int TOTAL_FACTURA { get; set; }
    }
}
