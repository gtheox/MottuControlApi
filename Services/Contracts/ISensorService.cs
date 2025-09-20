using MottuControlApi.Dtos.Sensor;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuControlApi.Services.Contracts
{
    public interface ISensorService
    {
        Task<PagedList<SensorDto>> GetAllAsync(PaginationParams paginationParams);
        Task<SensorDto?> GetByIdAsync(int id);
        Task<IEnumerable<SensorDto>> GetByTipoAsync(string tipo);
        Task<SensorDto> CreateAsync(CreateSensorDto createDto);
        Task<SensorDto?> UpdateAsync(int id, UpdateSensorDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}