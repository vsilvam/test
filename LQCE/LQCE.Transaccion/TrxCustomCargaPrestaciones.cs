using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;
using LQCE.Transaccion.DTO;
using LQCE.Transaccion.Enum;
using System.Linq;

namespace LQCE.Transaccion
{
    public partial class TrxCARGA_PRESTACIONES_ENCABEZADO
    {
        /// <summary>
        /// Realiza carga en tablas temporales del contenido del archivo Excel
        /// </summary>
        /// <param name="IdTipoPrestacion">Id. de tipo de prestación</param>
        /// <param name="NombreArchivo">Nombre del archivo adjunto</param>
        /// <param name="ContenidoArchivo">Contenido binario del archivo adjunto</param>
        /// <returns>Retorna Id. de encabezado de carga de prestaciones</returns>
        public int UploadArchivoPrestaciones(int IdTipoPrestacion, string NombreArchivo, byte[] ContenidoArchivo)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    if (string.IsNullOrEmpty(NombreArchivo))
                        throw new Exception("No se ha señalado nombre de archivo Excel");
                    if (ContenidoArchivo == null)
                        throw new Exception("No se ha incluido contenido del archivo Excel");

                    RepositorioTIPO_PRESTACION _RepositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
                    RepositorioCARGA_PRESTACIONES_ESTADO _RepositorioCARGA_PRESTACIONES_ESTADO = new RepositorioCARGA_PRESTACIONES_ESTADO(context);
                    RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);

                    var objTipoPrestacion = _RepositorioTIPO_PRESTACION.GetById(IdTipoPrestacion);
                    if (objTipoPrestacion == null)
                        throw new Exception("No se ha encontrado información del Tipo de Prestación");

