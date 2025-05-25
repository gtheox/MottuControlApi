using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Models;

namespace MottuControlApi.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moto>> GetAllAsync()
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Include(m => m.Sensores)
                .Include(m => m.Status)
                .ToListAsync();
        }

        public async Task<Moto?> GetByIdAsync(int id)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Include(m => m.Sensores)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Moto>> BuscarPorPlacaAsync(string placa)
        {
            return await _context.Motos
                .Where(m => m.Placa.ToLower().Contains(placa.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Moto>> BuscarPorStatusAsync(string status)
        {
            return await _context.Motos
                .Where(m => m.Status.ToLower() == status.ToLower())
                .ToListAsync();
        }

        public async Task<Moto> CriarAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<bool> AtualizarAsync(int id, Moto moto)
        {
            if (id != moto.Id) return false;

            _context.Entry(moto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Motos.AnyAsync(m => m.Id == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
