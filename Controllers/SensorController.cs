using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Dtos.Sensor;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Text.Json;

namespace MottuControlApi.Controllers
{
    [ApiController]
    [Route("api/sensores")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Busca uma lista paginada de sensores.
        /// </summary>
        /// <param name="paginationParams">Parâmetros de paginação (PageNumber, PageSize).</param>
        /// <returns>Uma lista paginada de sensores.</returns>
        /// <response code="200">Retorna a lista de sensores com cabeçalho de paginação.</response>
        /// <response code="404">Se nenhum sensor for encontrado.</response>
        [HttpGet(Name = "GetSensores")]
        [ProducesResponseType(typeof(IEnumerable<SensorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedResult = await _sensorService.GetAllAsync(paginationParams);

            if (pagedResult == null || !pagedResult.Items.Any())
                return NotFound("Nenhum sensor encontrado.");

            var paginationMetadata = new
            {
                pagedResult.TotalCount,
                pagedResult.PageSize,
                pagedResult.CurrentPage,
                pagedResult.TotalPages,
                pagedResult.HasNext,
                pagedResult.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(pagedResult.Items);
        }

        /// <summary>
        /// Busca um sensor específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do sensor a ser buscado.</param>
        /// <returns>Os detalhes do sensor encontrado.</returns>
        /// <response code="200">Retorna o sensor encontrado.</response>
        /// <response code="404">Se o sensor com o ID especificado não for encontrado.</response>
        [HttpGet("{id}", Name = "GetSensorById")]
        [ProducesResponseType(typeof(SensorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDto>> GetById(int id)
        {
            var sensor = await _sensorService.GetByIdAsync(id);
            if (sensor == null)
                return NotFound($"Sensor com ID {id} não encontrado.");

            return Ok(sensor);
        }

        /// <summary>
        /// Busca sensores por tipo.
        /// </summary>
        /// <param name="tipo">O tipo de sensor a ser buscado (ex: GPS, RFID).</param>
        /// <returns>Uma lista de sensores do tipo especificado.</returns>
        /// <response code="200">Retorna a lista de sensores encontrados.</response>
        [HttpGet("buscar/{tipo}", Name = "GetSensoresByTipo")]
        [ProducesResponseType(typeof(IEnumerable<SensorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetByTipo(string tipo)
        {
            var sensores = await _sensorService.GetByTipoAsync(tipo);
            return Ok(sensores);
        }

        /// <summary>
        /// Cria um novo sensor.
        /// </summary>
        /// <param name="createDto">Dados para a criação do novo sensor.</param>
        /// <returns>O sensor recém-criado.</returns>
        /// <response code="201">Retorna o sensor recém-criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost(Name = "CreateSensor")]
        [ProducesResponseType(typeof(SensorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SensorDto>> Create([FromBody] CreateSensorDto createDto)
        {
            var novoSensor = await _sensorService.CreateAsync(createDto);
            return CreatedAtRoute("GetSensorById", new { id = novoSensor.Id }, novoSensor);
        }

        /// <summary>
        /// Atualiza os dados de um sensor existente.
        /// </summary>
        /// <param name="id">ID do sensor a ser atualizado.</param>
        /// <param name="updateDto">Dados do sensor para atualização.</param>
        /// <returns>O sensor com os dados atualizados.</returns>
        /// <response code="200">Retorna o sensor atualizado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Se o sensor não for encontrado.</response>
        [HttpPut("{id}", Name = "UpdateSensor")]
        [ProducesResponseType(typeof(SensorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDto>> Update(int id, [FromBody] UpdateSensorDto updateDto)
        {
            var sensorAtualizado = await _sensorService.UpdateAsync(id, updateDto);
            if (sensorAtualizado == null)
                return NotFound($"Sensor com ID {id} não encontrado para atualização.");

            return Ok(sensorAtualizado);
        }

        /// <summary>
        /// Deleta um sensor pelo ID.
        /// </summary>
        /// <param name="id">ID do sensor a ser deletado.</param>
        /// <response code="204">Sensor deletado com sucesso.</response>
        /// <response code="404">Se o sensor não for encontrado.</response>
        [HttpDelete("{id}", Name = "DeleteSensor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _sensorService.DeleteAsync(id);
            if (!sucesso)
                return NotFound($"Sensor com ID {id} não encontrado para exclusão.");

            return NoContent();
        }
    }
}