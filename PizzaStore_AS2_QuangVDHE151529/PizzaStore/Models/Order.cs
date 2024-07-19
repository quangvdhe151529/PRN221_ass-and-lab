using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Order
    {
        public Order() { 
            OrderDate = DateTime.Now;
            RequiredDate = DateTime.Now;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Freight { get; set; }

        [Required]
        public string ShipAddress { get; set; }

        //Relationship
        public Customer Customer { get; set; }
    }
}
