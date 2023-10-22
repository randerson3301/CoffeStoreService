using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
using FluentValidation.TestHelper;

namespace CoffeStore.Tests.ValidatorTests
{
    internal class CustomerDtoValidatorTests
    {
        private CustomerDtoValidator<BaseCustomerDto> validator;

        [SetUp]
        public void SetUp()
        {
            validator = new CustomerDtoValidator<BaseCustomerDto>();
        }

        [Test]
        public void ValidCustomerDto_PassesValidation()
        {
            var customerDto = CustomerMock.GetSignUpDto();

            var result = validator.TestValidate(customerDto);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void InvalidCustomerDto_FailsValidation()
        {
            var customerDto = new CustomerSignUpDto(); 

            var result = validator.TestValidate(customerDto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(12, result.Errors.Count); 
        }
    }
}
