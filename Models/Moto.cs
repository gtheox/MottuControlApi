using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuControlApi.Models
{
    [Table("Motos")]
    public class Moto
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Modelo da moto (ex: Honda Pop 110i)
        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; } = string.Empty;

        // Placa da moto
        [Required]
        [MaxLength(10)]
        public string Placa { get; set; } = string.Empty;

        // Status da moto (Disponível, Alugada, Manutenção)
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = string.Empty;

        // Chave estrangeira do pátio
        [ForeignKey("Patio")]
        public int PatioId { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? UltimaAtualizacaoLocalizacao { get; set; }


        // Navegação: pátio associado à moto
        public Patio Patio { get; set; } = null!;

        // Lista de sensores conectados à moto
        public ICollection<SensorIoT> Sensores { get; set; } = new List<SensorIoT>();

        // Histórico de status da moto
        public ICollection<StatusMonitoramento> HistoricoStatus { get; set; } = new List<StatusMonitoramento>();
    }
}
