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

                    string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + NombreArchivo;
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
                            bool TieneDatos = false;
                            foreach (var col in item.ItemArray)
                            {
                                if (!string.IsNullOrEmpty(col.ToString()))
                                    TieneDatos = true;
                            }
                            if (TieneDatos)
                            {
                                CARGA_PRESTACIONES_HUMANAS_DETALLE objDetalle = new CARGA_PRESTACIONES_HUMANAS_DETALLE();
                                objDetalle.CARGA_PRESTACIONES_ENCABEZADO = objEncabezado;
                                objDetalle.NOMBRE = item["NOMBRE"].ToString();
                                objDetalle.FICHA = item["FICHA"].ToString();
                                objDetalle.FECHA_RECEPCION = item["FECHA RECEPCION"].ToString() + " " + item["HORA RECEPCION"].ToString();
                                objDetalle.TELEFONO = item["TELEFONO"].ToString();
                                objDetalle.MEDICO = item["MEDICO"].ToString();
                                objDetalle.PROCEDENCIA = item["PROCEDENCIA"].ToString();
                                objDetalle.PREVISION = item["PREVISION"].ToString();
                                objDetalle.GARANTIA = item["GARANTIA"].ToString();
                                objDetalle.PENDIENTE = item["PENDIENTE"].ToString();
                                objDetalle.PAGADO = item["PAGADO"].ToString();
                                objDetalle.TOTAL = item["TOTAL"].ToString();
                                objDetalle.RECEPCION = item["RECEPCION"].ToString();
                                objDetalle.EDAD = item["EDAD"].ToString();
                                objDetalle.RUT = item["RUT"].ToString();
                                
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

                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 5", "VALOR 5");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 6", "VALOR 6");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 7", "VALOR 7");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 8", "VALOR 8");
                                AgregarExamenHumano(context, objDetalle, item, "EXAMEN 9", "VALOR 9");

                                ValidarPrestacionHumana(context, objDetalle);

                                context.AddToCARGA_PRESTACIONES_HUMANAS_DETALLE(objDetalle);
                            }
                        }
                    }
                    else if (IdTipoPrestacion == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                    {
                        foreach (DataRow item in datos.Rows)
                        {
                            CARGA_PRESTACIONES_VETERINARIAS_DETALLE objDetalle = new CARGA_PRESTACIONES_VETERINARIAS_DETALLE();
                            objDetalle.CARGA_PRESTACIONES_ENCABEZADO = objEncabezado;
                            objDetalle.FICHA = item["INGRESO"].ToString();
                            objDetalle.NOMBRE = item["NOMBRE"].ToString();
                            objDetalle.ESPECIE = item["ESPECIE"].ToString();
                            objDetalle.RAZA = item["RAZA"].ToString();
                            objDetalle.SEXO = item["SEXO"].ToString();
                            objDetalle.EDAD = item["EDAD"].ToString();
                            objDetalle.TELEFONO = item["TELEFONO"].ToString();
                            objDetalle.PROCEDENCIA = item["PROCEDENCIA"].ToString();
                            objDetalle.GARANTIA = item["GARANTIA"].ToString();
                            objDetalle.PENDIENTE = item["PENDIENTE"].ToString();
                            objDetalle.TOTAL = item["TOTAL"].ToString();
                            objDetalle.RECEPCION = item["RECEPCION"].ToString();
                            objDetalle.MEDICO = item["MEDICO"].ToString();
                            objDetalle.SOLICITA = item["SOLICITANTE"].ToString();
                            objDetalle.FECHA_RECEPCION = item["FECHA RECEPCION"].ToString() + " " + item["HORA RECEPCION"].ToString();
                            objDetalle.FICHA_CLINICA = item["FICHA"].ToString();
                            //objDetalle.FECHA_MUESTRA = item["FECHA MUESTRA"].ToString();
                            //objDetalle.FECHA_RESULTADOS = item["FECHA RESULTADOS"].ToString();
                            //objDetalle.PAGADO = item["PAGADO"].ToString();
                            objDetalle.ACTIVO = true;
                            objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                            objDetalle.MENSAJE_ERROR = "";
                            objDetalle.FECHA_ACTUALIZACION = DateTime.Now;
                            objDetalle.VALOR_FICHA = null;
                            objDetalle.CLIENTE = null;
                            //objDetalle.VALOR_FECHA_MUESTRA = null;
                            objDetalle.VALOR_FECHA_RECEPCION = null;
                            objDetalle.PREVISION = null;
                            objDetalle.GARANTIA1 = null;
                            //objDetalle.VALOR_FECHA_ENTREGA_RESULTADOS = null;
                            objDetalle.ESPECIE1 = null;
                            objDetalle.RAZA1 = null;
                            
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 5", "VALOR 5");

                           ValidarPrestacionVeterinaria(context, objDetalle);

                            context.AddToCARGA_PRESTACIONES_VETERINARIAS_DETALLE(objDetalle);

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
                        var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(dto.estado, dto.id,
                                null, null, null, null, dto.numero, dto.nombre,
                                "", "", "", "", dto.prodedencia, "", "", "", "", "", "", "", "", "", "", null, null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.VALOR_FICHA).ThenBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize)
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
                        var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(dto.estado, dto.id,
                                null, null, null, null, null, null,
                                dto.numero, dto.nombre, "", "", "", "", "", "", "", dto.prodedencia,
                                "", "", "", "", "", "", "", "", "", "",
                                null, null, null, null, null, null)
                                select d;

                        var r = from item in q.OrderBy(d => d.VALOR_FICHA).ThenBy(d => d.ID).Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize)
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
                        var q = from d in _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByFilterWithReferences(dto.estado, 
                                   dto.id, null, null, null, null, dto.numero, dto.nombre,
                                "", "", "", "", dto.prodedencia, "", "", "", "", "", "", "", "", "", "", null, null, null, null, null)
                                select d;


                        return q.Count();
                    }
                    else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                    {
                        var q = from d in _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByFilterWithReferences(dto.estado, dto.id,
                                null, null, null, null, null, null,
                                dto.numero, dto.nombre, "", "", "", "", "", "", "", dto.prodedencia,
                                "", "", "", "", "", "", "", "", "", null, null, null, null, null, null)
                                select d;

                        return q.Count();
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
                    RepositorioTIPO_PRESTACION _RepositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
                    RepositorioCLIENTE _RepositorioCLIENTE = new RepositorioCLIENTE(context);

                    CARGA_PRESTACIONES_ENCABEZADO objEncabezado = _RepositorioCARGA_PRESTACIONES_ENCABEZADO.GetByIdWithReferencesFull(IdCargaPrestacionesEncabezado);
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
                    {
                        if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
                        {
                            if (objEncabezado.CARGA_PRESTACIONES_HUMANAS_DETALLE
                           .Any(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdCargaDetalleEstadoPendiente))
                            {
                                throw new Exception("La carga no se puede completar mientras tenga fichas pendientes");
                            }
                        }
                        else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
                        {
                            if (objEncabezado.CARGA_PRESTACIONES_VETERINARIAS_DETALLE
                           .Any(d => d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == IdCargaDetalleEstadoPendiente))
                            {
                                throw new Exception("La carga no se puede completar mientras tenga fichas pendientes");
                            }
                        }
                    }

                    objEncabezado.CARGA_PRESTACIONES_ESTADO = objEstado;
                    context.ApplyPropertyChanges("CARGA_PRESTACIONES_ENCABEZADO", objEncabezado);

                    // Mover prestaciones y examenes a tablas definitivas
                    if (objEstado.ID == (int)ENUM_CARGA_PRESTACIONES_ESTADO.Completado)
                    {
                        if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Humanas)
                        {
                            TIPO_PRESTACION _TIPO_PRESTACION = _RepositorioTIPO_PRESTACION.GetById((int)ENUM_TIPO_PRESTACION.Humanas);
                            if (_TIPO_PRESTACION == null)
                                throw new Exception("No se encuentra informacion de tipo de prestación humana");

                            foreach (CARGA_PRESTACIONES_HUMANAS_DETALLE _CARGA_PRESTACIONES_HUMANAS_DETALLE in objEncabezado.CARGA_PRESTACIONES_HUMANAS_DETALLE
                                 .Where(d => d.ACTIVO && d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Validado))
                            {
                                if (string.IsNullOrEmpty(_CARGA_PRESTACIONES_HUMANAS_DETALLE.NOMBRE))
                                    throw new Exception("No se ha señalado nombre");
                                if (!_CARGA_PRESTACIONES_HUMANAS_DETALLE.VALOR_FICHA.HasValue)
                                    throw new Exception("No se ha señalado ficha");
                                if (!_CARGA_PRESTACIONES_HUMANAS_DETALLE.VALOR_FECHA_RECEPCION.HasValue)
                                    throw new Exception("Fecha de recepción debe tener valor");
                                if(_CARGA_PRESTACIONES_HUMANAS_DETALLE.CLIENTE == null)
                                    throw new Exception("No se ha identificado al cliente");
                                if (!_CARGA_PRESTACIONES_HUMANAS_DETALLE.VALOR_TOTAL.HasValue)
                                    throw new Exception("No se ha señalado valor total de prestaciones");

                                PRESTACION _PRESTACION = new PRESTACION();
                                _PRESTACION.ID = _CARGA_PRESTACIONES_HUMANAS_DETALLE.VALOR_FICHA.Value;
                                _PRESTACION.TIPO_PRESTACION = _TIPO_PRESTACION;
                                _PRESTACION.FECHA_RECEPCION = _CARGA_PRESTACIONES_HUMANAS_DETALLE.VALOR_FECHA_RECEPCION.Value;
                                _PRESTACION.MEDICO = _CARGA_PRESTACIONES_HUMANAS_DETALLE.MEDICO;
                                _PRESTACION.CLIENTE = _CARGA_PRESTACIONES_HUMANAS_DETALLE.CLIENTE;
                                _PRESTACION.PREVISION = _CARGA_PRESTACIONES_HUMANAS_DETALLE.PREVISION1;
                                _PRESTACION.GARANTIA = _CARGA_PRESTACIONES_HUMANAS_DETALLE.GARANTIA1;
                                _PRESTACION.PENDIENTE = _CARGA_PRESTACIONES_HUMANAS_DETALLE.PENDIENTE;
                                _PRESTACION.RECEPCION = _CARGA_PRESTACIONES_HUMANAS_DETALLE.RECEPCION;
                                _PRESTACION.ACTIVO = true;
                                context.AddToPRESTACION(_PRESTACION);

                                PRESTACION_HUMANA _PRESTACION_HUMANA = new PRESTACION_HUMANA();
                                _PRESTACION_HUMANA.PRESTACION = _PRESTACION;
                                _PRESTACION_HUMANA.NOMBRE = _CARGA_PRESTACIONES_HUMANAS_DETALLE.NOMBRE;
                                _PRESTACION_HUMANA.TELEFONO = _CARGA_PRESTACIONES_HUMANAS_DETALLE.TELEFONO;
                                _PRESTACION_HUMANA.EDAD = _CARGA_PRESTACIONES_HUMANAS_DETALLE.EDAD;
                                _PRESTACION_HUMANA.RUT = _CARGA_PRESTACIONES_HUMANAS_DETALLE.RUT;
                                _PRESTACION_HUMANA.ACTIVO = true;
                                context.AddToPRESTACION_HUMANA(_PRESTACION_HUMANA);

                                foreach (CARGA_PRESTACIONES_HUMANAS_EXAMEN _CARGA_PRESTACIONES_HUMANAS_EXAMEN in _CARGA_PRESTACIONES_HUMANAS_DETALLE
                                    .CARGA_PRESTACIONES_HUMANAS_EXAMEN.Where(d => d.ACTIVO))
                                {
                                    PRESTACION_EXAMEN _PRESTACION_EXAMEN = new PRESTACION_EXAMEN();
                                    _PRESTACION_EXAMEN.PRESTACION = _PRESTACION;
                                    _PRESTACION_EXAMEN.EXAMEN = _CARGA_PRESTACIONES_HUMANAS_EXAMEN.EXAMEN;
                                    _PRESTACION_EXAMEN.VALOR = _CARGA_PRESTACIONES_HUMANAS_EXAMEN.VALOR_VALOR_EXAMEN;
                                    _PRESTACION_EXAMEN.ACTIVO = true;
                                    context.AddToPRESTACION_EXAMEN(_PRESTACION_EXAMEN);
                                }
                            }
                        }
                        else if (objEncabezado.TIPO_PRESTACION.ID == (int)ENUM_TIPO_PRESTACION.Veterinarias)
                        {
                            TIPO_PRESTACION _TIPO_PRESTACION = _RepositorioTIPO_PRESTACION.GetById((int)ENUM_TIPO_PRESTACION.Veterinarias);
                            if (_TIPO_PRESTACION == null)
                                throw new Exception("No se encuentra informacion de tipo de prestación veterinaria");

                            foreach (CARGA_PRESTACIONES_VETERINARIAS_DETALLE _CARGA_PRESTACIONES_VETERINARIAS_DETALLE in objEncabezado.CARGA_PRESTACIONES_VETERINARIAS_DETALLE
                                 .Where(d => d.ACTIVO && d.CARGA_PRESTACIONES_DETALLE_ESTADO.ID == (int)ENUM_CARGA_PRESTACIONES_DETALLE_ESTADO.Validado))
                            {
                                if (string.IsNullOrEmpty(_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.NOMBRE))
                                    throw new Exception("No se ha señalado nombre");
                                if (!_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FICHA.HasValue)
                                    throw new Exception("No se ha señalado ingreso");
                                if (_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE1 == null)
                                    throw new Exception("No se ha identificado ESPECIE");
                                if (_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CLIENTE == null)
                                    throw new Exception("No se ha identificado al cliente");
                                if (!_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_TOTAL.HasValue)
                                    throw new Exception("No se ha señalado valor total de prestaciones");
                                if (string.IsNullOrEmpty(_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SOLICITA))
                                    throw new Exception("No se ha señalado solicitante");
                                if (!_CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_RECEPCION.HasValue)
                                    throw new Exception("Fecha de recepción debe tener valor");

                                PRESTACION _PRESTACION = new PRESTACION();
                                _PRESTACION.ID = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FICHA.Value;
                                _PRESTACION.TIPO_PRESTACION = _TIPO_PRESTACION;
                                _PRESTACION.CLIENTE = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.CLIENTE;
                                _PRESTACION.GARANTIA = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.GARANTIA1;
                                _PRESTACION.PENDIENTE = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.PENDIENTE;
                                _PRESTACION.RECEPCION = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RECEPCION;
                                _PRESTACION.MEDICO = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.MEDICO;
                                _PRESTACION.FECHA_RECEPCION = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FECHA_RECEPCION.Value;
                                _PRESTACION.PREVISION = null;
                                _PRESTACION.ACTIVO = true;

                                context.AddToPRESTACION(_PRESTACION);

                                PRESTACION_VETERINARIA _PRESTACION_VETERINARIA = new PRESTACION_VETERINARIA();
                                _PRESTACION_VETERINARIA.PRESTACION = _PRESTACION;
                                _PRESTACION_VETERINARIA.NOMBRE = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.NOMBRE;
                                _PRESTACION_VETERINARIA.ESPECIE = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.ESPECIE1;
                                _PRESTACION_VETERINARIA.RAZA = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.RAZA1;
                                _PRESTACION_VETERINARIA.SEXO = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SEXO;
                                _PRESTACION_VETERINARIA.EDAD = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.EDAD;
                                _PRESTACION_VETERINARIA.TELEFONO = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.TELEFONO;
                                _PRESTACION_VETERINARIA.SOLICITANTE = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.SOLICITA;
                                _PRESTACION_VETERINARIA.FICHA_CLINICA = _CARGA_PRESTACIONES_VETERINARIAS_DETALLE.VALOR_FICHA_CLINICA;
                                _PRESTACION_VETERINARIA.ACTIVO = true;

                                context.AddToPRESTACION_VETERINARIA(_PRESTACION_VETERINARIA);

                                foreach (CARGA_PRESTACIONES_VETERINARIAS_EXAMEN _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN in _CARGA_PRESTACIONES_VETERINARIAS_DETALLE
                                    .CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.Where(d => d.ACTIVO))
                                {
                                    PRESTACION_EXAMEN _PRESTACION_EXAMEN = new PRESTACION_EXAMEN();
                                    _PRESTACION_EXAMEN.PRESTACION = _PRESTACION;
                                    _PRESTACION_EXAMEN.EXAMEN = _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.EXAMEN;
                                    _PRESTACION_EXAMEN.VALOR = _CARGA_PRESTACIONES_VETERINARIAS_EXAMEN.VALOR_VALOR_EXAMEN;
                                    _PRESTACION_EXAMEN.ACTIVO = true;
                                    context.AddToPRESTACION_EXAMEN(_PRESTACION_EXAMEN);
                                }
                            }
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

        public DTO_RESULTADO_ACTUALIZACION_FICHA ActualizarCargaPrestacionHumana(int IdCargaPrestacionHumanaDetalle,
            string Ficha, string Nombre, string FechaRecepcion, string Telefono, string Medico,
            string Procedencia, string Prevision, string Garantia, string Pendiente, string Pagado,
            string Total, string Recepcion, string Edad, string Rut,
             int IdCargaPrestacionesDetalleEstado, string MensajeError,
            List<DTOExamen> Examenes)
        {
            Init();
            try
            {
                //ISException.RegisterExcepcion("FechaRecepcion " + FechaRecepcion);
                //ISException.RegisterExcepcion("FechaResultados " + FechaResultados);

                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO = new RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO(context);

                    var objEstadoDetalle = _RepositorioCARGA_PRESTACIONES_DETALLE_ESTADO.GetById(IdCargaPrestacionesDetalleEstado);
                    if (objEstadoDetalle == null)
                        throw new Exception("No se encuentra estado de detalle");

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferencesFull(IdCargaPrestacionHumanaDetalle);
                    if (objDetalle.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID != (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                        throw new Exception("La carga seleccionada no se encuentra en proceso de Revisión Pendiente");

                    objDetalle.FICHA = Ficha;
                    objDetalle.NOMBRE = Nombre;
                    objDetalle.FECHA_RECEPCION = FechaRecepcion;
                    objDetalle.TELEFONO = Telefono;
                    objDetalle.MEDICO = Medico;
                    objDetalle.PROCEDENCIA = Procedencia;
                    objDetalle.PREVISION = Prevision;
                    objDetalle.GARANTIA = Garantia;
                    objDetalle.PENDIENTE = Pendiente;
                    objDetalle.PAGADO = Pagado;
                    objDetalle.TOTAL = Total;
                    objDetalle.RECEPCION = Recepcion;
                    objDetalle.EDAD = Edad;
                    objDetalle.RUT = Rut;
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
                        objExamen.ACTIVO = true;
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

        public DTO_RESULTADO_ACTUALIZACION_FICHA ActualizarCargaPrestacionVeterinarias(int IdCargaPrestacionVeterinariaDetalle, 
            string Ficha, string Nombre,
               string Especie, string Raza, string Sexo, string Edad, string Telefono, string Procedencia,
            string Garantia, string Pendiente, string Total, string Recepcion,
            string Medico, string Solicitante, string FechaRecepcion, string FichaClinica,
               int IdCargaPrestacionesDetalleEstado, string MensajeError,
               List<DTOExamen> Examenes)
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

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferencesFull(IdCargaPrestacionVeterinariaDetalle);
                    if (objDetalle.CARGA_PRESTACIONES_ENCABEZADO.CARGA_PRESTACIONES_ESTADO.ID != (int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente)
                        throw new Exception("La carga seleccionada no se encuentra en proceso de Revisión Pendiente");

                    objDetalle.FICHA = Ficha;
                    objDetalle.NOMBRE = Nombre;
                    objDetalle.ESPECIE = Especie;
                    objDetalle.RAZA = Raza;
                    objDetalle.SEXO = Sexo;
                    objDetalle.EDAD = Edad;
                    objDetalle.TELEFONO = Telefono;
                    objDetalle.PROCEDENCIA = Procedencia;
                    objDetalle.GARANTIA = Garantia;
                    objDetalle.PENDIENTE = Pendiente;
                    objDetalle.TOTAL = Total;
                    objDetalle.RECEPCION = Recepcion;
                    objDetalle.MEDICO = Medico;
                    objDetalle.SOLICITA = Solicitante;
                    objDetalle.FECHA_RECEPCION = FechaRecepcion;
                    objDetalle.FICHA_CLINICA = FichaClinica;
                    objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = objEstadoDetalle;
                    objDetalle.MENSAJE_ERROR = MensajeError;
                    objDetalle.FECHA_ACTUALIZACION = DateTime.Now;
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
                        objExamen.ACTIVO = true;
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

        public List<string> ValidarPrestacionHumana(int ID_CARGA_PRESTACIONES_HUMANAS_DETALLE)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);

                    var objCARGA_PRESTACIONES_HUMANAS_DETALLE = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferencesFull(ID_CARGA_PRESTACIONES_HUMANAS_DETALLE);
                    if (objCARGA_PRESTACIONES_HUMANAS_DETALLE == null)
                        throw new Exception("No se encuentra detalle de prestación");

                    return ValidarPrestacionHumana(context, objCARGA_PRESTACIONES_HUMANAS_DETALLE);
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public List<string> ValidarPrestacionVeterinaria(int ID_CARGA_PRESTACIONES_VETERINARIAS_DETALLE)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

                    var objCARGA_PRESTACIONES_VETERINARIAS_DETALLE = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferencesFull(ID_CARGA_PRESTACIONES_VETERINARIAS_DETALLE);
                    if (objCARGA_PRESTACIONES_VETERINARIAS_DETALLE == null)
                        throw new Exception("No se encuentra detalle de prestación");

                    return ValidarPrestacionVeterinaria(context, objCARGA_PRESTACIONES_VETERINARIAS_DETALLE);
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

            // Nombre
            if (string.IsNullOrEmpty(objDetalle.NOMBRE))
            {
                ListaValidaciones.Add("No se ha señalado nombre del paciente");
            }

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

            // Cliente
            if (string.IsNullOrEmpty(objDetalle.PROCEDENCIA))
            {
                ListaValidaciones.Add("No se ha señalado PROCEDENCIA en la ficha");
            }
            else
            {
                var objCliente = _RepositorioCLIENTE.GetByFilter(null, null, null, null, "", objDetalle.PROCEDENCIA).FirstOrDefault();
                if (objCliente != null)
                {
                    objDetalle.CLIENTE = objCliente;
                }
                else
                {
                    var objClienteSinonimo = _RepositorioCLIENTE_SINONIMO.GetByFilterWithReferences(null, objDetalle.PROCEDENCIA).FirstOrDefault();
                    if (objClienteSinonimo != null)
                    {
                        objDetalle.CLIENTE = objClienteSinonimo.CLIENTE;
                    }
                    else
                    {
                        ListaValidaciones.Add("No se ha podido identificar PROCEDENCIA de la prestación");
                    }
                }
            }

            // Prevision
            if (string.IsNullOrEmpty(objDetalle.PREVISION))
            {
                //ListaValidaciones.Add("No se ha señalado PREVISION en la ficha");
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
                //ListaValidaciones.Add("No se ha señalado GARANTIA en la ficha");
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

           // Total
            if (string.IsNullOrEmpty(objDetalle.TOTAL))
            {
                ListaValidaciones.Add("No se ha señalado TOTAL en la ficha");
            }
            else
            {
                int? _total = ISConvert.ToNullableInteger(objDetalle.TOTAL);
                if (!_total.HasValue)
                {
                    ListaValidaciones.Add("Total no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_TOTAL = _total.Value;
                }
            }

           



            // Examenes
            int contadorExamen = 1;
            int contadorExamenesRegistrados = 0;
            int contadorValorExamen = 0;
            foreach (var item in objDetalle.CARGA_PRESTACIONES_HUMANAS_EXAMEN)
            {
                if (!string.IsNullOrEmpty(item.VALOR_EXAMEN) || !string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                {
                    contadorExamenesRegistrados++;
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
                        //ListaValidaciones.Add("No se ha señalado valor de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        contadorValorExamen++;
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
                     //PENDIENTE: Validar que el valor del examen sea igual al convenio


                    //context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_EXAMEN", item);
                }
                else if (contadorExamen == 1)
                {
                    ListaValidaciones.Add("No ha señalado nombre y valor de examen [" + contadorExamen.ToString() + "]");
                }
                contadorExamen++;
            }

            if (contadorExamenesRegistrados == 0)
                ListaValidaciones.Add("La ficha debe registrar al menos 1 examen. ");
            if (contadorValorExamen == 0)
                ListaValidaciones.Add("La ficha debe registrar al menos 1 examen con valor. ");

            if (ListaValidaciones.Any())
            {
                objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = EstadoConError;
                string errores = "";
                foreach(var item in ListaValidaciones)
                    errores += item + Environment.NewLine;
                objDetalle.MENSAJE_ERROR = errores;
            }
            //context.ApplyPropertyChanges("CARGA_PRESTACIONES_HUMANAS_DETALLE", objDetalle);

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

            // Ingreso
            if (string.IsNullOrEmpty(objDetalle.FICHA))
            {
                ListaValidaciones.Add("No se ha señalado numero de ingreso");
            }
            else
            {
                int? _ficha = ISConvert.ToNullableInteger(objDetalle.FICHA);
                if (!_ficha.HasValue)
                {
                    ListaValidaciones.Add("INGRESO debe ser numérico");
                }
                else
                {
                    objDetalle.VALOR_FICHA = _ficha.Value;

                    var objPrestacionVeterinaria = _RepositorioPRESTACION_VETERINARIA.GetById(_ficha.Value);
                    if (objPrestacionVeterinaria != null)
                        ListaValidaciones.Add("Ya existe una prestación en el sistema con el mismo número de ingreso");
                }
            }

            // Nombre
            if (string.IsNullOrEmpty(objDetalle.NOMBRE))
            {
                ListaValidaciones.Add("No se ha señalado nombre del paciente");
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
                        //ListaValidaciones.Add("No se ha señalado RAZA en la ficha");
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
                var objCliente = _RepositorioCLIENTE.GetByFilter(null, null, null, null, "", objDetalle.PROCEDENCIA).FirstOrDefault();
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
                        ListaValidaciones.Add("No se ha podido identificar PROCEDENCIA de la prestación");
                    }
                }
            }

            // Garantia
            if (string.IsNullOrEmpty(objDetalle.GARANTIA))
            {
                //ListaValidaciones.Add("No se ha señalado GARANTIA en la ficha");
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

            // Total
            if (string.IsNullOrEmpty(objDetalle.TOTAL))
            {
                ListaValidaciones.Add("No se ha señalado TOTAL en la ficha");
            }
            else
            {
                int? _total = ISConvert.ToNullableInteger(objDetalle.TOTAL);
                if (!_total.HasValue)
                {
                    ListaValidaciones.Add("Total no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_TOTAL = _total.Value;
                }
            }

            // Solicitante
            if (string.IsNullOrEmpty(objDetalle.SOLICITA))
            {
                ListaValidaciones.Add("No se ha señalado SOLICITANTE en la ficha");
            }
            else
            {
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

            // Ficha Clinica
            if (string.IsNullOrEmpty(objDetalle.FICHA_CLINICA))
            {
                //ListaValidaciones.Add("No se ha señalado TOTAL en la ficha");
            }
            else
            {
                int? _ficha = ISConvert.ToNullableInteger(objDetalle.FICHA_CLINICA);
                if (!_ficha.HasValue)
                {
                    ListaValidaciones.Add("FICHA no tiene el formato correcto");
                }
                else
                {
                    objDetalle.VALOR_FICHA_CLINICA = _ficha.Value;
                }
            }
          

            // Examenes
            int contadorExamen = 1;
            int contadorExamenesRegistrados = 0;
            int contadorValorExamen = 0;
            foreach (var item in objDetalle.CARGA_PRESTACIONES_VETERINARIAS_EXAMEN)
            {
                if (!string.IsNullOrEmpty(item.VALOR_EXAMEN) || !string.IsNullOrEmpty(item.NOMBRE_EXAMEN))
                {
                    contadorExamenesRegistrados++;
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
                        //ListaValidaciones.Add("No se ha señalado valor de examen [" + contadorExamen.ToString() + "]");
                    }
                    else
                    {
                        contadorValorExamen++;
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


                    //context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_EXAMEN", item);
                }
                else if (contadorExamen == 1)
                {
                    ListaValidaciones.Add("No ha señalado nombre y valor de examen [" + contadorExamen.ToString() + "]");
                }
                contadorExamen++;
            }

            if(contadorExamenesRegistrados == 0)
                ListaValidaciones.Add("La ficha debe registrar al menos 1 examen. ");
            if (contadorValorExamen == 0)
                ListaValidaciones.Add("La ficha debe registrar al menos 1 examen con valor. ");

            if (ListaValidaciones.Any())
            {
                objDetalle.CARGA_PRESTACIONES_DETALLE_ESTADO = EstadoConError;
                string errores = "";
                foreach (var item in ListaValidaciones)
                    errores += item + Environment.NewLine;
                objDetalle.MENSAJE_ERROR = errores;
            }
            //context.ApplyPropertyChanges("CARGA_PRESTACIONES_VETERINARIAS_DETALLE", objDetalle);

            return ListaValidaciones;
        }

        public int? GetIdSiguienteFichaHumana(int CARGA_PRESTACIONES_HUMANAS_DETALLE_ID)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);
                    CARGA_PRESTACIONES_HUMANAS_DETALLE _FICHA_ACTUAL = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferences(CARGA_PRESTACIONES_HUMANAS_DETALLE_ID);
                    if (_FICHA_ACTUAL == null)
                    {
                        throw new Exception("No se encuentra información de la ficha actual");
                    }

                    var q = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetAllWithReferences();
                    q = q.Where(d => d.CARGA_PRESTACIONES_ENCABEZADO.ID == _FICHA_ACTUAL.CARGA_PRESTACIONES_ENCABEZADO.ID);
                    q = q.Where(d => d.VALOR_FICHA > _FICHA_ACTUAL.VALOR_FICHA);
                    var _FICHA_SIGUIENTE = q.OrderBy(d => d.VALOR_FICHA).FirstOrDefault();
                    if (_FICHA_SIGUIENTE == null)
                        return (int?)null;
                    else
                        return _FICHA_SIGUIENTE.ID;
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public int? GetIdSiguienteFichaVeterinaria(int CARGA_PRESTACIONES_VETERINARIAS_DETALLE_ID)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);
                    CARGA_PRESTACIONES_VETERINARIAS_DETALLE _FICHA_ACTUAL = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferences(CARGA_PRESTACIONES_VETERINARIAS_DETALLE_ID);
                    if (_FICHA_ACTUAL == null)
                    {
                        throw new Exception("No se encuentra información de la ficha actual");
                    }

                    var q = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetAllWithReferences();
                    q = q.Where(d => d.CARGA_PRESTACIONES_ENCABEZADO.ID == _FICHA_ACTUAL.CARGA_PRESTACIONES_ENCABEZADO.ID);
                    q = q.Where(d => d.VALOR_FICHA > _FICHA_ACTUAL.VALOR_FICHA);
                    var _FICHA_SIGUIENTE = q.OrderBy(d => d.VALOR_FICHA).FirstOrDefault();
                    if (_FICHA_SIGUIENTE == null)
                        return (int?)null;
                    else
                        return _FICHA_SIGUIENTE.ID;
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