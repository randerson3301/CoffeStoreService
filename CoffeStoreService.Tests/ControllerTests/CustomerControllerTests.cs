using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Controllers;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStoreService.Tests.Mocks;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CoffeStore.Tests.ControllerTests
{
    internal class CustomerControllerTests
    {
        private CustomerController _controller;
        private ILogger<CustomerController> _logger;
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<CustomerController>>();
            _repository = Substitute.For<ICustomerRepository>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _controller = new CustomerController(_repository, _adapter, _logger);
        }

        [Test]
        public async Task Add_Customer_Success()
        {
            var dto = new CustomerDto()
            {
                Name = "name",
                BirthDate = DateOnly.MinValue,
                Document = "745645687",
                Email = "email",
                Password = "password",
                Address = "address",
                City = "address",
                Complement = "address" ,
                Neighborhood = "address",
                Number = 100,
                State = "state",
                ZipCode = "address" 
            };

            var customer = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(Arg.Any<CustomerDto>()).Returns(customer);
            _repository.Add(Arg.Any<Customer>()).Returns(customer);

            var result = await _controller.Add(dto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Customer>(result);
        }

        [Test]
        public async Task Add_Customer_Fail()
        {
            CustomerDto dto = null;            

            var result = await _controller.Add(dto);

            Assert.IsNull(result);
        }
    }
}
