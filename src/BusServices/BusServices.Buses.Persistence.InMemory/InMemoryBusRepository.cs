using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Buses.Domain;
using BusServices.Buses.Domain.Ports;

namespace BusServices.Buses.Persistence.InMemory
{
    public class InMemoryBusRepository : IBusRepository
    {
        private static ConcurrentDictionary<Guid, Bus> _buses;
        private static readonly object BusCreationLock = new object();

        public InMemoryBusRepository()
        {
            if (_buses == null)
            {
                lock (BusCreationLock)
                {
                    if (_buses == null)
                    {
                        _buses = new ConcurrentDictionary<Guid, Bus>();
                    }
                }
            }
        }

        public Task Save(Bus bus, CancellationToken cancellationToken)
        {
            _buses.AddOrUpdate(bus.Id, bus, (id, existingBus) =>
            {
                if (bus.Id != existingBus.Id)
                    throw new Exception("Updating different Bus");

                return bus;
            });

            return Task.CompletedTask;
        }

        public Task<Bus> Get(Guid id, CancellationToken cancellationToken)
        {
            if (_buses.ContainsKey(id))
            {
                return Task.FromResult(_buses[id]);
            }

            return Task.FromResult(null as Bus);
        }
    }
}
