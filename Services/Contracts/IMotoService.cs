using MottuControlApi.Dtos.Moto;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Services.Contracts
{
    public interface IMotoService
    {
        Task<PagedList<MotoDto>?> GetAllAsync(PaginationParams paginationParams); // <--- MUDANÇA AQUI
        Task<MotoDto?> GetByIdAsync(int id);
        Task<IEnumerable<MotoDto>> GetByPlacaAsync(string placa);
        Task<IEnumerable<MotoDto>> GetByStatusAsync(string status);
        Task<MotoDto> CreateAsync(CreateMotoDto createDto);
        Task<MotoDto?> UpdateAsync(int id, UpdateMotoDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateLocationAsync(int motoId, UpdateLocationDto locationDto);
    }
}