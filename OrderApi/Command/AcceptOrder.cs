using OrderService.Model;

namespace OrderService.Command
{
    public record AcceptOrder
    {
        public int OrderId { get; set; } 
        public string Secret { get; set; }
        public string Key { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; } 
    }
}