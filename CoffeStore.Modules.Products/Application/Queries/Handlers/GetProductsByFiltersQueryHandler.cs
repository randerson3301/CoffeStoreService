using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries.Handlers
{
    internal class GetProductsByFiltersQueryHandler : IRequestHandler<GetProductsByFiltersQuery, IReadOnlyCollection<ProductViewModel>>
    {
        private IProductRepository _repository;
        private IProductAdapter _adapter;

        public GetProductsByFiltersQueryHandler(IProductRepository repository, IProductAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }

        public async Task<IReadOnlyCollection<ProductViewModel>> Handle(GetProductsByFiltersQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product> products = await _repository.GetProductsByFiltersAsync(request);

            return _adapter.ConvertToViewModel(products);
        }
    }
}
