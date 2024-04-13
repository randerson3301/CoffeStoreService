namespace CoffeStore.Modules.Orders.Domain
{
    internal sealed class OrderItem
    {
        public Guid ProductId { get; }
        public decimal Price { get; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;
        public short RatingNumber { get; private set; }

        public OrderItem(Guid productId, decimal price, int quantity)
        {
            Quantity = quantity;
            ProductId = productId;
            Price = price;
        }

        public void Rate(short ratingNumber)
        {
            RatingNumber = ratingNumber;
        }
    }
}
