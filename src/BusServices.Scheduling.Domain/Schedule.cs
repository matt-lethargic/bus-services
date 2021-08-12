using System;
using BusServices.Scheduling.Domain.Projections;

namespace BusServices.Scheduling.Domain
{
    public class Schedule
    {
        public Guid Id { get; private set; }
        public Guid TimeTableId { get; private set; }
        public Guid BusId { get; private set; }
        public Guid DriverId { get; private set; }

        private Schedule(Guid id, Guid timeTableId, Guid busId, Guid driverId)
        {
            Id = id;
            TimeTableId = timeTableId;
            BusId = busId;
            DriverId = driverId;
        }

        public static Schedule Create(Guid id, TimeTable timeTable, Bus bus, Driver driver)
        {
            return new Schedule(id, timeTable.Id, bus.Id, driver.Id);
        }
    }
}
