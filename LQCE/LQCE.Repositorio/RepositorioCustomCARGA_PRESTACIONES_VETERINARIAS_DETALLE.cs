using System;
using System.Linq;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
    public partial class RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE
    {
        public CARGA_PRESTACIONES_VETERINARIAS_DETALLE GetByIdWithReferences2(int id)
        {
            Error = string.Empty;
            try
            {
                return _context.CARGA_PRESTACIONES_VETERINARIAS_DETALLE                    
                    .Include("CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO")
                    .Include("CARGA_PRESTACIONES_HUMANAS_EXAMEN")
                    .Include("CARGA_PRESTACIONES_DETALLE_ESTADO")
                    .Include("CLIENTE")
                    .Include("GARANTIA1")
                    .Include("PREVISION1")
                    .FirstOrDefault(i => i.ID == id && i.ACTIVO);
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
