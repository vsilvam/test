using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTO_PAGO_PRESTACIONES
    {
        public int ID_FACTURA { get; set; }
        public int ID_FACTURA_DETALLE { get; set; }
        public int? NUMERO_FACTURA { get; set; }
        public string PRESTACION { get; set; }
        public string EXAMEN { get; set; }
        public int VALOR_EXAMEN { get; set; }
        public DateTime FECHA_RECEPCION { get; set; }
        public string NOMBRE_PACIENTE { get; set; }
    }
}
