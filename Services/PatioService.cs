using MottuControlApi.Dtos.Patio;
using MottuControlApi.Mappings;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Services
{
    public class PatioService : IPatioService
    {
        private readonly IPatioRepository _patioRepository;

        public PatioService(IPatioRepository patioRepository)
        {
            _patioRepository = patioRepository;
        }

        public async Task<PagedList<PatioDto>?> GetAllAsync(PaginationParams paginationParams)
        {
            var patios = await _patioRepository.GetAllAsync(paginationParams);
            if (patios == null || !patios.Items.Any())
                return null;

            var patiosDto = patios.Items.Select(p => p.ToDto()).ToList();

            return new PagedList<PatioDto>(patiosDto, patios.TotalCount, patios.CurrentPage, patios.PageSize);
        }

        public async Task<PatioDto?> GetByIdAsync(int id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            return patio?.ToDto();
        }

        public async Task<IEnumerable<PatioDto>> GetByNomeAsync(string nome)
        {
            var patios = await _patioRepository.GetByNomeAsync(nome);
            return patios.Select(p => p.ToDto());
        }

        public async Task<PatioDto> CreateAsync(CreatePatioDto createDto)
        {
            var patio = createDto.ToModel();

            await _patioRepository.CreateAsync(patio);
            await _patioRepository.SaveChangesAsync();

            return patio.ToDto();
        }

        public async Task<PatioDto?> UpdateAsync(int id, UpdatePatioDto updateDto)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return null;

            updateDto.ToModel(patio);
            _patioRepository.Update(patio);
            await _patioRepository.SaveChangesAsync();

            return patio.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patio = await _patioRepository.GetByIdAsync(id);
            if (patio == null) return false;

            _patioRepository.Delete(patio);
            return await _patioRepository.SaveChangesAsync();
        }
    }
}