                    var objEstado = _RepositorioCARGA_PRESTACIONES_ESTADO.GetById((int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente);
                    if (objEstado == null)
                        throw new Exception("No se ha encontrado información del Estado de Carga de Prestaciones");

                    var objEstadoDetalle = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById((int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Pendiente);
                    if (objEstadoDetalle == null)
                        throw new Exception("No se ha encontrado información del Estado de Detalle de Carga de Prestaciones");

                    string archivo = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + NombreArchivo;
                    File.WriteAllBytes(Properties.Settings.Default.DIR_CARGA_EXCEL + archivo, ContenidoArchivo);

                    var datos = ISExcel.ReadExcelFile(Properties.Settings.Default.DIR_CARGA_EXCEL + archivo, true);
                    if (datos == null)
                        throw new Exception("No se ha leido información en la planilla Excel");

                    CARGA_PRESTACIONES_ENCABEZADO objEncabezado = new CARGA_PRESTACIONES_ENCABEZADO();
                    objEncabezado.FECHA_CARGA = DateTime.Now;
                    objEncabezado.TIPO_PRESTACION = objTipoPrestacion;
                    objEncabezado.CARGA_PRESTACIONES_ESTADO = objEstado;
                    objEncabezado.ARCHIVO = archivo;
                    objEncabezado.ACTIVO = true;
                    context.AddToCARGA_PRESTACIONES_ENCABEZADO(objEncabezado);

                    if (IdTipoPrestacion == (int)ENUM_TIPO_PRESTACION.Humanas)
                    {
                        foreach (DataRow item in datos.Rows)
                        {
                            CARGA_PRESTACIONES_HUMANAS_DETALLE objDetalle = new CARGA_PRESTACIONES_HUMANAS_DETALLE();
                            objDetalle.CARGA_PRESTACIONES_ENCABEZADO = objEncabezado;
                            objDetalle.FICHA = item["FICHA"].ToString();
                            objDetalle.NOMBRE = item["NOMBRE"].ToString();
                            objDetalle.RUT = item["RUT"].ToString();
                            objDetalle.MEDICO = item["MEDICO"].ToString();
                            objDetalle.EDAD = item["EDAD"].ToString();
                            objDetalle.TELEFONO = item["TELEFONO"].ToString();
                            objDetalle.PROCEDENCIA = item["PROCEDENCIA"].ToString();
                            objDetalle.FECHA_RECEPCION = item["FECHA RECEPCION"].ToString();
                            objDetalle.MUESTRA = item["MUESTRA"].ToString();
                            objDetalle.FECHA_RESULTADOS = item["FECHA RESULTADOS"].ToString();
                            objDetalle.PREVISION = item["PREVISION"].ToString();
                            objDetalle.GARANTIA = item["GARANTIA"].ToString();
                            objDetalle.PAGADO = item["PAGADO"].ToString();
                            objDetalle.PENDIENTE = item["PENDIENTE"].ToString();
                            objDetalle.ACTIVO = true;
                            objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                            objDetalle.MENSAJE_ERROR = "";
                            objDetalle.FECHA_ACTUALIZACION = DateTime.Now;
                            objDetalle.CLIENTE = null;
                            objDetalle.VALOR_FICHA = null;
                            objDetalle.VALOR_FECHA_MUESTRA = null;
                            objDetalle.VALOR_FECHA_RECEPCION = null;
                            objDetalle.PREVISION1 = null;
                            objDetalle.GARANTIA1 = null;
                            objDetalle.VALOR_FECHA_ENTREGA_RESULTADOS = null;
                            context.AddToCARGA_PRESTACIONES_HUMANAS_DETALLE(objDetalle);

                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 5", "VALOR 5");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 6", "VALOR 6");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 7", "VALOR 7");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 8", "VALOR 8");

                            ValidarPrestacionHumana(context, objDetalle);
                        }
                    }
                    else if (IdTipoPrestacion == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                    {
                        foreach (DataRow item in datos.Rows)
                        {
                            CARGA_PRESTACIONES_VETERINARIAS_DETALLE objDetalle = new CARGA_PRESTACIONES_VETERINARIAS_DETALLE();
                            objDetalle.CARGA_PRESTACIONES_ENCABEZADO = objEncabezado;
                            objDetalle.FICHA = item["FICHA"].ToString();
                            objDetalle.NOMBRE = item["NOMBRE"].ToString();
                            objDetalle.ESPECIE = item["ESPECIE"].ToString();
                            objDetalle.RAZA = item["RAZA"].ToString();
                            objDetalle.EDAD = item["EDAD"].ToString();
                            objDetalle.SEXO = item["SEXO"].ToString();
                            objDetalle.SOLICITA = item["SOLICITA"].ToString();
                            objDetalle.TELEFONO = item["TELEFONO"].ToString();
                            objDetalle.MEDICO = item["MEDICO"].ToString();
                            objDetalle.PROCEDENCIA = item["PROCEDENCIA"].ToString();
                            objDetalle.FECHA_RECEPCION = item["FECHA RECEPCION"].ToString();
                            objDetalle.FECHA_MUESTRA = item["FECHA MUESTRA"].ToString();
                            objDetalle.FECHA_RESULTADOS = item["FECHA RESULTADOS"].ToString();
                            objDetalle.PENDIENTE = item["PENDIENTE"].ToString();
                            objDetalle.GARANTIA = item["GARANTIA"].ToString();
                            objDetalle.PAGADO = item["PAGADO"].ToString();
                            objDetalle.TOTAL = item["TOTAL"].ToString();
                            objDetalle.ACTIVO = true;
                            objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                            objDetalle.MENSAJE_ERROR = "";
                            objDetalle.FECHA_ACTUALIZACION = DateTime.Now;
                            objDetalle.VALOR_FICHA = null;
                            objDetalle.CLIENTE = null;
                            objDetalle.VALOR_FECHA_MUESTRA = null;
                            objDetalle.VALOR_FECHA_RECEPCION = null;
                            objDetalle.PREVISION = null;
                            objDetalle.GARANTIA1 = null;
                            objDetalle.VALOR_FECHA_ENTREGA_RESULTADOS = null;
                            objDetalle.ESPECIE1 = null;
                            objDetalle.RAZA1 = null;
                            context.AddToCARGA_PRESTACIONES_VETERINARIAS_DETALLE(objDetalle);

                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 5", "VALOR 5");

                            ValidarPrestacionVeterinaria(context, objDetalle);
                        }
                    }
                    else
                    {
                        throw new Exception("Tipo de prestación no válido");
                    }

                    context.SaveChanges();

                    return objEncabezado.ID;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        private void AgregarExamenHumano(LQCEEntities context, CARGA_PRESTACIONES_HUMANAS_DETALLE objDetalle, DataRow item, string ColumnaExamen, string ColumnaValor)
        {
            if (!string.IsNullOrEmpty(item[ColumnaExamen].ToString()) || !string.IsNullOrEmpty(item[ColumnaValor].ToString()))
            {
                CARGA_PRESTACIONES_HUMANAS_EXAMEN objExamen = new CARGA_PRESTACIONES_HUMANAS_EXAMEN();
                objExamen.CARGA_PRESTACIONES_HUMANAS_DETALLE = objDetalle;
                objExamen.NOMBRE_EXAMEN = item[ColumnaExamen].ToString();
                objExamen.VALOR_EXAMEN = item[ColumnaValor].ToString();
                objExamen.ACTIVO = true;
                objExamen.FECHA_ACTUALIZACION = DateTime.Now;
                objExamen.EXAMEN = null;
                context.AddToCARGA_PRESTACIONES_HUMANAS_EXAMEN(objExamen);
            }
        }

        private void AgregarExamenVeterinario(LQCEEntities context, CARGA_PRESTACIONES_VETERINARIAS_DETALLE objDetalle, DataRow item, string ColumnaExamen, string ColumnaValor)
        {
            if (!string.IsNullOrEmpty(item[ColumnaExamen].ToString()) || !string.IsNullOrEmpty(item[ColumnaValor].ToString()))
            {
                CARGA_PRESTACIONES_VETERINARIAS_EXAMEN objExamen = new CARGA_PRESTACIONES_VETERINARIAS_EXAMEN();
                objExamen.CARGA_PRESTACIONES_VETERINARIAS_DETALLE = objDetalle;
                objExamen.NOMBRE_EXAMEN = item[ColumnaExamen].ToString();
                objExamen.VALOR_EXAMEN = item[ColumnaValor].ToString();
                objExamen.ACTIVO = true;
                objExamen.FECHA_ACTUALIZACION = DateTime.Now;
                objExamen.EXAMEN = null;
                context.AddToCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(objExamen);
            }
        }

        /// <summary>
        /// Obtiene listado de resumen de carga de prestaciones
        /// </summary>
        /// <param name="IdCargaPrestacionesEstado">(Opcional) Filtra por estado de carga</param>
        /// <returns>Listado de resumen de carga de prestaciones</returns>
        public List<DTO_RESUMEN_CARGA_PRESTACIONES> GetResumenCargaPrestaciones(int? IdCargaPrestacionesEstado, int? IdTipoPrestacion)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);

