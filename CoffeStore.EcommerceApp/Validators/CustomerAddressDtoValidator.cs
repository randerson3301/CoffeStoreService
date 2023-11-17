using CoffeStore.EcommerceApp.Helpers;
using CoffeStore.EcommerceApp.Requests.Customer.Dtos;
using FluentValidation;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerAddressDtoValidator : AbstractValidator<CustomerAddressDto>
    {
        public CustomerAddressDtoValidator()
        {
            RuleFor(dto => dto.ZipCode)
               .NotEmpty()
               .Length(ValidationHelper.ZIP_CODE_LENGTH)
               .Matches(ValidationHelper.ZIP_CODE_REGEX)
               .WithName("CEP");

            RuleFor(dto => dto.Address)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Logradouro");


            RuleFor(dto => dto.Number)
               .NotEmpty()               
               .WithName("Número");

            RuleFor(dto => dto.Neighborhood)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Bairro");

            RuleFor(dto => dto.City)
               .NotEmpty()
               .Matches(ValidationHelper.ADDRESS_REGEX)
               .WithName("Cidade");

            RuleFor(dto => dto.State)
               .NotEmpty()
               .Length(ValidationHelper.STATE_LENGTH)
               .Matches(ValidationHelper.STATE_REGEX)
               .WithName("Estado");

        }
    }
}
