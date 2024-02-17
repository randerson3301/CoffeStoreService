using CoffeStore.Modules.Customers.Application.Commands.Handlers;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStoreService.Tests.Mocks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStore.Modules.Customers.Application.Validators;
using CoffeStore.Modules.Customers.Application.ViewModels;
using NSubstitute.ReturnsExtensions;
using CoffeStore.Modules.Customers.Application.ErrorContext;
using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStore.Tests.CommandHandlers
{
    internal class CreateCustomerAddressCommandHandlerTests
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CreateCustomerCommandHandler> _logger;
        private IValidator<CreateCustomerAddressCommand> _validator;
        private IErrorContext _errorContext;

        private CreateCustomerAddressCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICustomerRepository>();
            _logger = Substitute.For<ILogger<CreateCustomerCommandHandler>>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _validator = new CreateCustomerAddressCommandValidator();
            _errorContext = Substitute.For<IErrorContext>();
            _handler = new CreateCustomerAddressCommandHandler(_repository, _adapter, _logger, _validator, _errorContext);
        }

        [Test]
        public async Task Add_CustomerAddress_Returns_Success()
        {
            var commandCustomer = CustomerMock.GetCommand();
            var customer = CustomerMock.GetCustomer();
            
            var commandNewAddress = new CreateCustomerAddressCommand() { DeliveryAddress = commandCustomer.DeliveryAddress, Id = customer.Id };           

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(customer);

            var viewModel = new CustomerViewModel()
            {
                Addresses = customer.DeliveryAddresses.Select(a => new DeliveryAddress(a.ZipCode, a.Address, a.Number, a.Complement, a.Neighborhood, a.City, a.State)).AsEnumerable(),
                Id = customer.Id,
                BirthDate = customer.BirthDate,
                Document = customer.Document,
                Email = customer.Email,
                Name = customer.FullName,
            };

            _adapter.ConvertToViewModel(Arg.Any<Customer>()).Returns(viewModel);

            var result = await _handler.Handle(commandNewAddress, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CustomerViewModel>(result);
        }

        [Test]
        public async Task Add_Address_CustomerNotFound_Returns_Null()
        {
            var commandCustomer = CustomerMock.GetCommand();
            var customer = CustomerMock.GetCustomer();

            var commandNewAddress = new CreateCustomerAddressCommand() { DeliveryAddress = commandCustomer.DeliveryAddress, Id = customer.Id };

            _repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();            

            var result = await _handler.Handle(commandNewAddress, new CancellationToken());

            Assert.IsNull(result);
        }

        [Test]
        public async Task Add_Address_InvalidCommand_Returns_Null()
        {
            var customer = CustomerMock.GetCustomer();

            var commandNewAddress = new CreateCustomerAddressCommand() { DeliveryAddress = CustomerMock.GetCustomerAddress(), Id = Guid.Empty };

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(customer);

            var result = await _handler.Handle(commandNewAddress, new CancellationToken());

            Assert.IsNull(result);
        }
    }
}
