using CoffeStore.EcommerceApp.Requests.Products;

namespace CoffeStore.EcommerceApp.Requests.Order
{
    public class OrderItemDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
