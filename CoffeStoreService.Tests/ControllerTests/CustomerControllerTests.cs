using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Controllers;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
using CoffeStore.Models.Contracts.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace CoffeStore.Tests.ControllerTests
{
    internal class CustomerControllerTests
    {
        private CustomerController _customerController;
        private ILogger<CustomerController> _logger;
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private IValidator<CustomerDto> _validator;
        private IValidator<CustomerAddressDto> _validatorAddress;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger<CustomerController>>();
            _repository = Substitute.For<ICustomerRepository>();
            _adapter = Substitute.For<ICustomerAdapter>();
            _validator = new CustomerDtoValidator();
            _validatorAddress = new CustomerAddressDtoValidator();

            _customerController = new CustomerController(_repository, _adapter, _logger, _validator, _validatorAddress);
        }

        [Test]
        public async Task Add_ValidDto_ReturnsCreatedAtAction()
        {

            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();
            var viewModel = CustomerMock.GetCustomerViewModel();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.AddAsync(Arg.Any<Customer>()).Returns(domain);
            _adapter.ConvertToViewModel(domain).Returns(viewModel);

            var result = await _customerController.Add(validDto);
            var createdAtResult = (CreatedAtActionResult)result;

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            Assert.AreSame(viewModel, createdAtResult.Value);
        }

        [Test]
        public async Task Add_InvalidDto_ReturnsBadRequest()
        {
            var invalidDto = new CustomerDto {};
            invalidDto.DeliveryAddress = new CustomerAddressDto();
            var result = await _customerController.Add(invalidDto);

            var badRequestResult = (BadRequestObjectResult)result;
            var badRequestResultValidations = (ValidationResult?)badRequestResult.Value;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.IsTrue(badRequestResultValidations?.Errors.Any());
        }

        [Test]
        public async Task Add_Repository_Failed_ThrowsException()
        {
            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.AddAsync(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.Add(validDto);
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Adding customer has failed", badRequestResult.Value);
        }

        [Test]
        public async Task Update_ValidDto_ReturnsNoContent()
        {
            var validDto = CustomerMock.GetDto();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.GetByIdAsync(validDto.Id).Returns(domain);

            var result = await _customerController.Update(validDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Update_ValidDtoWithAddress_ReturnsNoContent()
        {
            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto, domain).Returns(domain);
            _adapter.ConvertToDomainAddress(validDto?.DeliveryAddress).Returns(domain.DeliveryAddress.SingleOrDefault());
            _repository.GetByIdAsync(validDto.Id).Returns(domain);

            var result = await _customerController.Update(validDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Update_InvalidDto_ReturnsBadRequest()
        {
            var invalidDto = new CustomerDto { };
            var invalidResults = await _validator.ValidateAsync(invalidDto);
            var result = await _customerController.Update(invalidDto);

            var badRequestResult = (BadRequestObjectResult)result;
            var badRequestResultValidations = (ValidationResult?)badRequestResult.Value;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(invalidResults.Errors.Count, badRequestResultValidations?.Errors.Count);
        }

        [Test]
        public async Task Update_NonExistingCustomer_ReturnsNotFound()
        {
            var validDto = CustomerMock.GetDto();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.GetByIdAsync(validDto.Id).ReturnsNull();
            _repository.UpdateAsync(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.Update(validDto);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Update_Repository_Failed_ThrowsException()
        {
            var validDto = CustomerMock.GetDto();
            var domain = CustomerMock.GetCustomer();

            _adapter.ConvertToDomain(validDto).Returns(domain);
            _repository.GetByIdAsync(validDto.Id).Returns(domain);
            _repository.UpdateAsync(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.Update(validDto);
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Updating customer has failed", badRequestResult.Value);
        }

        [Test]
        public async Task Remove_ValidDtoWithAddress_ReturnsNoContent()
        {
            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();

            domain.AddAddress(CustomerMock.GetCustomerAddress());
            domain.AddAddress(CustomerMock.GetCustomerAddress());

            _adapter.ConvertToDomainAddress(validDto?.DeliveryAddress).Returns(CustomerMock.GetCustomerAddress());
            _repository.GetByIdAsync(validDto.Id).Returns(domain);

            var result = await _customerController.DeleteAddress(validDto.Id.Value, validDto.DeliveryAddress);
            
            Assert.AreEqual(1, domain.DeliveryAddress.Count);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Remove_InvalidDtoWithAddress_ReturnsBadRequest()
        {
            var invalidDto = new CustomerAddressDto { };            

            var invalidResults = await _validatorAddress.ValidateAsync(invalidDto);
            var result = await _customerController.DeleteAddress(Guid.Empty, invalidDto);

            var badRequestResult = (BadRequestObjectResult)result;
            var badRequestResultValidations = (ValidationResult?)badRequestResult.Value;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(invalidResults.Errors.Count, badRequestResultValidations?.Errors.Count);            
        }

        [Test]
        public async Task Remove_NonExisting_ReturnsNotFound()
        {
            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();

            domain.AddAddress(CustomerMock.GetCustomerAddress());
            domain.AddAddress(CustomerMock.GetCustomerAddress());

            _adapter.ConvertToDomainAddress(validDto?.DeliveryAddress).Returns(CustomerMock.GetCustomerAddress());
            _repository.GetByIdAsync(validDto.Id).ReturnsNull();

            var result = await _customerController.DeleteAddress(validDto.Id.Value, validDto.DeliveryAddress);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Remove_Repository_Failed_ThrowsException()
        {
            var validDto = CustomerMock.GetDtoWithAddress();
            var domain = CustomerMock.GetCustomer();

            domain.AddAddress(CustomerMock.GetCustomerAddress());
            domain.AddAddress(CustomerMock.GetCustomerAddress());

            _adapter.ConvertToDomainAddress(validDto?.DeliveryAddress).Returns(CustomerMock.GetCustomerAddress());
            _repository.GetByIdAsync(validDto.Id).Returns(domain);
            _repository.UpdateAsync(Arg.Any<Customer>()).ThrowsForAnyArgs<Exception>();

            var result = await _customerController.DeleteAddress(validDto.Id.Value, validDto.DeliveryAddress);
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Updating customer has failed", badRequestResult.Value);
        }
    }
}
