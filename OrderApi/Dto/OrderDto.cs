using OrderService.Common;

namespace OrderService.Dto
{
    public class OrderDto
    {
        public Status Status { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public double FillPercentage { get; set; }
    }
}