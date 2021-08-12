using System;

namespace BusServices.Buses.Api.V1.Models
{
    public class CreateBusModel
    {
        public Guid? Id { get; set; }
        public string Registration { get; set; }
        public int YearBuilt { get; set; }
    }
}
