using MottuControlApi.Models;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories.Contracts
{
    public interface IStatusMonitoramentoRepository
    {
        Task<PagedList<StatusMonitoramento>> GetAllAsync(PaginationParams paginationParams);
        Task<PagedList<StatusMonitoramento>> GetByMotoIdAsync(int motoId, PaginationParams paginationParams);
        Task<StatusMonitoramento?> GetByIdAsync(int id);
        Task CreateAsync(StatusMonitoramento status);
        void Update(StatusMonitoramento status);
        void Delete(StatusMonitoramento status);
        Task<bool> SaveChangesAsync();
    }
}