namespace OrderService.IntegrationsEvent
{
    public record OrderCancelledEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}