                    int IdEstadoDetalleValidado = (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Validado;
                    int IdEstadoDetalleConError = (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.ConError;

                    var q = from item in repositorio.GetByFilterWithReferences(IdCargaPrestacionesEstado,
                            IdTipoPrestacion, null, "")
                            select new DTO_RESUMEN_CARGA_PRESTACIONES
                            {
                                ID = item.ID,
                                ARCHIVO = item.ARCHIVO,
                                FECHA_CARGA = item.FECHA_CARGA,
                                FECHA_ACTUALIZACION = item.FECHA_CARGA,
                                //FECHA_ACTUALIZACION = (item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) > item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) ?
                                //    item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) : item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION),
                                ID_ESTADO = item.CARGA_PRESTACIONES_ESTADO.ID,
                                NOMBRE_ESTADO = item.CARGA_PRESTACIONES_ESTADO.NOMBRE,
                                ID_TIPO_PRESTACION = item.TIPO_PRESTACION.ID,
                                NOMBRE_TIPO_PRESTACION = item.TIPO_PRESTACION.NOMBRE,
                                TOTAL_REGISTROS = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count()
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(),
                                REGISTROS_VALIDADOS = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdEstadoDetalleValidado)
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdEstadoDetalleValidado),
                                REGISTROS_CON_ERRORES = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdEstadoDetalleConError)
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdEstadoDetalleConError)
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

        public List<DTO_DETALLE_CARGA_PRESTACIONES> GetDetalleCargaPrestaciones(DTOFindPrestaciones dto)
        {
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO _RepositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

                    CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferences(dto.id);
                    if (objEncabezado == null)
                        throw new Exception("No se encuentra informacion de carga de prestaciones");

                    if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
                    {
                        var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(dto.id,
                                dto.estado, null, null, null, null, dto.numero, dto.nombre,
                                "", "", "", "", dto.prodedencia, "", "", "", "", "", "", "", "", "", null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(10)
                                select new DTO_DETALLE_CARGA_PRESTACIONES
                                {
                                    ID = item.ID,
                                    ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
                                    NUMERO_FICHA = item.FICHA,
                                    NOMBRE = item.NOMBRE,
                                    ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
                                    NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
                                    PROCEDENCIA = item.PROCEDENCIA,
                                    FECHA_RECEPCION = item.FECHA_RECEPCION
                                };

                        return r.ToList();
                    }
                    else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                    {
                        var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(dto.id,
                                dto.estado, null, null, null, null, null, null,
                                dto.numero, dto.nombre, "", "", "", "", "", "", "", dto.prodedencia,
                                "", "", "", "", "", "", "", "", null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(10)
                                select new DTO_DETALLE_CARGA_PRESTACIONES
                                {
                                    ID = item.ID,
                                    ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
                                    NUMERO_FICHA = item.FICHA,
                                    NOMBRE = item.NOMBRE,
                                    ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
                                    NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
                                    PROCEDENCIA = item.PROCEDENCIA,
                                    FECHA_RECEPCION = item.FECHA_RECEPCION
                                };

                        return r.ToList();
                    }
                    else
                    {
                        throw new Exception("Tipo de carga no identificada");
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

        //public List<DTO_DETALLE_CARGA_PRESTACIONES> GetDetalleCargaPrestaciones(int IdCargaPrestacionesEncabezado,
        //    string NUMERO_FICHA, string NOMBRE, int? ID_ESTADO_DETALLE, string PROCEDENCIA, int PAGINA, int REGISTROS)
        //{
        //    try
        //    {
        //        using (LQCEEntities context = new LQCEEntities())
        //        {
        //            RepositorioCARGA_PRESTACIONES_ENCABEZADO _RepositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
        //            RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
        //            RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

        //            CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferences(IdCargaPrestacionesEncabezado);
        //            if (objEncabezado == null)
        //                throw new Exception("No se encuentra informacion de carga de prestaciones");

        //            if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
        //            {
        //                var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(IdCargaPrestacionesEncabezado,
        //                        ID_ESTADO_DETALLE, null, null, null, null, NUMERO_FICHA, NOMBRE,
        //                        "", "", "", "", PROCEDENCIA, "", "", "", "", "", "", "", "", "", null, null, null, null)
        //                        select d;

        //                var r = from item in q.OrderBy(d => d.ID).Skip((PAGINA - 1) * REGISTROS).Take(10)
        //                        select new DTO_DETALLE_CARGA_PRESTACIONES
        //                        {
        //                            ID = item.ID,
        //                            ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
        //                            NUMERO_FICHA = item.FICHA,
        //                            NOMBRE = item.NOMBRE,
        //                            ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
        //                            NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
        //                            PROCEDENCIA = item.PROCEDENCIA,
        //                            FECHA_RECEPCION = item.FECHA_RECEPCION
        //                        };

        //                return r.ToList();
        //            }
        //            else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
        //            {
        //                var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(IdCargaPrestacionesEncabezado,
        //                        ID_ESTADO_DETALLE, null, null, null, null, null, null,
        //                        NUMERO_FICHA, NOMBRE, "", "", "", "", "", "", "", PROCEDENCIA,
        //                        "", "", "", "", "", "", "", "", null, null, null, null)
        //                        select d;

        //                var r = from item in q.OrderBy(d => d.ID).Skip((PAGINA - 1) * REGISTROS).Take(10)
        //                        select new DTO_DETALLE_CARGA_PRESTACIONES
        //                        {
        //                            ID = item.ID,
        //                            ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
        //                            NUMERO_FICHA = item.FICHA,
        //                            NOMBRE = item.NOMBRE,
        //                            ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
        //                            NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
        //                            PROCEDENCIA = item.PROCEDENCIA,
        //                            FECHA_RECEPCION = item.FECHA_RECEPCION
        //                        };

        //                return r.ToList();
        //            }
        //            else
        //            {
        //                throw new Exception("Tipo de carga no identificada");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ISException.RegisterExcepcion(ex);
        //        Error = ex.Message;
        //        throw ex;
        //    }
        //}
                
        public int GetDetalleCargaPrestacionesCount(DTOFindPrestaciones dto)
        {
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO _RepositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

                    CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferences(dto.id);
                    if (objEncabezado == null)
                        throw new Exception("No se encuentra informacion de carga de prestaciones");

                    if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
                    {
                        var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(dto.id,
                                dto.estado, null, null, null, null, dto.numero, dto.nombre,
                                "", "", "", "", dto.prodedencia, "", "", "", "", "", "", "", "", "", null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(10)
                                select new DTO_DETALLE_CARGA_PRESTACIONES
                                {
                                    ID = item.ID,
                                    ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
                                    NUMERO_FICHA = item.FICHA,
                                    NOMBRE = item.NOMBRE,
                                    ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
                                    NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
                                    PROCEDENCIA = item.PROCEDENCIA,
                                    FECHA_RECEPCION = item.FECHA_RECEPCION
                                };

                        return r.ToList().Count();
                    }
                    else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                    {
                        var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(dto.id,
                                dto.estado, null, null, null, null, null, null,
                                dto.numero, dto.nombre, "", "", "", "", "", "", "", dto.prodedencia,
                                "", "", "", "", "", "", "", "", null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(10)
                                select new DTO_DETALLE_CARGA_PRESTACIONES
                                {
                                    ID = item.ID,
                                    ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
                                    NUMERO_FICHA = item.FICHA,
                                    NOMBRE = item.NOMBRE,
                                    ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
                                    NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
                                    PROCEDENCIA = item.PROCEDENCIA,
                                    FECHA_RECEPCION = item.FECHA_RECEPCION
                                };

                        return r.ToList().Count();
                    }
                    else
                    {
                        throw new Exception("Tipo de carga no identificada");
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

        //public int GetDetalleCargaPrestacionesCount(int IdCargaPrestacionesEncabezado,
        //    string NUMERO_FICHA, string NOMBRE, int? ID_ESTADO_DETALLE, string PROCEDENCIA, int PAGINA, int REGISTROS)
        //{
        //    try
        //    {
        //        using (LQCEEntities context = new LQCEEntities())
        //        {
        //            RepositorioCARGA_PRESTACIONES_ENCABEZADO _RepositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
        //            RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
        //            RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

        //            CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferences(IdCargaPrestacionesEncabezado);
        //            if (objEncabezado == null)
        //                throw new Exception("No se encuentra informacion de carga de prestaciones");

        //            if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
        //            {
        //                var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(IdCargaPrestacionesEncabezado,
        //                        ID_ESTADO_DETALLE, null, null, null, null, NUMERO_FICHA, NOMBRE,
        //                        "", "", "", "", PROCEDENCIA, "", "", "", "", "", "", "", "", "", null, null, null, null)
        //                        select d;

        //                var r = from item in q.OrderBy(d => d.ID).Skip((PAGINA - 1) * REGISTROS).Take(10)
        //                        select new DTO_DETALLE_CARGA_PRESTACIONES
        //                        {
        //                            ID = item.ID,
        //                            ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
        //                            NUMERO_FICHA = item.FICHA,
        //                            NOMBRE = item.NOMBRE,
        //                            ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
        //                            NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
        //                            PROCEDENCIA = item.PROCEDENCIA,
        //                            FECHA_RECEPCION = item.FECHA_RECEPCION
        //                        };

        //                return r.ToList().Count();
        //            }
        //            else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
        //            {
        //                var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(IdCargaPrestacionesEncabezado,
        //                        ID_ESTADO_DETALLE, null, null, null, null, null, null,
        //                        NUMERO_FICHA, NOMBRE, "", "", "", "", "", "", "", PROCEDENCIA,
        //                        "", "", "", "", "", "", "", "", null, null, null, null)
        //                        select d;

        //                var r = from item in q.OrderBy(d => d.ID).Skip((PAGINA - 1) * REGISTROS).Take(10)
        //                        select new DTO_DETALLE_CARGA_PRESTACIONES
        //                        {
        //                            ID = item.ID,
        //                            ID_TIPO_PRESTACION = item.CARGA_PRESTACIONES_ENCABEZADO.TIPO_PRESTACION.ID,
        //                            NUMERO_FICHA = item.FICHA,
        //                            NOMBRE = item.NOMBRE,
        //                            ID_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.ID,
        //                            NOMBRE_ESTADO_DETALLE = item.CARGA_PRESTACIONES_DETALLE_ESTADO.NOMBRE,
        //                            PROCEDENCIA = item.PROCEDENCIA,
        //                            FECHA_RECEPCION = item.FECHA_RECEPCION
        //                        };

        //                return r.ToList().Count();
        //            }
        //            else
        //            {
        //                throw new Exception("Tipo de carga no identificada");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ISException.RegisterExcepcion(ex);
        //        Error = ex.Message;
        //        throw ex;
        //    }
        //}

        public void CambiarEstadoCarga(int IdCargaPrestacionesEncabezado, int IdCargaPrestacionesEstado)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO _RepositorioCARGA_PRESTACIONES_ENCABEZADO = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    RepositorioCARGA_PRESTACIONES_ESTADO _RepositorioCARGA_PRESTACIONES_ESTADO = new RepositorioCARGA_PRESTACIONES_ESTADO(context);

                    CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferences(IdCargaPrestacionesEncabezado);
                    if (objEncabezado == null)
                        throw new Exception("No se encuentra informacion de la carga");

                    CARGA_PRESTACIONES_ESTADO objEstado = _RepositorioCARGA_PRESTACIONES_ESTADO.GetById(IdCargaPrestacionesEstado);
                    if (objEstado == null)
                        throw new Exception("No se encuentra informacion del estado");

                    // Validaciones
                    if (objEncabezado.CARGA_PRESTACIONES_ESTADO.ID != (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                        throw new Exception("La carga seleccionada no esta en revision pendiente");

                    int IdCargaDetalleEstadoPendiente = (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Pendiente;

                    if (objEstado.ID == (int)ENUM_CARGA_PRESTACIONES_ESTADO.Completado)
                        if (objEncabezado.CARGA_PRESTACIONES_HUMANAS_DETALLE
                            .Any(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdCargaDetalleEstadoPendiente)
                            || objEncabezado.CARGA_PRESTACIONES_VETERINARIAS_DETALLE
                            .Any(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdCargaDetalleEstadoPendiente))
                            throw new Exception("La carga no se puede completar mientras tenga fichas pendientes");

                    objEncabezado.CARGA_PRESTACIONES_ESTADO = objEstado;
                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_ENCABEZADO", objEncabezado);

                    // PENDIENTE: Mover prestaciones y examenes a tablas definitivas

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

        public DTO_RESULTADO_ACTUALIZACION_FICHA ActualizarCargaPrestacionHumana(int IdCargaPrestacionHumanaDetalle, string Ficha, string Nombre,
            string Rut, string Medico, string Edad, string Telefono, string Procedencia, string FechaRecepcion,
            string Muestra, string FechaResultados, string Prevision, string Garantia, string Pagado,
            string Pendiente, int IdCargaPrestacionesDetalleEstado, string MensajeError,
            List<DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN> Examenes)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);

                    var objEstadoDetalle = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById(IdCargaPrestacionesDetalleEstado);
                    if (objEstadoDetalle == null)
                        throw new Exception("No se encuentra estado de detalle");

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferences(IdCargaPrestacionHumanaDetalle);
                    if (objDetalle.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID != (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                        throw new Exception("La carga seleccionada no se encuentra en proceso de Revisión Pendiente");

                    objDetalle.FICHA = Ficha;
                    objDetalle.NOMBRE = Nombre;
                    objDetalle.RUT = Rut;
                    objDetalle.MEDICO = Medico;
                    objDetalle.EDAD = Edad;
                    objDetalle.TELEFONO = Telefono;
                    objDetalle.PROCEDENCIA = Procedencia;
                    objDetalle.FECHA_RECEPCION = FechaRecepcion;
                    objDetalle.MUESTRA = Muestra;
                    objDetalle.FECHA_RESULTADOS = FechaResultados;
                    objDetalle.PREVISION = Prevision;
                    objDetalle.GARANTIA = Garantia;
                    objDetalle.PAGADO = Pagado;
                    objDetalle.PENDIENTE = Pendiente;
                    objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                    objDetalle.MENSAJE_ERROR = MensajeError;
                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_DETALLE", objDetalle);

                    // Eliminar filas de examenes
                    foreach (var objExamen in objDetalle.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Where(obj => obj.ACTIVO && !Examenes.Any(dtoExamen => dtoExamen.ID == obj.ID)).ToList())
                    {
                        objExamen.ACTIVO = false;
                        context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_EXAMEN", objExamen);
                    }

                    // Actualizar filas de examenes
                    foreach (var objExamen in objDetalle.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Where(obj => obj.ACTIVO && Examenes.Any(dtoExamen => dtoExamen.ID == obj.ID)).ToList())
                    {
                        var dtoExamen = Examenes.First(d => d.ID == objExamen.ID);
                        objExamen.NOMBRE_EXAMEN = dtoExamen.NOMBRE_EXAMEN;
                        objExamen.VALOR_EXAMEN = dtoExamen.VALOR_EXAMEN;
                        objExamen.ACTIVO = false;
                        context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_EXAMEN", objExamen);
                    }

                    // Nuevas filas de examenes
                    foreach (var dtoExamen in Examenes.Where(dto => !objDetalle.CARGA_PRESTACIONES_HUMANAS_EXAMEN.Any(obj => obj.ID == dto.ID)).ToList())
                    {
                        CARGA_PRESTACIONES_HUMANAS_EXAMEN objExamen = new CARGA_PRESTACIONES_HUMANAS_EXAMEN();
                        objExamen.CARGA_PRESTACIONES_HUMANAS_DETALLE = objDetalle;
                        objExamen.NOMBRE_EXAMEN = dtoExamen.NOMBRE_EXAMEN;
                        objExamen.VALOR_EXAMEN = dtoExamen.VALOR_EXAMEN;
                        objExamen.ACTIVO = true;
                        context.AddToCARGA_PRESTACIONES_HUMANAS_EXAMEN(objExamen);
                    }

                    DTO_RESULTADO_ACTUALIZACION_FICHA dtoResultado = new DTO_RESULTADO_ACTUALIZACION_FICHA();
                    dtoResultado.ERRORES_VALIDACION = ValidarPrestacionHumana(context, objDetalle);
                    dtoResultado.RESULTADO = !dtoResultado.ERRORES_VALIDACION.Any();

                    context.SaveChanges();

                    return dtoResultado;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public DTO_RESULTADO_ACTUALIZACION_FICHA ActualizarCargaPrestacionVeterinarias(int IdCargaPrestacionVeterinariaDetalle, string Ficha, string Nombre,
               string Especie, string Raza, string Edad, string Sexo, string Solicita, string Telefono, string Medico,
            string Procedencia, string FechaRecepcion, string FechaMuestra, string FechaResultados, string Pendiente,
            string Garantia, string Pagado, string Total,
               int IdCargaPrestacionesDetalleEstado, string MensajeError,
               List<DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> Examenes)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

                    RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);

                    var objEstadoDetalle = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById(IdCargaPrestacionesDetalleEstado);
                    if (objEstadoDetalle == null)
                        throw new Exception("No se encuentra estado de detalle");

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferences(IdCargaPrestacionVeterinariaDetalle);
                    if (objDetalle.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID != (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                        throw new Exception("La carga seleccionada no se encuentra en proceso de Revisión Pendiente");

                    objDetalle.FICHA = Ficha;
                    objDetalle.NOMBRE = Nombre;
                    objDetalle.ESPECIE = Especie;
                    objDetalle.RAZA = Raza;
                    objDetalle.EDAD = Edad;
                    objDetalle.SEXO = Sexo;
                    objDetalle.SOLICITA = Solicita;
                    objDetalle.TELEFONO = Telefono;
                    objDetalle.MEDICO = Medico;
                    objDetalle.PROCEDENCIA = Procedencia;
                    objDetalle.FECHA_RECEPCION = FechaRecepcion;
                    objDetalle.FECHA_MUESTRA = FechaMuestra;
                    objDetalle.FECHA_RESULTADOS = FechaResultados;
                    objDetalle.PENDIENTE = Pendiente;
                    objDetalle.GARANTIA = Garantia;
                    objDetalle.PAGADO = Pagado;
                    objDetalle.TOTAL = Total;
                    objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                    objDetalle.MENSAJE_ERROR = MensajeError;
                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_DETALLE", objDetalle);

                    // Eliminar filas de examenes
                    foreach (var objExamen in objDetalle.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Where(obj => obj.ACTIVO && !Examenes.Any(dtoExamen => dtoExamen.ID == obj.ID)).ToList())
                    {
                        objExamen.ACTIVO = false;
                        context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN", objExamen);
                    }

                    // Actualizar filas de examenes
                    foreach (var objExamen in objDetalle.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Where(obj => obj.ACTIVO && Examenes.Any(dtoExamen => dtoExamen.ID == obj.ID)).ToList())
                    {
                        var dtoExamen = Examenes.First(d => d.ID == objExamen.ID);
                        objExamen.NOMBRE_EXAMEN = dtoExamen.NOMBRE_EXAMEN;
                        objExamen.VALOR_EXAMEN = dtoExamen.VALOR_EXAMEN;
                        objExamen.ACTIVO = false;
                        context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN", objExamen);
                    }

                    // Nuevas filas de examenes
                    foreach (var dtoExamen in Examenes.Where(dto => !objDetalle.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Any(obj => obj.ID == dto.ID)).ToList())
                    {
                        CARGA_PRESTACIONES_VETERINARIAS_EXAMEN objExamen = new CARGA_PRESTACIONES_VETERINARIAS_EXAMEN();
                        objExamen.CARGA_PRESTACIONES_VETERINARIAS_DETALLE = objDetalle;
                        objExamen.NOMBRE_EXAMEN = dtoExamen.NOMBRE_EXAMEN;
                        objExamen.VALOR_EXAMEN = dtoExamen.VALOR_EXAMEN;
                        objExamen.ACTIVO = true;
                        context.AddToCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(objExamen);
                    }

                    DTO_RESULTADO_ACTUALIZACION_FICHA dtoResultado = new DTO_RESULTADO_ACTUALIZACION_FICHA();
                    dtoResultado.ERRORES_VALIDACION = ValidarPrestacionVeterinaria(context, objDetalle);
                    dtoResultado.RESULTADO = !dtoResultado.ERRORES_VALIDACION.Any();

                    context.SaveChanges();

                    return dtoResultado;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        private List<string> ValidarPrestacionHumana(LQCEEntities context, CARGA_PRESTACIONES_HUMANAS_DETALLE objDetalle)
        {
            RepositorioPRESTACION_HUMANA _RepositorioPRESTACION_HUMANA = new RepositorioPRESTACION_HUMANA(context);
            RepositorioCLIENTE _RepositorioCLIENTE = new RepositorioCLIENTE(context);
            RepositorioCLIENTE_SINONIMO _RepositorioCLIENTE_SINONIMO = new RepositorioCLIENTE_SINONIMO(context);
            RepositorioPREVISION _RepositorioPREVISION = new RepositorioPREVISION(context);
            RepositorioGARANTIA _RepositorioGARANTIA = new RepositorioGARANTIA(context);
            RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);
            RepositorioEXAMEN _RepositorioEXAMEN = new RepositorioEXAMEN(context);
            RepositorioEXAMEN_SINONIMO _RepositorioEXAMEN_SINONIMO = new RepositorioEXAMEN_SINONIMO(context);


            var EstadoConError = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById((int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.ConError);
            if(EstadoConError == null)
                throw new Exception("No se encuentra registro de error en la tabla Estado de Detalle Carga Prestaciones");

            List<string> ListaValidaciones = new List<string>();

            // Ficha
            if (string.IsNullOrEmpty(objDetalle.FICHA))
            {
                ListaValidaciones.Add("No se ha señalado numero de ficha");
            }
            else
            {
                int? _ficha = ISConvert.ToNullableInteger(objDetalle.FICHA);
                if (!_ficha.HasValue)
                {
                    ListaValidaciones.Add("FICHA debe ser numérico");
                }
                else
                {
                    objDetalle.VALOR_FICHA = _ficha.Value;

                    var objPrestacionHumana = _RepositorioPRESTACION_HUMANA.GetById(_ficha.Value);
                    if (objPrestacionHumana != null)
                        ListaValidaciones.Add("Ya existe una prestación en el sistema con el mismo número de ficha");
                }
            }

            // Cliente
            if (string.IsNullOrEmpty(objDetalle.PROCEDENCIA))
            {
                ListaValidaciones.Add("No se ha señalado PROCEDENCIA en la ficha");
            }
            else
            {
                var objCliente = _RepositorioCLIENTE.GetByFilter(null, null, null, "", objDetalle.PROCEDENCIA).FirstOrDefault();
                if (objCliente != null)
                {
                    objDetalle.CLIENTE = objCliente;
                }
                else
                {
                    var objClienteSinonimo = _RepositorioCLIENTE_SINONIMO.GetByFilterWithReferences(null, objDetalle.PROCEDENCIA).FirstOrDefault();
                    if (objClienteSinonimo != null)
                    {
                        objClienteSinonimo.CLIENTE = objClienteSinonimo.CLIENTE;
                    }
                    else
                    {
                        ListaValidaciones.Add("No se ha podido identificar cliente de la prestación");
                    }
                }
            }

            // Fecha de Muestra


            // Fecha de Recepcion
            if (string.IsNullOrEmpty(objDetalle.FECHA_RECEPCION))
            {
                ListaValidaciones.Add("No se ha señalado FECHA DE RECEPCIÓN en la ficha");
            }
            else
            {
                DateTime? _fechaRecepcion = ISConvert.ToNullableDateTime(objDetalle.FECHA_RECEPCION);
                if (!_fechaRecepcion.HasValue)
                {
                    ListaValidaciones.Add("FECHA DE RECEPCIÓN no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FECHA_RECEPCION = _fechaRecepcion.Value;
                }
            }

            // Prevision
            if (string.IsNullOrEmpty(objDetalle.PREVISION))
            {
                ListaValidaciones.Add("No se ha señalado PREVISION en la ficha");
            }
            else
            {
                var objPrevision = _RepositorioPREVISION.GetByFilter(objDetalle.PREVISION).FirstOrDefault();
                if (objPrevision != null)
                {
                    objDetalle.PREVISION1 = objPrevision;
                }
                else
                {
                    ListaValidaciones.Add("No se ha podido identificar la PREVISION en la ficha");
                }
            }

            // Garantia
            if (string.IsNullOrEmpty(objDetalle.GARANTIA))
            {
                ListaValidaciones.Add("No se ha señalado GARANTIA en la ficha");
            }
            else
            {
                var objGarantia = _RepositorioGARANTIA.GetByFilter(objDetalle.GARANTIA).FirstOrDefault();
                if (objGarantia != null)
                {
                    objDetalle.GARANTIA1 = objGarantia;
                }
                else
                {
                    ListaValidaciones.Add("No se ha podido identificar la GARANTIA en la ficha");
                }
            }

            // Fecha de Entrega de Resultados
            if (string.IsNullOrEmpty(objDetalle.FECHA_RESULTADOS))
            {
                ListaValidaciones.Add("No se ha señalado FECHA DE ENTREGA DE RESULTADOS en la ficha");
            }
            else
            {
                DateTime? _fechaResultados = ISConvert.ToNullableDateTime(objDetalle.FECHA_RESULTADOS);
                if (!_fechaResultados.HasValue)
                {
                    ListaValidaciones.Add("FECHA DE ENTREGA DE RESULTADOS no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FECHA_ENTREGA_RESULTADOS = _fechaResultados.Value;
                }
            }

            // Examenes
            int contadorExamen = 1;
            foreach (var item in objDetalle.CARGA_PRESTACIONES_HUMANAS_EXAMEN)
            {
                // PENDIENTE: Validar Examen
                if (!string.IsNullOrEmpty(item.VALOR_EXAMEN) || !string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                {
                    if (string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                    {
                        ListaValidaciones.Add("No ha señalado nombre de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        var objExamen = _RepositorioEXAMEN.GetByFilter((int)ENUM_TIPO_PRESTACION.Humanas, "", item.NOMBRE_EXAMEN).FirstOrDefault();
                        if (objExamen != null)
                        {
                            item.EXAMEN = objExamen;
                        }
                        else
                        {
                            var objExamenSinonimo = _RepositorioEXAMEN_SINONIMO.GetByFilterWithReferences(null, item.NOMBRE_EXAMEN).FirstOrDefault();
                            if (objExamenSinonimo != null)
                            {
                                item.EXAMEN = objExamenSinonimo.EXAMEN;
                            }
                            else
                            {
                                ListaValidaciones.Add("No se ha encontrado información del examen [" + contadorExamen.ToString() + "]");
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(item.VALOR_EXAMEN))
                    {
                        ListaValidaciones.Add("No se ha señalado valor de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        int? _valorExamen = ISConvert.ToNullableInteger(item.VALOR_EXAMEN);
                        if (!_valorExamen.HasValue)
                        {
                            ListaValidaciones.Add("Valor de examen no tiene el formato correcto [" + contadorExamen.ToString() + "]");
                        }
                        else
                        {
                            item.VALOR_VALOR_EXAMEN = _valorExamen;
                        }
                    }
                    // PENDIENTE: Validar que el valor del examen sea igual al convenio


                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_EXAMEN", item);
                }
                contadorExamen++;
            }

            if (ListaValidaciones.Any())
            {
                objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = EstadoConError;
                string errores = "";
                foreach(var item in ListaValidaciones)
                    errores += item + Environment.NewLine;
                objDetalle.MENSAJE_ERROR = errores;
            }
            context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_DETALLE", objDetalle);

            return ListaValidaciones;
        }

        private List<string> ValidarPrestacionVeterinaria(LQCEEntities context, CARGA_PRESTACIONES_VETERINARIAS_DETALLE objDetalle)
        {
            RepositorioPRESTACION_VETERINARIA _RepositorioPRESTACION_VETERINARIA = new RepositorioPRESTACION_VETERINARIA(context);
            RepositorioCLIENTE _RepositorioCLIENTE = new RepositorioCLIENTE(context);
            RepositorioCLIENTE_SINONIMO _RepositorioCLIENTE_SINONIMO = new RepositorioCLIENTE_SINONIMO(context);
            RepositorioPREVISION _RepositorioPREVISION = new RepositorioPREVISION(context);
            RepositorioGARANTIA _RepositorioGARANTIA = new RepositorioGARANTIA(context);
            RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);
            RepositorioEXAMEN _RepositorioEXAMEN = new RepositorioEXAMEN(context);
            RepositorioEXAMEN_SINONIMO _RepositorioEXAMEN_SINONIMO = new RepositorioEXAMEN_SINONIMO(context);
            RepositorioESPECIE _RepositorioESPECIE = new RepositorioESPECIE(context);
            RepositorioRAZA _RepositorioRAZA = new RepositorioRAZA(context);

            var EstadoConError = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById((int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.ConError);
            if (EstadoConError == null)
                throw new Exception("No se encuentra registro de error en la tabla Estado de Detalle Carga Prestaciones");

            List<string> ListaValidaciones = new List<string>();

            // Ficha
            if (string.IsNullOrEmpty(objDetalle.FICHA))
            {
                ListaValidaciones.Add("No se ha señalado numero de ficha");
            }
            else
            {
                int? _ficha = ISConvert.ToNullableInteger(objDetalle.FICHA);
                if (!_ficha.HasValue)
                {
                    ListaValidaciones.Add("FICHA debe ser numérico");
                }
                else
                {
                    objDetalle.VALOR_FICHA = _ficha.Value;

                    var objPrestacionVeterinaria = _RepositorioPRESTACION_VETERINARIA.GetById(_ficha.Value);
                    if (objPrestacionVeterinaria != null)
                        ListaValidaciones.Add("Ya existe una prestación en el sistema con el mismo número de ficha");
                }
            }

            // Especie
            if (string.IsNullOrEmpty(objDetalle.ESPECIE))
            {
                ListaValidaciones.Add("No se ha señalado ESPECIE en la ficha");
            }
            else
            {
                var objEspecie = _RepositorioESPECIE.GetByFilter(objDetalle.ESPECIE).FirstOrDefault();
                if (objEspecie != null)
                {
                    objDetalle.ESPECIE1 = objEspecie;

                    // Raza
                    if (string.IsNullOrEmpty(objDetalle.RAZA))
                    {
                        ListaValidaciones.Add("No se ha señalado RAZA en la ficha");
                    }
                    else
                    {
                        var objRaza = _RepositorioRAZA.GetByFilter(objEspecie.ID, objDetalle.RAZA).FirstOrDefault();
                        if (objRaza != null)
                        {
                            objDetalle.RAZA1 = objRaza;
                        }
                        else
                        {
                            ListaValidaciones.Add("No se ha podido identificar la RAZA en la ficha");
                        }
                    }
                }
                else
                {
                    ListaValidaciones.Add("No se ha podido identificar la ESPECIE en la ficha");
                }
            }

            

            // Cliente
            if (string.IsNullOrEmpty(objDetalle.PROCEDENCIA))
            {
                ListaValidaciones.Add("No se ha señalado PROCEDENCIA en la ficha");
            }
            else
            {
                var objCliente = _RepositorioCLIENTE.GetByFilter(null, null, null, "", objDetalle.PROCEDENCIA).FirstOrDefault();
                if (objCliente != null)
                {
                    objDetalle.CLIENTE = objCliente;
                }
                else
                {
                    var objClienteSinonimo = _RepositorioCLIENTE_SINONIMO.GetByFilterWithReferences(null, objDetalle.PROCEDENCIA).FirstOrDefault();
                    if (objClienteSinonimo != null)
                    {
                        objClienteSinonimo.CLIENTE = objClienteSinonimo.CLIENTE;
                    }
                    else
                    {
                        ListaValidaciones.Add("No se ha podido identificar cliente de la prestación");
                    }
                }
            }

            // Fecha de Muestra
            if (string.IsNullOrEmpty(objDetalle.FECHA_MUESTRA))
            {
                ListaValidaciones.Add("No se ha señalado FECHA DE MUESTRA en la ficha");
            }
            else
            {
                DateTime? _fechaMuestra = ISConvert.ToNullableDateTime(objDetalle.FECHA_RECEPCION);
                if (!_fechaMuestra.HasValue)
                {
                    ListaValidaciones.Add("FECHA DE MUESTRA no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FECHA_MUESTRA = _fechaMuestra.Value;
                }
            }

            // Fecha de Recepcion
            if (string.IsNullOrEmpty(objDetalle.FECHA_RECEPCION))
            {
                ListaValidaciones.Add("No se ha señalado FECHA DE RECEPCIÓN en la ficha");
            }
            else
            {
                DateTime? _fechaRecepcion = ISConvert.ToNullableDateTime(objDetalle.FECHA_RECEPCION);
                if (!_fechaRecepcion.HasValue)
                {
                    ListaValidaciones.Add("FECHA DE RECEPCIÓN no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FECHA_RECEPCION = _fechaRecepcion.Value;
                }
            }

            // Prevision
            

            // Garantia
            if (string.IsNullOrEmpty(objDetalle.GARANTIA))
            {
                ListaValidaciones.Add("No se ha señalado GARANTIA en la ficha");
            }
            else
            {
                var objGarantia = _RepositorioGARANTIA.GetByFilter(objDetalle.GARANTIA).FirstOrDefault();
                if (objGarantia != null)
                {
                    objDetalle.GARANTIA1 = objGarantia;
                }
                else
                {
                    ListaValidaciones.Add("No se ha podido identificar la GARANTIA en la ficha");
                }
            }

            // Fecha de Entrega de Resultados
            if (string.IsNullOrEmpty(objDetalle.FECHA_RESULTADOS))
            {
                ListaValidaciones.Add("No se ha señalado FECHA DE ENTREGA DE RESULTADOS en la ficha");
            }
            else
            {
                DateTime? _fechaResultados = ISConvert.ToNullableDateTime(objDetalle.FECHA_RESULTADOS);
                if (!_fechaResultados.HasValue)
                {
                    ListaValidaciones.Add("FECHA DE ENTREGA DE RESULTADOS no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FECHA_ENTREGA_RESULTADOS = _fechaResultados.Value;
                }
            }

            // Examenes
            int contadorExamen = 1;
            foreach (var item in objDetalle.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN)
            {
                // PENDIENTE: Validar Examen
                if (!string.IsNullOrEmpty(item.VALOR_EXAMEN) || !string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                {
                    if (string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                    {
                        ListaValidaciones.Add("No ha señalado nombre de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        var objExamen = _RepositorioEXAMEN.GetByFilter((int)ENUM_TIPO_PRESTACION.Humanas, "", item.NOMBRE_EXAMEN).FirstOrDefault();
                        if (objExamen != null)
                        {
                            item.EXAMEN = objExamen;
                        }
                        else
                        {
                            var objExamenSinonimo = _RepositorioEXAMEN_SINONIMO.GetByFilterWithReferences(null, item.NOMBRE_EXAMEN).FirstOrDefault();
                            if (objExamenSinonimo != null)
                            {
                                item.EXAMEN = objExamenSinonimo.EXAMEN;
                            }
                            else
                            {
                                ListaValidaciones.Add("No se ha encontrado información del examen [" + contadorExamen.ToString() + "]");
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(item.VALOR_EXAMEN))
                    {
                        ListaValidaciones.Add("No se ha señalado valor de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        int? _valorExamen = ISConvert.ToNullableInteger(item.VALOR_EXAMEN);
                        if (!_valorExamen.HasValue)
                        {
                            ListaValidaciones.Add("Valor de examen no tiene el formato correcto [" + contadorExamen.ToString() + "]");
                        }
                        else
                        {
                            item.VALOR_VALOR_EXAMEN = _valorExamen;
                        }
                    }
                    // PENDIENTE: Validar que el valor del examen sea igual al convenio


                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN", item);
                }
                contadorExamen++;
            }

            if (ListaValidaciones.Any())
            {
                objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = EstadoConError;
                string errores = "";
                foreach (var item in ListaValidaciones)
                    errores += item + Environment.NewLine;
                objDetalle.MENSAJE_ERROR = errores;
            }
            context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_DETALLE", objDetalle);

            return ListaValidaciones;
        }

    }
}