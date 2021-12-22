namespace OrderService.IntegrationsEvent
{
    public record OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}