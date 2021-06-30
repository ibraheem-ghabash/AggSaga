using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

#pragma warning disable 162

namespace Sales
{
    using Aggregates;
    using ICS.Shared.Infrastructure.Messages;
    using System;

    public class PlaceOrderHandler :
        IHandleCommands<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        static Random random = new Random();

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            // This is normally where some business logic would occur

            // Uncomment to test throwing a systemic exception
            //throw new Exception("BOOM");

            // Uncomment to test throwing a transient exception
            //if (random.Next(0, 5) == 0)
            //{
            //    throw new Exception("Oops");
            //}

            // var orderPlaced = new OrderPlaced
            // {
            //     OrderId = message.OrderId
            // };
            var order = await context.For<OrderModel>()
                .New(message.OrderId)
                .ConfigureAwait(false);
            
            order.Create(message.OrderId);

            // await context.Publish(orderPlaced);
        }
    }

    public class OrderModel : Entity<OrderModel, OrderModelState>
    {
        private OrderModel()
        {

        }

        public void Create(string orderId)
        {
            Apply<OrderPlaced>(x =>
            {
                x.OrderId = orderId;
            });
        }
    }


    public class OrderModelState : State<OrderModelState>
    {
        public string Id { get; set; }
        private void Handle(OrderPlaced e)
        {
            this.Id = e.OrderId;
        }
    }
}