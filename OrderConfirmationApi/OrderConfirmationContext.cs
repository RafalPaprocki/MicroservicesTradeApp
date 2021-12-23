using Microsoft.EntityFrameworkCore;
using OrderConfirmationApi.Domain;

namespace OrderConfirmationApi
{
    public class OrderConfirmationContext : DbContext
    {
        public OrderConfirmationContext(DbContextOptions<OrderConfirmationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Order> Orders { get; set; } 
    }
}