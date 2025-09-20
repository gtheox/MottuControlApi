using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Sensor
{
    public class CreateSensorDto
    {
        [Required(ErrorMessage = "O nome do sensor é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo do sensor é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo não pode ter mais de 50 caracteres.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "É obrigatório informar o ID da moto.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da moto inválido.")]
        public int MotoId { get; set; }
    }
}