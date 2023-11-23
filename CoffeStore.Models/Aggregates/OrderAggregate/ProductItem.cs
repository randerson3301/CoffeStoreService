namespace CoffeStore.Models.Aggregates.OrderAggregate
{
    public class ProductItem
    {
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }

        public ProductItem(string productId, decimal price, string imagePath, string title)
        {
            ProductId = productId;
            Price = price;
            ImagePath = imagePath;
            Title = title;
        }
    }
}