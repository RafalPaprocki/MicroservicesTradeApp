using System;
using Microsoft.EntityFrameworkCore;
using OrderService.Model;

namespace OrderService.Infrastructure
{
    public class OrderContext : DbContext
    { 
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { 
        }
        
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            base.OnModelCreating(modelBuilder);
        }
    }
}