using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuControlApi.Models
{
    [Table("Patios")]
    public class Patio
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nome do pátio
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        // Lista de motos alocadas neste pátio
        public ICollection<Moto> Motos { get; set; } = new List<Moto>();

        // Lista de imagens associadas ao pátio
        public ICollection<ImagemPatio> Imagens { get; set; } = new List<ImagemPatio>();
    }
}
