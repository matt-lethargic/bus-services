using System;

namespace BusServices.Messages.Buses
{
    public class BusCreated : IBusServicesEvent
    {
        public Guid Id { get; private set; }
        public string Registration { get; private set; }
        public int YearBuilt { get; private set; }

        public BusCreated(Guid id, string registration, int yearBuilt)
        {
            Id = id;
            Registration = registration;
            YearBuilt = yearBuilt;
        }
    }
}
