using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.ViewModels.Customer;
using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public interface ICustomerAdapter
    {
        Customer ConvertToDomain(CustomerDto customerDto);
        Customer ConvertToDomain(CustomerDto customerDto, Customer domain);
        DeliveryAddress ConvertToDomainAddress(CustomerAddressDto deliveryAddress);
        CustomerViewModel ConvertToViewModel(Customer customerDto);
    }
}