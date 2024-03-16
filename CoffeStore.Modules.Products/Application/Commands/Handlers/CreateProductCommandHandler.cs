using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using FluentValidation;
using MediatR;

namespace CoffeStore.Modules.Products.Application.Commands.Handlers
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewModel?>
    {
        private IProductAdapter _adapter;
        private IValidator<CreateProductCommand> _validator;
        private IErrorContext _errorContext;
        private ILogger<CreateProductCommandHandler> _logger;
        private IProductRepository _repository;

        public CreateProductCommandHandler(IProductAdapter adapter, IValidator<CreateProductCommand> validator, IErrorContext errorContext, ILogger<CreateProductCommandHandler> logger, IProductRepository repository)
        {
            _adapter = adapter;
            _validator = validator;
            _errorContext = errorContext;
            _logger = logger;
            _repository = repository;
        }

        public async Task<ProductViewModel?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.IsValid)
            {
                try
                {
                    Product domain = await _repository.AddAsync(_adapter.ConvertToDomain(request));

                    return _adapter.ConvertToViewModel(domain);
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
