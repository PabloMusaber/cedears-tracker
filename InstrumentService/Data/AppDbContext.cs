using InstrumentService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Instrument> Instruments { get; set; } = null!;
    }
}