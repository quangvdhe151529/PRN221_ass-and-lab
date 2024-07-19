using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStore.Models
{
    public class Product
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int QuantityPerUnit { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public string ProductImage { get; set; }

        //Relationship
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
