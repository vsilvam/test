using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
    public partial class TrxCARGA_PRESTACIONES_HUMANAS_DETALLE
    {
        public CARGA_PRESTACIONES_HUMANAS_DETALLE GetByIdWithReferencesFull(int ID)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE repositorio = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    return repositorio.GetByIdWithReferencesFull(ID);
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
