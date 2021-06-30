using ICS.Shared.Messages.Core;
using NServiceBus;

namespace Messages
{
    [CommandTo("Shipping")]
    public class ShipOrder :
        BaseCommand
    {
        public string OrderId { get; set; }
    }
}
