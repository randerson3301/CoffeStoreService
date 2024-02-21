using CoffeStore.Common.Seedwork;

namespace CoffeStore.Modules.Customers.Domain.DomainEvents
{
    public record CustomerAccessCreatedDomainEvent(Guid CustomerId, string Email, string Password): IDomainEvent
    {
    }
}
