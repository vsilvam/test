using System;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;

namespace LQCE.Repositorio
{
    public partial class RepositorioCLIENTE
    {
        public CLIENTE GetByIdWithFullReferences(int id)
        {
            Error = string.Empty;
            try
            {

                return _context.CLIENTE
                    .Include("COMUNA.REGION")
                    .Include("CONVENIO")
                    .Include("CLIENTE_SINONIMO")
                    .Include("TIPO_PRESTACION")
                    .Include("PAGO")
                    .Include("TIPO_FACTURA")
                    .Include("NOTA_COBRO")
                    .Include("FACTURA")
                    .Include("CARGA_PRESTACIONES_HUMANAS_DETALLE")
                    .Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")
                    .Include("PRESTACION")
                    .FirstOrDefault(i => i.ID == id && i.ACTIVO);

            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public IQueryable<CLIENTE> GetByFilterWithFullReferences(string RUT, string NOMBRE, 
            int? ID_REGION, int? ID_COMUNA,
            int? ID_TIPO_PRESTACION, int? ID_CONVENIO)
        {
            Error = string.Empty;
            try
            {
                var q = from i in _context.CLIENTE
                            .Include("COMUNA.REGION")
                            .Include("CONVENIO")
                            .Include("CLIENTE_SINONIMO")
                            .Include("TIPO_PRESTACION")
                            .Include("PAGO")
                            .Include("TIPO_FACTURA")
                            .Include("NOTA_COBRO")
                            .Include("FACTURA")
                            .Include("CARGA_PRESTACIONES_HUMANAS_DETALLE")
                            .Include("CARGA_PRESTACIONES_VETERINARIAS_DETALLE")
                            .Include("PRESTACION") 
                        where i.ACTIVO select i;

                if (!string.IsNullOrEmpty(RUT))
                {
                    q = q.Where(i => i.RUT.Contains(RUT));
                }
                if (!string.IsNullOrEmpty(NOMBRE))
                {
                    q = q.Where(i => i.NOMBRE.Contains(NOMBRE));
                }
                if (ID_REGION.HasValue)
                {
                    q = q.Where(i => i.COMUNA.REGION.ID == ID_REGION.Value);
                }
                if (ID_COMUNA.HasValue)
                {
                    q = q.Where(i => i.COMUNA.ID == ID_COMUNA.Value);
                }
                if (ID_CONVENIO.HasValue)
                {
                    q = q.Where(i => i.CONVENIO.ID == ID_CONVENIO.Value);
                }
                if (ID_TIPO_PRESTACION.HasValue)
                {
                    q = q.Where(i => i.TIPO_PRESTACION.ID == ID_TIPO_PRESTACION.Value);
                }
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
