using System;
using BusServices.Buses.Application.DataContracts;
using MediatR;

namespace BusServices.Buses.Application.Queries.V1
{
    public class GetBus : IRequest<BusDataContract>
    {
        public Guid Id { get; }

        public GetBus(Guid id)
        {
            Id = id;
        }
    }
}
