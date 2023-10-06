namespace CoffeStoreService.API.Models.Aggregates.OrderAggregate
{
    public readonly struct ProductPrice
    {
        public Guid ProductId { get; }
        public decimal Price { get; }

        public ProductPrice(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
    }
}