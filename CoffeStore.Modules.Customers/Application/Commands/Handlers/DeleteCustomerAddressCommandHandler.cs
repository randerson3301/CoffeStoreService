using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStore.Modules.Customers.Domain.DomainExceptions;
using CoffeStore.Modules.Customers.Resources;
using FluentValidation;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeStore.Modules.Customers.Application.Commands.Handlers
{
    internal class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, bool>
    {
        private ICustomerRepository _repository;
        private ILogger<DeleteCustomerAddressCommandHandler> _logger;
        private IValidator<DeleteCustomerAddressCommand> _validator;
        private ICustomerAdapter _adapter;
        private readonly IErrorContext _errorContext;



        public DeleteCustomerAddressCommandHandler(ICustomerRepository repository, ILogger<DeleteCustomerAddressCommandHandler> logger, IValidator<DeleteCustomerAddressCommand> validator, ICustomerAdapter adapter, IErrorContext errorContext)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _adapter = adapter;
            _errorContext = errorContext;
        }

        public async Task<bool> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.IsValid)
            {
                try
                {
                    Customer? customer = await _repository.GetByIdAsync(request.Id);

                    if (customer == null)
                    {
                        _errorContext.AddError(ErrorType.NotFound, ErrorMessages.CUSTOMER_NOT_FOUND);
                        return false;
                    }

                    if (!customer.TryRemoveAddress(_adapter.ConvertToDomain(request)))
                    {
                        _errorContext.AddError(ErrorType.InvalidOperation, ErrorMessages.CANNOT_REMOVE_ADDRESS);
                        return false;
                    }                    

                    await _repository.UpdateAsync(customer);
                    return true;

                }
                catch (Exception error)
                {
                    _logger.LogError(error.Message);
                    throw;
                }
            }

            _errorContext.AddError(ErrorType.FailedValidation, result.Errors.Select(e => e.ErrorMessage).ToArray());
            return false;
        }
    }
}
