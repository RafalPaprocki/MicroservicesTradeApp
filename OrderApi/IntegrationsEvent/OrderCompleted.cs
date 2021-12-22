namespace OrderService.IntegrationsEvent
{
    public record OrderCompleted
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}