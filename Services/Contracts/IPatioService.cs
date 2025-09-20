using MottuControlApi.Dtos.Patio;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Services.Contracts
{
    public interface IPatioService
    {
        Task<PagedList<PatioDto>?> GetAllAsync(PaginationParams paginationParams); // <--- MUDANÇA AQUI
        Task<PatioDto?> GetByIdAsync(int id);
        Task<IEnumerable<PatioDto>> GetByNomeAsync(string nome);
        Task<PatioDto> CreateAsync(CreatePatioDto createDto);
        Task<PatioDto?> UpdateAsync(int id, UpdatePatioDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}