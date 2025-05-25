namespace MottuControlApi.Dtos
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

        public int MotoId { get; set; }
        public string? PlacaMoto { get; set; }
    }
}
