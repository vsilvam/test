using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_CONVENIO_EXAMEN
	{
		public int? ID { get; set; }				
		public int? VALOR { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_CONVENIO_TARIFARIO { get; set; }				
		public int? ID_EXAMEN { get; set; }				
	}
}
