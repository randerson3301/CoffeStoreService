using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Helpers;
using FluentValidation;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CustomerDtoValidator()
        {

            RuleFor(dto => dto.Name)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_NAME_LENGTH)
                .WithName("Nome completo");


            RuleFor(dto => dto.BirthDate)
                .NotEmpty()
                .InclusiveBetween(ValidationHelper.GetMinBirthDate(), ValidationHelper.GetMaxBirthDate())
                .WithName("Data de nascimento");

            RuleFor(dto => dto.Document)
                 .Length(ValidationHelper.DOCUMENT_LENGTH)
                 .Must(ValidationHelper.BeValidDocument)
                 .WithName("CPF");

            RuleFor(dto => dto.Login.Email)
               .NotEmpty()
               .EmailAddress()
               .WithName("E-mail");

            RuleFor(dto => dto.Login.Password)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_PASSWORD_LENGTH)
                .WithName("Senha");

            //When(dto => dto.Id != null, () =>
            //{
            //    RuleFor(dto => dto.Id)
            //        .NotEmpty()
            //        .NotEqual(Guid.Empty);
            //});
        }
    }
}
