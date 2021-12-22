namespace OrderService.IntegrationsEvent
{
    public class OrderRejected
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}