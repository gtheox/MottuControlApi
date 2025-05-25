using MottuControlApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MottuControlApi.Data
{
    public static class SeedData
    {
        public static void Inicializar(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Patios.Any()) return; // Se já tem dados, não insere novamente

            // Criar Pátio
            var patio = new Patio { Nome = "Pátio Central" };
            context.Patios.Add(patio);
            context.SaveChanges();

            // Criar Motos
            var moto1 = new Moto { Modelo = "Honda Biz", Placa = "ABC1234", Status = "Disponível", PatioId = patio.Id };
            var moto2 = new Moto { Modelo = "Yamaha Neo", Placa = "XYZ5678", Status = "Alugada", PatioId = patio.Id };

            context.Motos.AddRange(moto1, moto2);
            context.SaveChanges();

            // Criar Sensores
            var sensor1 = new SensorIoT { Nome = "Sensor GPS", Tipo = "GPS", MotoId = moto1.Id };
            var sensor2 = new SensorIoT { Nome = "Sensor RFID", Tipo = "RFID", MotoId = moto2.Id };

            context.Sensores.AddRange(sensor1, sensor2);

            // Criar Status
            var status1 = new StatusMonitoramento { MotoId = moto1.Id, Status = "Disponível", DataHora = DateTime.UtcNow };
            var status2 = new StatusMonitoramento { MotoId = moto2.Id, Status = "Alugada", DataHora = DateTime.UtcNow };

            context.StatusMonitoramentos.AddRange(status1, status2);

            context.SaveChanges();
        }
    }
}
