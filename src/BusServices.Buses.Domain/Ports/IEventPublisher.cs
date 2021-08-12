using System.Threading;
using System.Threading.Tasks;
using BusServices.Messages;

namespace BusServices.Buses.Domain.Ports
{
    public interface IEventPublisher
    {
        Task Publish(IBusServicesEvent eventToPublish, CancellationToken cancellationToken);
    }
}
