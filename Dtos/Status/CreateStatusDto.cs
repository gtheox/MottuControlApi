using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Status
{
    public class CreateStatusDto
    {
        [Required(ErrorMessage = "O status é obrigatório.")]
        [StringLength(30, ErrorMessage = "O status não pode ter mais de 30 caracteres.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "É obrigatório informar o ID da moto.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID da moto inválido.")]
        public int MotoId { get; set; }
    }
}