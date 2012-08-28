using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
    public partial class TrxCARGA_PRESTACIONES_VETERINARIAS_DETALLE
    {
        public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetByIdWithReferences2(int ID)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    return repositorio.GetByIdWithReferences2(ID);
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }
    }
}
