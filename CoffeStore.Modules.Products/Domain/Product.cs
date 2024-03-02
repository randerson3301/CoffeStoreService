using CoffeStore.Common.Seedwork;

namespace CoffeStore.Modules.Products.Domain
{
    internal sealed class Product: Entity
    {
        public Product(string productName, string imagePath, decimal price, string description, Guid addedBy)
        {
            ProductName = productName;
            ImagePath = imagePath;
            Price = price;
            Description = description;
            AddedBy = addedBy;
            IsAvailable = true;
            CreatedAt = DateTime.Now;
            _productReviews = new List<ProductReview>();
        }

        private readonly ICollection<ProductReview> _productReviews;      
        public string ProductName { get; private set; }
        public string ImagePath { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public bool IsAvailable { get; private set; }
        public Guid AddedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public double AverageRate => _productReviews.Any() ? _productReviews.Average(r => r.RateNumber) : 0;

        public IReadOnlyCollection<ProductReview> ProductReviews => _productReviews.ToList().AsReadOnly();        

        public void AddReview(ProductReview review)
        {           
            _productReviews.Add(review);
        }

        public void Disable()
        {
            IsAvailable = false;
        }

        public void Enable()
        {
            IsAvailable = true;
        }

        public void RemoveReview(ProductReview reviewToRemove)
        {
            _productReviews.Remove(reviewToRemove);
        }
    }
}
