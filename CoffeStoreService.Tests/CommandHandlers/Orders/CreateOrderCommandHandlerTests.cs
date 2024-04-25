using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Orders.Application.Adapters;
using CoffeStore.Modules.Orders.Application.Commands;
using CoffeStore.Modules.Orders.Application.Commands.Handlers;
using CoffeStore.Modules.Orders.Application.Dtos;
using CoffeStore.Modules.Orders.Application.Validators;
using CoffeStore.Modules.Orders.Application.ViewModels;
using CoffeStore.Modules.Orders.Domain;
using CoffeStore.Modules.Orders.Domain.Contracts;
using CoffeStore.Tests.Mocks;
using CoffeStoreService.Tests.Mocks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace CoffeStore.Tests.CommandHandlers.Orders
{
    internal class CreateOrderCommandHandlerTests
    {
        private IOrderAdapter _adapter;
        private IValidator<CreateOrderCommand> _validator;
        private IErrorContext _errorContext;
        private ILogger<CreateOrderCommandHandler> _logger;
        private IOrderRepository _repository;

        private CreateOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateOrderCommandValidator();
            _adapter = Substitute.For<IOrderAdapter>();
            _errorContext = new ErrorContext();
            _logger = Substitute.For<ILogger<CreateOrderCommandHandler>>();
            _repository = Substitute.For<IOrderRepository>();

            _handler = new CreateOrderCommandHandler(_adapter, _validator, _errorContext, _logger, _repository);
        }

        [Test]
        public async Task Add_NewOrder_Returns_ViewModel()
        {
            var customerId = Guid.NewGuid();
            var request = new CreateOrderCommand()
            {
                CustomerId = Guid.NewGuid(),
                DeliveryAddress = CustomerMock.GetCustomerAddress(),
                OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };
            _adapter.ConvertToViewModel(Arg.Any<Order>()).Returns(new OrderViewModel() { Id = Guid.NewGuid(), CustomerId = customerId, Amount = default, DeliveryStatus = default, OrderItems = new List<OrderItemViewModel>() });

            _repository.AddAsync(Arg.Any<Order>()).Returns(new Order(CustomerMock.GetCustomerAddress(), customerId));
            var result = await _handler.Handle(request, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OrderViewModel>(result);
        }

        [Test]
        public async Task Add_InvalidOrder_Returns_Null()
        {
            var request = new CreateOrderCommand()
            {
                CustomerId = Guid.NewGuid(),
                DeliveryAddress = CustomerMock.GetCustomerAddress(),
                OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };

            request.CustomerId = Guid.Empty;

            var result = await _handler.Handle(request, new CancellationToken());

            Assert.IsNull(result);
            Assert.IsNotNull(_errorContext.GetErrors());
        }

        [Test]
        public void Add_Order_Repository_Throws_Exception()
        {
            var request = new CreateOrderCommand()
            {
                CustomerId = Guid.NewGuid(),
                DeliveryAddress = CustomerMock.GetCustomerAddress(),
                OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };

            _repository.AddAsync(Arg.Any<Order>()).Throws<Exception>();

            string? errorMessage = Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(request, new CancellationToken())).Message;

            _logger.Received().LogError(errorMessage);
        }
    }
}
