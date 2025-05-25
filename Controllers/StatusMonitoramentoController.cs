using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Models;
using MottuControlApi.Services;

namespace MottuControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusMonitoramentoController : ControllerBase
    {
        private readonly StatusService _service;

        public StatusMonitoramentoController(StatusService service)
        {
            _service = service;
        }

        // GET: api/statusmonitoramento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusMonitoramento>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/statusmonitoramento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusMonitoramento>> GetById(int id)
        {
            var status = await _service.GetByIdAsync(id);
            if (status == null)
                return NotFound();

            return Ok(status);
        }

        // GET: api/statusmonitoramento/moto/3
        [HttpGet("moto/{motoId}")]
        public async Task<ActionResult<IEnumerable<StatusMonitoramento>>> GetByMotoId(int motoId)
        {
            var historico = await _service.GetByMotoIdAsync(motoId);
            return Ok(historico);
        }

        // POST: api/statusmonitoramento
        [HttpPost]
        public async Task<ActionResult<StatusMonitoramento>> Criar(StatusMonitoramento status)
        {
            var criado = await _service.CriarAsync(status);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT: api/statusmonitoramento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, StatusMonitoramento status)
        {
            var sucesso = await _service.AtualizarAsync(id, status);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        // DELETE: api/statusmonitoramento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }
}
