using ICS.Shared.Endpoint.Console;
using ICS.Shared.Endpoint.EventStore;
using System;

namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sales";

            string endpointName = "Sales";

            var endpointConfig = ConsoleEndpoint.NewInstance(endpointName)
                                                    .AddEventStore(endpointName)
                                                    .UsePersistent();

            var startedEndpoint = endpointConfig.Build().Start();

            Console.WriteLine("Press CTRL+C to exit...");

            Console.ReadLine();

            startedEndpoint.Stop();
        }
    }
}
