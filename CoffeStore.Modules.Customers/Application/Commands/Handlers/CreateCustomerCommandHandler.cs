﻿using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.ErrorContext;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStore.Modules.Customers.Resources;
using FluentValidation;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Commands.Handlers
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerViewModel>
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CreateCustomerCommandHandler> _logger;
        private readonly IValidator<CreateCustomerCommand> _validator;
        private readonly IErrorContext _errorContext;

        public CreateCustomerCommandHandler(ICustomerRepository repository, ICustomerAdapter adapter, 
            ILogger<CreateCustomerCommandHandler> logger, IValidator<CreateCustomerCommand> validator, IErrorContext errorContext)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _validator = validator;
            _errorContext = errorContext;
        }

        public async Task<CustomerViewModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.IsValid)
            {
                try
                {
                    var domain = await _repository.AddAsync(_adapter.ConvertToDomain(request));
                    return _adapter.ConvertToViewModel(domain);
                }
                catch (Exception error)
                {
                    _errorContext.AddError(ErrorType.ExceptionThrowed, error.Message);
                    _logger.LogError(error.Message);
                    throw;
                }               
            }

            _errorContext.AddError(ErrorType.FailedValidation, result.Errors.Select(e => e.ErrorMessage).ToArray());

            _logger.LogError(ErrorMessages.ADD_CUSTOMER_FAILED + "| Reason: " + result.ToString());
            
            return null;           
        }
    }
}