using AutoMapper;
using Grpc.Core;
using InstrumentService.Services.Interfaces;

namespace InstrumentService.SyncDataServices.Grpc
{
    public class GrpcInstrumentService : GrpcInstrument.GrpcInstrumentBase
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IMapper _mapper;

        public GrpcInstrumentService(IInstrumentService instrumentService, IMapper mapper)
        {
            _instrumentService = instrumentService;
            _mapper = mapper;
        }

        public override Task<InstrumentResponse> GetAllInstruments(GetAllRequest request, ServerCallContext context)
        {
            var response = new InstrumentResponse();
            var instruments = _instrumentService.GetAllInstruments();

            foreach (var inst in instruments)
            {
                response.Instrument.Add(_mapper.Map<GrpcInstrumentModel>(inst));
            }

            return Task.FromResult(response);
        }
    }
}