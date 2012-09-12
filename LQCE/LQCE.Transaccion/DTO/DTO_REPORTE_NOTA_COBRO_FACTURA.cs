using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTO_REPORTE_NOTA_COBRO_FACTURA
    {
        public int ID_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }

        public int ID_FACTURA { get; set; }
        public DateTime FECHA_FACTURACION { get; set; }
        public int NUMERO_FACTURA { get; set; }
        public int MONTO_TOTAL { get; set; }
        public int MONTO_PENDIENTE { get; set; }
    }
}
