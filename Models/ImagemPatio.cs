using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuControlApi.Models
{
    [Table("ImagensPatio")]
    public class ImagemPatio
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Caminho da imagem (pode ser local ou URL)
        [Required]
        [MaxLength(255)]
        public string CaminhoImagem { get; set; }

        // Data e hora em que a imagem foi capturada
        public DateTime DataCaptura { get; set; }

        // Chave estrangeira para o pátio
        public int PatioId { get; set; }

        // Navegação: pátio ao qual essa imagem pertence
        public Patio Patio { get; set; }
    }
}
