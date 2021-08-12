using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusServices.Buses.Application.DataContracts;
using BusServices.Buses.Domain.Ports;
using MediatR;

namespace BusServices.Buses.Application.Queries.V1
{
    public class GetBusHandler : IRequestHandler<GetBus, BusDataContract>
    {
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;

        public GetBusHandler(IBusRepository busRepository, IMapper mapper)
        {
            _busRepository = busRepository ?? throw new ArgumentNullException(nameof(busRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BusDataContract> Handle(GetBus request, CancellationToken cancellationToken)
        {
            var bus = await _busRepository.Get(request.Id, cancellationToken);
            var busDataContract = _mapper.Map<BusDataContract>(bus);

            return busDataContract;
        }
    }
}