using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BusServices.Buses.Api.V1.Models;
using BusServices.Buses.Application.Commands.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusServices.Buses.Api.V1.Endpoints
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CreateBusEndpoint : BaseAsyncEndpoint
        .WithRequest<CreateBusModel>
        .WithoutResponse
    {
        private readonly ILogger<CreateBusEndpoint> _logger;
        private readonly IMediator _mediator;

        public CreateBusEndpoint(ILogger<CreateBusEndpoint> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public override async Task<ActionResult> HandleAsync(CreateBusModel request, CancellationToken cancellationToken = new CancellationToken())
        {
            request.Id ??= Guid.NewGuid();

            var command = new CreateBus(request.Id.Value, request.Registration, request.YearBuilt);

            await _mediator.Send(command, cancellationToken);

            return Created(request.Id.Value.ToString(), request);
        }
    }
}
