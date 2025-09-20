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
    public class PatioRepository : IPatioRepository
    {
        private readonly AppDbContext _context;

        public PatioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Patio>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Patios
                .Include(p => p.Motos)
                .AsQueryable();

            return await PagedList<Patio>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<Patio?> GetByIdAsync(int id)
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .Include(p => p.Imagens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patio>> GetByNomeAsync(string nome)
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync();
        }

        public async Task CreateAsync(Patio patio)
        {
            await _context.Patios.AddAsync(patio);
        }

        public void Update(Patio patio)
        {
            _context.Entry(patio).State = EntityState.Modified;
        }

        public void Delete(Patio patio)
        {
            _context.Patios.Remove(patio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}