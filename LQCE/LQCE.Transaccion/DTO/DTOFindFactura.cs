using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTOFindFactura : DTOPaginador
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public DateTime? fecha { get; set; }
        public int? numero { get; set; }
        public bool? estado { get; set; }
    }
}
