using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Controllers;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
using CoffeStore.EcommerceApp.ViewModels.Customer;
using CoffeStore.Models.Contracts.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace CoffeStore.Tests.ControllerTests
{
    internal class CustomerControllerTests
    {
        private CustomerController _customerController;
        private ILogger<CustomerController> _logger;
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private IValidator<BaseCustomerDto> _validator;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<CustomerController>>();
            _repository = Substitute.For<ICustomerRepository>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _validator = new CustomerDtoValidator<BaseCustomerDto>();

            _customerController = new CustomerController(_repository, _adapter, _logger, _validator);
        }

        [Test]
        public async Task Add_ValidDto_ReturnsCreatedAtAction()
        {

            var validDto = CustomerMock.GetSignUpDto();
            var domain = CustomerMock.GetCustomer();
            var viewModel = CustomerMock.GetCustomerViewModel();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.Add(Arg.Any<Customer>()).Returns(domain);
            _adapter.ConvertToViewModel(domain).Returns(viewModel);

            var result = await _customerController.Add(validDto);
            var createdAtResult = (CreatedAtActionResult)result;

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            Assert.AreSame(viewModel, createdAtResult.Value);
        }

        [Test]
        public async Task Add_InvalidDto_ReturnsBadRequest()
        {

            var invalidDto = new CustomerSignUpDto {};
            var invalidResults = await _validator.ValidateAsync(invalidDto);
            var result = await _customerController.Add(invalidDto);

            var badRequestResult = (BadRequestObjectResult)result;
            var badRequestResultValidations = (ValidationResult?)badRequestResult.Value;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(invalidResults.Errors.Count, badRequestResultValidations?.Errors.Count);
        }

        [Test]
        public async Task Add_Repository_Failed_ThrowsException()
        {
            var validDto = CustomerMock.GetSignUpDto();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.Add(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.Add(validDto);
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Adding customer has failed", badRequestResult.Value);
        }

        [Test]
        public async Task Update_ValidDto_ReturnsNoContent()
        {
            var validDto = CustomerMock.GetUpdateDto(Guid.NewGuid());
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            await _repository.Update(Arg.Any<Customer>()).ReceivedWithAnyArgs();

            var result = await _customerController.Update(validDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Update_InvalidDto_ReturnsBadRequest()
        {

            var invalidDto = new CustomerUpdateDto { };
            var invalidResults = await _validator.ValidateAsync(invalidDto);
            var result = await _customerController.Update(invalidDto);

            var badRequestResult = (BadRequestObjectResult)result;
            var badRequestResultValidations = (ValidationResult?)badRequestResult.Value;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(invalidResults.Errors.Count, badRequestResultValidations?.Errors.Count);
        }

        [Test]
        public async Task Update_Repository_Failed_ThrowsException()
        {
            var validDto = CustomerMock.GetUpdateDto(Guid.NewGuid());
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.Update(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.Update(validDto);
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Updating customer has failed", badRequestResult.Value);
        }
    }
}
