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

        // Nome ou identificação do pátio
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        // Relacionamento: Um pátio pode ter várias motos
        public ICollection<Moto> Motos { get; set; }

        // Relacionamento: Um pátio pode ter várias imagens
        public ICollection<ImagemPatio> Imagens { get; set; }

        // Construtor: garante que as coleções não sejam nulas
        public Patio()
        {
            Motos = new List<Moto>();
            Imagens = new List<ImagemPatio>();
        }
    }
}
