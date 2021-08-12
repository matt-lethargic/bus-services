using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusServices.Buses.Domain.Ports
{
    public interface IBusRepository
    {
        Task Save(Bus bus, CancellationToken cancellationToken);
        Task<Bus> Get(Guid id, CancellationToken cancellationToken);
    }
}
