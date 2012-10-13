using System;

namespace LQCE.Transaccion.DTO
{
    public class DTO_RESUMEN_FACTURA
    {
        public int? NUMERO_FACTURA { get; set; }
        public int ID_FACTURA { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public int VALOR_TOTAL { get; set; }
        public int VALOR_PAGADO { get; set; }
        public int PAGOS_REGISTRADOS { get; set; }
        public int SALDO_DEUDOR { get; set; }
        public bool PAGADA { get; set; }
    }
}
