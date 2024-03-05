using CoffeStore.Modules.Products.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries
{
    internal class GetProductByIdQuery: IRequest<ProductViewModel>
    {
        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; }
    }
}
