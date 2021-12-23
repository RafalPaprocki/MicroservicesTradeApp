using System.ComponentModel.DataAnnotations;
using OrderService.Common;

namespace OrderService.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public double FillPercentage { get; set; }
        public string Side { get; set; }
        public int UserId { get; set; }
    }
}