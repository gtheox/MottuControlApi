using MottuControlApi.Models;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories.Contracts
{
    public interface IPatioRepository
    {
        Task<PagedList<Patio>> GetAllAsync(PaginationParams paginationParams);
        Task<Patio?> GetByIdAsync(int id);
        Task<IEnumerable<Patio>> GetByNomeAsync(string nome);
        Task CreateAsync(Patio patio);
        void Update(Patio patio);
        void Delete(Patio patio);
        Task<bool> SaveChangesAsync();
    }
}