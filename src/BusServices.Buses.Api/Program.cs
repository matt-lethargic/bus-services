using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using BusServices.NServiceBus;
using MongoDB.Driver;
using NServiceBus;

namespace BusServices.Buses.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
