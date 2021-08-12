using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BusServices.Buses.Api.V1.Models;
using BusServices.Buses.Application.Queries.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusServices.Buses.Api.V1.Endpoints
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class GetBusEndpoint : BaseAsyncEndpoint
        .WithRequest<Guid>
        .WithResponse<BusModel>
    {
        private readonly ILogger<GetBusEndpoint> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetBusEndpoint(ILogger<GetBusEndpoint> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(BusModel), 200)]
        [ProducesResponseType(404)]
        public override async Task<ActionResult<BusModel>> HandleAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            var query = new GetBus(id);

            var busDataContract = await _mediator.Send(query, cancellationToken);

            if (busDataContract == null)
                return NotFound();

            return Ok(_mapper.Map<BusModel>(busDataContract));
        }
    }
}
