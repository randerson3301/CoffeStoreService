using CoffeStore.Common.Seedwork;
using CoffeStore.Modules.Orders.Application.Dtos;
using CoffeStore.Modules.Orders.Application.ViewModels;
using MediatR;

namespace CoffeStore.Modules.Orders.Application.Commands
{
    internal class CreateOrderCommand: IRequest<OrderViewModel>
    {
        public Guid CustomerId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public required ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
