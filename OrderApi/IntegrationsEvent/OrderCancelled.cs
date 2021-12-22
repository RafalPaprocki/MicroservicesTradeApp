namespace OrderService.IntegrationsEvent
{
    public record OrderCancelled
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}