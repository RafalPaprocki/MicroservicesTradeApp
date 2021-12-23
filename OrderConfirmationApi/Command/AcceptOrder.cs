namespace OrderService.Command
{
    public class AcceptOrder
    {
        public int OrderId { get; set; } 
        public string Secret { get; set; }
        public string Key { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; } 
    }
}