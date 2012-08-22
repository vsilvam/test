using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PAGO
	{
		public int? ID { get; set; }				
		public int? FECHA_PAGO { get; set; }				
		public int? MONTO_PAGO { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_CLIENTE { get; set; }				
	}
}
