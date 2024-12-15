using AutoMapper;
using Grpc.Core;
using MovementService.Services.Interfaces;

namespace MovementService.SyncDataServices.Grpc
{
    public class GrpcInstrumentBalanceService : GrpcInstrumentBalance.GrpcInstrumentBalanceBase
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IMapper _mapper;

        public GrpcInstrumentBalanceService(IInstrumentService instrumentService, IMapper mapper)
        {
            _instrumentService = instrumentService;
            _mapper = mapper;
        }

        public override async Task<InstrumentBalanceResponse> GetAllInstrumentsBalance(GetAllInstrumentsBalanceRequest request, ServerCallContext context)
        {
            var response = new InstrumentBalanceResponse();
            var instruments = await _instrumentService.GetAllInstrumentsBalanceAsync();

            foreach (var inst in instruments)
            {
                response.InstrumentBalance.Add(_mapper.Map<GrpcInstrumentBalanceModel>(inst));
            }

            return response;
        }
    }
}