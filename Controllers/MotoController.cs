using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Models;
using MottuControlApi.Services;

namespace MottuControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _service;

        public MotoController(MotoService service)
        {
            _service = service;
        }

        // GET: api/moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/moto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _service.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        // GET: api/moto/buscar?placa=XYZ1234
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Moto>>> BuscarPorPlaca([FromQuery] string placa)
        {
            var result = await _service.BuscarPorPlacaAsync(placa);
            return Ok(result);
        }

        // GET: api/moto/status?status=alugada
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Moto>>> BuscarPorStatus([FromQuery] string status)
        {
            var result = await _service.BuscarPorStatusAsync(status);
            return Ok(result);
        }

        // POST: api/moto
        [HttpPost]
        public async Task<ActionResult<Moto>> Criar(Moto moto)
        {
            var criado = await _service.CriarAsync(moto);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT: api/moto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Moto moto)
        {
            var sucesso = await _service.AtualizarAsync(id, moto);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        // DELETE: api/moto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }
}
