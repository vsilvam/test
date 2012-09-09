﻿using System;
using System.Collections.Generic;
using LQCE.Transaccion.DTO;
using LQCE.Modelo;
using LQCE.Repositorio;
using App.Infrastructure.Runtime;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;
using Microsoft.SharePoint;
using LQCE.Transaccion.Properties;

namespace LQCE.Transaccion
{
    public partial class TrxFACTURACION
    {
        private IList<Stream> m_streams;
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }


        private IList<Stream> m_streams2;
        private Stream CreateStream2(string name, string fileNameExtension, Encoding encoding,
            string mimeType, bool willSeek)
        {
            Stream stream2 = new MemoryStream();
            m_streams2.Add(stream2);
            return stream2;
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
                                RUT_CLIENTE = g.First().RUT,
                                NOMBRE_CLIENTE = g.First().NOMBRE,
                                CANTIDAD_PRESTACIONES = g.Count(),
                                TOTAL_PRESTACIONES = g.Sum(p => p.TOTAL),
                                DESCUENTO = g.First().DESCUENTO,
                                TOTAL_FACTURA = g.Sum(p => (int)(p.TOTAL * (1 - (double)g.First().DESCUENTO / 100.0)))
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
                        context.ApplyPropertyChanges("FACTURA", _FACTURA);

                        correlativo++;
                    }

                    context.SaveChanges();

                    // PENDIENTE: Generar PDFs
                    var LISTA_DTO_REPORTE_FACTURA = GetReporteFactura(_FACTURACION.ID);

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
                    m_streams = new List<Stream>();
                    m_streams2 = new List<Stream>(); 

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
                        // _ReportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ReporteAlertasSubreportProcessingEventHandler);

                        // PENDIENTE: Definir dinamicamente que factura se va a ocupar, dependiendo del cliente
                        _ReportViewer.LocalReport.ReportEmbeddedResource = "Marcas.Transacciones.Reporte." + facturas.Key;

                        _ReportViewer.LocalReport.Render("PDF", deviceInfo, CreateStream, out warnings);
                        foreach (Stream stream in m_streams)
                            stream.Position = 0;

                        using (SPWeb spWeb = new SPSite(Settings.Default.SP_WEB).OpenWeb())
                        {
                            SPList spList = spWeb.GetList(Settings.Default.SP_LIBRERIA_FACTURAS);
                            //spWeb.Fields.Add("http://siteurl/doclib/file.docx", m_streams[0], true);
                            string strNombreFactura = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + facturas.Key + ".pdf";
                            spList.RootFolder.Files.Add(spList.RootFolder.Url + "/" + strNombreFactura, m_streams[0], true);
                            spList.Update();
                        }
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

        protected List<DTO_REPORTE_FACTURA> GetReporteFactura(int IdFacturacion)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioFACTURACION _RepositorioFACTURACION = new RepositorioFACTURACION(context);

                    FACTURACION _FACTURACION = _RepositorioFACTURACION.GetByIdWithReferences(IdFacturacion);
                    if (_FACTURACION == null)
                        throw new Exception("No se encuentra información de la facturación");

                    return (from f in _FACTURACION.FACTURA
                            where f.ACTIVO
                            select new DTO_REPORTE_FACTURA
                            {
                                NOMBRE_REPORTE_FACTURA = f.TIPO_FACTURA.NOMBRE_REPORTE_FACTURA,
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
                                TOTAL = f.TOTAL
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

        public void NumerarFacturas()
        {
            // PENDIENTE IMPLEMENTACION
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
