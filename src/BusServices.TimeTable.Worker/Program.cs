using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace BusServices.TimeTable.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration(context.Configuration.GetValue<string>("EndpointName"));
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString(context.Configuration.GetValue<string>("EndpointConnectionString"));
                    
                    // endpointConfiguration.SendFailedMessagesTo("error");
                    // endpointConfiguration.AuditProcessedMessagesTo("audit");

                    return endpointConfiguration;
                });
        }
    }
}
