using MottuControlApi.Dtos.Status;
using MottuControlApi.Mappings;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Services
{
    public class StatusMonitoramentoService : IStatusMonitoramentoService
    {
        private readonly IStatusMonitoramentoRepository _statusRepository;

        public StatusMonitoramentoService(IStatusMonitoramentoRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<PagedList<StatusDto>> GetAllAsync(PaginationParams paginationParams)
        {
            var statusList = await _statusRepository.GetAllAsync(paginationParams);
            if (statusList == null || !statusList.Items.Any())
                return null;

            var statusDtoList = statusList.Items.Select(s => s.ToDto()).ToList();

            return new PagedList<StatusDto>(statusDtoList, statusList.TotalCount, statusList.CurrentPage, statusList.PageSize);
        }

        public async Task<StatusDto?> GetByIdAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            return status?.ToDto();
        }

        public async Task<PagedList<StatusDto>> GetByMotoIdAsync(int motoId, PaginationParams paginationParams)
        {
            var statusList = await _statusRepository.GetByMotoIdAsync(motoId, paginationParams);
            if (statusList == null || !statusList.Items.Any())
                return null;

            var statusDtoList = statusList.Items.Select(s => s.ToDto()).ToList();

            return new PagedList<StatusDto>(statusDtoList, statusList.TotalCount, statusList.CurrentPage, statusList.PageSize);
        }

        public async Task<StatusDto> CreateAsync(CreateStatusDto createDto)
        {
            var status = createDto.ToModel();

            await _statusRepository.CreateAsync(status);
            await _statusRepository.SaveChangesAsync();

            return status.ToDto();
        }

        public async Task<StatusDto?> UpdateAsync(int id, UpdateStatusDto updateDto)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null) return null;

            updateDto.ToModel(status);
            _statusRepository.Update(status);
            await _statusRepository.SaveChangesAsync();

            return status.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null) return false;

            _statusRepository.Delete(status);
            return await _statusRepository.SaveChangesAsync();
        }
    }
}