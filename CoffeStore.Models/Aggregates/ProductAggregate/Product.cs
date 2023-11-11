using CoffeStore.Models.DomainExceptions;


namespace CoffeStore.Models.Aggregates.ProductAggregate
{
    public sealed class Product
    {   

        private readonly Guid _id;
        private readonly List<ProductReview> _productReviews;

        public Guid Id => _id;
        public string ProductName { get; }
        public string ImagePath { get; }
        public decimal Price { get; }
        public string Description { get; }
        public bool IsAvailable { get; private set; }
        public Guid AddedBy { get; }
        public DateTime CreatedAt { get; private set; }
        public double AverageRate =>  _productReviews.Any() ? _productReviews.Average(r => r.RateNumber) : 0;
        public IReadOnlyCollection<ProductReview> ProductReviews => _productReviews.AsReadOnly();

        public Product(string productName, string imagePath, decimal price, string description, Guid addedBy)
        {
            ProductName = productName;
            ImagePath = imagePath;
            Price = price;
            Description = description;
            IsAvailable = true;
            AddedBy = addedBy;
            CreatedAt = DateTime.Now;
            _id = Guid.NewGuid();
            _productReviews = new List<ProductReview>();            
        }


        public void AddReview(ProductReview review)
        {
            if (!IsAvailable)
            {
                throw new ReviewUnavailableProductException();
            }

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