using CoffeStore.Modules.Customers.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CoffeStore.Modules.Customers.Application
{
    public interface ICustomerApiService
    {
        internal Task<IResult> AddCustomer(CreateCustomerCommand newCustomer);
        internal Task<IResult> AddCustomerAddress(Guid id, CreateCustomerAddressCommand newAddress);
        internal Task<IResult> RemoveCustomerAddress(Guid id, DeleteCustomerAddressCommand addressToRemove);

        public Task<IResult> GetCustomerById(Guid id);
    }
}
