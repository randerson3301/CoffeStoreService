namespace CoffeStore.Models.Aggregates.OrderAggregate
{
    public struct OrderItem
    {
        public ProductPrice ProductPrice { get; }
        public int Quantity { get; private set; }
        public decimal Subtotal => ProductPrice.Price * Quantity;

        public OrderItem(ProductPrice productPrice, int quantity) : this()
        {
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public void RaiseQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            Quantity--;
        }
    }
}