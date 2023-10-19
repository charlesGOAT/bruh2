using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary
{
    public class ShoppingCart
    {
     
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection <CartItem> CartItems { get; set; }
        public Customer Customer { get; set; }

        public ShoppingCart()
        {
        }
    }
}
