using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LQCE.Modelo;
using App.Infrastructure.Runtime;

namespace LQCE.Repositorio
{
    public partial class RepositorioFACTURACION
    {
        public IQueryable<VISTA_PRESTACIONES_POR_FACTURAR> GetPrestacionesPorFacturar(DateTime? FechaDesde,
            DateTime? FechaHasta, int? IdCliente)
        {
            Error = string.Empty;
            try
            {
                
                var q = from i in _context.VISTA_PRESTACIONES_POR_FACTURAR
                        select i;

                if (FechaDesde.HasValue)
                {
                    FechaDesde = FechaDesde.Value.Date;
                    q = q.Where(i => i.FECHA_RECEPCION >= FechaDesde.Value);
                }
                if (FechaHasta.HasValue)
                {
                    FechaHasta = FechaHasta.Value.Date.AddDays(1).AddTicks(-1);
                    q = q.Where(i => i.FECHA_RECEPCION <= FechaHasta.Value);
                }
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
