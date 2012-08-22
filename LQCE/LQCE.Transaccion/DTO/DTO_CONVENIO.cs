using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_CONVENIO
	{
		public int? ID { get; set; }				
		public string NOMBRE { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_TIPO_PRESTACION { get; set; }				
	}
}
