using ICS.Shared.Messages.Core;
using NServiceBus;

namespace Messages
{
    [CommandTo("Sales")]
    public class PlaceOrder :
        BaseCommand
    {
        public string OrderId { get; set; }
    }

    public class StrtOrderSaga : PlaceOrder { }
}