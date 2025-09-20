using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Dtos.Patio;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Text.Json;

namespace MottuControlApi.Controllers
{
    [ApiController]
    [Route("api/patios")]
    public class PatioController : ControllerBase
    {
        private readonly IPatioService _patioService;

        public PatioController(IPatioService patioService)
        {
            _patioService = patioService;
        }

        /// <summary>
        /// Busca uma lista paginada de pátios.
        /// </summary>
        /// <param name="paginationParams">Parâmetros de paginação (PageNumber, PageSize).</param>
        /// <returns>Uma lista paginada de pátios.</returns>
        /// <response code="200">Retorna a lista de pátios com cabeçalho de paginação.</response>
        /// <response code="404">Se nenhum pátio for encontrado.</response>
        [HttpGet(Name = "GetPatios")]
        [ProducesResponseType(typeof(IEnumerable<PatioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PatioDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedResult = await _patioService.GetAllAsync(paginationParams);

            if (pagedResult == null || !pagedResult.Items.Any())
                return NotFound("Nenhum pátio encontrado.");

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
        /// Busca um pátio específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do pátio a ser buscado.</param>
        /// <returns>Os detalhes do pátio encontrado.</returns>
        /// <response code="200">Retorna o pátio encontrado.</response>
        /// <response code="404">Se o pátio com o ID especificado não for encontrado.</response>
        [HttpGet("{id}", Name = "GetPatioById")]
        [ProducesResponseType(typeof(PatioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatioDto>> GetById(int id)
        {
            var patio = await _patioService.GetByIdAsync(id);
            if (patio == null)
                return NotFound($"Pátio com ID {id} não encontrado.");

            return Ok(patio);
        }

        /// <summary>
        /// Cria um novo pátio.
        /// </summary>
        /// <param name="createDto">Dados para a criação do novo pátio.</param>
        /// <returns>O pátio recém-criado.</returns>
        /// <response code="201">Retorna o pátio recém-criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost(Name = "CreatePatio")]
        [ProducesResponseType(typeof(PatioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PatioDto>> Create([FromBody] CreatePatioDto createDto)
        {
            var novoPatio = await _patioService.CreateAsync(createDto);
            return CreatedAtRoute("GetPatioById", new { id = novoPatio.Id }, novoPatio);
        }

        /// <summary>
        /// Atualiza os dados de um pátio existente.
        /// </summary>
        /// <param name="id">ID do pátio a ser atualizado.</param>
        /// <param name="updateDto">Dados do pátio para atualização.</param>
        /// <returns>O pátio com os dados atualizados.</returns>
        /// <response code="200">Retorna o pátio atualizado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Se o pátio não for encontrado.</response>
        [HttpPut("{id}", Name = "UpdatePatio")]
        [ProducesResponseType(typeof(PatioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatioDto>> Update(int id, [FromBody] UpdatePatioDto updateDto)
        {
            var patioAtualizado = await _patioService.UpdateAsync(id, updateDto);
            if (patioAtualizado == null)
                return NotFound($"Pátio com ID {id} não encontrado para atualização.");

            return Ok(patioAtualizado);
        }

        /// <summary>
        /// Deleta um pátio pelo ID.
        /// </summary>
        /// <param name="id">ID do pátio a ser deletado.</param>
        /// <response code="204">Pátio deletado com sucesso.</response>
        /// <response code="404">Se o pátio não for encontrado.</response>
        [HttpDelete("{id}", Name = "DeletePatio")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _patioService.DeleteAsync(id);
            if (!sucesso)
                return NotFound($"Pátio com ID {id} não encontrado para exclusão.");

            return NoContent();
        }
    }
}