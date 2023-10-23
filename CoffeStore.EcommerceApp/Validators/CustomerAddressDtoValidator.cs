using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Helpers;
using FluentValidation;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerAddressDtoValidator : AbstractValidator<CustomerAddressDto>
    {
        public CustomerAddressDtoValidator()
        {
            When(dto => dto.Id != null, () =>
            {
                RuleFor(dto => dto.Id)
                    .NotEmpty()
                    .NotEqual(Guid.Empty);
            });

            RuleFor(dto => dto.ZipCode)
               .NotEmpty()
               .Length(ValidationHelper.ZIP_CODE_LENGTH)
               .Matches(ValidationHelper.ZIP_CODE_REGEX);

            RuleFor(dto => dto.Address)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX);

            RuleFor(dto => dto.Number)
               .NotNull();

            RuleFor(dto => dto.Neighborhood)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX);

            RuleFor(dto => dto.City)
               .NotEmpty()
               .Matches(ValidationHelper.ADDRESS_REGEX);

            RuleFor(dto => dto.State)
               .NotEmpty()
               .Length(ValidationHelper.STATE_LENGTH)
               .Matches(ValidationHelper.STATE_REGEX);
        }
    }
}
