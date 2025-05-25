using System.Collections.Generic;

namespace MottuControlApi.Dtos
{
    public class MotoDto
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Status { get; set; }

        public int PatioId { get; set; }
        public string? NomePatio { get; set; }

        public List<SensorDto>? Sensores { get; set; }
        public List<StatusDto>? StatusMonitoramentos { get; set; }
    }
}
