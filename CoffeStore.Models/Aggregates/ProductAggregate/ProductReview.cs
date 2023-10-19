namespace CoffeStore.Models.Aggregates.ProductAggregate
{
    public class ProductReview
    {
        public Guid CustomerId { get; }
        public string Comment { get; }
        public int RateNumber { get; }

        public ProductReview(Guid customerId, string comment, int rateNumber)
        {
            CustomerId = customerId;
            Comment = comment;
            RateNumber = rateNumber;
        }

    }
}