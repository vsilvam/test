using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_GARANTIA
	{
		public int? ID { get; set; }				
		public string NOMBRE { get; set; }				
		public bool? ACTIVO { get; set; }				
	}
}
