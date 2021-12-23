using OrderService.Model;

namespace OrderService.Command
{
    public record AuthorizeOrder
    {
        public int OrderId { get; set; } 
        public decimal Price { get; set; }
        public decimal Amount { get; set; } 
    }
}