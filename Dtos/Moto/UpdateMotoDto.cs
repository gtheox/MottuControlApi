using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Moto
{
    public class UpdateMotoDto
    {
        [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O modelo não pode ter mais de 100 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status da moto é obrigatório.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "É obrigatório informar o ID do pátio.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do pátio inválido.")]
        public int PatioId { get; set; }
    }
}