using System;

namespace BusServices.Buses.Api.V1.Models
{
    public class BusModel
    {
        public Guid Id { get; set; }
        public string Registration { get; set; }
        public int YearBuilt { get; set; }
    }
}
