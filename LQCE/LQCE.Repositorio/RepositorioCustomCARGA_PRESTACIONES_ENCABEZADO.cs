using System;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;

namespace LQCE.Repositorio
{
    public partial class RepositorioCARGA_PRESTACIONES_ENCABEZADO
    {
        public CARGA_PRESTACIONES_ENCABEZADO GetByIdWithReferencesFull(int id)
        {
            Error = string.Empty;
            try
            {

                return _context.CARGA_PRESTACIONES_ENCABEZADO
                    .Include("CARGA_PRESTACIONES_HUMANAS_DETALLE")
                    .Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")
                    .Include("CARGA_PRESTACIONES_HUMANAS_DETALLE.CARGA_PRESTACIONES_HUMANAS_EXAMEN")
                    .Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN")
                    .Include("CARGA_PRESTACIONES_HUMANAS_DETALLE.CARGA_PRESTACIONES_DETALLE_ESTADO")
                    .Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CARGA_PRESTACIONES_DETALLE_ESTADO")
                    .Include("CARGA_PRESTACIONES_ESTADO")
                    .Include("TIPO_PRESTACION").FirstOrDefault(i => i.ID == id);

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
