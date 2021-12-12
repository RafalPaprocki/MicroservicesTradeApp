namespace OrderService.IntegrationsEvent
{
    public record OrderCompletedEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}