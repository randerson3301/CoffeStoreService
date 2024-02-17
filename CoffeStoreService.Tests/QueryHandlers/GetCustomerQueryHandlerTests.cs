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
using CoffeStore.Modules.Customers.Application.Queries.Handlers;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Domain.Contracts;
using System.Runtime.CompilerServices;
using CoffeStoreService.Tests.Mocks;
using CoffeStore.Modules.Customers.Application.Queries;
using CoffeStore.Modules.Customers.Application.ViewModels;
using NSubstitute.ReturnsExtensions;
using CoffeStore.Modules.Customers.Application.ErrorContext;

namespace CoffeStore.Tests.QueryHandlers
{
    internal class GetCustomerQueryHandlerTests
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<GetCustomerQueryHandler> _logger;
        private IErrorContext _errorContext;
        private GetCustomerQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICustomerRepository>();
            _logger = Substitute.For<ILogger<GetCustomerQueryHandler>>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _errorContext = Substitute.For<IErrorContext>();
            _handler = new GetCustomerQueryHandler(_repository, _adapter, _logger, _errorContext);
        }

        [Test]
        public async Task Get_Customer_Returns_Success()
        {
            var customer = CustomerMock.GetCustomer();

            var customerQuery = new GetCustomerQuery()
            {
                Id = Guid.NewGuid()
            };

            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(customer);
            _adapter.ConvertToViewModel(customer).Returns(new CustomerViewModel());

            var result = await _handler.Handle(customerQuery, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CustomerViewModel>(result);
        }

        [Test]
        public async Task Get_CustomerNotFound_ReturnsNull()
        {
            var customer = CustomerMock.GetCustomer();

            var customerQuery = new GetCustomerQuery()
            {
                Id = Guid.NewGuid()
            };

            _repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            var result = await _handler.Handle(customerQuery, new CancellationToken());

            Assert.IsNull(result);
        }
    }
}
