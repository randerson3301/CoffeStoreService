using CoffeStore.Common.Helpers;
using CoffeStore.Modules.Products.Application.Commands;
using FluentValidation;
using System.Globalization;

namespace CoffeStore.Modules.Products.Application.Validators
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("pt-BR");

            RuleFor(command => command.Name)
                .NotEmpty()
                .MinimumLength(ValidationHelper.MIN_NAME_LENGTH)
                .WithName("Nome do produto");

            RuleFor(command => command.Description)
                .NotEmpty()
                .WithName("Descrição do produto");

            RuleFor(command => command.ImagePath)
                .NotEmpty()
                .WithName("Imagem do produto");

            RuleFor(command => command.Price)
                .GreaterThan(decimal.Zero)
                .WithName("Preço do produto");
        }
    }
}
