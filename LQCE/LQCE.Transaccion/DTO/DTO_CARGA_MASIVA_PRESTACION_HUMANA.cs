using System.Collections.Generic;

namespace LQCE.Transaccion.DTO
{
    public class DTO_CARGA_MASIVA_PRESTACION_HUMANA
    {
        public string ORIGINAL_FICHA {get; set; }
        public int? FICHA { get; set; }
        public string NOMBRE {get; set; }
        public string RUT {get; set; }
        public string MEDICO {get; set; }
        public string EDAD {get; set; }
        public string TELEFONO {get; set; }
        public string PROCEDENCIA {get; set; }
        public string FECHA_RECEPCION {get; set; }
        public string MUESTRA {get; set; }
        public string FECHA_RESULTADOS {get; set; }
        public string PREVISION {get; set; }
        public string GARANTIA {get; set; }
        public string ORIGINAL_PAGADO {get; set; }
        public string ORIGINAL_PENDIENTE { get; set; }
        public string ORIGINAL_TOTAL { get; set; }
        public int? PAGADO { get; set; }
        public int? PENDIENTE { get; set; }
        public int? TOTAL { get; set; }
        public List<DTO_CARGA_MASIVA_PRESTACION_HUMANA_EXAMEN> EXAMENES { get; set; }

        public DTO_CARGA_MASIVA_PRESTACION_HUMANA()
        {
            EXAMENES = new List<DTO_CARGA_MASIVA_PRESTACION_HUMANA_EXAMEN>();
        }
    }

    public class DTO_CARGA_MASIVA_PRESTACION_HUMANA_EXAMEN
    {
        public string EXAMEN { get; set; }
        public string ORIGINAL_VALOR { get; set; }
        public int? VALOR { get; set; }
    }
}
