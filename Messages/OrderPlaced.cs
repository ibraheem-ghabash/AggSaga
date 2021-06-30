using ICS.Shared.Messages.Core;
using NServiceBus;

namespace Messages
{
    public class OrderPlaced :
        BaseEvent
    {
        public string OrderId { get; set; }
    }
}