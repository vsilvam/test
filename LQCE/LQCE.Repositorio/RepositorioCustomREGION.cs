using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
    public partial class RepositorioREGION
    {
        public IQueryable<REGION> GetByFilter(int ID, bool ACTIVO)
        {
            Error = string.Empty;
            try
            {
                var q = from i in _context.REGION where i.ID == ID && i.ACTIVO == ACTIVO select i;
                return q;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }
    }
}
