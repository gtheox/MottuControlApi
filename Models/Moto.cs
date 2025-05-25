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

        // Modelo da moto (Ex: Honda Pop 110i)
        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }

        // Placa da moto
        [Required]
        [MaxLength(10)]
        public string Placa { get; set; }

        // Status: Disponível, Alugada, Manutenção
        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        // Chave estrangeira para o pátio
        public int PatioId { get; set; }

        // Navegação: Pátio onde a moto está
        public Patio Patio { get; set; }

        // Relacionamento: sensores IoT ligados a essa moto
        public ICollection<SensorIoT> Sensores { get; set; }

        // Relacionamento: histórico de status da moto
        public ICollection<StatusMonitoramento> Status { get; set; }

        public Moto()
        {
            Sensores = new List<SensorIoT>();
            Status = new List<StatusMonitoramento>();
        }
    }
}
