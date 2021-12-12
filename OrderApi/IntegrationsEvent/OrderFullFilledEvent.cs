namespace OrderService.IntegrationsEvent
{
    public record OrderFillingEvent
    {
        public int OrderId { get; set; }
        public double PercentOfFilled { get; set; }
    }
}