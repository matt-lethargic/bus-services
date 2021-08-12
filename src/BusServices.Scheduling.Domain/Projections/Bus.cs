using System;

namespace BusServices.Scheduling.Domain.Projections
{
    public class Bus : IProjection<Guid>
    {
        public Guid Id { get; private set; }
        public string Registration { get; private set; }

        private Bus(Guid id, string registration)
        {
            Id = id;
            Registration = registration;
        }

        public static Bus Create(Guid id, string registration)
        {
            return new Bus(id, registration);
        }
    }
}