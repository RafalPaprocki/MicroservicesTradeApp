namespace OrderService.Dto
{
    public class OrderCreateDto
    {
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}