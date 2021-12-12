namespace OrderService.IntegrationsEvent
{
    public record OrderConfirmedEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}