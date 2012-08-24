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
                                ID_TIPO_PRESTACION = item.TIPO_PRESTACION.ID,
                                NOMBRE_TIPO_PRESTACION = item.TIPO_PRESTACION.NOMBRE,
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
                    RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE = new RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE(context);

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_HUMANAS_DETALLE.GetByIdWithReferences(IdCargaPrestacionHumanaDetalle);
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
                    objDetalle.VALIDADO = MarcarValidado;
                    objDetalle.ERROR = MarcarConError;
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


        public void ActualizarCargaPrestacionVeterinarias(int IdCargaPrestacionVeterinariaDetalle, string Ficha, string Nombre,
               string Especie, string Raza, string Edad, string Sexo, string Solicita, string Telefono, string Medico,
            string Procedencia, string FechaRecepcion, string FechaMuestra, string FechaResultados, string Pendiente, 
            string Garantia, string Pagado, string Total,
               bool MarcarValidado, bool MarcarConError, string MensajeError,
               List<DTO_CARGA_PRESTACIONES_VETERINARIAS_EXAMEN> Examenes)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE = new RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE(context);

                    var objDetalle = _RepositorioCARGA_PRESTACIONES_VETERINARIAS_DETALLE.GetByIdWithReferences(IdCargaPrestacionVeterinariaDetalle);
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
                    objDetalle.VALIDADO = MarcarValidado;
                    objDetalle.ERROR = MarcarConError;
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
