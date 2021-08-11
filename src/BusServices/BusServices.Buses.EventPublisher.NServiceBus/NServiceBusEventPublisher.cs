using System;
using System.Threading;
using System.Threading.Tasks;
using BusServices.Buses.Domain.Ports;
using BusServices.Messages;
using NServiceBus;

namespace BusServices.Buses.EventPublisher.NServiceBus
{
    public class NServiceBusEventPublisher : IEventPublisher
    {
        private readonly IMessageSession _endpointInstance;

        public NServiceBusEventPublisher(IMessageSession messageSession)
        {
            _endpointInstance = messageSession ?? throw new ArgumentNullException(nameof(messageSession));
        }

        public Task Publish(IBusServicesEvent eventToPublish, CancellationToken cancellationToken)
        {
            return _endpointInstance.Publish(eventToPublish);
        }
    }
}
