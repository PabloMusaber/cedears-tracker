using CEDEARsTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CEDEARsTracker.Infraestructure
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Instrument>? Instruments { get; set; } //This indicates how to map the internal data with our database.
        public DbSet<Movement>? Movements { get; set; }

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

