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
                    if (!string.IsNullOrEmpty(NombreArchivo))
                        throw new Exception("No se ha señalado nombre de archivo Excel");
                    if (ContenidoArchivo == null)
                        throw new Exception("No se ha incluido contenido del archivo Excel");

                    RepositorioTIPO_PRESTACION _RepositorioTIPO_PRESTACION = new RepositorioTIPO_PRESTACION(context);
                    RepositorioCARGA_PRESTACIONES_ESTADO _RepositorioCARGA_PRESTACIONES_ESTADO = new RepositorioCARGA_PRESTACIONES_ESTADO(context);

                    var objTipoPrestacion = _RepositorioTIPO_PRESTACION.GetById(IdTipoPrestacion);
                    if (objTipoPrestacion == null)
                        throw new Exception("No se ha encontrado información del Tipo de Prestación");

                    var objEstado = _RepositorioCARGA_PRESTACIONES_ESTADO.GetById((int)ENUM_CARGA_PRESTACIONES_ESTADO.Pendiente);
                    if (objEstado == null)
                        throw new Exception("No se ha encontrado información del Estado de Carga de Prestaciones");

                    string archivo = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + NombreArchivo;
                    File.WriteAllBytes(Properties.Settings.Default.DIR_CARGA_EXCEL + archivo, ContenidoArchivo);

                    var datos = ISExcel.ReadExcelFile(archivo, true);

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
                            objDetalle.VALIDADO = false;
                            objDetalle.ERROR = false;
                            objDetalle.MENSAJE_ERROR = "";
                            context.AddToCARGA_PRESTACIONES_HUMANAS_DETALLE(objDetalle);

                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 5", "VALOR 5");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 6", "VALOR 6");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 7", "VALOR 7");
                            AgregarExamenHumano(context, objDetalle, item, "EXAMEN 8", "VALOR 8");
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
                            objDetalle.VALIDADO = false;
                            objDetalle.ERROR = false;
                            objDetalle.MENSAJE_ERROR = "";
                            context.AddToCARGA_PRESTACIONES_VETERINARIAS_DETALLE(objDetalle);

                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 1", "VALOR 1");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 2", "VALOR 2");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 3", "VALOR 3");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 4", "VALOR 4");
                            AgregarExamenVeterinario(context, objDetalle, item, "EXAMEN 5", "VALOR 5");
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
                context.AddToCARGA_PRESTACIONES_VETERINARIAS_EXAMEN(objExamen);
            }
        }

        /// <summary>
        /// Obtiene listado de resumen de carga de prestaciones
        /// </summary>
        /// <param name="IdCargaPrestacionesEstado">(Opcional) Filtra por estado de carga</param>
        /// <returns>Listado de resumen de carga de prestaciones</returns>
        public List<DTO_RESUMEN_CARGA_PRESTACIONES> GetResumenCargaPrestaciones(int? IdCargaPrestacionesEstado)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_ENCABEZADO repositorio = new RepositorioCARGA_PRESTACIONES_ENCABEZADO(context);
                    var q = from item in repositorio.GetAllWithReferences()
                            select new DTO_RESUMEN_CARGA_PRESTACIONES
                            {
                                ID = item.ID,
                                ARCHIVO = item.ARCHIVO,
                                FECHA_CARGA = item.FECHA_CARGA,
                                FECHA_ACTUALIZACION = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) > item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) ?
                                    item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION) : item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Max(d => d.FECHA_ACTUALIZACION),
                                ID_ESTADO = item.CARGA_PRESTACIONES_ESTADO.ID,
                                NOMBRE_ESTADO = item.CARGA_PRESTACIONES_ESTADO.NOMBRE,
                                TOTAL_REGISTROS = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count()
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(),
                                REGISTROS_VALIDADOS = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count(d => d.VALIDADO)
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(d => d.VALIDADO),
                                REGISTROS_CON_ERRORES = item.CARGA_PRESTACIONES_HUMANAS_DETALLE.Count(d => d.ERROR)
                                    + item.CARGA_PRESTACIONES_VETERINARIAS_DETALLE.Count(d => d.ERROR)
                            };
                    return q.ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                return null;
            }
        }


        public void ActualizarCargaPrestacionHumana(int IdCargaPrestacionHumanaDetalle, string Ficha, string Nombre, 
            string Rut, string Medico, string Edad, string Telefono, string Procedencia, string FechaRecepcion,
            string Muestra, string FechaResultados, string Prevision, string Garantia, string Pagado,
            string Pendiente, bool MarcarValidado, bool MarcarConError, string MensajeError,
            List<DTO_CARGA_PRESTACIONES_HUMANAS_EXAMEN> Examenes)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                   
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
    }
}
