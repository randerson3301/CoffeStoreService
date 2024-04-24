using CoffeStore.Modules.Orders.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Orders.Application.Commands
{
    internal class CreateOrderCommand: IRequest<OrderViewModel>
    {
    }
}
