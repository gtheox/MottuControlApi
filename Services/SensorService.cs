using MottuControlApi.Dtos.Sensor;
using MottuControlApi.Mappings;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<PagedList<SensorDto>?> GetAllAsync(PaginationParams paginationParams)
        {
            var sensores = await _sensorRepository.GetAllAsync(paginationParams);
            if (sensores == null || !sensores.Items.Any())
                return null;

            var sensoresDto = sensores.Items.Select(s => s.ToDto()).ToList();

            return new PagedList<SensorDto>(sensoresDto, sensores.TotalCount, sensores.CurrentPage, sensores.PageSize);
        }

        public async Task<SensorDto?> GetByIdAsync(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            return sensor?.ToDto();
        }

        public async Task<IEnumerable<SensorDto>> GetByTipoAsync(string tipo)
        {
            var sensores = await _sensorRepository.GetByTipoAsync(tipo);
            return sensores.Select(s => s.ToDto());
        }

        public async Task<SensorDto> CreateAsync(CreateSensorDto createDto)
        {
            var sensor = createDto.ToModel();

            await _sensorRepository.CreateAsync(sensor);
            await _sensorRepository.SaveChangesAsync();

            return sensor.ToDto();
        }

        public async Task<SensorDto?> UpdateAsync(int id, UpdateSensorDto updateDto)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return null;

            updateDto.ToModel(sensor);
            _sensorRepository.Update(sensor);
            await _sensorRepository.SaveChangesAsync();

            return sensor.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sensor = await _sensorRepository.GetByIdAsync(id);
            if (sensor == null) return false;

            _sensorRepository.Delete(sensor);
            return await _sensorRepository.SaveChangesAsync();
        }
    }
}