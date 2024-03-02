using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries.Handlers
{
    internal class GetAllAvailableProductsQueryHandler : IRequestHandler<GetAllAvailableProductsQuery, IReadOnlyCollection<ProductViewModel>>
    {
        private IProductRepository _repository;
        private IProductAdapter _adapter;

        public GetAllAvailableProductsQueryHandler(IProductRepository repository, IProductAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }

        public async Task<IReadOnlyCollection<ProductViewModel>> Handle(GetAllAvailableProductsQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product> availableProducts = await _repository.GetAvailableProductsAsync();

            return _adapter.ConvertToViewModel(availableProducts);
        }
    }
}
