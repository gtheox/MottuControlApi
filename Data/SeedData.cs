using System;
using System.Collections.Generic;
using MottuControlApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MottuControlApi.Data
{
    public static class SeedData
    {
        public static void Inicializar(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Verifica se já há dados inseridos
            if (context.Patios.Count() > 0) return;

            // Criação do pátio
            var patio = new Patio { Nome = "Pátio Central" };
            context.Patios.Add(patio);
            context.SaveChanges();

            // Criação de motos associadas ao pátio
            var motos = new List<Moto>
            {
                new Moto { Modelo = "Honda Biz 110i", Placa = "ABC1D23", Status = "Disponível", PatioId = patio.Id },
                new Moto { Modelo = "Yamaha Neo 125", Placa = "XYZ4G56", Status = "Alugada", PatioId = patio.Id },
                new Moto { Modelo = "Honda Pop 100", Placa = "DEF7H89", Status = "Manutenção", PatioId = patio.Id },
                new Moto { Modelo = "Shineray Worker 125", Placa = "GHI1J23", Status = "Disponível", PatioId = patio.Id },
                new Moto { Modelo = "Suzuki Yes 125", Placa = "KLM4N56", Status = "Alugada", PatioId = patio.Id }
            };

            context.Motos.AddRange(motos);
            context.SaveChanges();

            // Criação de sensores (um para cada moto)
            var sensores = new List<SensorIoT>
            {
                new SensorIoT { Nome = "Sensor GPS A", Tipo = "GPS", MotoId = motos[0].Id },
                new SensorIoT { Nome = "Sensor RFID B", Tipo = "RFID", MotoId = motos[1].Id },
                new SensorIoT { Nome = "Sensor GPS C", Tipo = "GPS", MotoId = motos[2].Id },
                new SensorIoT { Nome = "Sensor RFID D", Tipo = "RFID", MotoId = motos[3].Id },
                new SensorIoT { Nome = "Sensor GPS E", Tipo = "GPS", MotoId = motos[4].Id }
            };

            context.Sensores.AddRange(sensores);
            context.SaveChanges();

            // Histórico de status
            var status = new List<StatusMonitoramento>
            {
                new StatusMonitoramento { MotoId = motos[0].Id, Status = "Disponível", DataHora = DateTime.UtcNow },
                new StatusMonitoramento { MotoId = motos[1].Id, Status = "Alugada", DataHora = DateTime.UtcNow },
                new StatusMonitoramento { MotoId = motos[2].Id, Status = "Manutenção", DataHora = DateTime.UtcNow },
                new StatusMonitoramento { MotoId = motos[3].Id, Status = "Disponível", DataHora = DateTime.UtcNow },
                new StatusMonitoramento { MotoId = motos[4].Id, Status = "Alugada", DataHora = DateTime.UtcNow }
            };

            context.StatusMonitoramentos.AddRange(status);
            context.SaveChanges();
        }
    }
}
