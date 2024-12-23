using MovementService.Dtos;
using MovementService.Models;
using MovementService.Services.Interfaces;
using MovementService.SyncDataServices.Grpc;
using static MovementService.Enumerations.Enumerations;

namespace MovementService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IInstrumentDataClient>();

                if (grpcClient != null)
                {
                    var instruments = grpcClient.ReturnAllInstruments();

                    if (instruments != null)
                    {
                        SeedInstrumentsData(serviceScope.ServiceProvider.GetService<IInstrumentService>() ?? throw new ArgumentNullException("GrpcInstrument configuration is missing."), instruments);

                        var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                        if (dbContext != null)
                        {
                            SeedMovementsData(dbContext);
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("grpcClient is null.");
                }

            }
        }

        private static void SeedInstrumentsData(IInstrumentService instrumentService, IEnumerable<InstrumentCreateDto> instruments)
        {
            Console.WriteLine("--> Seeding new instruments...");

            foreach (var inst in instruments)
            {
                if (!instrumentService.ExternalInstrumentExists(inst.ExternalId))
                {
                    instrumentService.CreateInstrument(inst);
                }
            }
        }

        private static void SeedMovementsData(AppDbContext context)
        {
            Console.WriteLine("--> Seeding movements...");
            var instruments = context.Instruments.ToList();

            if (!instruments.Any())
            {
                Console.WriteLine("--> No instruments found. Skipping movements seeding.");
                return;
            }

            var random = new Random();
            foreach (var instrument in instruments)
            {
                if (!context.Movements.Any(m => m.InstrumentId == instrument.Id))
                {
                    context.Movements.AddRange(
                        new Movement
                        {
                            InstrumentId = instrument.Id,
                            MovementType = (char)MovementType.Buy,
                            Quantity = random.Next(1, 30),
                            Price = random.Next(1000, 25000)
                        },
                        new Movement
                        {
                            InstrumentId = instrument.Id,
                            MovementType = (char)MovementType.Buy,
                            Quantity = random.Next(1, 30),
                            Price = random.Next(1000, 25000)
                        }
                    );
                }
            }

            context.SaveChanges();
            Console.WriteLine("--> Movements seeding completed.");
        }
    }
}