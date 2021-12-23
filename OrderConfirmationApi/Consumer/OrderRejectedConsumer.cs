using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationsEvent;

namespace OrderConfirmationApi.Consumer
{
    public class OrderRejectedConsumer : IConsumer<OrderRejected>
    {
        private readonly OrderConfirmationContext _context;
        private readonly ILogger<OrderAcceptedConsumer> _logger;
        
        public OrderRejectedConsumer(OrderConfirmationContext context, ILogger<OrderAcceptedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderRejected> context)
        {
            try
            {
                var order = _context.Orders.First(x => x.Id == context.Message.OrderId);
                order.Status = context.Message.Status;
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInformation($"Not found order with id {context.Message.OrderId}", ex);
            }   
        }
    }
}