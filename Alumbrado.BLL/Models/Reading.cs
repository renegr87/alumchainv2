namespace Alumbrado.BLL.Models
{
    public class Reading
    {
        public string Id { get; set; } = String.Empty;
        public float Consumo { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMedidor TipoMedidor { get; set; }
        public string? Notas { get; set; }

    }
}
