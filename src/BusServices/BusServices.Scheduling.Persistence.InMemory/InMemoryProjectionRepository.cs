using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Scheduling.Domain.Ports;
using BusServices.Scheduling.Domain.Projections;

namespace BusServices.Scheduling.Persistence.InMemory
{
    public class InMemoryProjectionRepository : IProjectionRepository
    {
        private static ConcurrentDictionary<Guid, IProjection<Guid>> _projections;
        private static readonly object ProjectionsLock = new object();

        public InMemoryProjectionRepository()
        {
            if (_projections == null)
            {
                lock (ProjectionsLock)
                {
                    if (_projections == null) _projections = new ConcurrentDictionary<Guid, IProjection<Guid>>();
                }
            }
        }

        public Task Save<T>(T projection, CancellationToken cancellationToken)
            where T : class, IProjection<Guid>
        {
            _projections.AddOrUpdate(projection.Id, projection, (guid, existing) =>
            {
                if (projection.Id != existing.Id)
                    throw new Exception("Updating different Bus");

                return projection;
            });

            return Task.CompletedTask;
        }

        public Task<T> Get<T>(Guid id, CancellationToken cancellationToken)
            where T : class, IProjection<Guid>
        {
            var proj = _projections[id];
            if (proj == null)
                return Task.FromResult(null as T);

            return Task.FromResult(proj as T);
        }
    }
}
