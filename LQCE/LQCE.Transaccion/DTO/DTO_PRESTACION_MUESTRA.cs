using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PRESTACION_MUESTRA
	{
		public int? ID { get; set; }				
		public string NOMBRE { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_PRESTACION { get; set; }				
	}
}
