using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Models;
using MottuControlApi.Services;

namespace MottuControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _service;

        public SensorController(SensorService service)
        {
            _service = service;
        }

        // GET: api/sensor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorIoT>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/sensor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorIoT>> GetById(int id)
        {
            var sensor = await _service.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        // GET: api/sensor/buscar?tipo=gps
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<SensorIoT>>> BuscarPorTipo([FromQuery] string tipo)
        {
            var result = await _service.BuscarPorTipoAsync(tipo);
            return Ok(result);
        }

        // POST: api/sensor
        [HttpPost]
        public async Task<ActionResult<SensorIoT>> Criar(SensorIoT sensor)
        {
            var criado = await _service.CriarAsync(sensor);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }

        // PUT: api/sensor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, SensorIoT sensor)
        {
            var sucesso = await _service.AtualizarAsync(id, sensor);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        // DELETE: api/sensor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }
}
