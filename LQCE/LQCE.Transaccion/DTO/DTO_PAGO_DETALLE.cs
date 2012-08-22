using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PAGO_DETALLE
	{
		public int? ID { get; set; }				
		public int? MONTO { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_FACTURA_DETALLE { get; set; }				
		public int? ID_PAGO { get; set; }				
	}
}
