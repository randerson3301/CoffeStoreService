using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.ViewModels.Customer;
using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public interface ICustomerAdapter
    {
        Customer ConvertToDomain(BaseCustomerDto customerDto);
        CustomerViewModel ConvertToViewModel(Customer customerDto);
    }
}