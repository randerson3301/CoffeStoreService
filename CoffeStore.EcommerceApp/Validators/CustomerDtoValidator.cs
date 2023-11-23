using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Helpers;
using FluentValidation;
using System.Globalization;

namespace CoffeStore.EcommerceApp.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CustomerDtoValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

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
        }
    }
}
