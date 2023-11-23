using CoffeStore.EcommerceApp.Requests.Order;
using CoffeStore.Models.Aggregates.OrderAggregate;

namespace CoffeStore.EcommerceApp.Adapters.Contracts
{
    public interface IOrderAdapter
    {
        Order ConvertToDomain(CreateOrderRequest request);

    }
}
