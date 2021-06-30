using ICS.Shared.Endpoint.Console;
using System;

namespace Billing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Billing";

            string endpointName = "Billing";

            var endpointConfig = ConsoleEndpoint.NewInstance(endpointName)
                                                    .UsePersistent()
                                                    .NoEventStore();

            var startedEndpoint = endpointConfig.Build().Start();

            Console.WriteLine("Press CTRL+C to exit...");

            Console.ReadLine();

            startedEndpoint.Stop();
        }
    }
}
