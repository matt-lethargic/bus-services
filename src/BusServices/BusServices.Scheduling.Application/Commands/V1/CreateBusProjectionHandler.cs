using System;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Scheduling.Domain.Ports;
using BusServices.Scheduling.Domain.Projections;
using MediatR;

namespace BusServices.Scheduling.Application.Commands.V1
{
    public class CreateBusProjectionHandler : IRequestHandler<CreateBusProjection>
    {
        private readonly IProjectionRepository _repository;

        public CreateBusProjectionHandler(IProjectionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(CreateBusProjection request, CancellationToken cancellationToken)
        {
            var bus = Bus.Create(request.Id, request.Registration);

            await _repository.Save(bus, cancellationToken);

            return Unit.Value;
        }
    }
}