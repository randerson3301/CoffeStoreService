using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain.Contracts;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Queries.Handlers
{
    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        private IProductRepository _repository;
        private IProductAdapter _adapter;
        private IErrorContext _errorContext;

        public GetProductByIdQueryHandler(IProductRepository repository, IProductAdapter adapter, IErrorContext errorContext)
        {
            _repository = repository;
            _adapter = adapter;
            _errorContext = errorContext;
        }

        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                _errorContext.AddError(ErrorType.NotFound, "Product not found");
                return null;
            }

            return _adapter.ConvertToViewModel(product);
        }
    }
}
