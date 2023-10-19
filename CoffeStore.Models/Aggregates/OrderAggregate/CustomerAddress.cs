using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.Models.Aggregates.OrderAggregate
{
    public readonly struct CustomerAddress
    {
        public CustomerAddress(Guid customerId, DeliveryAddress customerAddress)
        {
            CustomerId = customerId;
            Address = customerAddress;
        }

        public Guid CustomerId { get; }
        public DeliveryAddress Address { get; }
    }
}