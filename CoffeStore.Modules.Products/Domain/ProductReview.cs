namespace CoffeStore.Modules.Products.Domain
{
    internal sealed record ProductReview(Guid CustomerId, string? Comment, int RateNumber)
    {        
        public Guid ProductId { get; set; }
    }
}
