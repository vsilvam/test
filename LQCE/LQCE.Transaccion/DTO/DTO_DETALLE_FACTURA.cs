using System;
using System.Collections.Generic;

namespace LQCE.Transaccion.DTO
{
    public class DTO_DETALLE_FACTURA
    {
        public int? NUMERO_FACTURA { get; set; }
        public int ID_FACTURA { get; set; }        
        public string RUT_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public int VALOR_TOTAL { get; set; }
        public int VALOR_PAGADO { get; set; }
        public int PAGOS_REGISTRADOS { get; set; }
        public int SALDO_DEUDOR { get; set; }
        public bool PAGADA { get; set; }
        public List<DTO_DETALLE_FACTURA_PRESTACION> LISTA_PRESTACIONES { get; set; }
        public List<DTO_DETALLE_FACTURA_COBRO> LISTA_COBROS { get; set; }
        public List<DTO_DETALLE_FACTURA_PAGO> LISTA_PAGOS { get; set; }

        public DTO_DETALLE_FACTURA()
        {
            LISTA_PRESTACIONES = new List<DTO_DETALLE_FACTURA_PRESTACION>();
            LISTA_COBROS = new List<DTO_DETALLE_FACTURA_COBRO>();
            LISTA_PAGOS = new List<DTO_DETALLE_FACTURA_PAGO>();
        }
    }

    public class DTO_DETALLE_FACTURA_PRESTACION
    {
        public int ID_CLIENTE { get; set; }
        public int ID_FACTURA_DETALLE { get; set; }
        public int NUMERO_FICHA { get; set; }
        public int MONTO_TOTAL { get; set; }
        public int MONTO_COBRADO { get; set; }
        public DateTime FECHA_RECEPCION { get; set; }
        public string NOMBRE_PACIENTE { get; set; }
    }

    public class DTO_DETALLE_FACTURA_COBRO
    {
        public int ID_NOTA_COBRO { get; set; }
        public DateTime FECHA_COBRO { get; set; }
        public string NOMBRE_TIPO_COBRO { get; set; }
        public int MONTO_PENDIENTE_TOTAL { get; set; }
        public int MONTO_PENDIENTE_FACTURA { get; set; }
    }

    public class DTO_DETALLE_FACTURA_PAGO
    {
        public int ID_PAGO { get; set; }
        public int FECHA_PAGO { get; set; }
        public int MONTO_PAGO_TOTAL { get; set; }
        public int MONTO_PAGO_FACTURA { get; set; }
    }
}
