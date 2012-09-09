using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public class DTOFindPrestaciones : DTOPaginador
    {
        public int id { get; set; }
        public string numero { get; set; }
        public string nombre { get; set; }
        public int? estado { get; set; }
        public string prodedencia { get; set; }
        public DateTime? fechaRecepcion { get; set; }
    }
}
