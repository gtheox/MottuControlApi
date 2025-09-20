using MottuControlApi.Models;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories.Contracts
{
    public interface ISensorRepository
    {
        Task<PagedList<SensorIoT>> GetAllAsync(PaginationParams paginationParams);
        Task<SensorIoT?> GetByIdAsync(int id);
        Task<IEnumerable<SensorIoT>> GetByTipoAsync(string tipo);
        Task CreateAsync(SensorIoT sensor);
        void Update(SensorIoT sensor);
        void Delete(SensorIoT sensor);
        Task<bool> SaveChangesAsync();
    }
}