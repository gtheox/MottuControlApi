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

        // Status atual da moto: "Disponível", "Alugada", "Manutenção"
        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        // Data e hora em que o status foi registrado
        public DateTime DataHora { get; set; }

        // Chave estrangeira da moto relacionada ao status
        public int MotoId { get; set; }

        // Navegação: moto que possui esse status
        public Moto Moto { get; set; }
    }
}
