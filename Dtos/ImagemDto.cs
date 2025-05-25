using System;

namespace MottuControlApi.Dtos
{
    public class ImagemDto
    {
        public int Id { get; set; }
        public string CaminhoImagem { get; set; }
        public DateTime DataCaptura { get; set; }

        public int PatioId { get; set; }
        public string? NomePatio { get; set; }
    }
}
