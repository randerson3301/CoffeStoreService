using CoffeStore.Common.Helpers;
using CoffeStore.Modules.Customers.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CoffeStore.Modules.Customers.Application.Validators
{
    internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            RuleFor(command => command.Name)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_NAME_LENGTH)
                .WithName("Nome completo");


            RuleFor(command => command.BirthDate)
                .NotEmpty()
                .InclusiveBetween(ValidationHelper.GetMinBirthDate(), ValidationHelper.GetMaxBirthDate())
                .WithName("Data de nascimento");

            RuleFor(command => command.Document)
                 .Length(ValidationHelper.DOCUMENT_LENGTH)
                 .Must(ValidationHelper.BeValidDocument)
                 .WithName("CPF");

            RuleFor(command => command.Email)
               .NotEmpty()
               .EmailAddress()
               .WithName("E-mail");

            RuleFor(command => command.Password)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_PASSWORD_LENGTH)
                .WithName("Senha");

            RuleFor(command => command.DeliveryAddress.ZipCode)
              .NotEmpty()
              .Length(ValidationHelper.ZIP_CODE_LENGTH)
              .Matches(ValidationHelper.ZIP_CODE_REGEX)
              .WithName("CEP");

            RuleFor(command => command.DeliveryAddress.Address)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Logradouro");


            RuleFor(command => command.DeliveryAddress.Number)
               .NotEmpty()
               .WithName("Número");

            RuleFor(command => command.DeliveryAddress.Neighborhood)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Bairro");

            RuleFor(command => command.DeliveryAddress.City)
               .NotEmpty()
               .Matches(ValidationHelper.ADDRESS_REGEX)
               .WithName("Cidade");

            RuleFor(command => command.DeliveryAddress.State)
               .NotEmpty()
               .Length(ValidationHelper.STATE_LENGTH)
               .Matches(ValidationHelper.STATE_REGEX)
               .WithName("Estado");
        }
    }
}
