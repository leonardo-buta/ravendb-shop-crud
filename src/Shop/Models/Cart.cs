namespace Shop.Models
{
    public class Cart
    {
        public Cart()
        {

        }

        public Cart(string id, string customer)
        {
            Id = id;
            Customer = customer;
        }

        public string? Id { get; set; }
        public string? Customer { get; set; }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
    }
}
