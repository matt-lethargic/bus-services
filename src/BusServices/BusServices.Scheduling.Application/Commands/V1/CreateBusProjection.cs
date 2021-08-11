using System;
using MediatR;

namespace BusServices.Scheduling.Application.Commands.V1
{
    public class CreateBusProjection : IRequest
    {
        public Guid Id { get; }
        public string Registration { get; }

        public CreateBusProjection(Guid id, string registration)
        {
            Id = id;
            Registration = registration;
        }
    }
}
