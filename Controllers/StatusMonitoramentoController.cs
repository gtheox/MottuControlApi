using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Dtos.Status;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Text.Json;

namespace MottuControlApi.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusMonitoramentoController : ControllerBase
    {
        private readonly IStatusMonitoramentoService _statusService;

        public StatusMonitoramentoController(IStatusMonitoramentoService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Busca uma lista paginada de todos os registros de status.
        /// </summary>
        /// <param name="paginationParams">Parâmetros de paginação.</param>
        /// <returns>Uma lista paginada de registros de status.</returns>
        /// <response code="200">Retorna a lista de status com cabeçalho de paginação.</response>
        /// <response code="404">Se nenhum registro for encontrado.</response>
        [HttpGet(Name = "GetAllStatus")]
        [ProducesResponseType(typeof(IEnumerable<StatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedResult = await _statusService.GetAllAsync(paginationParams);

            if (pagedResult == null || !pagedResult.Items.Any())
                return NotFound("Nenhum registro de status encontrado.");

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
        /// Busca um registro de status pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do registro a ser buscado.</param>
        /// <returns>Os detalhes do registro encontrado.</returns>
        /// <response code="200">Retorna o registro encontrado.</response>
        /// <response code="404">Se o registro com o ID especificado não for encontrado.</response>
        [HttpGet("{id}", Name = "GetStatusById")]
        [ProducesResponseType(typeof(StatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusDto>> GetById(int id)
        {
            var status = await _statusService.GetByIdAsync(id);
            if (status == null)
                return NotFound($"Registro de status com ID {id} não encontrado.");

            return Ok(status);
        }

        /// <summary>
        /// Busca o histórico paginado de status de uma moto específica.
        /// </summary>
        /// <param name="motoId">O ID da moto para buscar o histórico.</param>
        /// <param name="paginationParams">Parâmetros de paginação.</param>
        /// <returns>Uma lista paginada do histórico de status da moto.</returns>
        /// <response code="200">Retorna o histórico da moto.</response>
        /// <response code="404">Se a moto ou o histórico não forem encontrados.</response>
        [HttpGet("moto/{motoId}", Name = "GetStatusByMotoId")]
        [ProducesResponseType(typeof(IEnumerable<StatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetByMotoId(int motoId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedResult = await _statusService.GetByMotoIdAsync(motoId, paginationParams);

            if (pagedResult == null || !pagedResult.Items.Any())
                return NotFound($"Nenhum histórico de status encontrado para a moto com ID {motoId}.");

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
        /// Cria um novo registro de status para uma moto.
        /// </summary>
        /// <param name="createDto">Dados para a criação do novo registro.</param>
        /// <returns>O registro recém-criado.</returns>
        /// <response code="201">Retorna o registro recém-criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost(Name = "CreateStatus")]
        [ProducesResponseType(typeof(StatusDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusDto>> Create([FromBody] CreateStatusDto createDto)
        {
            var novoStatus = await _statusService.CreateAsync(createDto);
            return CreatedAtRoute("GetStatusById", new { id = novoStatus.Id }, novoStatus);
        }

        /// <summary>
        /// Atualiza um registro de status existente.
        /// </summary>
        /// <param name="id">ID do registro a ser atualizado.</param>
        /// <param name="updateDto">Dados para atualização.</param>
        /// <returns>O registro com os dados atualizados.</returns>
        /// <response code="200">Retorna o registro atualizado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Se o registro não for encontrado.</response>
        [HttpPut("{id}", Name = "UpdateStatus")]
        [ProducesResponseType(typeof(StatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusDto>> Update(int id, [FromBody] UpdateStatusDto updateDto)
        {
            var statusAtualizado = await _statusService.UpdateAsync(id, updateDto);
            if (statusAtualizado == null)
                return NotFound($"Registro de status com ID {id} não encontrado para atualização.");

            return Ok(statusAtualizado);
        }

        /// <summary>
        /// Deleta um registro de status pelo ID.
        /// </summary>
        /// <param name="id">ID do registro a ser deletado.</param>
        /// <response code="204">Registro deletado com sucesso.</response>
        /// <response code="404">Se o registro não for encontrado.</response>
        [HttpDelete("{id}", Name = "DeleteStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _statusService.DeleteAsync(id);
            if (!sucesso)
                return NotFound($"Registro de status com ID {id} não encontrado para exclusão.");

            return NoContent();
        }
    }
}