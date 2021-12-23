using System.Threading.Tasks;
using MassTransit;
using OrderConfirmationApi.Domain;
using OrderService.IntegrationsEvent;

namespace OrderConfirmationApi.Consumer
{
    public class OrderSubmittedConsumer : IConsumer<OrderSubmitted>
    {
        private readonly OrderConfirmationContext _context;
        
        public OrderSubmittedConsumer(OrderConfirmationContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderSubmitted> context)
        { 
            Order order = new Order()
            {
                Id = context.Message.OrderId,
                Broker = context.Message.Exchange,
                Market = context.Message.Symbol,
                PercentOfRealized = context.Message.FillPercentage,
                Status = context.Message.Status,
                Side = context.Message.Side
            };
            _context.Add(order);
            await _context.SaveChangesAsync(); 
        }
    }
}