using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStore.Models
{
    public class Supplier
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        //Relationship
        public ICollection<Product> Products { get; set; }
    }
}
