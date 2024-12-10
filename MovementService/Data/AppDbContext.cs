using Microsoft.EntityFrameworkCore;
using MovementService.Models;

namespace MovementService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Instrument> Instruments { get; set; } = null!;
        public DbSet<Movement> Movements { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Instrument>()
               .HasMany(p => p.Movements)
               .WithOne(p => p.Instrument!)
               .HasForeignKey(p => p.InstrumentId);

            modelBuilder
                .Entity<Movement>()
                .HasOne(p => p.Instrument)
                .WithMany(p => p.Movements)
                .HasForeignKey(p => p.InstrumentId);
        }
    }
}