using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Helpers;
using FluentValidation;
using System.Globalization;

namespace CoffeStore.Modules.Customers.Application.Validators
{
    internal class DeleteCustomerAddressCommandValidator : AbstractValidator<DeleteCustomerAddressCommand>
    {
        public DeleteCustomerAddressCommandValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            RuleFor(command => command.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(command => command.AddressToRemove.ZipCode)
              .NotEmpty()
              .Length(ValidationHelper.ZIP_CODE_LENGTH)
              .Length(ValidationHelper.ZIP_CODE_LENGTH)
              .Matches(ValidationHelper.ZIP_CODE_REGEX)
              .WithName("CEP");

            RuleFor(command => command.AddressToRemove.Address)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Logradouro");


            RuleFor(command => command.AddressToRemove.Number)
               .NotEmpty()
               .WithName("Número");

            RuleFor(command => command.AddressToRemove.Neighborhood)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_ADDRESS_LENGTH)
                .Matches(ValidationHelper.ADDRESS_REGEX)
                .WithName("Bairro");

            RuleFor(command => command.AddressToRemove.City)
               .NotEmpty()
               .Matches(ValidationHelper.ADDRESS_REGEX)
               .WithName("Cidade");

            RuleFor(command => command.AddressToRemove.State)
               .NotEmpty()
               .Length(ValidationHelper.STATE_LENGTH)
               .Matches(ValidationHelper.STATE_REGEX)
               .WithName("Estado");
        }

    }
}
