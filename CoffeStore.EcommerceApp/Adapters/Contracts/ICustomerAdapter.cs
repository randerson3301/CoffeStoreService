using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public interface ICustomerAdapter
    {
        Customer ConvertToDomain(CreateCustomerRequest customerDto);
        Customer ConvertToDomain(CreateCustomerRequest customerDto, Customer domain);
        DeliveryAddress ConvertToDomainAddress(CustomerAddressDto deliveryAddress);
        CustomerViewModel ConvertToViewModel(Customer domain);
    }
}