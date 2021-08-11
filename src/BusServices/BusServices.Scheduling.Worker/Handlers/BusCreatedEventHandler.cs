using System;
using System.Threading.Tasks;
using BusServices.Messages.Buses;
using BusServices.Scheduling.Application.Commands.V1;
using MediatR;
using NServiceBus;

namespace BusServices.Scheduling.Worker.Handlers
{
    public class BusCreatedEventHandler : IHandleMessages<BusCreated>
    {
        private readonly IMediator _mediator;

        public BusCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(BusCreated message, IMessageHandlerContext context)
        {
            var createBus = new CreateBusProjection(message.Id, message.Registration);
            await _mediator.Send(createBus);
        }
    }
}
