using CoffeStore.Common.Helpers;
using CoffeStore.Modules.Orders.Application.Commands;
using FluentValidation;
using System.Globalization;

namespace CoffeStore.Modules.Orders.Application.Validators
{
    internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            RuleFor(command => command.CustomerId)
                .NotEmpty()
                .WithName("Id do cliente");

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

            RuleForEach(command => command.OrderItems).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .WithName("Id do produto");

                item.RuleFor(i => i.Quantity)
                    .NotEmpty()
                    .WithName("Quantidade do produto");
            });
        }
    }
}
