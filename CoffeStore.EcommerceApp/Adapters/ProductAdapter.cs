using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.EcommerceApp.Dtos.Products;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Aggregates.ProductAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public class ProductAdapter : IProductAdapter
    {
        public Product ConvertToDomain(CreateProductRequest request)
        {
            return new Product(request.Name, request.ImagePath, request.Price, request.Description, Guid.NewGuid());
        }

        public IReadOnlyCollection<ProductViewModel> ConvertToViewModel(IEnumerable<Product> products)
        {
            return products.Select(p => ConvertToViewModel(p)).ToList();
        }

        public ProductViewModel ConvertToViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Title = product.ProductName,
                ImagePath = product.ImagePath,
                Price = product.Price,
                Description = product.Description,
                RateNumber = product.AverageRate,
            };
        }
    }
}
