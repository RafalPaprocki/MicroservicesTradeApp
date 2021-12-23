using System;
using System.Linq;
using System.Threading.Tasks;
using BinanceExchange;
using CryptoExchange.Net.Authentication;
using MassTransit;
using OrderService.Command;
using OrderService.IntegrationsEvent;

namespace OrderConfirmationApi.Consumer
{
    public class AcceptOrderConsumer : IConsumer<AcceptOrder>
    {
        private readonly OrderConfirmationContext _context;
        public AcceptOrderConsumer(OrderConfirmationContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<AcceptOrder> context)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == context.Message.OrderId);
            if (order == null)
            {
                await context.Publish<OrderRejected>(new OrderRejected()
                {
                    OrderId = context.Message.OrderId,
                    Status = "Rejected",
                    Message = "Order with given id was not found, perhaps not submitted yet."
                });
                return;
            }
            var client = new BinanceAdapter();
            var responseMessage = await client.CreateOrder(new ApiCredentials(context.Message.Key, context.Message.Secret),
                order.Market, order.Side, context.Message.Price, context.Message.Amount);

            if (responseMessage.Equals("Created", StringComparison.InvariantCulture))
            {
                await context.Publish<OrderAccepted>(new OrderAccepted()
                {
                    OrderId = context.Message.OrderId,
                    Status = "Accepted"
                });
            }
            else
            {
                await context.Publish<OrderRejected>(new OrderRejected()
                {
                    OrderId = context.Message.OrderId,
                    Status = "Rejected",
                    Message = responseMessage
                }); 
            } 
        }
    }
}