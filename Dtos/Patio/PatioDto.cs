using MottuControlApi.Dtos.Moto;
using System.Collections.Generic;

// O DTO para ImagemDto não foi fornecido, então comentei a referência por enquanto.
// using MottuControlApi.Dtos.Imagem; 

namespace MottuControlApi.Dtos.Patio
{
    /// <summary>
    /// Objeto de transferência de dados para representar um Pátio.
    /// </summary>
    public class PatioDto
    {
        /// <summary>
        /// ID do pátio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do pátio (ex: "Pátio Central").
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Lista de motos alocadas neste pátio.
        /// </summary>
        public List<MotoDto>? Motos { get; set; }

        // A propriedade de Imagens será adicionada quando tivermos o ImagemDto.
        // public List<ImagemDto>? Imagens { get; set; }
    }
}