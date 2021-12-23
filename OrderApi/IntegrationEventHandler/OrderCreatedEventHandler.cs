using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationsEvent;

namespace OrderService.IntegrationEventHandler
{
    public class OrderCreatedEventHandler : IConsumer<OrderSubmitted>
    {
        private readonly ILogger<OrderCreatedEventHandler> _logger;
        public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<OrderSubmitted> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.OrderId);
            return Task.CompletedTask;
        }
    }
}