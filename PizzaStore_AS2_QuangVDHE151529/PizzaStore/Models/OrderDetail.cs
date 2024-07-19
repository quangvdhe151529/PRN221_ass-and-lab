using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class OrderDetail
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        //Relationship
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
