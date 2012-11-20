
namespace LQCE.Transaccion.DTO
{
    public class DTO_REPORTE_DETALLEFACTURA_FACTURA
    {
        public int ID_FACTURA { get; set; }
        public int ID_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string DETALLE { get; set; }
        public int SUMA_PENDIENTE { get; set; }


        //public List<DTO_REPORTE_DETALLEFACTURA_PRESTACION> DETALLE { get; set; }

        //public DTO_REPORTE_DETALLEFACTURA_FACTURA()
        //{
        //    DETALLE = new List<DTO_REPORTE_DETALLEFACTURA_PRESTACION>
        //}
    }
}
