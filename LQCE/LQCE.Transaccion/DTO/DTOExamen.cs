using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LQCE.Modelo;

namespace LQCE.Transaccion.DTO
{
    public class DTOExamen
    {
        public string NOMBRE_EXAMEN { get; set; }
        public int ID { get; set; }
        public string VALOR_EXAMEN { get; set; }

        public DTOExamen()
        {
        }

        public DTOExamen(CARGA_PRESTACIONES_HUMANAS_EXAMEN objExamen)
        {
            NOMBRE_EXAMEN = objExamen.NOMBRE_EXAMEN;
            ID = objExamen.ID;
            VALOR_EXAMEN = objExamen.VALOR_EXAMEN;
        }

        public DTOExamen(CARGA_PRESTACIONES_VETERINARIAS_EXAMEN objExamen)
        {
            NOMBRE_EXAMEN = objExamen.NOMBRE_EXAMEN;
            ID = objExamen.ID;
            VALOR_EXAMEN = objExamen.VALOR_EXAMEN;
        }
    }
}
