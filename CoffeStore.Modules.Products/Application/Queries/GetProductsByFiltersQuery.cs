using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries
{
    internal class GetProductsByFiltersQuery: IRequest<IReadOnlyCollection<ProductViewModel>>
    {
        public GetProductsByFiltersQuery(bool onlyAvailable = true)
        {
            OnlyAvailable = onlyAvailable;
        }

        public bool OnlyAvailable { get; set; }
    }
}
