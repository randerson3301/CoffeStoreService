using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Helpers;
using FluentValidation;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {

            RuleFor(dto => dto.Name)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_NAME_LENGTH);

            RuleFor(dto => dto.BirthDate)
                .NotEmpty()
                .InclusiveBetween(ValidationHelper.GetMinBirthDate(), ValidationHelper.GetMaxBirthDate());

            RuleFor(dto => dto.Document)
                 .Length(ValidationHelper.DOCUMENT_LENGTH)
                 .Must(ValidationHelper.BeValidDocument);

            RuleFor(dto => dto.Email)
               .NotEmpty()
               .EmailAddress();

            When(dto => !string.IsNullOrEmpty(dto.Password), () =>
            {
                RuleFor(dto => dto.Password)
                    .NotEmpty()
                    .MinimumLength(ValidationHelper.MIN_PASSWORD_LENGTH);
            });

            When(dto => dto.Id != null, () =>
            {
                RuleFor(dto => dto.Id)
                    .NotEmpty()
                    .NotEqual(Guid.Empty);
            });

            //When(dto => dto.DeliveryAddress != null, () =>
            //{
            //    RuleFor(dto => dto.DeliveryAddress.ZipCode)
            //       .NotEmpty()
            //       .Length(ValidationHelper.ZIP_CODE_LENGTH)
            //       .Matches(ValidationHelper.ZIP_CODE_REGEX);

            //    RuleFor(dto => dto.DeliveryAddress.Address)
            //        .NotEmpty()
            //        .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
            //        .Matches(ValidationHelper.ADDRESS_REGEX);

            //    RuleFor(dto => dto.DeliveryAddress.Number)
            //       .NotNull();

            //    RuleFor(dto => dto.DeliveryAddress.Neighborhood)
            //        .NotEmpty()
            //        .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
            //        .Matches(ValidationHelper.ADDRESS_REGEX);

            //    RuleFor(dto => dto.DeliveryAddress.City)
            //       .NotEmpty()
            //       .Matches(ValidationHelper.ADDRESS_REGEX);

            //    RuleFor(dto => dto.DeliveryAddress.State)
            //       .NotEmpty()
            //       .Length(ValidationHelper.STATE_LENGTH)
            //       .Matches(ValidationHelper.STATE_REGEX);
            //});
        }       
    }
}
