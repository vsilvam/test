using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_FACTURACION
	{
		public int? ID { get; set; }				
		public DateTime? FECHA_FACTURACION { get; set; }				
		public bool? ACTIVO { get; set; }				
	}
}
