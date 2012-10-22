using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;
using LQCE.Transaccion.DTO;
using LQCE.Transaccion.Properties;
using Microsoft.Reporting.WebForms;
using Microsoft.SharePoint;
using System.Collections;

namespace LQCE.Transaccion
{
    public partial class TrxFACTURACION
    {
        List<DTO_REPORTE_DETALLEFACTURA_PRESTACION> ListaDetalleFactura;

        List<DTO_REPORTE_NOTA_COBRO_DETALLE> ListaNotaCobro;

        private IList<Stream> m_streams_matriz;
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams_matriz.Add(stream);
            return stream;
        }

        //private IList<Stream> m_streams_individual;
        //private Stream CreateStream2(string name, string fileNameExtension, Encoding encoding,
        //    string mimeType, bool willSeek)
        //{
        //    Stream stream2 = new MemoryStream();
        //    m_streams_individual.Add(stream2);
        //    return stream2;
        //}

        private IList<Stream> m_streams_DetalleFactura;
        private Stream CreateStreamDetalleFactura(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream streamDetalleFactura = new MemoryStream();
            m_streams_DetalleFactura.Add(streamDetalleFactura);
            return streamDetalleFactura;
        }

        private IList<Stream> m_streams_NotaCobro;
        private Stream CreateStreamNotaCobro(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream streamNotaCobro = new MemoryStream();
            m_streams_NotaCobro.Add(streamNotaCobro);
            return streamNotaCobro;
        }

        private IList<Stream> m_streams_NotaCobroIndividual;
        private Stream CreateStreamNotaCobroIndividual(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream streamNotaCobroIndividual = new MemoryStream();
            m_streams_NotaCobroIndividual.Add(streamNotaCobroIndividual);
            return streamNotaCobroIndividual;
        }

        public List<DTO_RESUMEN_PRESTACIONES_FACTURAR> GetClientesAFacturar(DateTime FechaDesde,
            DateTime FechaHasta, int? IdCliente)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    var q = from c in _RepositorioFACTURACION.GetPrestacionesPorFacturar(FechaDesde, FechaHasta, IdCliente)
                            group c by c.ID_CLIENTE into g
                            select new DTO_RESUMEN_PRESTACIONES_FACTURAR
                            {
                                ID_CLIENTE = g.Key,
                                RUT_CLIENTE = g.FirstOrDefault().RUT,
                                NOMBRE_CLIENTE = g.FirstOrDefault().NOMBRE,
                                CANTIDAD_PRESTACIONES = g.Count(),
                                TOTAL_PRESTACIONES = g.Sum(p => p.TOTAL),
                                DESCUENTO = g.FirstOrDefault().DESCUENTO,
                                TOTAL_FACTURA = g.Sum(p => (int)(p.TOTAL * (1 - (double)g.FirstOrDefault().DESCUENTO / 100.0)))
                            };
                    return q.ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void EmitirFacturas(List<DTO_EMISION_FACTURA> ListaClientesFacturar, DateTime FechaDesde,
            DateTime FechaHasta)
        {
            Init();
            ListaDetalleFactura = new List<DTO_REPORTE_DETALLEFACTURA_PRESTACION>();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);
                    RepositorioCLIENTE _RepositorioCLIENTE = new RepositorioCLIENTE(context);
                    RepositorioPRESTACION _RepositorioPRESTACION = new RepositorioPRESTACION(context);

                    FACTURACION _FACTURACION = new FACTURACION();
                    _FACTURACION.FECHA_FACTURACION = DateTime.Now;
                    _FACTURACION.ACTIVO = true;
                    context.AddToFACTURACION(_FACTURACION);

                    int correlativo = 1;
                    foreach (var item in ListaClientesFacturar)
                    {
                        CLIENTE _CLIENTE = _RepositorioCLIENTE.GetByIdWithReferences(item.ID_CLIENTE);
                        if (_CLIENTE == null)
                            throw new Exception("No se encuentra información del cliente");

                        var prestaciones = _RepositorioFACTURACION.GetPrestacionesPorFacturar(FechaDesde, FechaHasta, item.ID_CLIENTE).ToList();

                        FACTURA _FACTURA = new FACTURA();
                        _FACTURA.FACTURACION = _FACTURACION;
                        _FACTURA.CORRELATIVO = correlativo;
                        _FACTURA.CLIENTE = _CLIENTE;
                        _FACTURA.NUMERO_FACTURA = null;
                        _FACTURA.RUT_LABORATORIO = _CLIENTE.TIPO_FACTURA.RUT_FACTURA;
                        _FACTURA.ACTIVO = true;
                        _FACTURA.DESCUENTO = item.DESCUENTO;
                        _FACTURA.NOMBRE_CLIENTE = _CLIENTE.NOMBRE;
                        _FACTURA.RUT_CLIENTE = _CLIENTE.RUT;
                        _FACTURA.DIRECCION = _CLIENTE.DIRECCION;
                        if (_CLIENTE.COMUNA != null)
                        {
                            _FACTURA.NOMBRE_COMUNA = _CLIENTE.COMUNA.NOMBRE;
                        }
                        _FACTURA.FONO = _CLIENTE.FONO;
                        _FACTURA.GIRO = _CLIENTE.GIRO;
                        _FACTURA.DETALLE = "Exámenes realizados del " + FechaDesde.ToString("dd MMMM yyyy") + " al " + FechaHasta.ToString("dd MMMM yyyy");
                        _FACTURA.TIPO_FACTURA = _CLIENTE.TIPO_FACTURA;

                        context.AddToFACTURA(_FACTURA);

                        int suma_total = 0;
                        foreach (var prestacion in prestaciones)
                        {
                            PRESTACION _PRESTACION = _RepositorioPRESTACION.GetById(prestacion.ID);
                            if (_PRESTACION == null)
                                throw new Exception("No se encuentra información de la prestación");

                            int total = (int)(prestacion.TOTAL * (1 - (double)item.DESCUENTO / 100.0));
                            suma_total += total;

                            FACTURA_DETALLE _FACTURA_DETALLE = new FACTURA_DETALLE();
                            _FACTURA_DETALLE.FACTURA = _FACTURA;
                            _FACTURA_DETALLE.PRESTACION = _PRESTACION;
                            _FACTURA_DETALLE.MONTO_TOTAL = total;
                            _FACTURA_DETALLE.MONTO_COBRADO = 0;
                            _FACTURA_DETALLE.ACTIVO = true;
                            context.AddToFACTURA_DETALLE(_FACTURA_DETALLE);
                        }

                        _FACTURA.NETO = suma_total;
                        if (_CLIENTE.TIPO_FACTURA.AFECTO_IVA)
                        {
                            _FACTURA.IVA = (int)(suma_total * 0.19);
                            _FACTURA.TOTAL = (int)(suma_total * 1.19);
                        }
                        else
                        {
                            _FACTURA.IVA = 0;
                            _FACTURA.TOTAL = suma_total;
                        }
                        //context.ApplyPropertyChanges("FACTURA", _FACTURA);

                        correlativo++;
                    }

