using System;

namespace MottuControlApi.Dtos.Status
{
    /// <summary>
    /// Objeto de transferência de dados para representar um registro de Status.
    /// </summary>
    public class StatusDto
    {
        /// <summary>
        /// ID do registro de status.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Status da moto (ex: "Disponível", "Alugada").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora em que o status foi registrado.
        /// </summary>
        public DateTime DataHora { get; set; }

        /// <summary>
        /// ID da moto associada a este status.
        /// </summary>
        public int MotoId { get; set; }

        /// <summary>
        /// Placa da moto para facilitar a visualização.
        /// </summary>
        public string? PlacaMoto { get; set; }
    }
}