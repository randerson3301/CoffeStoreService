using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Helpers;
using FluentValidation;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerDtoValidator<T> : AbstractValidator<T> where T : BaseCustomerDto
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

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).Password)
                    .NotEmpty()
                    .MinimumLength(ValidationHelper.MIN_PASSWORD_LENGTH);
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).ZipCode)
                    .NotEmpty()
                    .Length(ValidationHelper.ZIP_CODE_LENGTH)
                    .Matches(ValidationHelper.ZIP_CODE_REGEX);
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).Address)
                    .NotEmpty()
                    .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                    .Matches(ValidationHelper.ADDRESS_REGEX);
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).Number)
                    .NotNull();
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).Neighborhood)
                    .NotEmpty()
                    .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                    .Matches(ValidationHelper.ADDRESS_REGEX);
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).City)
                    .NotEmpty()
                    .Matches(ValidationHelper.ADDRESS_REGEX);
            });

            When(dto => dto is CustomerSignUpDto, () =>
            {
                RuleFor(dto => (dto as CustomerSignUpDto).State)
                    .NotEmpty()
                    .Length(ValidationHelper.STATE_LENGTH)
                    .Matches(ValidationHelper.STATE_REGEX);
            });

            When(dto => dto is CustomerUpdateDto, () =>
            {
                RuleFor(dto => (dto as CustomerUpdateDto).Id)
                    .NotEmpty()
                    .NotEqual(Guid.Empty);
            });
        }
    }
}
