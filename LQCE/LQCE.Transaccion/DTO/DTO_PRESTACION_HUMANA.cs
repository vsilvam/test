using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PRESTACION_HUMANA
	{
		public int? ID { get; set; }				
		public string NOMBRE { get; set; }				
		public string RUT { get; set; }				
		public string EDAD { get; set; }				
		public string TELEFONO { get; set; }				
		public bool? ACTIVO { get; set; }				
	}
}
