using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Seedwork;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Commands
{
    internal class CreateCustomerAddressCommand: IRequest<CustomerViewModel>
    {
        public Guid Id = Guid.Empty;
        public required DeliveryAddress DeliveryAddress { get; set; }
    }
}
