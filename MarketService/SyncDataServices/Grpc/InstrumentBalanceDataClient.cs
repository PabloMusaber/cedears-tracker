using AutoMapper;
using Grpc.Net.Client;
using MarketService.Dtos;

namespace MarketService.SyncDataServices.Grpc
{
    public class InstrumentBalanceDataClient : IInstrumentBalanceDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public InstrumentBalanceDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<InstrumentBalanceCreateDto>? ReturnAllInstrumentsBalance()
        {
            Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcInstrumentBalance"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcInstrumentBalance"] ?? throw new ArgumentNullException("GrpcInstrumentBalance configuration is missing."));
            var client = new GrpcInstrumentBalance.GrpcInstrumentBalanceClient(channel);
            var request = new GetAllInstrumentsBalanceRequest();

            try
            {
                var reply = client.GetAllInstrumentsBalance(request);
                return _mapper.Map<IEnumerable<InstrumentBalanceCreateDto>>(reply.InstrumentBalance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call gRPC Server {ex.Message}");
                return null;
            }
        }
    }
}