using CoffeStore.EcommerceApp.Dtos.Products;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Aggregates.ProductAggregate;

namespace CoffeStore.EcommerceApp.Adapters.Contracts
{
    public interface IProductAdapter
    {
        Product ConvertToDomain(CreateProductRequest request);

        IReadOnlyCollection<ProductViewModel> ConvertToViewModel(IEnumerable<Product> products);
        ProductViewModel ConvertToViewModel(Product product);
    }
}
