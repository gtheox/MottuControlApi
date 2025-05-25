using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Models;

namespace MottuControlApi.Services
{
    public class PatioService
    {
        private readonly AppDbContext _context;

        public PatioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patio>> GetAllAsync()
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .Include(p => p.Imagens)
                .ToListAsync();
        }

        public async Task<Patio?> GetByIdAsync(int id)
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .Include(p => p.Imagens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patio>> BuscarPorNomeAsync(string nome)
        {
            return await _context.Patios
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .ToListAsync();
        }

        public async Task<Patio> CriarAsync(Patio patio)
        {
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
            return patio;
        }

        public async Task<bool> AtualizarAsync(int id, Patio patio)
        {
            if (id != patio.Id) return false;

            _context.Entry(patio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Patios.AnyAsync(p => p.Id == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null) return false;

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
