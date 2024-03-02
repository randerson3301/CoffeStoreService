using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain.Contracts;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Queries.Handlers
{
    internal class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<GetCustomerQueryHandler> _logger;
        private IErrorContext _errorContext;

        public GetCustomerQueryHandler(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<GetCustomerQueryHandler> logger, IErrorContext errorContext)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _errorContext = errorContext;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);

            if (customer == null)
            {
                _errorContext.AddError(ErrorType.NotFound, "Customer not found");
                return null;
            }

            return _adapter.ConvertToViewModel(customer);
        }
    }
}
