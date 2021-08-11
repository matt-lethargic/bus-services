using System;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Scheduling.Domain.Projections;

namespace BusServices.Scheduling.Domain.Ports
{
    public interface IProjectionRepository
    {
        Task Save<T>(T projection, CancellationToken cancellationToken)
            where T : class, IProjection<Guid>;

        Task<T> Get<T>(Guid id, CancellationToken cancellationToken)
            where T : class, IProjection<Guid>;
    }
}
