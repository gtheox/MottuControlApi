using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Models;
using MottuControlApi.Services;

namespace MottuControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        private readonly ImagemService _service;

        public ImagemController(ImagemService service)
        {
            _service = service;
        }

        // GET: api/imagem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagemPatio>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/imagem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagemPatio>> GetById(int id)
        {
            var imagem = await _service.GetByIdAsync(id);
            if (imagem == null)
                return NotFound();

            return Ok(imagem);
        }

        // GET: api/imagem/patio/3
        [HttpGet("patio/{patioId}")]
        public async Task<ActionResult<IEnumerable<ImagemPatio>>> GetByPatio(int patioId)
        {
            var result = await _service.GetByPatioIdAsync(patioId);
            return Ok(result);
        }

        // POST: api/imagem
        [HttpPost]
        public async Task<ActionResult<ImagemPatio>> Criar(ImagemPatio imagem)
        {
            var criado = await _service.CriarAsync(imagem);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT: api/imagem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ImagemPatio imagem)
        {
            var sucesso = await _service.AtualizarAsync(id, imagem);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        // DELETE: api/imagem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }
}
