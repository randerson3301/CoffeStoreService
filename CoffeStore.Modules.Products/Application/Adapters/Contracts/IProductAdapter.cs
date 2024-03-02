using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;

namespace CoffeStore.Modules.Products.Application.Adapters.Contracts
{
    internal interface IProductAdapter
    {
        Product ConvertToDomain(CreateProductCommand request);
        ProductViewModel ConvertToViewModel(Product domain);
    }
}
