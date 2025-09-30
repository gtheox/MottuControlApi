using Microsoft.AspNetCore.Mvc;
using MottuControlApi.Dtos.Moto;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Text.Json;

namespace MottuControlApi.Controllers
{
    [ApiController]
    [Route("api/motos")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        /// <summary>
        /// Busca uma lista paginada de motos.
        /// </summary>
        /// <param name="paginationParams">Parâmetros de paginação (PageNumber, PageSize).</param>
        /// <returns>Uma lista paginada de motos.</returns>
        /// <response code="200">Retorna a lista de motos com cabeçalho de paginação.</response>
        /// <response code="404">Se nenhuma moto for encontrada.</response>
        [HttpGet(Name = "GetMotos")]
        [ProducesResponseType(typeof(IEnumerable<MotoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var pagedResult = await _motoService.GetAllAsync(paginationParams);

            if (pagedResult == null || !pagedResult.Items.Any())
                return NotFound("Nenhuma moto encontrada.");

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
        /// Busca uma moto específica pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da moto a ser buscada.</param>
        /// <returns>Os detalhes da moto encontrada.</returns>
        /// <response code="200">Retorna a moto encontrada.</response>
        /// <response code="404">Se a moto com o ID especificado não for encontrada.</response>
        [HttpGet("{id}", Name = "GetMotoById")]
        [ProducesResponseType(typeof(MotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDto>> GetById(int id)
        {
            var moto = await _motoService.GetByIdAsync(id);
            if (moto == null)
                return NotFound($"Moto com ID {id} não encontrada.");

            return Ok(moto);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        /// <param name="createDto">Dados para a criação da nova moto.</param>
        /// <returns>A moto recém-criada.</returns>
        /// <response code="201">Retorna a moto recém-criada.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost(Name = "CreateMoto")]
        [ProducesResponseType(typeof(MotoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoDto>> Create([FromBody] CreateMotoDto createDto)
        {
            var novaMoto = await _motoService.CreateAsync(createDto);
            return CreatedAtRoute("GetMotoById", new { id = novaMoto.Id }, novaMoto);
        }

        /// <summary>
        /// Atualiza os dados de uma moto existente.
        /// </summary>
        /// <param name="id">ID da moto a ser atualizada.</param>
        /// <param name="updateDto">Dados da moto para atualização.</param>
        /// <returns>A moto com os dados atualizados.</returns>
        /// <response code="200">Retorna a moto atualizada.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Se a moto não for encontrada.</response>
        [HttpPut("{id}", Name = "UpdateMoto")]
        [ProducesResponseType(typeof(MotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDto>> Update(int id, [FromBody] UpdateMotoDto updateDto)
        {
            var motoAtualizada = await _motoService.UpdateAsync(id, updateDto);
            if (motoAtualizada == null)
                return NotFound($"Moto com ID {id} não encontrada para atualização.");

            return Ok(motoAtualizada);
        }

        /// <summary>
        /// Deleta uma moto pelo ID.
        /// </summary>
        /// <param name="id">ID da moto a ser deletada.</param>
        /// <response code="204">Moto deletada com sucesso.</response>
        /// <response code="404">Se a moto não for encontrada.</response>
        [HttpDelete("{id}", Name = "DeleteMoto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _motoService.DeleteAsync(id);
            if (!sucesso)
                return NotFound($"Moto com ID {id} não encontrada para exclusão.");

            return NoContent();
        }

        // NOVO ENDPOINT PARA O IOT
        /// <summary>
        /// Atualiza a localização GPS de uma moto específica.
        /// </summary>
        /// <remarks>Este endpoint é para ser consumido por dispositivos IoT.</remarks>
        /// <param name="id">O ID da moto a ser atualizada.</param>
        /// <param name="locationDto">Objeto com latitude e longitude.</param>
        /// <response code="204">Localização atualizada com sucesso.</response>
        /// <response code="400">Dados de localização inválidos.</response>
        /// <response code="404">Moto com o ID especificado não encontrada.</response>
        [HttpPost("{id}/localizacao")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateLocationDto locationDto)
        {
            var sucesso = await _motoService.UpdateLocationAsync(id, locationDto);

            if (!sucesso)
            {
                return NotFound($"Moto com ID {id} não encontrada.");
            }

            return NoContent();
        }
    }
}