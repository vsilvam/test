using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_NOTA_COBRO_DETALLE
	{
		public int? ID { get; set; }				
		public int? MONTO_PENDIENTE { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_FACTURA { get; set; }				
		public int? ID_NOTA_COBRO { get; set; }				
	}
}
