using CoffeStore.EcommerceApp.Dtos.Products;
using CoffeStore.Models.Aggregates.ProductAggregate;

namespace CoffeStore.EcommerceApp.Adapters.Contracts
{
    public interface IProductAdapter
    {
        Product ConvertToDomain(CreateProductRequest request);
    }
}
