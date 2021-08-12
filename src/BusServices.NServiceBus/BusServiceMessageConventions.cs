using System;
using NServiceBus;

namespace BusServices.NServiceBus
{
    public class BusServiceMessageConventions : IMessageConvention
    {
        public bool IsMessageType(Type type) => false;
        public bool IsCommandType(Type type) => false;
        public bool IsEventType(Type type) => type.Namespace.StartsWith("BusServices.Messages");
        public string Name => "BusServices Message Convention";
    }
}
