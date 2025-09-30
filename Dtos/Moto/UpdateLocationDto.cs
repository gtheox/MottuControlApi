using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Moto
{
    public class UpdateLocationDto
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}