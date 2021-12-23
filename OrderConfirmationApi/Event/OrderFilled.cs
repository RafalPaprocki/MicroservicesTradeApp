namespace OrderService.IntegrationsEvent
{
    public record OrderFilled
    {
        public int OrderId { get; set; }
        public double PercentOfFill { get; set; }
    }
}