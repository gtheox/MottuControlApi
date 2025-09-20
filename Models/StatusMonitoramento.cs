using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuControlApi.Models
{
    [Table("StatusMonitoramentos")]
    public class StatusMonitoramento
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Status da moto (ex: "Disponível", "Alugada", "Manutenção")
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = string.Empty;

        // Data e hora do registro
        public DateTime DataHora { get; set; }

        // Chave estrangeira para a moto
        public int MotoId { get; set; }

        // Navegação: moto relacionada
        public Moto Moto { get; set; } = null!;
    }
}
