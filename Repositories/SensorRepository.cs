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
    public class SensorRepository : ISensorRepository
    {
        private readonly AppDbContext _context;

        public SensorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<SensorIoT>> GetAllAsync(PaginationParams paginationParams)
        {
            var query = _context.Sensores
                .Include(s => s.Moto)
                .AsQueryable();

            return await PagedList<SensorIoT>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<SensorIoT?> GetByIdAsync(int id)
        {
            return await _context.Sensores
                .Include(s => s.Moto)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SensorIoT>> GetByTipoAsync(string tipo)
        {
            return await _context.Sensores
                .Include(s => s.Moto)
                .Where(s => s.Tipo.ToLower().Contains(tipo.ToLower()))
                .ToListAsync();
        }

        public async Task CreateAsync(SensorIoT sensor)
        {
            await _context.Sensores.AddAsync(sensor);
        }

        public void Update(SensorIoT sensor)
        {
            _context.Entry(sensor).State = EntityState.Modified;
        }

        public void Delete(SensorIoT sensor)
        {
            _context.Sensores.Remove(sensor);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}