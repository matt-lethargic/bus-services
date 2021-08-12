using System;

namespace BusServices.Buses.Application.DataContracts
{
    public class BusDataContract
    {
        public Guid Id { get; private set; }
        public string Registration { get; private set; }
        public int YearBuilt { get; private set; }
    }
}
