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
        public DbSet<ImagemPatio> ImagensPatio { get; set; }
        public DbSet<StatusMonitoramento> StatusMonitoramentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabela: Patio
            modelBuilder.Entity<Patio>()
                .HasKey(p => p.Id);

            // Tabela: Moto
            modelBuilder.Entity<Moto>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)
                .WithMany(p => p.Motos)
                .HasForeignKey(m => m.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabela: SensorIoT
            modelBuilder.Entity<SensorIoT>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<SensorIoT>()
                .HasOne(s => s.Moto)
                .WithMany(m => m.Sensores)
                .HasForeignKey(s => s.MotoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabela: ImagemPatio
            modelBuilder.Entity<ImagemPatio>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<ImagemPatio>()
                .HasOne(i => i.Patio)
                .WithMany(p => p.Imagens)
                .HasForeignKey(i => i.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tabela: StatusMonitoramento
            modelBuilder.Entity<StatusMonitoramento>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<StatusMonitoramento>()
                .HasOne(s => s.Moto)
                .WithMany(m => m.Status)
                .HasForeignKey(s => s.MotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
