namespace CoffeStore.Modules.Products.Application.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public double RateNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
