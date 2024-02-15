using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.Helpers;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CoffeStore.Modules.Customers.Application.Validators
{
    internal class CreateCustomerAddressCommandValidator : AbstractValidator<CreateCustomerAddressCommand>
    {
        public CreateCustomerAddressCommandValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            RuleFor(command => command.Id)
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(command => command.DeliveryAddress.ZipCode)
              .NotEmpty()
              .Length(ValidationHelper.ZIP_CODE_LENGTH)
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
