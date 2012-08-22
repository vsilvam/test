using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PRESTACION_EXAMEN
	{
		public int? ID { get; set; }				
		public int? VALOR { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_EXAMEN { get; set; }				
		public int? ID_PRESTACION { get; set; }				
	}
}