                    context.SaveChanges();

                    try
                    {
                        // PENDIENTE: Generar PDFs
                        var LISTA_DTO_REPORTE_FACTURA = GetReporteFacturaByID_FACTURACION(_FACTURACION.ID);

                        ListaDetalleFactura = GetReporteDetalleFacturaByID_FACTURACION(_FACTURACION.ID);

                        string deviceInfo =
                                      "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>11in</PageWidth>" +
                                      "  <PageHeight>8.5in</PageHeight>" +
                                      "  <MarginTop>0.5in</MarginTop>" +
                                      "  <MarginLeft>1in</MarginLeft>" +
                                      "  <MarginRight>1in</MarginRight>" +
                                      "  <MarginBottom>0.5in</MarginBottom>" +
                                      "</DeviceInfo>";
                        Warning[] warnings;
                        m_streams_matriz = new List<Stream>();
                        m_streams_DetalleFactura = new List<Stream>();

                        // m_streams_individual = new List<Stream>();

                        // Documento 1: Un archivo con todas las facturas sin fondo para imprimir en matriz de punto
                        var tf = from f in LISTA_DTO_REPORTE_FACTURA
                                 group f by f.NOMBRE_REPORTE_FACTURA into g
                                 select g;

                        foreach (var facturas in tf)
                        {
                            Hashtable propiedades = new Hashtable();
                            propiedades.Add("Fecha de Documento", _FACTURACION.FECHA_FACTURACION);
                            propiedades.Add("Tipo de Documento", "Factura " + facturas.FirstOrDefault().NOMBRE_TIPO_FACTURA);
                            propiedades.Add("Formato", "Consolidado");

                            ReportViewer _ReportViewer = new ReportViewer();
                            _ReportViewer.ProcessingMode = ProcessingMode.Local;
                            _ReportViewer.LocalReport.ShowDetailedSubreportMessages = true;
                            _ReportViewer.LocalReport.DataSources.Clear();
                            _ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", facturas));

                            _ReportViewer.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte." + facturas.Key;

                            _ReportViewer.LocalReport.Render("PDF", deviceInfo, CreateStream, out warnings);
                            foreach (Stream stream in m_streams_matriz)
                                stream.Position = 0;

                            using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                            {
                                SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                                string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + facturas.Key + ".pdf";
                                spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_matriz[0], propiedades, true);
                                spList.Update();
                            }
                        }

                        // Documento 2: Un archivo con todos los detalles de facturas
                        List<DTO_REPORTE_DETALLEFACTURA_FACTURA> LISTA_DTO_REPORTE_DETALLEFACTURA_FACTURA =
                            (from df in ListaDetalleFactura
                             group df by df.ID_FACTURA into g
                             select new DTO_REPORTE_DETALLEFACTURA_FACTURA
                             {
                                 ID_FACTURA = g.Key,
                                 ID_CLIENTE = g.FirstOrDefault().ID_CLIENTE,
                                 NOMBRE_CLIENTE = g.FirstOrDefault().NOMBRE_CLIENTE,
                                 RUT_CLIENTE = g.FirstOrDefault().RUT_CLIENTE
                             }).ToList();


                        Hashtable propiedadesDetalle = new Hashtable();
                        propiedadesDetalle.Add("Fecha de Documento", _FACTURACION.FECHA_FACTURACION);
                        propiedadesDetalle.Add("Tipo de Documento", "Detalle de Factura");
                        propiedadesDetalle.Add("Formato", "Consolidado");

