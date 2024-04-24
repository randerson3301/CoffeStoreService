using CoffeStore.Modules.Orders.Application.Commands;
using CoffeStore.Modules.Orders.Application.ViewModels;
using CoffeStore.Modules.Orders.Domain;

namespace CoffeStore.Modules.Orders.Application.Adapters
{
    internal interface IOrderAdapter
    {
        Order ConvertToDomain(CreateOrderCommand request);
        public OrderViewModel ConvertToViewModel(Order order);
    }
}
