using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.Models.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer> GetByEmail(string email);
        Task<Customer> GetByIdAsync(string id);
        Task UpdateAsync(Customer customer);
    }
}