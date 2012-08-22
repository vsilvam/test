using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_FACTURA_DETALLE
	{
		public int? ID { get; set; }				
		public int? MONTO_TOTAL { get; set; }				
		public int? MONTO_COBRADO { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_FACTURA { get; set; }				
		public int? ID_PRESTACION { get; set; }				
	}
}
