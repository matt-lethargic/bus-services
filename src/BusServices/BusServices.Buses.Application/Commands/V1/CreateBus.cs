using System;
using MediatR;

namespace BusServices.Buses.Application.Commands.V1
{
    public class CreateBus : IRequest
    {
        public Guid Id { get; }
        public string Registration { get; }
        public int YearBuilt { get; }

        public CreateBus(Guid id, string registration, int yearBuilt)
        {
            Id = id;
            Registration = registration;
            YearBuilt = yearBuilt;
        }
    }
}
