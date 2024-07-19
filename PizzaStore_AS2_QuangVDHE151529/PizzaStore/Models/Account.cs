using PizzaStore.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Account
    {
        [Key]
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public AccountType Type { get; set; }
    }
}
