using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Patio
{
    public class UpdatePatioDto
    {
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do pátio não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
    }
}