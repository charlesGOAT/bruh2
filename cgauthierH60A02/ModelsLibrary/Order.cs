namespace ModelsLibrary
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFufilled { get; set; }
        public decimal Total { get; set; }
        public decimal Taxes { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrdersItems { get; set; }

        public Order()
        {
        }
    }
}
