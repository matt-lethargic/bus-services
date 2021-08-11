using System;
using BusServices.NServiceBus;
using BusServices.Scheduling.Application.Commands.V1;
using BusServices.Scheduling.Domain.Ports;
using BusServices.Scheduling.Persistence.InMemory;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace BusServices.Scheduling.Worker
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddMediatR(typeof(CreateBusProjectionHandler).Assembly);
                    services.AddTransient<IProjectionRepository, InMemoryProjectionRepository>();
                })
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration(context.Configuration.GetValue<string>("EndpointName"));
                    endpointConfiguration.UseTransport<LearningTransport>();
                    
                    endpointConfiguration.SendFailedMessagesTo("error");
                    endpointConfiguration.AuditProcessedMessagesTo("audit");
                    endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
                    
                    var metrics = endpointConfiguration.EnableMetrics();
                    metrics.SendMetricDataToServiceControl("Particular.Monitoring",
                        TimeSpan.FromMilliseconds(500));

                    endpointConfiguration.Conventions().Add(new BusServiceMessageConventions());

                    return endpointConfiguration;
                });
        }
    }
}
