using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_CONVENIO_TARIFARIO
	{
		public int? ID { get; set; }				
		public DateTime? FECHA_VIGENCIA { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_CONVENIO { get; set; }				
	}
}
