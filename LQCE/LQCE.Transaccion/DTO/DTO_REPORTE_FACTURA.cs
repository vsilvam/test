
namespace LQCE.Transaccion.DTO
{
    public class DTO_REPORTE_FACTURA
    {
        public string NOMBRE_REPORTE_FACTURA { get; set; }
        public string NOMBRE_REPORTE_FACTURA_INDIVIDUAL { get; set; }
        public int DIA { get; set; }
        public string MES { get; set; }
        public int AÑO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string DIRECCION { get; set; }
        public string COMUNA { get; set; }
        public string FONO { get; set; }
        public string GIRO {get; set; }
        public string DETALLE { get; set; }
        public int NETO { get; set; }
        public int IVA { get; set; }
        public int TOTAL {get; set; }
        public int? NUMERO_FACTURA { get; set; }
        public string NOMBRE_TIPO_FACTURA { get; set; }
    }
}
