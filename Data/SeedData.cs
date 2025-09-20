using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MottuControlApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MottuControlApi.Data
{
    public static class SeedData
    {
        public static void Inicializar(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Garante que o banco de dados foi criado
            context.Database.EnsureCreated();

            // Verifica se já há dados inseridos para não duplicar
            if (context.Patios.Any())
            {
                return; // O banco de dados já foi populado
            }

            // Criação de Pátios
            var patioCentral = new Patio { Nome = "Pátio Central" };
            var patioLeste = new Patio { Nome = "Pátio Leste" };

            context.Patios.AddRange(patioCentral, patioLeste);
            context.SaveChanges(); // Salva os pátios para obter seus IDs gerados pelo banco

            // Criação de Motos
            var motos = new List<Moto>
            {
                new Moto { Modelo = "Honda Biz 110i", Placa = "ABC1D23", Status = "Disponível", PatioId = patioCentral.Id },
                new Moto { Modelo = "Yamaha Neo 125", Placa = "XYZ4G56", Status = "Alugada", PatioId = patioCentral.Id },
                new Moto { Modelo = "Honda Pop 100", Placa = "DEF7H89", Status = "Manutenção", PatioId = patioLeste.Id },
                new Moto { Modelo = "Shineray Worker 125", Placa = "GHI1J23", Status = "Disponível", PatioId = patioLeste.Id }
            };

            context.Motos.AddRange(motos);
            context.SaveChanges(); // Salva as motos para obter seus IDs

            // Criação de Sensores
            var sensores = new List<SensorIoT>
            {
                new SensorIoT { Nome = "Sensor GPS A", Tipo = "GPS", MotoId = motos[0].Id },
                new SensorIoT { Nome = "Sensor RFID B", Tipo = "RFID", MotoId = motos[1].Id },
                new SensorIoT { Nome = "Sensor GPS C", Tipo = "GPS", MotoId = motos[2].Id },
                new SensorIoT { Nome = "Sensor RFID D", Tipo = "RFID", MotoId = motos[3].Id }
            };

            context.Sensores.AddRange(sensores);

            // Histórico de Status inicial
            var statusInicial = new List<StatusMonitoramento>
            {
                new StatusMonitoramento { MotoId = motos[0].Id, Status = "Disponível", DataHora = DateTime.UtcNow.AddDays(-2) },
                new StatusMonitoramento { MotoId = motos[1].Id, Status = "Disponível", DataHora = DateTime.UtcNow.AddDays(-5) },
                new StatusMonitoramento { MotoId = motos[1].Id, Status = "Alugada", DataHora = DateTime.UtcNow.AddDays(-4) }, // Moto 2 tem um histórico
                new StatusMonitoramento { MotoId = motos[2].Id, Status = "Manutenção", DataHora = DateTime.UtcNow.AddDays(-1) },
                new StatusMonitoramento { MotoId = motos[3].Id, Status = "Disponível", DataHora = DateTime.UtcNow.AddDays(-3) }
            };

            context.StatusMonitoramentos.AddRange(statusInicial);

            context.SaveChanges(); // Salva os sensores e o histórico de status
        }
    }
}