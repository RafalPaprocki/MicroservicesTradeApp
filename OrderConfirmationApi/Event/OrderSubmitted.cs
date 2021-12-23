namespace OrderService.IntegrationsEvent
{
    public record OrderSubmitted
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; } 
        public double FillPercentage { get; set; }
        public string Side { get; set; }
        public int UserId { get; set; }
    }
}