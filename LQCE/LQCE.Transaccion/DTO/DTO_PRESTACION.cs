using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_PRESTACION
	{
		public int? ID { get; set; }				
		public string MEDICO { get; set; }				
		public DateTime? FECHA_MUESTRA { get; set; }				
		public DateTime? FECHA_RECEPCION { get; set; }				
		public DateTime? FECHA_ENTREGA_RESULTADOS { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_CLIENTE { get; set; }				
		public int? ID_GARANTIA { get; set; }				
		public int? ID_PREVISION { get; set; }				
		public int? ID_TIPO_PRESTACION { get; set; }				
	}
}
