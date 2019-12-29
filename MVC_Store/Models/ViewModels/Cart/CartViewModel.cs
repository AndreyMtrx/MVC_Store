namespace MVC_Store.Models.Cart
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Quantity * Price;
            }
        }
        public string Image { get; set; }
    }
}