using System;
using BusServices.NServiceBus;
using BusServices.Scheduling.Application.Commands.V1;
using BusServices.Scheduling.Domain.Ports;
using BusServices.Scheduling.Persistence.InMemory;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
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
                    endpointConfiguration.EnableInstallers();
                    var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
                    persistence.MongoClient(new MongoClient(context.Configuration.GetValue<string>("MongoConnectionString")));
                    persistence.DatabaseName(context.Configuration.GetValue<string>("MongoDatabase"));
                    persistence.UseTransactions(false);

                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString(context.Configuration.GetValue<string>("EndpointConnectionString"));
                    
                    endpointConfiguration.Conventions().Add(new BusServiceMessageConventions());

                    return endpointConfiguration;
                });
        }
    }
}
