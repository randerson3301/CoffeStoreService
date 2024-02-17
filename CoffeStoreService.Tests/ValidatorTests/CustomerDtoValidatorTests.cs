using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

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

        //[Test]
        //public void ValidCustomerDto_PassesValidation()
        //{
        //    var customerDto = CustomerMock.GetDto();

        //    var result = validator.TestValidate(customerDto);

        //    Assert.IsTrue(result.IsValid);
        //}
    }
}
