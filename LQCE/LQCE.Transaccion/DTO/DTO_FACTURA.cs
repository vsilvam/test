using System;

namespace LQCE.Transaccion.DTO
{
	[Serializable()]
	public class DTO_FACTURA
	{
		public int? ID { get; set; }				
		public int? CORRELATIVO { get; set; }				
		public int? NUMERO_FACTURA { get; set; }				
		public string RUT_LABORATORIO { get; set; }				
		public bool? ACTIVO { get; set; }				
		public int? ID_CLIENTE { get; set; }				
		public int? ID_FACTURACION { get; set; }				
	}
}
