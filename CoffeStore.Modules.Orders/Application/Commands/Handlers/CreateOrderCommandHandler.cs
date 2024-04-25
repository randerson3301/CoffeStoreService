﻿using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Orders.Application.Adapters;
using CoffeStore.Modules.Orders.Application.ViewModels;
using CoffeStore.Modules.Orders.Domain;
using CoffeStore.Modules.Orders.Domain.Contracts;
using FluentValidation;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoffeStore.Modules.Orders.Application.Commands.Handlers
{
    internal class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, OrderViewModel?>
    {
        private IOrderAdapter _adapter;
        private IValidator<CreateOrderCommand> _validator;
        private IErrorContext _errorContext;
        private ILogger<CreateOrderCommandHandler> _logger;
        private IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderAdapter adapter, IValidator<CreateOrderCommand> validator, IErrorContext errorContext, ILogger<CreateOrderCommandHandler> logger, IOrderRepository repository)
        {
            _adapter = adapter;
            _validator = validator;
            _errorContext = errorContext;
            _logger = logger;
            _repository = repository;
        }

        public async Task<OrderViewModel?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.IsValid)
            {
                try
                {
                    //TODO: Validar se cliente existe
                    //TODO: Validar se produto existe
                    Order order = await _repository.AddAsync(_adapter.ConvertToDomain(request));
                    return _adapter.ConvertToViewModel(order);
                } 
                catch (Exception error)
                {
                    _logger.LogError(error.Message);
                    throw;
                }
            }

            _errorContext.AddError(ErrorType.FailedValidation, result.Errors.Select(e => e.ErrorMessage).ToArray());
            return null;
        }
    }
}
