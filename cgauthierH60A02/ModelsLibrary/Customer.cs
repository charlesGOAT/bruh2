using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary
{
    public class Customer
    {
        
        public enum Provinces
        {
            AB,

            BC,

            MB,

            NB,

            NL,

            NT,

            NS,

            NU,

            ON,

            PE,

            QC,

            SK,

            YT
        }
        
    
        [Required]
        public int CustomerId { get; set; }
        public string Username { get; set; }
        [Required]
        [StringLength(16)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 0)]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        [RegularExpression("[0-9]{10}")]
        public string PhoneNumber { get; set; }

        public string Province { get; set; }
        
        [RegularExpression("([0-9]{4}\\ ){3}[0-9]{4}")]
        public string CreditCard { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public Customer()
        {
        }
    }
}
