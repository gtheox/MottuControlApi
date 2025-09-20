using MottuControlApi.Dtos.Status;
using MottuControlApi.Shared.Pagination;
using System.Threading.Tasks;

namespace MottuControlApi.Services.Contracts
{
    public interface IStatusMonitoramentoService
    {
        Task<PagedList<StatusDto>> GetAllAsync(PaginationParams paginationParams);
        Task<StatusDto?> GetByIdAsync(int id);
        Task<PagedList<StatusDto>> GetByMotoIdAsync(int motoId, PaginationParams paginationParams);
        Task<StatusDto> CreateAsync(CreateStatusDto createDto);
        Task<StatusDto?> UpdateAsync(int id, UpdateStatusDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}