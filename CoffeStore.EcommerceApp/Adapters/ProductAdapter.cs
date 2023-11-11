using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.EcommerceApp.Dtos.Products;
using CoffeStore.Models.Aggregates.ProductAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public class ProductAdapter : IProductAdapter
    {
        public Product ConvertToDomain(CreateProductRequest request)
        {
            return new Product(request.Name, request.ImagePath, request.Price, request.Description, Guid.NewGuid());
        }
    }
}
