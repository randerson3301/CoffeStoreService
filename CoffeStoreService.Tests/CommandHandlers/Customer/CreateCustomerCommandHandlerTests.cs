using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Commands.Handlers;
using CoffeStore.Modules.Customers.Application.ErrorContext;
using CoffeStore.Modules.Customers.Application.Validators;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStoreService.Tests.Mocks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;


namespace CoffeStore.Tests.CommandHandlers
{
    public class CreateCustomerCommandHandlerTests
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CreateCustomerCommandHandler> _logger;
        private IValidator<CreateCustomerCommand> _validator;
        private IErrorContext _errorContext;
        private IMediator _mediator;

        private CreateCustomerCommandHandler _handler;
        
        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICustomerRepository>();
            _logger = Substitute.For<ILogger<CreateCustomerCommandHandler>>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _validator = new CreateCustomerCommandValidator();
            _errorContext = Substitute.For<IErrorContext>();
            _mediator = Substitute.For<IMediator>();

            _handler = new CreateCustomerCommandHandler(_repository, _adapter, _logger, _validator, _errorContext, _mediator);
        }

        [Test]
        public async Task Add_Customer_Returns_Success()
        {
            var command = CustomerMock.GetCommand();
            var customer = CustomerMock.GetCustomer();
            customer.AddAccess(command.Password);

            _repository.AddAsync(Arg.Any<Customer>()).Returns(customer);
            _adapter.ConvertToViewModel(Arg.Any<Customer>()).Returns(new CustomerViewModel());
            
            var result = await _handler.Handle(command, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CustomerViewModel>(result);
        }        

        [Test]
        public async Task Add_Customer_CommandInvalid_Returns_Null()
        {
            var command = CustomerMock.GetCommand();
            var customer = CustomerMock.GetCustomer();
            
            command.Name = "";

            var result = await _handler.Handle(command, new CancellationToken());

            Assert.IsNull(result);
        }

    }
}
