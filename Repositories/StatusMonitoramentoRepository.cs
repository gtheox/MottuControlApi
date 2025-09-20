using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Models;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories
{
    public class StatusMonitoramentoRepository : IStatusMonitoramentoRepository
    {
        private readonly AppDbContext _context;

        public StatusMonitoramentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<StatusMonitoramento>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.StatusMonitoramentos
                .Include(s => s.Moto)
                .AsQueryable();

            return await PagedList<StatusMonitoramento>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<StatusMonitoramento?> GetByIdAsync(int id)
        {
            return await _context.StatusMonitoramentos
                .Include(s => s.Moto)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<PagedList<StatusMonitoramento>> GetByMotoIdAsync(int motoId, PaginationParams paginationParams)
        {
            var query = _context.StatusMonitoramentos
                .Where(s => s.MotoId == motoId)
                .Include(s => s.Moto)
                .OrderByDescending(s => s.DataHora)
                .AsQueryable();

            return await PagedList<StatusMonitoramento>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }


        public async Task CreateAsync(StatusMonitoramento status)
        {
            await _context.StatusMonitoramentos.AddAsync(status);
        }

        public void Update(StatusMonitoramento status)
        {
            _context.Entry(status).State = EntityState.Modified;
        }

        public void Delete(StatusMonitoramento status)
        {
            _context.StatusMonitoramentos.Remove(status);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}