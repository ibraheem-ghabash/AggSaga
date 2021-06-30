using ICS.Shared.Infrastructure.Extensions;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMessageSession bus;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMessageSession bus)
        {
            _logger = logger;
            this.bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString()
            };

            // Send the command
            _logger.LogInformation($"Sending PlaceOrder command, OrderId = {command.OrderId}");

            await bus.CommandToDomain(command).ConfigureAwait(false);

            return Ok(command.OrderId);
        }
    }
}
