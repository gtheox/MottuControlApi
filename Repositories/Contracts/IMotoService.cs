using MottuControlApi.Models;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories.Contracts
{
    public interface IMotoRepository
    {
        Task<PagedList<Moto>> GetAllAsync(PaginationParams paginationParams);
        Task<Moto?> GetByIdAsync(int id);
        Task<IEnumerable<Moto>> GetByPlacaAsync(string placa);
        Task<IEnumerable<Moto>> GetByStatusAsync(string status);
        Task CreateAsync(Moto moto);
        void Update(Moto moto);
        void Delete(Moto moto);
        Task<bool> SaveChangesAsync();
    }
}