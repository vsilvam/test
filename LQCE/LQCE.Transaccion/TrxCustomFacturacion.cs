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

namespace LQCE.Transaccion
{
    public partial class TrxFACTURACION
    {
         List<DTO_REPORTE_DETALLEFACTURA_PRESTACION> ListaDetalleFactura;

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
                        if(_CLIENTE == null)
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
                            if(_PRESTACION == null)
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
                                spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_matriz[0], true);
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
                            spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_DetalleFactura[0], true);
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
                                NUMERO_FACTURA = f.NUMERO_FACTURA
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
                             group f by new { ID_FACTURACION = f.FACTURACION.ID, ID_TIPO_FACTURA = f.TIPO_FACTURA.ID } into g
                             select new DTO_RESUMEN_FACTURACION
                             {
                                 ID_FACTURACION = g.Key.ID_FACTURACION,
                                 ID_TIPO_FACTURA = g.Key.ID_TIPO_FACTURA,
                                 NOMBRE_TIPO_FACTURA = g.First().TIPO_FACTURA.NOMBRE_FACTURA,
                                 FECHA_FACTURACION = g.First().FACTURACION.FECHA_FACTURACION,
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
                        null, "", null, null, null, null, null, "", "", "", "", "", "", "");

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
                        null, "", null, null, null, null, null, "", "", "", "", "", "", "");
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
                                    spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_matriz[0], true);
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
                                spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams_DetalleFactura[0], true);
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

        public void AnularFacturas()
        {
            // PENDIENTE IMPLEMENTACION
        }

        public void EmitirNotaCredito()
        {
            // PENDIENTE IMPLEMENTACION
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
                                       Facturas = g.ToList<VISTA_FACTURAS_POR_NOTIFICAR>()
                                   }).ToList();

                    int correlativo = 1;
                    foreach (var cf in cliente_facturas)
                    {
                        NOTA_COBRO _NOTA_COBRO = new NOTA_COBRO();
                        _NOTA_COBRO.COBRO = _COBRO;
                        _NOTA_COBRO.CORRELATIVO = correlativo;
                        _NOTA_COBRO.ID_CLIENTE = cf.IdCliente; // PENDIENTE: Cambiar por objeto CLIENTE
                        _NOTA_COBRO.ACTIVO = true;
                        context.AddToNOTA_COBRO(_NOTA_COBRO);

                        foreach (var f in cf.Facturas)
                        {
                            FACTURA _FACTURA = _RepositorioFACTURA.GetById(f.ID);
                            if(_FACTURA == null)
                                throw new Exception("No se encuentra información de la factura ");

                            NOTA_COBRO_DETALLE _NOTA_COBRO_DETALLE = new NOTA_COBRO_DETALLE();
                            _NOTA_COBRO_DETALLE.NOTA_COBRO = _NOTA_COBRO;
                            _NOTA_COBRO_DETALLE.FACTURA = _FACTURA;
                            _NOTA_COBRO_DETALLE.MONTO_PENDIENTE = 0; // PENDIENTE: Calcular saldo pendiente
                            _NOTA_COBRO_DETALLE.ACTIVO = true;
                            context.AddToNOTA_COBRO_DETALLE(_NOTA_COBRO_DETALLE);
                        }

                        correlativo++;
                    }

                    context.SaveChanges();
                    
                    // PENDIENTE: Generar PDFs
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