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
                    // Define the endpoint name
                    var endpointConfiguration = new EndpointConfiguration(context.Configuration.GetValue<string>("EndpointName"));

                    // Select the learning (filesystem-based) transport to communicate
                    // with other endpoints
                    endpointConfiguration.UseTransport<LearningTransport>();

                    // Enable monitoring errors, auditing, and heartbeats with the
                    // Particular Service Platform tools
                    endpointConfiguration.SendFailedMessagesTo("error");
                    endpointConfiguration.AuditProcessedMessagesTo("audit");
                    endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");

                    // Enable monitoring endpoint performance
                    var metrics = endpointConfiguration.EnableMetrics();
                    metrics.SendMetricDataToServiceControl("Particular.Monitoring",
                        TimeSpan.FromMilliseconds(500));

                    // Return the completed configuration
                    return endpointConfiguration;
                });
        }
    }
}
