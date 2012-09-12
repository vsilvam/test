using System;

namespace LQCE.Transaccion.DTO
{
    public class DTO_RESUMEN_FACTURACION
    {
        public int ID_FACTURACION { get; set; }
        public int ID_TIPO_FACTURA { get; set; }
        public string NOMBRE_TIPO_FACTURA { get; set; }
        public DateTime FECHA_FACTURACION { get; set; }
        public int TOTAL_FACTURAS { get; set; }
        public int TOTAL_FACTURAS_POR_NUMERAR { get; set; }
    }
}
