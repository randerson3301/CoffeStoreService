using CoffeStore.Common.MessageModels;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.DomainEvents;
using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStore.Modules.Customers.Application.Adapters
{
    internal interface ICustomerAdapter
    {
        Customer ConvertToDomain(CreateCustomerCommand request);
        CustomerAddress ConvertToDomain(DeleteCustomerAddressCommand request);
        CustomerAddress ConvertToDomain(Guid customerId, DeliveryAddress address);
        CustomerViewModel ConvertToViewModel(Customer domain);
        CustomerNewAccessAddedEvent ConvertToIntegrationEvent(CustomerAccessCreatedDomainEvent domainEvent);
    }
}