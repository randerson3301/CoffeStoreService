using CoffeStore.Common.MessageModels;
using CoffeStore.Modules.Customers.Application.Adapters;
using CoffeStore.Modules.Customers.Domain.DomainEvents;
using MassTransit;
using MediatR;

namespace CoffeStore.Modules.Customers.Application.DomainEventHandler
{
    internal class PublishWhenCustomerCreatedDomainEventHandler : INotificationHandler<CustomerAccessCreatedDomainEvent>
    {
        private readonly IBus _bus;
        private readonly ICustomerAdapter _adapter;

        public PublishWhenCustomerCreatedDomainEventHandler(IBus bus, ICustomerAdapter adapter)
        {
            _bus = bus;
            _adapter = adapter;
        }

        public async Task Handle(CustomerAccessCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = _adapter.ConvertToIntegrationEvent(notification);
            await _bus.Publish<CustomerNewAccessAddedEvent>(integrationEvent, cancellationToken);                    
        }
    }
}
