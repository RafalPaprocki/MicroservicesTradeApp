using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationsEvent;

namespace TradingStatisticAPI.IntegrationEventHandler
{
    public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEvent> _logger;

        public OrderCreatedEventHandler(ILogger<OrderCreatedEvent> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            _logger.LogInformation($"Statistic service received order with id {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}