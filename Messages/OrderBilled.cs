using ICS.Shared.Messages.Core;

namespace Messages
{
    public class OrderBilled :
        BaseEvent
    {
        public string OrderId { get; set; }
    }
}