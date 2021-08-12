using System;
using System.Collections.Generic;

namespace BusServices.Messages.TimeTable
{
    public interface ITimeTableUpdated
    {
        public DateTime TimeStamp { get; }
        public Guid Id { get; }
        public string Name { get; }
        public IDictionary<Guid, DateTime> Stops { get; }
    }
}