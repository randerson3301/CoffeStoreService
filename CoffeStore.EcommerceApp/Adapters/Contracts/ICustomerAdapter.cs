using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public interface ICustomerAdapter
    {
        Customer ConvertToDomain(CustomerDto customerDto);
    }
}