using MottuControlApi.Dtos.Sensor;
using MottuControlApi.Dtos.Status;
using System.Collections.Generic;

namespace MottuControlApi.Dtos.Moto
{
    /// <summary>
    /// Objeto de transferência de dados para representar uma Moto.
    /// </summary>
    public class MotoDto
    {
        /// <summary>
        /// ID da moto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Modelo da moto (ex: "Honda Biz 125").
        /// </summary>
        public string Modelo { get; set; } = string.Empty;

        /// <summary>
        /// Placa da moto.
        /// </summary>
        public string Placa { get; set; } = string.Empty;

        /// <summary>
        /// Status atual da moto (ex: "Disponível", "Alugada").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// ID do pátio onde a moto está alocada.
        /// </summary>
        public int PatioId { get; set; }

        /// <summary>
        /// Nome do pátio para facilitar a visualização.
        /// </summary>
        public string? NomePatio { get; set; }

        /// <summary>
        /// Lista de sensores IoT associados à moto.
        /// </summary>
        public List<SensorDto>? Sensores { get; set; }

        /// <summary>
        /// Histórico de status da moto.
        /// </summary>
        public List<StatusDto>? StatusMonitoramentos { get; set; }
    }
}