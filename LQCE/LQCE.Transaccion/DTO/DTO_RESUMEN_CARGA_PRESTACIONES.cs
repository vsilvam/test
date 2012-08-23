using System;

namespace LQCE.Transaccion.DTO
{
    public class DTO_RESUMEN_CARGA_PRESTACIONES
    {
        public int ID { get; set; }
        public string ARCHIVO { get; set; }
        public DateTime FECHA_CARGA { get; set; }
        public DateTime FECHA_ACTUALIZACION { get; set; }
        public int ID_ESTADO { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public int TOTAL_REGISTROS { get; set; }
        public int REGISTROS_VALIDADOS { get; set; }
        public int REGISTROS_CON_ERRORES { get; set; }
    }
}
