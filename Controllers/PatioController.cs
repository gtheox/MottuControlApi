using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Models;
using MottuControlApi.Services;

namespace MottuControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : ControllerBase
    {
        private readonly PatioService _service;

        public PatioController(PatioService service)
        {
            _service = service;
        }

        // GET: api/patio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patio>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/patio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> GetById(int id)
        {
            var patio = await _service.GetByIdAsync(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        // GET: api/patio/buscar?nome=central
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Patio>>> BuscarPorNome([FromQuery] string nome)
        {
            var result = await _service.BuscarPorNomeAsync(nome);
            return Ok(result);
        }

        // POST: api/patio
        [HttpPost]
        public async Task<ActionResult<Patio>> Criar(Patio patio)
        {
            var criado = await _service.CriarAsync(patio);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT: api/patio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Patio patio)
        {
            var sucesso = await _service.AtualizarAsync(id, patio);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        // DELETE: api/patio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }
}
