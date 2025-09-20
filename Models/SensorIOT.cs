using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MottuControlApi.Models
{
    [Table("Sensores")]
    public class SensorIoT
    {
        // Chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nome ou identificação do sensor (ex: "Sensor GPS 1")
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        // Tipo do sensor (ex: GPS, RFID, etc.)
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = string.Empty;

        // Chave estrangeira para a moto associada
        public int MotoId { get; set; }

        // Navegação: moto associada a este sensor
        public Moto Moto { get; set; } = null!;
    }
}
