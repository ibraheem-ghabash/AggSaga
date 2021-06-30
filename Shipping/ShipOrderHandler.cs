﻿using ICS.Shared.Infrastructure.Messages;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    class ShipOrderHandler : IHandleCommands<ShipOrder>
    {
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();

        public Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Successfully shipped.");
            return Task.CompletedTask;
        }
    }
}
