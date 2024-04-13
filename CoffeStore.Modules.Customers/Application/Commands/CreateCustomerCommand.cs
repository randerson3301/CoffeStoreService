using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Common.Seedwork;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.Commands
{
    internal class CreateCustomerCommand : IRequest<CustomerViewModel?>
    {
        public required string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public required string Document { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required DeliveryAddress DeliveryAddress { get; set; }
    }
}