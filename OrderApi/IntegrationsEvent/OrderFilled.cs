namespace OrderService.IntegrationsEvent
{
    public record OrderFilled
    {
        public int OrderId { get; set; }
        public double PercentOfFilled { get; set; }
    }
}