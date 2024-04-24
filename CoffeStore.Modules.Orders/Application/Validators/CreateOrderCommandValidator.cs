using CoffeStore.Modules.Orders.Application.Commands;
using FluentValidation;

namespace CoffeStore.Modules.Orders.Application.Validators
{
    internal class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
    {
    }
}
