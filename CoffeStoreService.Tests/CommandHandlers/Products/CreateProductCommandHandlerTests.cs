using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.Commands.Handlers;
using CoffeStore.Modules.Products.Application.Validators;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace CoffeStore.Tests.CommandHandlers.Products
{
    internal class CreateProductCommandHandlerTests
    {
        private IProductAdapter _adapter;
        private IValidator<CreateProductCommand> _validator;
        private IErrorContext _errorContext;
        private ILogger<CreateProductCommandHandler> _logger;
        private IProductRepository _repository;

        private CreateProductCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateProductCommandValidator();
            _adapter = Substitute.For<IProductAdapter>();
            _errorContext = new ErrorContext();
            _logger = Substitute.For<ILogger<CreateProductCommandHandler>>();
            _repository = Substitute.For<IProductRepository>();

            _handler = new CreateProductCommandHandler(_adapter, _validator, _errorContext, _logger, _repository);
        }

        [Test]
        public async Task Add_NewProduct_Returns_ViewModel()
        {
            var request = new CreateProductCommand()
            {
                Name = "Qualquer um",
                ImagePath = "<<imagem>>",
                Price = 10.65m,
                Description = "cafezin bão"
            };

            _adapter.ConvertToViewModel(Arg.Any<Product>()).Returns(new ProductViewModel() { Id = Guid.NewGuid(), Description = string.Empty, ImagePath = string.Empty, Title = string.Empty});
            _repository.AddAsync(Arg.Any<Product>()).Returns(new Product(request.Name, request.ImagePath, request.Price, request.Description, Guid.NewGuid()));
            var result = await _handler.Handle(request, new CancellationToken());
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProductViewModel>(result);
        }

        [Test]
        public async Task Add_InvalidProduct_Returns_Null()
        {
            var request = new CreateProductCommand()
            {
                Name = "",
                ImagePath = "",
                Price = 0,
                Description = ""
            };

            var result = await _handler.Handle(request, new CancellationToken());

            Assert.IsNull(result);
            Assert.IsNotNull(_errorContext.GetErrors());
        }

        [Test]
        public void Add_Product_Repository_Throws_Exception()
        {
            var request = new CreateProductCommand()
            {
                Name = "Qualquer um",
                ImagePath = "<<imagem>>",
                Price = 10.65m,
                Description = "cafezin bão"
            };

            _repository.AddAsync(Arg.Any<Product>()).Throws<Exception>();

            string? errorMessage = Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(request, new CancellationToken())).Message;

            _logger.Received().LogError(errorMessage);
        }
    }
}
