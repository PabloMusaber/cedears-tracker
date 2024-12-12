using MovementService.Dtos;
using MovementService.Services.Interfaces;
using MovementService.SyncDataServices.Grpc;

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
                        SeedData(serviceScope.ServiceProvider.GetService<IInstrumentService>() ?? throw new ArgumentNullException("GrpcInstrument configuration is missing."), instruments);
                    }
                }
                else
                {
                    throw new ArgumentNullException("grpcClient is null.");
                }

            }
        }

        private static void SeedData(IInstrumentService instrumentService, IEnumerable<InstrumentCreateDto> instruments)
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
    }
}