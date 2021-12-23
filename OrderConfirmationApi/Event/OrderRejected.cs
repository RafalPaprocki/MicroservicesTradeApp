namespace OrderService.IntegrationsEvent
{
    public record OrderRejected
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}