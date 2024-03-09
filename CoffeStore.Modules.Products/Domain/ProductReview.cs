namespace CoffeStore.Modules.Products.Domain
{
    internal sealed record ProductReview()
    {
        public Guid CustomerId{ get; set; }
        public string? Comment { get; set; }
        public int RateNumber { get; set; }
        public Guid ProductId { get; set; }
    }
}
