using System.Linq;
using OrderService.Model;

namespace OrderService.Infrastructure.SeedDatabase
{
    public static class DbInitializer
    {
        public static void Initialize(OrderContext context)
        {
            if (context.Orders.Any())
            {
                return;  
            }

            var orders = new Order[10];
            
            // TODO add seed data here, if needed
            
            context.SaveChanges();
        }
    }
}