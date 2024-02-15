using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStore.Modules.Customers.Domain.Contracts
{
    internal interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);        
        Task<Customer> GetByEmail(string email);
        Task<Customer?> GetByIdAsync(Guid id);
        Task UpdateAsync(Customer customer);
    }
}