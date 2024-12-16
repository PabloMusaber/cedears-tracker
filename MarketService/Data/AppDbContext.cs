using Microsoft.EntityFrameworkCore;
using MarketService.Models;

namespace MarketService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<InstrumentBalance> InstrumentsBalance { get; set; } = null!;

    }
}