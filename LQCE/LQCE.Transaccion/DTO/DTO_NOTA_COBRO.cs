using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_NOTA_COBRO
	{
		public int? ID { get; set; }				
		public int? CORRELATIVO { get; set; }				
		public int? ID_CLIENTE { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_COBRO { get; set; }				
	}
}
