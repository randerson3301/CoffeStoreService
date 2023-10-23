using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
using FluentValidation.TestHelper;

namespace CoffeStore.Tests.ValidatorTests
{
    internal class CustomerDtoValidatorTests
    {
        private CustomerDtoValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new CustomerDtoValidator();
        }

        [Test]
        public void ValidCustomerDto_PassesValidation()
        {
            var customerDto = CustomerMock.GetDto();

            var result = validator.TestValidate(customerDto);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void InvalidCustomerDto_FailsValidation()
        {
            var customerDto = new CustomerDto(); 

            var result = validator.TestValidate(customerDto);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors); 
        }
    }
}
