using System.ComponentModel.DataAnnotations;

namespace MottuControlApi.Dtos.Moto
{
    public class CreateMotoDto
    {
        [Required(ErrorMessage = "O modelo da moto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O modelo não pode ter mais de 100 caracteres.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A placa da moto é obrigatória.")]
        [RegularExpression(@"^[A-Z]{3}[0-9][A-Z\d][0-9]{2}$", ErrorMessage = "Formato de placa inválido. Use o padrão Mercosul (ex: ABC1D23) ou o antigo.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status inicial é obrigatório.")]
        public string Status { get; set; } = "Disponível";

        [Required(ErrorMessage = "É obrigatório informar o ID do pátio.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do pátio inválido.")]
        public int PatioId { get; set; }
    }
}