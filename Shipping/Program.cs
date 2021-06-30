using ICS.Shared.Endpoint.Console;
using System;

namespace Shipping
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Shipping";

            string endpointName = "Shipping";

            var endpointConfig = ConsoleEndpoint.NewInstance(endpointName).NoEventStore()
                                                    .UsePersistent();

            var startedEndpoint = endpointConfig.Build().Start();

            Console.WriteLine("Press CTRL+C to exit...");

            Console.ReadLine();

            startedEndpoint.Stop();
        }
    }
}
