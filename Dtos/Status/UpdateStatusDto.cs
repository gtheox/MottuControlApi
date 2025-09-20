using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Status
{
    public class UpdateStatusDto
    {
        [Required(ErrorMessage = "O status é obrigatório.")]
        [StringLength(30, ErrorMessage = "O status não pode ter mais de 30 caracteres.")]
        public string Status { get; set; } = string.Empty;
    }
}