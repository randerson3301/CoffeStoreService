namespace CoffeStore.Models.Aggregates.OrderAggregate
{
    public struct OrderItem
    {
        public ProductItem ProductItem { get; set; }
        public int Quantity { get; private set; }
        public decimal Subtotal { get; set; }

        public OrderItem(ProductItem productItem, int quantity, decimal subtotal)
        {
            ProductItem = productItem;
            Quantity = quantity;
            Subtotal = subtotal;
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