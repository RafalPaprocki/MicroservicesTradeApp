namespace OrderService.IntegrationsEvent
{
    public record OrderCreated
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}