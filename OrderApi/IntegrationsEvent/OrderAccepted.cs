namespace OrderService.IntegrationsEvent
{
    public record OrderAccepted
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}