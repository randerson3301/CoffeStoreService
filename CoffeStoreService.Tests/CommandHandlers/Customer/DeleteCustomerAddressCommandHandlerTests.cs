using CoffeStore.Modules.Customers.Application.Commands.Handlers;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeStore.Modules.Customers.Domain.Contracts;
using CoffeStoreService.Tests.Mocks;
using NSubstitute.ReturnsExtensions;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Common.ErrorContext;

namespace CoffeStore.Tests.CommandHandlers
{
    internal class DeleteCustomerAddressCommandHandlerTests
    {
        private ICustomerRepository _repository;
        private ILogger<DeleteCustomerAddressCommandHandler> _logger;
        private IValidator<DeleteCustomerAddressCommand> _validator;
        private ICustomerAdapter _adapter;
        private IErrorContext _errorContext;

        private DeleteCustomerAddressCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICustomerRepository>();
            _logger = Substitute.For<ILogger<DeleteCustomerAddressCommandHandler>>();
            _validator = new DeleteCustomerAddressCommandValidator();
            _adapter = Substitute.For<ICustomerAdapter>();
            _errorContext = Substitute.For<IErrorContext>();

            _handler = new DeleteCustomerAddressCommandHandler(_repository, _logger, _validator, _adapter, _errorContext);
        }

        [Test]
        public async Task Delete_CustomerAddress_Returns_Success()
        {
            var customer = CustomerMock.GetCustomer();

            var commandDeleteAddress = new DeleteCustomerAddressCommand() { AddressToRemove = CustomerMock.GetCustomerAddress(), Id = customer.Id };

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(customer);
            _adapter.ConvertToViewModel(Arg.Any<Customer>()).Returns(new CustomerViewModel());

            var result = await _handler.Handle(commandDeleteAddress, new CancellationToken());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Delete_Address_CustomerNotFound_Returns_False()
        {
            var customer = CustomerMock.GetCustomer();

            var commandDeleteAddress = new DeleteCustomerAddressCommand() { AddressToRemove = CustomerMock.GetCustomerAddress(), Id = customer.Id };

            _repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var result = await _handler.Handle(commandDeleteAddress, new CancellationToken());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Add_Address_InvalidCommand_Returns_False()
        {
            var customer = CustomerMock.GetCustomer();

            var commandDeleteAddress = new DeleteCustomerAddressCommand() { AddressToRemove = CustomerMock.GetCustomerAddress(), Id = Guid.Empty };

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(customer);

            var result = await _handler.Handle(commandDeleteAddress, new CancellationToken());

            Assert.IsFalse(result);
        }
    }
}
