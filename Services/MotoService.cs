using MottuControlApi.Dtos.Moto;
using MottuControlApi.Mappings;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Services.Contracts;
using MottuControlApi.Shared.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MottuControlApi.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _motoRepository;

        public MotoService(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<PagedList<MotoDto>?> GetAllAsync(PaginationParams paginationParams)
        {
            var motos = await _motoRepository.GetAllAsync(paginationParams);
            if (motos == null || !motos.Items.Any())
                return null;

            var motosDto = motos.Items.Select(m => m.ToDto()).ToList();

            return new PagedList<MotoDto>(motosDto, motos.TotalCount, motos.CurrentPage, motos.PageSize);
        }

        public async Task<MotoDto?> GetByIdAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            return moto?.ToDto();
        }

        public async Task<IEnumerable<MotoDto>> GetByPlacaAsync(string placa)
        {
            var motos = await _motoRepository.GetByPlacaAsync(placa);
            return motos.Select(m => m.ToDto());
        }

        public async Task<IEnumerable<MotoDto>> GetByStatusAsync(string status)
        {
            var motos = await _motoRepository.GetByStatusAsync(status);
            return motos.Select(m => m.ToDto());
        }

        public async Task<MotoDto> CreateAsync(CreateMotoDto createDto)
        {
            var moto = createDto.ToModel();

            await _motoRepository.CreateAsync(moto);
            await _motoRepository.SaveChangesAsync();

            return moto.ToDto();
        }

        public async Task<MotoDto?> UpdateAsync(int id, UpdateMotoDto updateDto)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return null;

            updateDto.ToModel(moto); // Aplica as alterações do DTO no modelo existente
            _motoRepository.Update(moto);
            await _motoRepository.SaveChangesAsync();

            return moto.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) return false;

            _motoRepository.Delete(moto);
            return await _motoRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateLocationAsync(int motoId, UpdateLocationDto locationDto)
        {
            var moto = await _motoRepository.GetByIdAsync(motoId);
            if (moto == null)
            {
                return false; // Moto não encontrada
            }

            moto.Latitude = locationDto.Latitude;
            moto.Longitude = locationDto.Longitude;
            moto.UltimaAtualizacaoLocalizacao = DateTime.UtcNow;

            _motoRepository.Update(moto);
            return await _motoRepository.SaveChangesAsync();
        }
    }
}