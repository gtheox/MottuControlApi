using System;

namespace MottuControlApi.Dtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DataHora { get; set; }

        public int MotoId { get; set; }
        public string? PlacaMoto { get; set; }
    }
}
