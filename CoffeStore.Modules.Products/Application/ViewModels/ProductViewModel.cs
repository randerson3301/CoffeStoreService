namespace CoffeStore.Modules.Products.Application.ViewModels
{
    public class ProductViewModel
    {
        public required Guid Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Title { get; set; }
        public double RateNumber { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
    }
}
