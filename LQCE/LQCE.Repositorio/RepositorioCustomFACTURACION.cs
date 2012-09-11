using System;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;

namespace LQCE.Repositorio
{
    public partial class RepositorioFACTURACION
    {
        public FACTURACION GetByIdWithReferencesFull(int ID)
        {
            Error = string.Empty;
            try
            {
                return _context.FACTURACION
                    .Include("FACTURA.TIPO_FACTURA")
                    .FirstOrDefault(i => i.ID == ID);
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetPrestacionesPorFacturar(DateTime FechaDesde,
            DateTime FechaHasta, int? IdCliente)
        {
            Error = string.Empty;
            try
            {

                var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR
                        select i;

                FechaDesde = FechaDesde.Date;
                q = q.Where(i => i.FECHA_RECEPCION >= FechaDesde);

                FechaHasta = FechaHasta.Date.AddDays(1).AddTicks(-1);
                q = q.Where(i => i.FECHA_RECEPCION <= FechaHasta);

                if (IdCliente.HasValue)
                {
                    q = q.Where(i => i.ID_CLIENTE == IdCliente.Value);
                }
                return q;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }

        public IQueryable<VISTA_FACTURAS_POR_NOTIFICAR> GetFacturasPorNotificar(DateTime FechaFacturacionDesde,
            DateTime FechaFacturacionHasta, int IdTipoCobro, int? IdCliente)
        {
            Error = string.Empty;
            try
            {
                FechaFacturacionDesde = FechaFacturacionDesde.Date;
                FechaFacturacionHasta = FechaFacturacionHasta.Date.AddDays(1).AddTicks(-1);
                var q = from i in _context.VISTA_FACTURAS_POR_NOTIFICAR
                        where i.FECHA_FACTURACION >= FechaFacturacionDesde
                            && i.FECHA_FACTURACION <= FechaFacturacionHasta
                            && i.CONTADOR_NOTAS_COBRO < IdTipoCobro
                        select i;

                if (IdCliente.HasValue)
                {
                    q = q.Where(i => i.ID_CLIENTE == IdCliente.Value);
                }
                return q;
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