                        ReportViewer _ReportViewerDetalle = new ReportViewer();
                        _ReportViewerDetalle.ProcessingMode = ProcessingMode.Local;
                        _ReportViewerDetalle.LocalReport.ShowDetailedSubreportMessages = true;
                        _ReportViewerDetalle.LocalReport.DataSources.Clear();
                        _ReportViewerDetalle.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", LISTA_DTO_REPORTE_DETALLEFACTURA_FACTURA));
                        _ReportViewerDetalle.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ReporteDetalleFactura_SubreportProcessingEventHandler);
                        _ReportViewerDetalle.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte.DetalleFactura.rdlc";

                        _ReportViewerDetalle.LocalReport.Render("PDF", deviceInfo, CreateStreamDetalleFactura, out warnings);
                        foreach (Stream stream in m_streams_DetalleFactura)
                            stream.Position = 0;

                        using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                        {
                            SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                            string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_DetalleFactura.pdf";
                            spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_DetalleFactura[0], propiedadesDetalle, true);
                            spList.Update();
                        }
                    }
                    catch (Exception ex)
                    {
                        // En caso de error, al generar los PDF se eliminan los registros de las facturas
                        _FACTURACION.ACTIVO = false;
                        foreach (var _FACTURA in _FACTURACION.FACTURA)
                        {
                            _FACTURA.ACTIVO = false;
                            foreach (var _FACTURA_DETALLE in _FACTURA.FACTURA_DETALLE)
                            {
                                _FACTURA_DETALLE.ACTIVO = false;
                                context.ApplyPropertyChanges("FACTURA_DETALLE", _FACTURA_DETALLE);
                            }
                            context.ApplyPropertyChanges("FACTURA", _FACTURA);
                        }
                        context.ApplyPropertyChanges("FACTURACION", _FACTURACION);
                        context.SaveChanges();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void ReporteDetalleFactura_SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Parameters[0].Values[0]))
                {
                    int FacturaId = Convert.ToInt32(e.Parameters[0].Values[0].ToString());
                    e.DataSources.Add(new ReportDataSource("DataSet1",
                        ListaDetalleFactura.Where(l => l.ID_FACTURA == FacturaId)));
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void ReporteNotaCobro_SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Parameters[0].Values[0]))
                {
                    int ClienteId = Convert.ToInt32(e.Parameters[0].Values[0].ToString());
                    e.DataSources.Add(new ReportDataSource("DataSet1",
                        ListaNotaCobro.Where(l => l.ID_CLIENTE == ClienteId)));
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }


        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        protected List<DTO_REPORTE_FACTURA> GetReporteFacturaByID_FACTURACION(int IdFacturacion)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    FACTURACION _FACTURACION = _RepositorioFACTURACION.GetByIdWithReferencesFull(IdFacturacion);
                    if (_FACTURACION == null)
                        throw new Exception("No se encuentra información de la facturación");

                    return (from f in _FACTURACION.FACTURA
                            where f.ACTIVO
                            select new DTO_REPORTE_FACTURA
                            {
                                NOMBRE_REPORTE_FACTURA = f.TIPO_FACTURA.NOMBRE_REPORTE_FACTURA,
                                NOMBRE_REPORTE_FACTURA_INDIVIDUAL = f.TIPO_FACTURA.NOMBRE_REPORTE_FACTURA_INDIVIDUAL,
                                DIA = f.FACTURACION.FECHA_FACTURACION.Day,
                                MES = f.FACTURACION.FECHA_FACTURACION.ToString("MMMM"),
                                AÑO = f.FACTURACION.FECHA_FACTURACION.Year,
                                NOMBRE_CLIENTE = f.NOMBRE_CLIENTE,
                                RUT_CLIENTE = f.RUT_CLIENTE,
                                DIRECCION = f.DIRECCION,
                                COMUNA = f.NOMBRE_COMUNA,
                                FONO = f.FONO,
                                GIRO = f.GIRO,
                                DETALLE = f.DETALLE,
                                NETO = f.NETO,
                                IVA = f.IVA,
                                TOTAL = f.TOTAL,
                                NUMERO_FACTURA = f.NUMERO_FACTURA,
                                NOMBRE_TIPO_FACTURA = f.TIPO_FACTURA.NOMBRE_FACTURA
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        protected List<DTO_REPORTE_FACTURA> GetReporteFacturaByID_FACTURA(int IdFactura)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    FACTURA _FACTURA = _RepositorioFACTURA.GetByIdWithReferences(IdFactura);
                    if (_FACTURA == null)
                        throw new Exception("No se encuentra información de la factura");

                    DTO_REPORTE_FACTURA _DTO_REPORTE_FACTURA = new DTO_REPORTE_FACTURA();

                    _DTO_REPORTE_FACTURA.NOMBRE_REPORTE_FACTURA = _FACTURA.TIPO_FACTURA.NOMBRE_REPORTE_FACTURA;
                    _DTO_REPORTE_FACTURA.NOMBRE_REPORTE_FACTURA_INDIVIDUAL = _FACTURA.TIPO_FACTURA.NOMBRE_REPORTE_FACTURA_INDIVIDUAL;
                    _DTO_REPORTE_FACTURA.DIA = _FACTURA.FACTURACION.FECHA_FACTURACION.Day;
                    _DTO_REPORTE_FACTURA.MES = _FACTURA.FACTURACION.FECHA_FACTURACION.ToString("MMMM");
                    _DTO_REPORTE_FACTURA.AÑO = _FACTURA.FACTURACION.FECHA_FACTURACION.Year;
                    _DTO_REPORTE_FACTURA.NOMBRE_CLIENTE = _FACTURA.NOMBRE_CLIENTE;
                    _DTO_REPORTE_FACTURA.RUT_CLIENTE = _FACTURA.RUT_CLIENTE;
                    _DTO_REPORTE_FACTURA.DIRECCION = _FACTURA.DIRECCION;
                    _DTO_REPORTE_FACTURA.COMUNA = _FACTURA.NOMBRE_COMUNA;
                    _DTO_REPORTE_FACTURA.FONO = _FACTURA.FONO;
                    _DTO_REPORTE_FACTURA.GIRO = _FACTURA.GIRO;
                    _DTO_REPORTE_FACTURA.DETALLE = _FACTURA.DETALLE;
                    _DTO_REPORTE_FACTURA.NETO = _FACTURA.NETO;
                    _DTO_REPORTE_FACTURA.IVA = _FACTURA.IVA;
                    _DTO_REPORTE_FACTURA.TOTAL = _FACTURA.TOTAL;
                    _DTO_REPORTE_FACTURA.NUMERO_FACTURA = _FACTURA.NUMERO_FACTURA;
                    _DTO_REPORTE_FACTURA.NOMBRE_TIPO_FACTURA = _FACTURA.TIPO_FACTURA.NOMBRE_FACTURA;

                    List<DTO_REPORTE_FACTURA> lista = new List<DTO_REPORTE_FACTURA>();
                    lista.Add(_DTO_REPORTE_FACTURA);
                    return lista;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        private List<DTO_REPORTE_NOTA_COBRO_DETALLE> GetReporteNotaCobroByID_COBRO(LQCEEntities context, int IdCobro)
        {
            RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

            var q = _RepositorioFACTURACION.GetNotaCobroDetalleByIdCobro(IdCobro);

            return (from nd in q
                    select new DTO_REPORTE_NOTA_COBRO_DETALLE
                    {
                        FECHA_FACTURACION = nd.FACTURA.FACTURACION.FECHA_FACTURACION,
                        ID_COBRO = nd.NOTA_COBRO.COBRO.ID,
                        ID_CLIENTE = nd.NOTA_COBRO.CLIENTE.ID,
                        CORRELATIVO = nd.NOTA_COBRO.CORRELATIVO,
                        NOMBRE_CLIENTE = nd.NOTA_COBRO.CLIENTE.NOMBRE,
                        RUT_CLIENTE = nd.NOTA_COBRO.CLIENTE.RUT,
                        NOMBRE_REPORTE = nd.NOTA_COBRO.COBRO.TIPO_COBRO.REPORTE,
                        ID_NOTA_COBRO_DETALLE = nd.ID,
                        NUMERO_FACTURA = nd.FACTURA.NUMERO_FACTURA.HasValue ? nd.FACTURA.NUMERO_FACTURA.Value : 0,
                        MONTO_TOTAL = nd.FACTURA.TOTAL,
                        MONTO_PENDIENTE = nd.MONTO_PENDIENTE
                    }).ToList();
        }

        protected List<DTO_REPORTE_DETALLEFACTURA_PRESTACION> GetReporteDetalleFacturaByID_FACTURACION(int IdFacturacion)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    var q = _RepositorioFACTURACION.GetFacturaDetalleByIdFacturacion(IdFacturacion);

                    return (from fd in q
                            select new DTO_REPORTE_DETALLEFACTURA_PRESTACION
                            {
                                ID_FACTURA = fd.FACTURA.ID,
                                ID_CLIENTE = fd.FACTURA.CLIENTE.ID,
                                NOMBRE_CLIENTE = fd.FACTURA.NOMBRE_CLIENTE,
                                RUT_CLIENTE = fd.FACTURA.RUT_CLIENTE,
                                ID_FACTURA_DETALLE = fd.ID,
                                NUMERO_FICHA = fd.PRESTACION.ID,
                                MONTO_TOTAL = fd.MONTO_TOTAL,
                                FECHA_RECEPCION = fd.PRESTACION.FECHA_RECEPCION,
                                NOMBRE = fd.PRESTACION.PRESTACION_HUMANA != null ? fd.PRESTACION.PRESTACION_HUMANA.NOMBRE : fd.PRESTACION.PRESTACION_VETERINARIA.NOMBRE
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        protected List<DTO_REPORTE_DETALLEFACTURA_PRESTACION> GetReporteDetalleFacturaByID_FACTURA(int IdFactura)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    var q = _RepositorioFACTURACION.GetFacturaDetalleByIdFactura(IdFactura);

                    return (from fd in q
                            select new DTO_REPORTE_DETALLEFACTURA_PRESTACION
                            {
                                ID_FACTURA = fd.FACTURA.ID,
                                ID_CLIENTE = fd.FACTURA.CLIENTE.ID,
                                NOMBRE_CLIENTE = fd.FACTURA.NOMBRE_CLIENTE,
                                RUT_CLIENTE = fd.FACTURA.RUT_CLIENTE,
                                ID_FACTURA_DETALLE = fd.ID,
                                NUMERO_FICHA = fd.PRESTACION.ID,
                                MONTO_TOTAL = fd.MONTO_TOTAL,
                                FECHA_RECEPCION = fd.PRESTACION.FECHA_RECEPCION,
                                NOMBRE = fd.PRESTACION.PRESTACION_HUMANA != null ? fd.PRESTACION.PRESTACION_HUMANA.NOMBRE : fd.PRESTACION.PRESTACION_VETERINARIA.NOMBRE
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// Retorna todas las facturaciones realizadas y que tienen facturas por numerar
        /// </summary>
        /// <returns></returns>
        public List<DTO_RESUMEN_FACTURACION> GetResumenFacturacionPorNumerar()
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    return (from f in _RepositorioFACTURA.GetAllWithReferences()
                            where f.ACTIVO && f.FACTURACION.ACTIVO
                            && f.FACTURACION.FACTURA.Any(fd => fd.ACTIVO && fd.NUMERO_FACTURA == null)
                            group f by new { ID_FACTURACION = f.FACTURACION.ID, ID_TIPO_FACTURA = f.TIPO_FACTURA.ID } into g
                            select new DTO_RESUMEN_FACTURACION
                            {
                                ID_FACTURACION = g.Key.ID_FACTURACION,
                                ID_TIPO_FACTURA = g.Key.ID_TIPO_FACTURA,
                                NOMBRE_TIPO_FACTURA = g.FirstOrDefault().TIPO_FACTURA.NOMBRE_FACTURA,
                                FECHA_FACTURACION = g.FirstOrDefault().FACTURACION.FECHA_FACTURACION,
                                TOTAL_FACTURAS = g.Count(),
                                TOTAL_FACTURAS_POR_NUMERAR = g.Count(fa => fa.NUMERO_FACTURA == null)
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// Retorna todas las facturaciones realizadas
        /// </summary>
        /// <returns></returns>
        public List<DTO_RESUMEN_FACTURACION> GetResumenFacturacion()
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    return (from f in _RepositorioFACTURA.GetAllWithReferences()
                            where f.ACTIVO && f.FACTURACION.ACTIVO
                            && f.FACTURACION.FACTURA.Any(fd => fd.ACTIVO)
                            group f by new { ID_FACTURACION = f.FACTURACION.ID, ID_TIPO_FACTURA = f.TIPO_FACTURA.ID } into g
                            select new DTO_RESUMEN_FACTURACION
                            {
                                ID_FACTURACION = g.Key.ID_FACTURACION,
                                ID_TIPO_FACTURA = g.Key.ID_TIPO_FACTURA,
                                NOMBRE_TIPO_FACTURA = g.FirstOrDefault().TIPO_FACTURA.NOMBRE_FACTURA,
                                FECHA_FACTURACION = g.FirstOrDefault().FACTURACION.FECHA_FACTURACION,
                                TOTAL_FACTURAS = g.Count(),
                                TOTAL_FACTURAS_POR_NUMERAR = g.Count(fa => fa.NUMERO_FACTURA == null)
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public List<DTO_RESUMEN_FACTURACION> GetResumenFacturacion(DateTime FechaDesde, DateTime FechaHasta)
        {
            Init();
            try
            {
                DateTime FechaInicio = FechaDesde.Date;
                DateTime FechaTermino = FechaHasta.Date.AddDays(1);
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    return (from f in _RepositorioFACTURA.GetAllWithReferences()
                            where f.ACTIVO && f.FACTURACION.ACTIVO
                            && f.FACTURACION.FACTURA.Any(fd => fd.ACTIVO)
                            && f.FACTURACION.FECHA_FACTURACION >= FechaInicio
                            && f.FACTURACION.FECHA_FACTURACION < FechaTermino
                            group f by new { ID_FACTURACION = f.FACTURACION.ID, ID_TIPO_FACTURA = f.TIPO_FACTURA.ID } into g
                            select new DTO_RESUMEN_FACTURACION
                            {
                                ID_FACTURACION = g.Key.ID_FACTURACION,
                                ID_TIPO_FACTURA = g.Key.ID_TIPO_FACTURA,
                                NOMBRE_TIPO_FACTURA = g.FirstOrDefault().TIPO_FACTURA.NOMBRE_FACTURA,
                                FECHA_FACTURACION = g.FirstOrDefault().FACTURACION.FECHA_FACTURACION,
                                TOTAL_FACTURAS = g.Count(),
                                TOTAL_FACTURAS_POR_NUMERAR = g.Count(fa => fa.NUMERO_FACTURA == null)
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }


        public void NumerarFacturas(int ID_FACTURACION, int ID_TIPO_FACTURA, bool NUMERAR_TODAS,
            int? CORRELATIVO_DESDE, int? CORRELATIVO_HASTA, int NUMERO_FACTURA_INICIAL)
        {
            Init();
            ListaDetalleFactura = new List<DTO_REPORTE_DETALLEFACTURA_PRESTACION>();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    var q = _RepositorioFACTURA.GetByFilterWithReferences(null, ID_FACTURACION, ID_TIPO_FACTURA,
                        null, "", null, null, null, "", "", "", "", null, null, "", "", "", null);

                    if (!NUMERAR_TODAS)
                    {
                        if (!CORRELATIVO_DESDE.HasValue)
                            throw new Exception("Debe señalar factura inicial a facturar");
                        if (!CORRELATIVO_HASTA.HasValue)
                            throw new Exception("Debe señalar factura final a facturar");
                        if (CORRELATIVO_DESDE.Value > CORRELATIVO_HASTA.Value)
                            throw new Exception("El rango de facturas está mal definido, el valor inicial es mayor al valor final");

                        q = q.Where(f => f.CORRELATIVO >= CORRELATIVO_DESDE.Value && f.CORRELATIVO <= CORRELATIVO_HASTA.Value);
                    }

                    if (q.Any(f => f.NUMERO_FACTURA.HasValue))
                        throw new Exception("Ya existen facturas numeradas en el rango seleccionado");

                    int NUMERO_FACTURA_FINAL = NUMERO_FACTURA_INICIAL + q.Count() - 1;

                    var q2 = _RepositorioFACTURA.GetByFilterWithReferences(null, null, ID_TIPO_FACTURA,
                        null, "", null, null, null, null, null, "", "", null, null, "", "", "", null);
                    if (q2.Any(f => f.NUMERO_FACTURA.HasValue
                        && f.NUMERO_FACTURA >= NUMERO_FACTURA_INICIAL
                        && f.NUMERO_FACTURA <= NUMERO_FACTURA_FINAL))
                        throw new Exception("Ya existen facturas numeradas con los numeros de facturas indicados");

                    int NUEVO_NUMERO_FACTURA = NUMERO_FACTURA_INICIAL;
                    foreach (var _FACTURA in q.OrderBy(f => f.CORRELATIVO).ToList())
                    {
                        if (_FACTURA.NUMERO_FACTURA.HasValue)
                            throw new Exception("Factura ya está numerada");

                        _FACTURA.NUMERO_FACTURA = NUEVO_NUMERO_FACTURA;
                        context.ApplyPropertyChanges("FACTURA", _FACTURA);

                        NUEVO_NUMERO_FACTURA++;
                    }
                    context.SaveChanges();

                    try
                    {
                        foreach (var _FACTURA in q.OrderBy(f => f.CORRELATIVO).ToList())
                        {
                            // PENDIENTE: Generar PDFs
                            var LISTA_DTO_REPORTE_FACTURA = GetReporteFacturaByID_FACTURA(_FACTURA.ID);

                            ListaDetalleFactura = GetReporteDetalleFacturaByID_FACTURACION(_FACTURA.ID);

                            string deviceInfo =
                                          "<DeviceInfo>" +
                                          "  <OutputFormat>PDF</OutputFormat>" +
                                          "  <PageWidth>11in</PageWidth>" +
                                          "  <PageHeight>8.5in</PageHeight>" +
                                          "  <MarginTop>0.5in</MarginTop>" +
                                          "  <MarginLeft>1in</MarginLeft>" +
                                          "  <MarginRight>1in</MarginRight>" +
                                          "  <MarginBottom>0.5in</MarginBottom>" +
                                          "</DeviceInfo>";
                            Warning[] warnings;
                            m_streams_matriz = new List<Stream>();
                            m_streams_DetalleFactura = new List<Stream>();

                            // m_streams_individual = new List<Stream>();

                            // Documento 1: Un archivo por factura con fondo
                            var tf = from f in LISTA_DTO_REPORTE_FACTURA
                                     group f by f.NOMBRE_REPORTE_FACTURA_INDIVIDUAL into g
                                     select g;

                            foreach (var facturas in tf)
                            {
                                Hashtable propiedades = new Hashtable();
                                propiedades.Add("Fecha de Documento", _FACTURA.FACTURACION.FECHA_FACTURACION);
                                propiedades.Add("Tipo de Documento", "Factura " + facturas.FirstOrDefault().NOMBRE_TIPO_FACTURA);
                                propiedades.Add("Formato", "Individual");
                                propiedades.Add("RUT Cliente", facturas.FirstOrDefault().RUT_CLIENTE);
                                propiedades.Add("Nombre Cliente", facturas.FirstOrDefault().NOMBRE_CLIENTE);

                                ReportViewer _ReportViewer = new ReportViewer();
                                _ReportViewer.ProcessingMode = ProcessingMode.Local;
                                _ReportViewer.LocalReport.ShowDetailedSubreportMessages = true;
                                _ReportViewer.LocalReport.DataSources.Clear();
                                _ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", facturas));

                                _ReportViewer.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte." + facturas.Key;

                                _ReportViewer.LocalReport.Render("PDF", deviceInfo, CreateStream, out warnings);
                                foreach (Stream stream in m_streams_matriz)
                                    stream.Position = 0;

                                using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                                {
                                    SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                                    string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + _FACTURA.NUMERO_FACTURA.Value.ToString() + "_" + facturas.Key + ".pdf";
                                    spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_matriz[0], propiedades, true);
                                    spList.Update();
                                }
                            }

                            // Documento 2: Un archivo por cada detalles de facturas
                            List<DTO_REPORTE_DETALLEFACTURA_FACTURA> LISTA_DTO_REPORTE_DETALLEFACTURA_FACTURA =
                                (from df in ListaDetalleFactura
                                 group df by df.ID_FACTURA into g
                                 select new DTO_REPORTE_DETALLEFACTURA_FACTURA
                                 {
                                     ID_FACTURA = g.Key,
                                     ID_CLIENTE = g.FirstOrDefault().ID_CLIENTE,
                                     NOMBRE_CLIENTE = g.FirstOrDefault().NOMBRE_CLIENTE,
                                     RUT_CLIENTE = g.FirstOrDefault().RUT_CLIENTE
                                 }).ToList();


                            Hashtable propiedadesDetalle = new Hashtable();
                            propiedadesDetalle.Add("Fecha de Documento", _FACTURA.FACTURACION.FECHA_FACTURACION);
                            propiedadesDetalle.Add("Tipo de Documento", "Detalle de Factura");
                            propiedadesDetalle.Add("Formato", "Individual");
                            propiedadesDetalle.Add("RUT Cliente", _FACTURA.RUT_CLIENTE);
                            propiedadesDetalle.Add("Nombre Cliente", _FACTURA.NOMBRE_CLIENTE);

                            ReportViewer _ReportViewerDetalle = new ReportViewer();
                            _ReportViewerDetalle.ProcessingMode = ProcessingMode.Local;
                            _ReportViewerDetalle.LocalReport.ShowDetailedSubreportMessages = true;
                            _ReportViewerDetalle.LocalReport.DataSources.Clear();
                            _ReportViewerDetalle.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", LISTA_DTO_REPORTE_DETALLEFACTURA_FACTURA));
                            _ReportViewerDetalle.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ReporteDetalleFactura_SubreportProcessingEventHandler);
                            _ReportViewerDetalle.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte.DetalleFactura.rdlc";

                            _ReportViewerDetalle.LocalReport.Render("PDF", deviceInfo, CreateStreamDetalleFactura, out warnings);
                            foreach (Stream stream in m_streams_DetalleFactura)
                                stream.Position = 0;

                            using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                            {
                                SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                                string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + _FACTURA.NUMERO_FACTURA.Value.ToString() + "_DetalleFactura.pdf";
                                spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_DetalleFactura[0], propiedadesDetalle, true);
                                spList.Update();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // En caso de error, al generar los PDF se eliminan los registros de las facturas
                        foreach (var _FACTURA in q.OrderBy(f => f.CORRELATIVO).ToList())
                        {

                            _FACTURA.NUMERO_FACTURA = null;
                            context.ApplyPropertyChanges("FACTURA", _FACTURA);
                        }
                        context.SaveChanges();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void BorrarNumeracionFacturas(int ID_FACTURACION, int ID_TIPO_FACTURA, int CORRELATIVO_DESDE, int CORRELATIVO_HASTA)
        {
            Init();
            //ListaDetalleFactura = new List<DTO_REPORTE_DETALLEFACTURA_PRESTACION>();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    if (CORRELATIVO_DESDE > CORRELATIVO_HASTA)
                        throw new Exception("El rango de facturas está mal definido, el valor inicial es mayor al valor final");

                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    var q = _RepositorioFACTURA.GetByFilterWithReferences(null, ID_FACTURACION, ID_TIPO_FACTURA,
                        null, "", null, null, null, "", "", "", "", null, null, "", "", "", null);
                    q = q.Where(f => f.CORRELATIVO >= CORRELATIVO_DESDE && f.CORRELATIVO <= CORRELATIVO_HASTA);

                    if (q.Any(f => f.NOTA_COBRO_DETALLE.Any(n => n.ACTIVO)))
                        throw new Exception("Ya existen facturas cobradas al cliente en el rango señalado");

                    if (q.Any(f => f.PAGADA.HasValue && f.PAGADA.Value == true))
                        throw new Exception("Ya existen facturas pagadas en el rango señalado");

                    if (q.Any(f => f.NOTA_CREDITO.Any(nc => nc.ACTIVO)))
                        throw new Exception("Existen facturas con notas de crédito en el rango señalado");

                    foreach (var _FACTURA in q.OrderBy(f => f.CORRELATIVO).ToList())
                    {
                        if (_FACTURA.NUMERO_FACTURA.HasValue)
                        {
                            _FACTURA.NUMERO_FACTURA = null;
                            context.ApplyPropertyChanges("FACTURA", _FACTURA);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void AnularFacturas(List<int> LISTA_ID_FACTURA)
        {
            Init();
            //ListaDetalleFactura = new List<DTO_REPORTE_DETALLEFACTURA_PRESTACION>();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    foreach (int ID_FACTURA in LISTA_ID_FACTURA)
                    {
                        FACTURA _FACTURA = _RepositorioFACTURA.GetByIdWithReferences(ID_FACTURA);
                        if (_FACTURA == null)
                            throw new Exception("No se encuentra informacion de la factura");

                        if (_FACTURA.NUMERO_FACTURA.HasValue)
                            throw new Exception("La factura ya ha sido numerada");

                        if (_FACTURA.NOTA_CREDITO.Any(nc => nc.ACTIVO))
                            throw new Exception("La factura tiene notas de créditos asociadas");

                        _FACTURA.ACTIVO = false;
                        foreach (FACTURA_DETALLE _FACTURA_DETALLE in _FACTURA.FACTURA_DETALLE.Where(fd => fd.ACTIVO))
                        {
                            _FACTURA_DETALLE.ACTIVO = false;
                            context.ApplyPropertyChanges("FACTURA_DETALLE", _FACTURA_DETALLE);
                        }
                        context.ApplyPropertyChanges("FACTURA", _FACTURA);

                        context.SaveChanges();

                    }

                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void EmitirNotaCredito(int IdFactura, int NumeroNotaCredito, bool CorreccionTotal)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);

                    FACTURA _FACTURA = _RepositorioFACTURA.GetByIdWithReferences(IdFactura);
                    if (_FACTURA == null)
                        throw new Exception("No se encuentra informacion de la factura");

                    if (!_FACTURA.NUMERO_FACTURA.HasValue)
                        throw new Exception("La factura no ha sido numerada");

                    if (_FACTURA.PAGADA.HasValue && _FACTURA.PAGADA.Value == true)
                        throw new Exception("La factura ya ha sido pagada");

                    NOTA_CREDITO _NOTA_CREDITO = new NOTA_CREDITO();
                    _NOTA_CREDITO.FACTURA = _FACTURA;
                    _NOTA_CREDITO.FECHA_EMISION = DateTime.Now;
                    _NOTA_CREDITO.NUMERO_NOTA_CREDITO = NumeroNotaCredito;
                    _NOTA_CREDITO.CORRECCION_TOTAL_PARCIAL = CorreccionTotal;
                    _NOTA_CREDITO.ACTIVO = true;
                    context.AddToNOTA_CREDITO(_NOTA_CREDITO);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public void EmitirNotasCobros(DateTime FechaFacturacionDesde, DateTime FechaFacturacionHasta,
            int IdTipoCobro, int? IdCliente)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);
                    RepositorioTIPO_COBRO _RepositorioTIPO_COBRO = new RepositorioTIPO_COBRO(context);
                    RepositorioFACTURA _RepositorioFACTURA = new RepositorioFACTURA(context);
                    RepositorioCLIENTE _RepositorioCLIENTE = new RepositorioCLIENTE(context);
                    RepositorioVISTA_REPORTE_FACTURA _RepositorioVISTA_REPORTE_FACTURA = new RepositorioVISTA_REPORTE_FACTURA(context);

                    TIPO_COBRO _TIPO_COBRO = _RepositorioTIPO_COBRO.GetById(IdTipoCobro);
                    if (_TIPO_COBRO == null)
                        throw new Exception("No se encuentra información del tipo de cobro");

                    COBRO _COBRO = new COBRO();
                    _COBRO.FECHA_COBRO = DateTime.Now;
                    _COBRO.TIPO_COBRO = _TIPO_COBRO;
                    _COBRO.ACTIVO = true;
                    context.AddToCOBRO(_COBRO);

                    var cliente_facturas = (from f in _RepositorioFACTURACION.GetFacturasPorNotificar(FechaFacturacionDesde,
                        FechaFacturacionHasta, IdTipoCobro, IdCliente)
                                            group f by f.ID_CLIENTE into g
                                            select new
                                            {
                                                IdCliente = g.Key,
                                                Facturas = g
                                            }).ToList();

                    if (!cliente_facturas.Any())
                        throw new Exception("No hay facturas que notificar");

                    int correlativo = 1;
                    foreach (var cf in cliente_facturas)
                    {
                        CLIENTE _CLIENTE = _RepositorioCLIENTE.GetById(cf.IdCliente);
                        if (_CLIENTE == null)
                            throw new Exception("No se encuentra información del cliente");

                        NOTA_COBRO _NOTA_COBRO = new NOTA_COBRO();
                        _NOTA_COBRO.COBRO = _COBRO;
                        _NOTA_COBRO.CORRELATIVO = correlativo;
                        _NOTA_COBRO.CLIENTE = _CLIENTE;
                        _NOTA_COBRO.ACTIVO = true;
                        context.AddToNOTA_COBRO(_NOTA_COBRO);

                        foreach (var f in cf.Facturas)
                        {
                            FACTURA _FACTURA = _RepositorioFACTURA.GetById(f.ID);
                            if (_FACTURA == null)
                                throw new Exception("No se encuentra información de la factura ");

                            VISTA_REPORTE_FACTURA _VISTA_REPORTE_FACTURA = _RepositorioVISTA_REPORTE_FACTURA.GetById(f.ID);
                            if (_VISTA_REPORTE_FACTURA == null)
                                throw new Exception("No se encuentra información de la factura ");


                            NOTA_COBRO_DETALLE _NOTA_COBRO_DETALLE = new NOTA_COBRO_DETALLE();
                            _NOTA_COBRO_DETALLE.NOTA_COBRO = _NOTA_COBRO;
                            _NOTA_COBRO_DETALLE.FACTURA = _FACTURA;
                            if (_FACTURA.PAGADA.HasValue && _FACTURA.PAGADA.Value == true)
                            {
                                _NOTA_COBRO_DETALLE.MONTO_PENDIENTE = 0;
                            }
                            else
                            {
                                _NOTA_COBRO_DETALLE.MONTO_PENDIENTE = _VISTA_REPORTE_FACTURA.SALDO_DEUDOR ?? 0;
                            }
                            _NOTA_COBRO_DETALLE.ACTIVO = true;
                            context.AddToNOTA_COBRO_DETALLE(_NOTA_COBRO_DETALLE);
                        }

                        correlativo++;
                    }

                    context.SaveChanges();

                    try
                    {
                        ListaNotaCobro = GetReporteNotaCobroByID_COBRO(context, _COBRO.ID);

                        string deviceInfo =
                                      "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>11in</PageWidth>" +
                                      "  <PageHeight>8.5in</PageHeight>" +
                                      "  <MarginTop>0.5in</MarginTop>" +
                                      "  <MarginLeft>1in</MarginLeft>" +
                                      "  <MarginRight>1in</MarginRight>" +
                                      "  <MarginBottom>0.5in</MarginBottom>" +
                                      "</DeviceInfo>";
                        Warning[] warnings;
                        m_streams_NotaCobro = new List<Stream>();

                        List<DTO_REPORTE_NOTA_COBRO> ListaNotaCobroEncabezado = (from nc in ListaNotaCobro
                                                                                 group nc by nc.CORRELATIVO into g
                                                                                 select new DTO_REPORTE_NOTA_COBRO
                                                                                 {
                                                                                     ID_COBRO = g.FirstOrDefault().ID_COBRO,
                                                                                     ID_CLIENTE = g.FirstOrDefault().ID_CLIENTE,
                                                                                     CORRELATIVO = g.Key,
                                                                                     NOMBRE_CLIENTE = g.FirstOrDefault().NOMBRE_CLIENTE,
                                                                                     RUT_CLIENTE = g.FirstOrDefault().RUT_CLIENTE,
                                                                                     NOMBRE_REPORTE = g.FirstOrDefault().NOMBRE_REPORTE,
                                                                                 }).ToList();


                        // Documento 1: Un archivo con todas las notas de cobro emitidas
                        Hashtable propiedades = new Hashtable();
                        propiedades.Add("Fecha de Documento", _COBRO.FECHA_COBRO);
                        propiedades.Add("Tipo de Documento", "Nota de Cobro " + ListaNotaCobroEncabezado.FirstOrDefault().NOMBRE_REPORTE);
                        propiedades.Add("Formato", "Consolidado");

                        ReportViewer _ReportViewer = new ReportViewer();
                        _ReportViewer.ProcessingMode = ProcessingMode.Local;
                        _ReportViewer.LocalReport.ShowDetailedSubreportMessages = true;
                        _ReportViewer.LocalReport.DataSources.Clear();
                        _ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ListaNotaCobroEncabezado));
                        _ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ReporteNotaCobro_SubreportProcessingEventHandler);
                        _ReportViewer.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte." + ListaNotaCobroEncabezado.FirstOrDefault().NOMBRE_REPORTE;

                        _ReportViewer.LocalReport.Render("PDF", deviceInfo, CreateStreamNotaCobro, out warnings);
                        foreach (Stream stream in m_streams_NotaCobro)
                            stream.Position = 0;

                        using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                        {
                            SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                            string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_NotaCobro_" + _COBRO.TIPO_COBRO.NOMBRE + ".pdf";
                            spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_NotaCobro[0], propiedades, true);
                            spList.Update();
                        }

                        foreach (var item in ListaNotaCobroEncabezado)
                        {
                            m_streams_NotaCobroIndividual = new List<Stream>();
                            // Documento 2: Un archivo por cada detalles de facturas
                            List<DTO_REPORTE_NOTA_COBRO> LISTA_DTO_REPORTE_NOTA_COBRO2 = new List<DTO_REPORTE_NOTA_COBRO>();
                            LISTA_DTO_REPORTE_NOTA_COBRO2.Add(item);

                            Hashtable propiedadesDetalle = new Hashtable();
                            propiedadesDetalle.Add("Fecha de Documento", _COBRO.FECHA_COBRO);
                            propiedadesDetalle.Add("Tipo de Documento", "Nota de Cobro " + item.NOMBRE_REPORTE);
                            propiedadesDetalle.Add("Formato", "Individual");
                            propiedadesDetalle.Add("RUT Cliente", item.RUT_CLIENTE);
                            propiedadesDetalle.Add("Nombre Cliente", item.NOMBRE_CLIENTE);

                            ReportViewer _ReportViewerDetalle = new ReportViewer();
                            _ReportViewerDetalle.ProcessingMode = ProcessingMode.Local;
                            _ReportViewerDetalle.LocalReport.ShowDetailedSubreportMessages = true;
                            _ReportViewerDetalle.LocalReport.DataSources.Clear();
                            _ReportViewerDetalle.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", LISTA_DTO_REPORTE_NOTA_COBRO2));
                            _ReportViewerDetalle.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ReporteNotaCobro_SubreportProcessingEventHandler);
                            _ReportViewerDetalle.LocalReport.ReportEmbeddedResource = "LQCE.Transaccion.Reporte." + item.NOMBRE_REPORTE;

                            _ReportViewerDetalle.LocalReport.Render("PDF", deviceInfo, CreateStreamNotaCobroIndividual, out warnings);
                            foreach (Stream stream in m_streams_NotaCobroIndividual)
                                stream.Position = 0;

                            using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                            {
                                SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                                string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_NotaCobroIndividual_" + _COBRO.TIPO_COBRO.NOMBRE + "_" + item.NOMBRE_CLIENTE + ".pdf";
                                spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_NotaCobroIndividual[0], propiedadesDetalle, true);
                                spList.Update();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        RepositorioCOBRO _RepositorioCOBRO = new RepositorioCOBRO(context);
                        COBRO _NUEVO_COBRO = _RepositorioCOBRO.GetByIdWithReferences(_COBRO.ID);

                        _NUEVO_COBRO.ACTIVO = false;
                        foreach (var _NOTA_COBRO in _NUEVO_COBRO.NOTA_COBRO)
                        {
                            _NOTA_COBRO.ACTIVO = false;
                            foreach (var _NOTA_COBRO_DETALLE in _NOTA_COBRO.NOTA_COBRO_DETALLE)
                            {
                                _NOTA_COBRO_DETALLE.ACTIVO = false;
                                context.ApplyPropertyChanges("NOTA_COBRO_DETALLE", _NOTA_COBRO_DETALLE);
                            }
                            context.ApplyPropertyChanges("NOTA_COBRO", _NOTA_COBRO);
                        }
                        context.ApplyPropertyChanges("COBRO", _NUEVO_COBRO);
                        context.SaveChanges();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public List<DTO_RESUMEN_FACTURA> GetResumenFacturasByFilter(int? ID_FACTURACION, int? ID_TIPO_FACTURA, string RUT_CLIENTE, string NOMBRE_CLIENTE,
            DateTime? FECHA_EMISION, int? NUMERO_FACTURA, bool? PAGADA, int PageIndex, int PageSize)
        {
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA _RepositorioVISTA_REPORTE_FACTURA = new RepositorioVISTA_REPORTE_FACTURA(context);

                    return (from f in _RepositorioVISTA_REPORTE_FACTURA.GetByFilter(ID_FACTURACION, ID_TIPO_FACTURA, FECHA_EMISION, "", null, null, null,
                            PAGADA, NOMBRE_CLIENTE, RUT_CLIENTE, "", "", NUMERO_FACTURA, null, "", "", "", null, null, null)
                            .OrderBy(f => f.FECHA_FACTURACION).ThenBy(f => f.NUMERO_FACTURA)
                            .Skip((PageIndex - 1) * PageSize).Take(PageSize)
                            select new DTO_RESUMEN_FACTURA
                            {
                                ID_FACTURA = f.ID,
                                NUMERO_FACTURA = f.NUMERO_FACTURA,
                                RUT_CLIENTE = f.RUT_CLIENTE,
                                NOMBRE_CLIENTE = f.NOMBRE_CLIENTE,
                                FECHA_EMISION = f.FECHA_FACTURACION,
                                VALOR_TOTAL = f.TOTAL,
                                VALOR_PAGADO = f.VALOR_PAGADO ?? 0,
                                PAGOS_REGISTRADOS = f.PAGOS_REGISTRADOS ?? 0,
                                SALDO_DEUDOR = f.SALDO_DEUDOR ?? 0,
                                PAGADA = f.PAGADA
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public int GetResumenFacturasByFilterCount(int? ID_FACTURACION, int? ID_TIPO_FACTURA, string RUT_CLIENTE, string NOMBRE_CLIENTE,
            DateTime? FECHA_EMISION, int? NUMERO_FACTURA, bool? PAGADA)
        {
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA _RepositorioVISTA_REPORTE_FACTURA = new RepositorioVISTA_REPORTE_FACTURA(context);

                    return (from f in _RepositorioVISTA_REPORTE_FACTURA.GetByFilter(ID_FACTURACION, ID_TIPO_FACTURA, FECHA_EMISION, "", null, null, null,
                            PAGADA, NOMBRE_CLIENTE, RUT_CLIENTE, "", "", NUMERO_FACTURA, null, "", "", "", null, null, null)
                            select f).Count();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public DTO_DETALLE_FACTURA GetDetalleFacturaById(int ID_FACTURA)
        {
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioVISTA_REPORTE_FACTURA _RepositorioVISTA_REPORTE_FACTURA = new RepositorioVISTA_REPORTE_FACTURA(context);
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    var f = _RepositorioVISTA_REPORTE_FACTURA.GetById(ID_FACTURA);
                    if (f == null)
                        throw new Exception("No se encuentra información de la factura");
                    var pagos = _RepositorioFACTURACION.GetPagosByIdFacturaWithReferencesFull(ID_FACTURA);
                    var detalle_factura = _RepositorioFACTURACION.GetFacturaDetalleByIdFactura(ID_FACTURA);
                    var notas_cobro = _RepositorioFACTURACION.GetNotasCobrosByIdFacturaWithReferencesFull(ID_FACTURA);

                    DTO_DETALLE_FACTURA _DTO_FACTURA = new DTO_DETALLE_FACTURA();
                    _DTO_FACTURA.ID_FACTURA = f.ID;
                    _DTO_FACTURA.NUMERO_FACTURA = f.NUMERO_FACTURA;
                    _DTO_FACTURA.RUT_CLIENTE = f.RUT_CLIENTE;
                    _DTO_FACTURA.NOMBRE_CLIENTE = f.NOMBRE_CLIENTE;
                    _DTO_FACTURA.FECHA_EMISION = f.FECHA_FACTURACION;
                    _DTO_FACTURA.VALOR_TOTAL = f.TOTAL;
                    _DTO_FACTURA.VALOR_PAGADO = f.VALOR_PAGADO ?? 0;
                    _DTO_FACTURA.PAGOS_REGISTRADOS = f.PAGOS_REGISTRADOS ?? 0;
                    _DTO_FACTURA.SALDO_DEUDOR = f.SALDO_DEUDOR ?? 0;
                    _DTO_FACTURA.PAGADA = f.PAGADA;

                    _DTO_FACTURA.LISTA_PRESTACIONES = (from df in detalle_factura
                                                       where df.ACTIVO && df.PRESTACION.ACTIVO
                                                       select new DTO_DETALLE_FACTURA_PRESTACION
                                                       {
                                                           ID_FACTURA_DETALLE = df.ID,
                                                           NUMERO_FICHA = df.PRESTACION.ID,
                                                           MONTO_TOTAL = df.MONTO_TOTAL,
                                                           MONTO_COBRADO = df.MONTO_COBRADO,
                                                           FECHA_RECEPCION = df.PRESTACION.FECHA_RECEPCION,
                                                           NOMBRE_PACIENTE = df.PRESTACION.PRESTACION_HUMANA != null ? df.PRESTACION.PRESTACION_HUMANA.NOMBRE : df.PRESTACION.PRESTACION_VETERINARIA.NOMBRE
                                                       }).ToList();

                    _DTO_FACTURA.LISTA_COBROS = (from c in notas_cobro
                                                 select new DTO_DETALLE_FACTURA_COBRO
                                                 {
                                                     ID_NOTA_COBRO = c.ID,
                                                     FECHA_COBRO = c.COBRO.FECHA_COBRO,
                                                     NOMBRE_TIPO_COBRO = c.COBRO.TIPO_COBRO.NOMBRE,
                                                     MONTO_PENDIENTE_TOTAL = c.NOTA_COBRO_DETALLE.Where(ncd => ncd.ACTIVO).Sum(ncd => ncd.MONTO_PENDIENTE),
                                                     MONTO_PENDIENTE_FACTURA = c.NOTA_COBRO_DETALLE.Where(ncd => ncd.ACTIVO && ncd.FACTURA.ID == ID_FACTURA).Sum(ncd => ncd.MONTO_PENDIENTE)
                                                 }).ToList();

                    _DTO_FACTURA.LISTA_PAGOS = (from p in pagos
                                                select new DTO_DETALLE_FACTURA_PAGO
                                                {
                                                    ID_PAGO = p.ID,
                                                    FECHA_PAGO = p.FECHA_PAGO,
                                                    MONTO_PAGO_TOTAL = p.MONTO_PAGO,
                                                    MONTO_PAGO_FACTURA = p.PAGO_DETALLE.Where(pd => pd.ACTIVO
                                                    && pd.FACTURA_DETALLE.ACTIVO
                                                    && pd.FACTURA_DETALLE.FACTURA.ID == ID_FACTURA).Sum(pd => pd.MONTO)
                                                }).ToList();

                    return _DTO_FACTURA;
                }
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