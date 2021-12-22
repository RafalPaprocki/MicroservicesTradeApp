namespace OrderService.IntegrationsEvent
{
    public record OrderAuthorized
    {
        public int OrderId { get; set; }
        public string ApiKey { get; set; }
        public string Status { get; set; }
    }
}