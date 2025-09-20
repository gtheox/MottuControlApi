using Microsoft.EntityFrameworkCore;
using MottuControlApi.Models;

namespace MottuControlApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<SensorIoT> Sensores { get; set; }
        public DbSet<StatusMonitoramento> StatusMonitoramentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da tabela Patio
            modelBuilder.Entity<Patio>()
                .HasKey(p => p.Id);

            // Configuração da tabela Moto
            modelBuilder.Entity<Moto>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)
                .WithMany(p => p.Motos)
                .HasForeignKey(m => m.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da tabela SensorIoT
            modelBuilder.Entity<SensorIoT>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<SensorIoT>()
                .HasOne(s => s.Moto)
                .WithMany(m => m.Sensores)
                .HasForeignKey(s => s.MotoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da tabela StatusMonitoramento
            modelBuilder.Entity<StatusMonitoramento>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<StatusMonitoramento>()
                .HasOne(s => s.Moto)
                .WithMany(m => m.HistoricoStatus)
                .HasForeignKey(s => s.MotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}