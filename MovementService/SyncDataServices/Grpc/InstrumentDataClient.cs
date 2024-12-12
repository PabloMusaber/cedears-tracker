using AutoMapper;
using MovementService.Models;
using Grpc.Net.Client;
using MovementService.Dtos;

namespace MovementService.SyncDataServices.Grpc
{
    public class InstrumentDataClient : IInstrumentDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public InstrumentDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<InstrumentCreateDto>? ReturnAllInstruments()
        {
            Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcInstrument"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcInstrument"] ?? throw new ArgumentNullException("GrpcInstrument configuration is missing."));
            var client = new GrpcInstrument.GrpcInstrumentClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllInstruments(request);
                return _mapper.Map<IEnumerable<InstrumentCreateDto>>(reply.Instrument);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call gRPC Server {ex.Message}");
                return null;
            }
        }
    }
}