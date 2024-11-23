using CEDEARsTracker.Models;
using Microsoft.EntityFrameworkCore;
using static CEDEARsTracker.Enumerations.Enumerations;

namespace CEDEARsTracker.Infraestructure;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (dbContext != null)
            {
                SeedData(dbContext, isProd);
            }
            else
            {
                throw new InvalidOperationException("Failed to get AppDbContext from the service provider.");
            }
        }
    }

    private static void SeedData(AppDbContext context, bool isProd)
    {
        if (isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }

        if (context.Instruments != null && !context.Instruments.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Instruments.AddRange(
                new Instrument()
                {
                    Id = Guid.Parse("ee8131f7-e019-4dd6-b0b6-295d6ab7f921"),
                    Ticker = "KO",
                    Description = "The Coca Cola Company",
                    AveragePurchasePrice = 0,
                    InstrumentType = InstrumentType.CEDEARs
                },
                new Instrument()
                {
                    Id = Guid.Parse("172d0456-784d-4af5-8a96-71fa9a00b512"),
                    Ticker = "MSFT",
                    Description = "Microsoft",
                    AveragePurchasePrice = 0,
                    InstrumentType = InstrumentType.CEDEARs
                },
                new Instrument()
                {
                    Id = Guid.Parse("fddb970b-2493-462d-8604-2f09757312e3"),
                    Ticker = "GOOGL",
                    Description = "Alphabet",
                    AveragePurchasePrice = 0,
                    InstrumentType = InstrumentType.CEDEARs
                },
                new Instrument()
                {
                    Id = Guid.Parse("c6b07770-5945-4521-92d6-04875d1de2a7"),
                    Ticker = "AMZN",
                    Description = "Amazon",
                    AveragePurchasePrice = 0,
                    InstrumentType = InstrumentType.CEDEARs
                },
                new Instrument()
                {
                    Id = Guid.Parse("4a5a379b-b785-408d-bba0-e6eeafa12759"),
                    Ticker = "AAPL",
                    Description = "Apple",
                    AveragePurchasePrice = 0,
                    InstrumentType = InstrumentType.CEDEARs
                }
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }

    }
}