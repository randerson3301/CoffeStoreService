using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.ErrorContext;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStore.Modules.Customers.Resources;
using FluentValidation;
using MediatR;
using System.Linq;

namespace CoffeStore.Modules.Customers.Application.Commands.Handlers
{
    internal class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, CustomerViewModel>
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CreateCustomerCommandHandler> _logger;
        private IValidator<CreateCustomerAddressCommand> _validator;
        private IErrorContext _errorContext;

        public CreateCustomerAddressCommandHandler(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<CreateCustomerCommandHandler> logger, IValidator<CreateCustomerAddressCommand> validator, IErrorContext errorContext)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _validator = validator;
            _errorContext = errorContext;
        }

        public async Task<CustomerViewModel> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);

            if (customer == null)
            {
                _errorContext.AddError(ErrorType.NotFound, ErrorMessages.CUSTOMER_NOT_FOUND);
                return null;
            }

            var result = await _validator.ValidateAsync(request);

            if (result.IsValid)
            {
                customer.AddAddress(_adapter.ConvertToDomain(customer.Id, request.DeliveryAddress));

                try
                {                    
                    await _repository.UpdateAsync(customer);

                    return _adapter.ConvertToViewModel(customer);
                }
                catch (Exception error)
                {
                    _errorContext.AddError(ErrorType.ExceptionThrowed, error.Message);
                    _logger.LogError(error.Message);
                    return null;
                }
            }

            _errorContext.AddError(ErrorType.FailedValidation, result.Errors.Select(e => e.ErrorMessage).ToArray());

            return null;
        }
    }
}
