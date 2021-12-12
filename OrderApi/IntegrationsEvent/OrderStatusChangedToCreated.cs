namespace OrderService.IntegrationsEvent
{
    public record OrderStatusChangedToCreated
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}