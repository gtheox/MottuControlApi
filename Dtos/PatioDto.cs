using System.Collections.Generic;

namespace MottuControlApi.Dtos
{
    public class PatioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<MotoDto>? Motos { get; set; }
        public List<ImagemDto>? Imagens { get; set; }
    }
}
