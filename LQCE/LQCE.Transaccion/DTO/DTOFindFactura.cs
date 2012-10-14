using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTOFindFactura : DTOPaginador
    {
        public int? ID_FACTURACION { get; set; }
        public int? ID_TIPO_FACTURA { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public DateTime? FECHA_FACTURACION { get; set; }
        public int? NUMERO_FACTURA { get; set; }
        public bool? ESTADO_FACTURA { get; set; }
    }
}
