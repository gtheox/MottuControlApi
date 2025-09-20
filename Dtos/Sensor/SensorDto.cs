namespace MottuControlApi.Dtos.Sensor
{
    /// <summary>
    /// Objeto de transferência de dados para representar um Sensor IoT.
    /// </summary>
    public class SensorDto
    {
        /// <summary>
        /// ID do sensor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do sensor (ex: "Sensor GPS A").
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Tipo do sensor (ex: "GPS", "RFID").
        /// </summary>
        public string Tipo { get; set; } = string.Empty;

        /// <summary>
        /// ID da moto à qual o sensor está vinculado.
        /// </summary>
        public int MotoId { get; set; }

        /// <summary>
        /// Placa da moto associada para facilitar a visualização.
        /// </summary>
        public string? PlacaMoto { get; set; }
    }
}