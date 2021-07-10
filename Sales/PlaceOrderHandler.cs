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
        IHandleCommands<PlaceOrder>,
        IHandleCommands<StrtOrderSaga>,
        IHandleCommands<ShipOrder>
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
            StrtOrderSaga sagaCmd = message as StrtOrderSaga;
            var saga = context.Saga(message.OrderId, "Sales")
                .Command(sagaCmd)
                .Command(new ShipOrder{OrderId = message.OrderId });
            await saga.Start().ConfigureAwait(false);
            //var order = await context.For<OrderModel>()
            //    .New(message.OrderId)
            //    .ConfigureAwait(false);
            
            //order.Create(message.OrderId);

            // await context.Publish(orderPlaced);
        }

        public async Task Handle(StrtOrderSaga message, IMessageHandlerContext context)
        {
            log.Info($"started saga, OrderId = {message.OrderId}");
        }

        public async Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            log.Info($"Ship order-saga completed, OrderId = {message.OrderId}");
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