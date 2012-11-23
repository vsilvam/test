
namespace LQCE.Transaccion.DTO
{
    public class DTOFindCliente: DTOPaginador
    {
        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public int? ID_REGION { get; set; }
        public int? ID_COMUNA { get; set; }
        public int? ID_TIPO_PRESTACION { get; set; }
        public int? ID_CONVENIO { get; set; }
    }
}
