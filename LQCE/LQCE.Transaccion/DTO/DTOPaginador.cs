using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LQCE.Transaccion.DTO
{
    public abstract class DTOPaginador
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DTOPaginador()
        {
            PageIndex = 1;
            PageSize = 10;
        }
    }
}
