using ICS.Shared.Endpoint.Console;
using ICS.Shared.Messages.Core;
using NServiceBus.Logging;
using StructureMap;
using System;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();
        static void Main(string[] args)
        {
            Console.Title = "ClientUI";

            string endpointName = "ClientUI";

            var endpointConfig = ConsoleEndpoint.NewInstance(endpointName)
                                                    .UsePersistent();

            var startedEndpoint = endpointConfig.Build().Start();

            Console.WriteLine("Press CTRL+C to exit...");

            startedEndpoint.Stop();
        }
    }
}
