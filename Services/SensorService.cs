using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Models;

namespace MottuControlApi.Services
{
    public class SensorService
    {
        private readonly AppDbContext _context;

        public SensorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SensorIoT>> GetAllAsync()
        {
            return await _context.Sensores
                .Include(s => s.Moto)
                .ToListAsync();
        }

        public async Task<SensorIoT?> GetByIdAsync(int id)
        {
            return await _context.Sensores
                .Include(s => s.Moto)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SensorIoT>> BuscarPorTipoAsync(string tipo)
        {
            return await _context.Sensores
                .Where(s => s.Tipo.ToLower().Contains(tipo.ToLower()))
                .Include(s => s.Moto)
                .ToListAsync();
        }

        public async Task<SensorIoT> CriarAsync(SensorIoT sensor)
        {
            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task<bool> AtualizarAsync(int id, SensorIoT sensor)
        {
            if (id != sensor.Id) return false;

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Sensores.AnyAsync(s => s.Id == id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor == null) return false;

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
