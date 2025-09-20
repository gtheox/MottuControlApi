using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Models;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly AppDbContext _context;

        public MotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Moto>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Motos
                .Include(m => m.Patio)
                .AsQueryable();

            return await PagedList<Moto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<Moto?> GetByIdAsync(int id)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Include(m => m.Sensores)
                .Include(m => m.HistoricoStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Moto>> GetByPlacaAsync(string placa)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Where(m => m.Placa.ToLower().Contains(placa.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Moto>> GetByStatusAsync(string status)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Where(m => m.Status.ToLower() == status.ToLower())
                .ToListAsync();
        }

        public async Task CreateAsync(Moto moto)
        {
            await _context.Motos.AddAsync(moto);
        }

        public void Update(Moto moto)
        {
            _context.Entry(moto).State = EntityState.Modified;
        }

        public void Delete(Moto moto)
        {
            _context.Motos.Remove(moto);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}