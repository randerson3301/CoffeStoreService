using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries
{
    internal class GetAllAvailableProductsQuery: IRequest<IReadOnlyCollection<ProductViewModel>>
    {
    }
}
