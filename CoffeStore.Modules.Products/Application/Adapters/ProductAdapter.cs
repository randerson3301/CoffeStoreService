using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;

namespace CoffeStore.Modules.Products.Application.Adapters
{
    internal class ProductAdapter : IProductAdapter
    {
        public Product ConvertToDomain(CreateProductCommand request)
        {
            //TODO: Trocar o NewGuid pelo id do funcionario que cadastrar o produto
            return new Product(request.Name, request.ImagePath, request.Price, request.Description, Guid.NewGuid());
        }

        public ProductViewModel ConvertToViewModel(Product domain)
        {
            return new ProductViewModel() 
            { 
                Title = domain.ProductName,
                Description = domain.Description,
                Id = domain.Id,
                ImagePath = domain.ImagePath,
                Price = domain.Price,
                RateNumber = domain.AverageRate
            };
        }

        public IReadOnlyCollection<ProductViewModel> ConvertToViewModel(ICollection<Product> products)
        {
            return products.Select((domain) => new ProductViewModel()
            {
                Title = domain.ProductName,
                Description = domain.Description,
                Id = domain.Id,
                ImagePath = domain.ImagePath,
                Price = domain.Price,
                RateNumber = domain.AverageRate
            }).ToList().AsReadOnly();
        }
    }
}
