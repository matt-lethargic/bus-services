using System;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Buses.Domain;
using BusServices.Buses.Domain.Ports;
using BusServices.Messages.Buses;
using MediatR;

namespace BusServices.Buses.Application.Commands.V1
{
    public class CreateBusHandler : IRequestHandler<CreateBus>
    {
        private readonly IBusRepository _busRepository;
        private readonly IEventPublisher _eventPublisher;

        public CreateBusHandler(IBusRepository busRepository, IEventPublisher eventPublisher)
        {
            _busRepository = busRepository ?? throw new ArgumentNullException(nameof(busRepository));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<Unit> Handle(CreateBus request, CancellationToken cancellationToken)
        {
            var bus = Bus.Create(request.Id, request.Registration, request.YearBuilt);
            await _busRepository.Save(bus, cancellationToken);

            // this should use outbox pattern really
            var busCreated = new BusCreated(bus.Id, bus.Registration, bus.YearBuilt);
            await _eventPublisher.Publish(busCreated, cancellationToken);

            return Unit.Value;
        }
    }
}