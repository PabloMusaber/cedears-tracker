using MarketService.Dtos;
using MarketService.Services.Interfaces;
using MarketService.SyncDataServices.Grpc;

namespace MarketService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IInstrumentBalanceDataClient>();

                if (grpcClient != null)
                {
                    var instruments = grpcClient.ReturnAllInstrumentsBalance();

                    if (instruments != null)
                    {
                        SeedData(serviceScope.ServiceProvider.GetService<IInstrumentBalanceService>() ??
                            throw new ArgumentNullException("GrpcInstrument configuration is missing."), instruments);
                    }
                }
                else
                {
                    throw new ArgumentNullException("grpcClient is null.");
                }

            }
        }

        private static void SeedData(IInstrumentBalanceService instrumentBalanceService, IEnumerable<InstrumentBalanceCreateDto> instruments)
        {
            Console.WriteLine("--> Seeding new instruments...");

            foreach (var inst in instruments)
            {
                if (!instrumentBalanceService.ExternalInstrumentExists(inst.ExternalId))
                {
                    instrumentBalanceService.CreateInstrumentBalance(inst);
                }
            }
        }
    }
}