using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_COBRO
	{
		public int? ID { get; set; }				
		public DateTime? FECHA_COBRO { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_TIPO_COBRO { get; set; }				
	}
}
