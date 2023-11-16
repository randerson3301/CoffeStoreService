using CoffeStore.Models.DomainExceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CoffeStore.Models.Aggregates.ProductAggregate
{
    public sealed class Product
    {
        private readonly List<ProductReview> _productReviews;
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string ProductName { get; private set; }
        public string ImagePath { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public bool IsAvailable { get; private set; }
        public Guid AddedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public double AverageRate =>  _productReviews.Any() ? _productReviews.Average(r => r.RateNumber) : 0;
        public IReadOnlyCollection<ProductReview> ProductReviews => _productReviews.AsReadOnly();

        public Product() 
        {
            _productReviews = new List<ProductReview>();
        }

        public Product(string productName, string imagePath, decimal price, string description, Guid addedBy)
        {
            ProductName = productName;
            ImagePath = imagePath;
            Price = price;
            Description = description;
            IsAvailable = true;
            AddedBy = addedBy;
            CreatedAt = DateTime.Now;
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