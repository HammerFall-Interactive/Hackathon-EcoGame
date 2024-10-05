using HammerFallInteractive.EcoGame.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace HammerFallInteractive.EcoGame.Server
{
    public class ApplicationDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Planet> Planets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("postgresql://app:gDwVjjVnLKSQ@localhost:5432/ecogame");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Database.EnsureCreatedAsync().Wait();

            base.OnModelCreating(modelBuilder);
        }
    }
}
