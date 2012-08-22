using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_EXAMEN_DETALLE
	{
		public int? ID { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_EXAMEN { get; set; }				
		public int? ID_EXAMEN1 { get; set; }				
	}
}
