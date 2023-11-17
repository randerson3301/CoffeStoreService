namespace CoffeStore.EcommerceApp.Dtos.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";

    }
}
