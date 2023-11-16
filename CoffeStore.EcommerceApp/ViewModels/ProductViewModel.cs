namespace CoffeStore.EcommerceApp.ViewModels
{
    public record ProductViewModel { 
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public double RateNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
