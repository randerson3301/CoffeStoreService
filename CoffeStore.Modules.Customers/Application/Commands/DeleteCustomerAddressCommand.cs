using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Common.Seedwork;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Commands
{
    public class DeleteCustomerAddressCommand : IRequest<bool>
    {
        public Guid Id = Guid.Empty;
        public required DeliveryAddress AddressToRemove { get; set; }
    }
}
