
using System;
namespace LQCE.Transaccion.DTO
{
    public class DTO_REPORTE_DETALLEFACTURA_PRESTACION
    {
        //// Encabezado
        public int ID_FACTURA { get; set; }
        public int ID_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }

        // Detalle
        public int ID_FACTURA_DETALLE { get; set; }
        public int NUMERO_FICHA { get; set; }
        public int MONTO_TOTAL { get; set; }
        public DateTime FECHA_RECEPCION { get; set; }
        public string NOMBRE { get; set; }
    }
}
