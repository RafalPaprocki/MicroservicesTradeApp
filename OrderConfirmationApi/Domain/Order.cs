using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderConfirmationApi.Domain
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Broker { get; set; }
        public string Market { get; set; }
        public double PercentOfRealized { get; set; }
        public string Side { get; set; }
    }
}