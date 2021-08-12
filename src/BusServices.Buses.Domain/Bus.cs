using System;

namespace BusServices.Buses.Domain
{
    public class Bus 
    {
        public Guid Id { get; }
        public string Registration { get; }
        public int YearBuilt { get; }


        private Bus(Guid id, string registration, int yearBuilt)
        {
            Id = id;
            Registration = registration;
            YearBuilt = yearBuilt;
        }

        public static Bus Create(Guid id, string registration, int yearBuilt)
        {
            return new Bus(id, registration, yearBuilt);
        }
    }
}
