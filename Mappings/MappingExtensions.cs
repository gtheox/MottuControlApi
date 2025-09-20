using MottuControlApi.Dtos.Moto;
using MottuControlApi.Dtos.Patio;
using MottuControlApi.Dtos.Sensor;
using MottuControlApi.Dtos.Status;
using MottuControlApi.Models;
using System.Linq;

namespace MottuControlApi.Mappings
{
    public static class MappingExtensions
    {
        // ####################
        // ## MOTO MAPPINGS ##
        // ####################

        public static MotoDto ToDto(this Moto moto)
        {
            return new MotoDto
            {
                Id = moto.Id,
                Modelo = moto.Modelo,
                Placa = moto.Placa,
                Status = moto.Status,
                PatioId = moto.PatioId,
                NomePatio = moto.Patio?.Nome,
                Sensores = moto.Sensores?.Select(s => s.ToDto()).ToList(),
                StatusMonitoramentos = moto.HistoricoStatus?.Select(s => s.ToDto()).ToList()
            };
        }

        public static Moto ToModel(this CreateMotoDto dto)
        {
            return new Moto
            {
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                Status = dto.Status,
                PatioId = dto.PatioId
            };
        }

        public static void ToModel(this UpdateMotoDto dto, Moto moto)
        {
            moto.Modelo = dto.Modelo;
            moto.Status = dto.Status;
            moto.PatioId = dto.PatioId;
        }

        // ####################
        // ## PATIO MAPPINGS ##
        // ####################

        public static PatioDto ToDto(this Patio patio)
        {
            return new PatioDto
            {
                Id = patio.Id,
                Nome = patio.Nome,
                Motos = patio.Motos?.Select(m => m.ToDto()).ToList()
            };
        }

        public static Patio ToModel(this CreatePatioDto dto)
        {
            return new Patio { Nome = dto.Nome };
        }

        public static void ToModel(this UpdatePatioDto dto, Patio patio)
        {
            patio.Nome = dto.Nome;
        }

        // ####################
        // ## SENSOR MAPPINGS ##
        // ####################

        public static SensorDto ToDto(this SensorIoT sensor)
        {
            return new SensorDto
            {
                Id = sensor.Id,
                Nome = sensor.Nome,
                Tipo = sensor.Tipo,
                MotoId = sensor.MotoId,
                PlacaMoto = sensor.Moto?.Placa
            };
        }

        public static SensorIoT ToModel(this CreateSensorDto dto)
        {
            return new SensorIoT
            {
                Nome = dto.Nome,
                Tipo = dto.Tipo,
                MotoId = dto.MotoId
            };
        }

        public static void ToModel(this UpdateSensorDto dto, SensorIoT sensor)
        {
            sensor.Nome = dto.Nome;
            sensor.Tipo = dto.Tipo;
            sensor.MotoId = dto.MotoId;
        }

        // ####################################
        // ## STATUS MONITORAMENTO MAPPINGS ##
        // ####################################

        public static StatusDto ToDto(this StatusMonitoramento status)
        {
            return new StatusDto
            {
                Id = status.Id,
                Status = status.Status,
                DataHora = status.DataHora,
                MotoId = status.MotoId,
                PlacaMoto = status.Moto?.Placa
            };
        }

        public static StatusMonitoramento ToModel(this CreateStatusDto dto)
        {
            return new StatusMonitoramento
            {
                Status = dto.Status,
                MotoId = dto.MotoId,
                DataHora = System.DateTime.UtcNow // Definido pelo servidor
            };
        }

        public static void ToModel(this UpdateStatusDto dto, StatusMonitoramento status)
        {
            status.Status = dto.Status;
        }
    }
}