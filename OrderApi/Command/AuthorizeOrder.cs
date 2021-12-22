namespace OrderService.Command
{
    public record AuthorizeOrder
    {
        public int OrderId { get; set; } 
    }
}