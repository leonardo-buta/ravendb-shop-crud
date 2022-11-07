namespace Shop.Models
{
    public class CartLine
    {
        public CartLine()
        {

        }

        public CartLine(string? productName, decimal productPrice, int quantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
